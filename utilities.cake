#tool nuget:?package=Cake.CoreCLR
/*
     dotnet cake spell-check.cake
    dotnet cake spell-check.cake -t=spell-check
 */
#addin nuget:?package=WeCantSpell.Hunspell&version=3.0.1
#addin nuget:?package=Newtonsoft.Json&version=12.0.3
#addin nuget:?package=Cake.FileHelpers&version=3.2.1

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var TARGET = Argument ("t", Argument ("target", "Default"));

string file_spell_errors = "./output/spell-errors.txt";
List<string> spell_errors = null;
JArray binderator_json_array = null;

Task ("list-artifacts")
    .Does
    (
        () =>
        {
            using (StreamReader reader = System.IO.File.OpenText(@"./config.json"))
            {
                JsonTextReader jtr = new JsonTextReader(reader);
                binderator_json_array = (JArray)JToken.ReadFrom(jtr);
            }

            Information("config.json list supported artifacts...");

            List<string> lines1 = new List<string>();
            List<string> lines2 = new List<string>();
            string space = " ";
            string dash = "-";
            int width1 = 70;
            int width2 = 20;

            lines1.Add($"# Artifacts supported");
            lines2.Add($"# Artifacts with versions supported");
            lines1.Add(Environment.NewLine);
            lines1.Add(Environment.NewLine);
            lines2.Add(Environment.NewLine);
            lines2.Add(Environment.NewLine);
            lines1.Add($@"|{space.PadRight(width1)}|{space.PadRight(width1)}|");
            lines1.Add($@"|{dash.PadRight(width1, '-')}|{dash.PadRight(width1, '-')}|");
            lines2.Add($@"|{space.PadRight(width1)}|{space.PadRight(width2)}|{space.PadRight(width1)}|{space.PadRight(width2)}|");
            lines2.Add($@"|{dash.PadRight(width1, '-')}|{dash.PadRight(width2, '-')}|{dash.PadRight(width1, '-')}|{dash.PadRight(width2, '-')}|");

            foreach(JObject jo in binderator_json_array[0]["artifacts"])
            {
                bool? dependency_only = (bool?) jo["dependencyOnly"];
                if ( dependency_only == true)
                {
                    continue;
                }

                string group_id  	= (string) jo["groupId"];
                string artifact_id  = (string) jo["artifactId"];
                string artifact_v   = (string) jo["version"];
                string nuget_id  	= (string) jo["nugetId"];
                string nuget_v  	= (string) jo["nugetVersion"];

                string maven = $"{group_id}:{artifact_id}";
                string nuget = $"{nuget_id}";
                string line1 = $@"|{maven.PadRight(width1)}|{nuget.PadRight(width1)}|";
                string line2 = $@"|{maven.PadRight(width1)}|{artifact_v.PadRight(width2)}|{nuget.PadRight(width1)}|{nuget_v.PadRight(width2)}|";

                lines1.Add(line1);
                lines2.Add(line2);
            }

            EnsureDirectoryExists("./output/");
			System.IO.File.WriteAllLines($"./output/artifact-list.md", lines1.ToArray());
			System.IO.File.WriteAllLines($"./output/artifact-list-with-versions.md", lines2.ToArray());
			System.IO.File.WriteAllLines($"./output/artifact-list-{DateTime.Now.ToString("yyyyMMdd")}.md", lines1.ToArray());
			System.IO.File.WriteAllLines($"./output/artifact-list-with-versions-{DateTime.Now.ToString("yyyyMMdd")}.md", lines2.ToArray());

        }
    );


Task ("spell-check")
    .Does
    (
        () =>
        {
            if (FileExists(file_spell_errors))
            {
                DeleteFile(file_spell_errors);
            }
            EnsureDirectoryExists("./externals/");
            string url = "https://raw.githubusercontent.com/titoBouzout/Dictionaries/master/";

            string[] files_dictionaries = new[]
            {
                "English (American).dic",
                "English (American).txt",
                "English (American).aff",
            };
            foreach(string file_dictionary in files_dictionaries)
            {
                string url_full = url + System.Uri.EscapeDataString(file_dictionary);
                Information($"Downloading ");
                Information($"      {url_full}");
                if (!FileExists($"./externals/{file_dictionary}"))
                {
                    DownloadFile(url_full, $"./externals/{file_dictionary}");
                }
            }

            var dictionary = WeCantSpell.Hunspell.WordList.CreateFromFiles(@"externals/English (American).dic");
            var words = new[]
            {
                "Xamarin",
                "AndroidX",
                "GooglePlayServices",
                "AFS",
                "Analytics",
                "Impl",
                "AppInvite",
                "Auth",
                "Api",
                "Clearcut",
                "CloudMessaging",
                "CroNet",
                "Gass",
                "Gcm",
                "Iid",
                "InstantApps",
                "Sdk",
                "MLKit",
                "ImageLabeling",
                "Oss",
                "PlaceReport",
                "SafetyNet",
                "TagManager",
                "ImageLabel",
                "ImageLabelingInternal",
                "Firebase",
                "AppIndexing",
                "Interop",
                "Config",
                "Crashlytics",
                "NDK",
                "Datatransport",
                "JSON",
                "InAppMessaging",
                "InterOp",
                "AutoML",
                "BarCode",
                "Vkp",
                "Perf",
                "ProtoliteWellKnownTypes",
                "BarcodeScanning",
                "DigitalInk",
                "LinkFirebase",
                "MediaPipe",
                "FaceDetection",
                "ObjectDetection",
                "PoseDetection",
                "SmartReply",
                "V4",
                "Abt",
                "Firestore",
                "AppCheck",
                "Odml",
                "TensorFlow",
                "Gpu",
                "FindBugs",
                "JSR305",            
                "GifDecoder",
                "DiskLruCache",
                "RecyclerViewIntegration",
                "JavaX",
            };
            var dictionary_custom = WeCantSpell.Hunspell.WordList.CreateFromWords(words);

            using (StreamReader reader = System.IO.File.OpenText(@"./config.json"))
            {
                JsonTextReader jtr = new JsonTextReader(reader);
                binderator_json_array = (JArray)JToken.ReadFrom(jtr);
            }

            spell_errors = new List<string>();
            Information("config.json spell checking...");

            foreach(JObject jo in binderator_json_array[0]["artifacts"])
            {
                bool? dependency_only = (bool?) jo["dependencyOnly"];
                if ( dependency_only == true)
                {
                    continue;
                }
                string nuget_id  	= (string) jo["nugetId"];
                Information($"       spell-check {nuget_id}");

                string[] nuget_id_parts = nuget_id.Split('.');
                foreach(string nuget_id_part in nuget_id_parts)
                {
                    bool check_dictionary_custom = dictionary_custom.Check(nuget_id_part);
                    Information($"      check_dictionary_custom {nuget_id_part} = {check_dictionary_custom}");
                    if (check_dictionary_custom)
                    {
                        Information($"          Found in custom dictionary!");
                        continue;
                    }
                    bool check_dictionary = dictionary.Check(nuget_id_part);
                    Information($"      check_dictionary {nuget_id_part} = {check_dictionary}");
                    if (check_dictionary)
                    {
                        Information($"          Found in EN-US dictionary!");
                        continue;
                    }

                    Information($"Added {nuget_id_part} from {nuget_id}");
                    spell_errors.Add(nuget_id_part);
                }
            }

            if (spell_errors.Count > 0)
            {
                System.IO.File.WriteAllLines(file_spell_errors, spell_errors);
            }
        }
    );


Task ("namespace-check")
    .Does
    (
        () =>
        {
            // Namespace Checks
            FilePath[] files = new FilePath[]{};
            FilePath[] files_com = GetFiles("./generated/**/Com.*.cs").ToArray();
            FilePath[] files_org = GetFiles("./generated/**/Org.*.cs").ToArray();
            FilePath[] files_io = GetFiles("./generated/**/Io.*.cs").ToArray();

            files = files.Concat(files_com.ToArray()).ToArray();
            files = files.Concat(files_org.ToArray()).ToArray();
            files = files.Concat(files_io.ToArray()).ToArray();

            if (files.Any())
            {
                string[] namespaces = Array.ConvertAll(files, x => x.ToString());
                System.IO.File.WriteAllLines("./output/namespaces.md", namespaces);

                StringBuilder msg = new StringBuilder("Namespaces!!!");
                msg.AppendLine();
                msg.AppendLine(string.Join(System.Environment.NewLine, namespaces));

                throw new Exception(msg.ToString());
            }

            return;
        }
    );

Task("binderate-diff")
    .Does
    (
        () =>
        {
			EnsureDirectoryExists("./output/");

			// "git diff -U999999 main:config.json config.json" > ./output/config.json.diff-from-main.txt"
			string process = "git";
			string process_args = "diff -U999999 main:config.json config.json";
			IEnumerable<string> redirectedStandardOutput;
			ProcessSettings process_settings = new ProcessSettings ()
			{
             Arguments = process_args,
             RedirectStandardOutput = true
         	};
			int exitCodeWithoutArguments = StartProcess(process, process_settings, out redirectedStandardOutput);
			System.IO.File.WriteAllLines("./output/config.json.diff-from-main.txt", redirectedStandardOutput.ToArray());
			Information("Exit code: {0}", exitCodeWithoutArguments);
		}
	);

Task ("api-diff-markdown-info-pr")
    .IsDependentOn("binderate-diff")
    .Does
    (
        () =>
        {
            // TODO: api-diff markdown info based on diff from main
            string[] lines_from_file = System.IO.File.ReadAllLines("./output/config.json.diff-from-main.txt");

            List<string> lines = new List<string>(lines_from_file);
            List<string> changelog = new List<string>();

            List<List<string>> changelog_blocks = new List<List<string>>();
            List<string> changelog_block = null;
            int idx_start = -1;
            int idx_stop = -1;
            for(int i = 0; i < lines.Count(); i++)
            {
                string line = lines[i];
                if (line.Contains("groupId"))
                {
                    idx_start = i;
                }

                if(line.Contains("dependencyOnly"))
                {
                    if (line.StartsWith("-"))
                    {
                        continue;
                    }
                    idx_stop = i;
                }

                if (idx_start != -1 && idx_stop != -1)
                {
                    changelog_block = lines.GetRange(idx_start, idx_stop - idx_start);
                    changelog_blocks.Add(changelog_block);
                    idx_start = -1;
                    idx_stop = -1;
                }
            }


            foreach (List<string> changelog_block_lines in changelog_blocks)
            {
                string g = null;
                string a = null;
                string v_artifact_new = null;
                string v_artifact_old = null;
                string v_nuget_new = null;
                string v_nuget_old = null;
                string nuget_id = null;

                foreach(string line in changelog_block_lines)
                {
                    if (line.Contains("groupId"))
                    {
                        g = ParseDiffLine(line, "groupId");
                        continue;
                    }
                    if (line.Contains("artifactId"))
                    {
                        a = ParseDiffLine(line, "artifactId");
                        continue;
                    }
                    if (line.Contains("version"))
                    {
                        if (line.StartsWith("+"))
                        {
                            v_artifact_new = ParseDiffLine(line, "version");
                            continue;
                        }

                        v_artifact_old = ParseDiffLine(line, "version");
                        continue;
                    }

                    if (line.Contains("nugetVersion"))
                    {
                        v_nuget_old = ParseDiffLine(line, "nugetVersion");
                        continue;
                    }
                    if (line.Contains("nugetVersion"))
                    {
                        v_nuget_new = ParseDiffLine(line, "nugetVersion");
                        continue;
                    }
                    if (line.Contains("nugetId"))
                    {
                        nuget_id = ParseDiffLine(line, "nugetId");
                        continue;
                    }
                }

                if(v_artifact_new == null)
                {
                    continue;
                }

                string changelog_line = $"- {g}:{a} - {v_artifact_old} -> {v_artifact_new}";

                changelog.Add(changelog_line);
            }

            if (changelog.Count > 0)
            {
                System.IO.File.WriteAllLines("./output/changelog.md", changelog);
            }

            return;
        }
    );

string ParseDiffLine(string line, string name)
{
    if (line[0] == '-')
    {
        StringBuilder sb = new StringBuilder(line);
        sb[0] = ' ';
        line = sb.ToString();
    }

    return new string
                    (
                        line
                            .ToCharArray()
                            .Where(c => !Char.IsWhiteSpace(c))
                            .ToArray()
                    )
                    .Replace("\"", "")
                    .Replace("+", "")
                    .Replace(":", "")
                    .Replace(name, "")
                    .Replace(",", "")
                    ;
}

Task ("read-analysis-files")
    .IsDependentOn ("binderate-diff")
    .IsDependentOn ("api-diff-markdown-info-pr")
    .IsDependentOn ("namespace-check")
    .IsDependentOn ("spell-check")
    .IsDependentOn ("list-artifacts")
    .Does
    (
        () =>
        {
            List<string> files = new List<string>
            {
                "./output/spell-errors.txt",
                "./output/changelog.md",
                "./output/config.json.diff-from-main.txt",
            };

            if ( ! FileExists("./output/spell-errors.txt") )
            {
                files.Remove("./output/spell-errors.txt");
            }

			string process = "code";
			string process_args = $"-n {string.Join(" ", files)}";
			IEnumerable<string> redirectedStandardOutput;
			ProcessSettings process_settings = new ProcessSettings ()
			{
             Arguments = process_args,
             RedirectStandardOutput = true
         	};
			int exitCodeWithoutArguments = StartProcess(process, process_settings, out redirectedStandardOutput);
			Information("Exit code: {0}", exitCodeWithoutArguments);
        }
    );


Task ("Default")
    .IsDependentOn ("read-analysis-files")
    ;

if (System.IO.File.Exists(file_spell_errors))
{
    string separator = System.Environment.NewLine + "\t" + "\t";
    string msg = "Spell Errors:" + System.Environment.NewLine + "\t" + "\t"
                    + string.Join(separator, System.IO.File.ReadAllLines(file_spell_errors));
    throw new Exception(msg);
}

RunTarget (TARGET);
