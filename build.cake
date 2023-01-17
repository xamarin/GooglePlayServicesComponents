// Tools needed by cake addins
#tool nuget:?package=vswhere&version=3.1.1

// Cake Addins
#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#addin nuget:?package=Newtonsoft.Json&version=13.0.2

//using Cake.Common.Tools.MSBuild;

using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var TARGET = Argument ("t", Argument ("target", "ci"));
var MAX_CPU_COUNT = Argument("maxcpucount", 0);

// Lists all the artifacts and their versions for com.android.support.*
// https://dl.google.com/dl/android/maven2/com/android/support/group-index.xml
// Master list of all the packages in the repo:
// https://dl.google.com/dl/android/maven2/master-index.xml

var REF_DOCS_URL = "https://raw.githubusercontent.com/xamarin/GooglePlayServicesComponents/main/data/docs/play-services-firebase.zip";
var REF_METADATA_URL = "https://raw.githubusercontent.com/xamarin/GooglePlayServicesComponents/main/data/paramnames/play-services-firebase-metadata.xml";

// These are a bunch of parameter names in the txt format which binding projects can use
var REF_PARAMNAMES_URL = "https://raw.githubusercontent.com/xamarin/GooglePlayServicesComponents/main/data/paramnames/play-services-firebase-paramnames.txt";

// Resolve Xamarin.Android installation
var XAMARIN_ANDROID_PATH = EnvironmentVariable ("XAMARIN_ANDROID_PATH");
var ANDROID_SDK_BASE_VERSION = "v1.0";
var ANDROID_SDK_VERSION = "v12.0";
string AndroidSdkBuildTools = $"32.0.0";

if (string.IsNullOrEmpty(XAMARIN_ANDROID_PATH)) {
	if (IsRunningOnWindows()) {
		var vsInstallPath = VSWhereLatest(new VSWhereLatestSettings { Requires = "Component.Xamarin", IncludePrerelease = true });
		XAMARIN_ANDROID_PATH = vsInstallPath.Combine("Common7/IDE/ReferenceAssemblies/Microsoft/Framework/MonoAndroid").FullPath;
	} else {
		if (DirectoryExists("/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/xamarin.android/xbuild-frameworks/MonoAndroid"))
			XAMARIN_ANDROID_PATH = "/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/xamarin.android/xbuild-frameworks/MonoAndroid";
		else
			XAMARIN_ANDROID_PATH = "/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/xbuild-frameworks/MonoAndroid";
	}
}
if (!DirectoryExists($"{XAMARIN_ANDROID_PATH}/{ANDROID_SDK_VERSION}"))
	throw new Exception($"Unable to find Xamarin.Android {ANDROID_SDK_VERSION} at {XAMARIN_ANDROID_PATH}.");

// Load all the git variables
var BUILD_COMMIT = EnvironmentVariable("BUILD_COMMIT") ?? "DEV";
var BUILD_NUMBER = EnvironmentVariable("BUILD_NUMBER") ?? "DEBUG";
var BUILD_TIMESTAMP = DateTime.UtcNow.ToString();

var REQUIRED_DOTNET_TOOLS = new [] {
	"xamarin-android-binderator",
	"xamarin.androidx.migration.tool"
};

string nuget_version_template =
							// "71.vvvv.0-preview3" 	// pre AndroidX version
							"1xx.yy.zz.ww-suffix"		// AndroidX version preview
							//"1xx.yy.zz"				// AndroidX version stable/release
							;
string nuget_version_suffix = "";

string[] Configs = new []
{
	"Debug",
	"Release"
};

var MONODROID_PATH = "/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mandroid/platforms/" + ANDROID_SDK_VERSION + "/";
if (IsRunningOnWindows ())
{
	var vsInstallPath = VSWhereLatest (new VSWhereLatestSettings { Requires = "Component.Xamarin", IncludePrerelease = true });
	MONODROID_PATH = vsInstallPath.Combine ("Common7/IDE/ReferenceAssemblies/Microsoft/Framework/MonoAndroid/" + ANDROID_SDK_VERSION).FullPath;
}

var MSCORLIB_PATH = "/Library/Frameworks/Xamarin.Android.framework/Libraries/mono/2.1/";
if (IsRunningOnWindows ()) {

	var DOTNETDIR = new DirectoryPath (Environment.GetFolderPath (Environment.SpecialFolder.Windows)).Combine ("Microsoft.NET/");

	if (DirectoryExists (DOTNETDIR.Combine ("Framework64")))
		MSCORLIB_PATH = MakeAbsolute (DOTNETDIR.Combine("Framework64/v4.0.30319/")).FullPath;
	else
		MSCORLIB_PATH = MakeAbsolute (DOTNETDIR.Combine("Framework/v4.0.30319/")).FullPath;
}


string JAVA_HOME = EnvironmentVariable ("JAVA_HOME") ?? Argument ("java_home", "");
string ANDROID_HOME = EnvironmentVariable ("ANDROID_HOME") ?? Argument ("android_home", "");
string ANDROID_SDK_ROOT = EnvironmentVariable ("ANDROID_SDK_ROOT") ?? Argument ("android_sdk_root", "");

// Log some variables
Information ($"JAVA_HOME            : {JAVA_HOME}");
Information ($"ANDROID_HOME         : {ANDROID_HOME}");
Information ($"ANDROID_SDK_ROOT     : {ANDROID_SDK_ROOT}");
Information ($"MONODROID_PATH       : {MONODROID_PATH}");
Information ($"MSCORLIB_PATH        : {MSCORLIB_PATH}");
Information ($"XAMARIN_ANDROID_PATH : {XAMARIN_ANDROID_PATH}");
Information ($"ANDROID_SDK_VERSION  : {ANDROID_SDK_VERSION}");
Information ($"BUILD_COMMIT:        : {BUILD_COMMIT}");
Information ($"BUILD_NUMBER:        : {BUILD_NUMBER}");
Information ($"BUILD_TIMESTAMP:     : {BUILD_TIMESTAMP}");

// You shouldn't have to configure anything below here
// ######################################################

void RunProcess(FilePath fileName, string processArguments)
{
	var exitCode = StartProcess(fileName, processArguments);
	if (exitCode != 0)
		throw new Exception ($"Process {fileName} exited with code {exitCode}.");
}

void RunGradle(DirectoryPath root, string target)
{
    root = MakeAbsolute(root);
    var proc = IsRunningOnWindows()
        ? root.CombineWithFilePath("gradlew.bat").FullPath
        : "bash";
    var args = IsRunningOnWindows()
        ? ""
        : root.CombineWithFilePath("gradlew").FullPath;
    args += $" {target} -p {root}";

    var exitCode = StartProcess(proc, args);
    if (exitCode != 0)
        throw new Exception($"Gradle exited with code {exitCode}.");
}

Task("javadocs")
	.Does(() =>
{
	EnsureDirectoryExists("./externals/");

	if (!FileExists("./externals/docs.zip"))
		DownloadFile(REF_DOCS_URL, "./externals/docs.zip");

	if (!DirectoryExists("./externals/docs"))
		Unzip ("./externals/docs.zip", "./externals/docs");

	if (!FileExists("./externals/paramnames.txt"))
		DownloadFile(REF_PARAMNAMES_URL, "./externals/paramnames.txt");

	if (!FileExists("./externals/paramnames.xml"))
		DownloadFile(REF_METADATA_URL, "./externals/paramnames.xml");

	var astJar = new FilePath("./util/JavaASTParameterNames-1.0.jar");
	var sourcesJars = GetFiles("./externals/**/*-sources.jar");

	foreach (var srcJar in sourcesJars) {
		var srcJarPath = MakeAbsolute(srcJar).FullPath;
		var outTxtPath = srcJarPath.Replace("-sources.jar", "-paramnames.txt");
		var outXmlPath = srcJarPath.Replace("-sources.jar", "-paramnames.xml");

		StartProcess("java", "-jar \"" + MakeAbsolute(astJar).FullPath + "\" --text \"" + srcJarPath + "\" \"" + outTxtPath + "\"");
		StartProcess("java", "-jar \"" + MakeAbsolute(astJar).FullPath + "\" --xml \"" + srcJarPath + "\" \"" + outXmlPath + "\"");
	}
});

Task("tools-update")
    .Does
    (
        () =>
        {
            /*
			      // dotnet cake	
            dotnet tool uninstall   -g Cake.Tool
            dotnet tool install     -g Cake.Tool
			      // binderator
            dotnet tool uninstall   -g xamarin.androidbinderator.tool
            dotnet tool install     -g xamarin.androidbinderator.tool
			      // androidx-migrator
            dotnet tool uninstall   -g xamarin.androidx.migration.tool
            dotnet tool install     -g xamarin.androidx.migration.tool
      			// apoi-tools
            dotnet tool uninstall   -g api-tools
            dotnet tool install     -g api-tools

            StartProcess("dotnet", "tool uninstall   -g Cake.Tool");
            StartProcess("dotnet", "tool install     -g Cake.Tool");
            */
            StartProcess("dotnet", "tool uninstall   -g xamarin.androidbinderator.tool");
            StartProcess("dotnet", "tool install     -g xamarin.androidbinderator.tool");
            StartProcess("dotnet", "tool uninstall   -g xamarin.androidx.migration.tool");
            StartProcess("dotnet", "tool install     -g xamarin.androidx.migration.tool");
            StartProcess("dotnet", "tool uninstall   -g api-tools");
            StartProcess("dotnet", "tool install     -g api-tools");
        }
    );

Task("binderate")
	.IsDependentOn("javadocs")
	.IsDependentOn("binderate-config-verify")
	.Does(() =>
{
	var configFile = MakeAbsolute(new FilePath("./config.json")).FullPath;
	var basePath = MakeAbsolute(new DirectoryPath ("./")).FullPath;

	RunProcess("xamarin-android-binderator",
		$"--config=\"{configFile}\" --basepath=\"{basePath}\"");

	RunTarget("binderate-prepare-dependencies-samples-packages-config");
	RunTarget("binderate-prepare-dependencies-samples-packagereferences");
});

Task("binderate-prepare-dependencies-samples-packagereferences")
	.Does
	(
		() =>
		{
			// needed for offline builds 28.0.0.1 to 28.0.0.3
			EnsureDirectoryExists("./output/");
			EnsureDirectoryExists("./externals/");

			FilePathCollection files = GetFiles("./samples/**/*.csproj");
			foreach(FilePath file in files)
			{
				Information($"File: {file}");

				XmlDocument xml = new XmlDocument();
				xml.Load($"{file}");
			}
		}
	);

Task("binderate-prepare-dependencies-samples-packages-config")
	.Does
	(
		() =>
		{
			// needed for offline builds 28.0.0.1 to 28.0.0.3
			EnsureDirectoryExists("./output/");
			EnsureDirectoryExists("./externals/");

			FilePathCollection files = GetFiles("./samples/**/packages.config");
			foreach(FilePath file in files)
			{
				Information($"File: {file}");

				XmlDocument xml = new XmlDocument();
				xml.Load($"{file}");
				XmlNodeList list = xml.SelectNodes("/packages/package");
				foreach (XmlNode xn in list)
				{
					string id = xn.Attributes["id"].Value; //Get attribute-id 
					//string text = xn["Text"].InnerText; //Get Text Node
					string v = xn.Attributes["version"].Value; //Get attribute-id 

					Information($"		id	   : {id}");
					Information($"		version: {v}");

					string url = $"https://www.nuget.org/api/v2/package/{id}/{v}";
					string file1 = $"./externals/{id.ToLower()}.{v}.nupkg";
					try
					{
						if ( ! FileExists(file1) )
						{
							DownloadFile(url, file1);
						}
					}
					catch (System.Exception)
					{
						Error($"Unable to download {url}");
					}
				}
			}
		
			return;
		}
	);

JArray binderator_json_array = null;

Task("binderate-config-verify")
	.IsDependentOn("binderate-fix")
	.Does
	(
		() =>
		{
			using (StreamReader reader = System.IO.File.OpenText(@"./config.json"))
			{
				JsonTextReader jtr = new JsonTextReader(reader);
				JArray ja = (JArray)JToken.ReadFrom(jtr);

				Information("config.json");
				//Information($"{ja}");
				foreach(JObject jo in ja[0]["artifacts"])
				{
					bool? dependency_only = (bool?) jo["dependencyOnly"];
					if ( dependency_only == true)
					{
						continue;
					}
					string version       = (string) jo["version"];
					string nuget_version = (string) jo["nugetVersion"];
					Information($"groupId       = {jo["groupId"]}");
					Information($"artifactId    = {jo["artifactId"]}");
					Information($"version       = {version}");
					Information($"nuget_version = {nuget_version}");
					Information($"nugetId       = {jo["nugetId"]}");

					string[] version_parsed = nuget_version.Split(new string[] {"."}, StringSplitOptions.None);
					string nuget_version_new = nuget_version_template;
					string version_parsed_xx = version_parsed[0];
					string version_parsed_yy = version_parsed[1];
					string version_parsed_zz = version_parsed[2];

					Information($"version_parsed_xx       = {version_parsed_xx}");
					if ( version_parsed_xx.Length == 1 )
					{
						version_parsed_xx = string.Concat("0", version_parsed_xx);
					}
					Information($"version_parsed_xx       = {version_parsed_xx}");

					nuget_version_new = nuget_version_new.Replace("1xx", version_parsed_xx);
					nuget_version_new = nuget_version_new.Replace("yy", version_parsed_yy);
					nuget_version_new = nuget_version_new.Replace("zz", version_parsed_zz);
					if (version_parsed.Length == 4)
					{
						nuget_version_new = nuget_version_new.Replace("ww", version_parsed[3]);
					}
					else
					{
						nuget_version_new = nuget_version_new.Replace(".ww", "");
					}

					nuget_version_new = nuget_version_new.Replace("-suffix", nuget_version_suffix);

					Information($"nuget_version_new       = {nuget_version_new}");
					Information($"nuget_version           = {nuget_version}");
					if( ! nuget_version_new.Contains($"{nuget_version}") )
					{
						// AndroidX version
						// // pre AndroidX version
						Error("check config.json for nuget id - pre AndroidX version");
						Error  ($"		groupId       = {jo["groupId"]}");
						Error  ($"		artifactId    = {jo["artifactId"]}");
						Error  ($"		version       = {version}");
						Error  ($"		nuget_version = {nuget_version}");
						Error  ($"		nugetId       = {jo["nugetId"]}");

						Warning($"	expected : ");
						Warning($"		nuget_version = {nuget_version_new}");
						throw new Exception("check config.json for nuget id");

						return;
					}
				}
			}
		}
	);

Task("binderate-diff")
	.IsDependentOn("binderate")
    .Does
    (
        () =>
        {
			EnsureDirectoryExists("./output/");

			// "git diff master:config.json config.json" > ./output/config.json.diff-from-master.txt"
			string process = "git";
			string process_args = "diff master:config.json config.json";
			IEnumerable<string> redirectedStandardOutput;
			ProcessSettings process_settings = new ProcessSettings ()
			{
             Arguments = process_args,
             RedirectStandardOutput = true
         	};
			int exitCodeWithoutArguments = StartProcess(process, process_settings, out redirectedStandardOutput);
			System.IO.File.WriteAllLines("./output/config.json.diff-from-master.txt", redirectedStandardOutput.ToArray());
			Information("Exit code: {0}", exitCodeWithoutArguments);
		}
	);

Task("binderate-fix")
    .Does
    (
        () =>
        {
            using (StreamReader reader = System.IO.File.OpenText(@"./config.json"))
            {
                JsonTextReader jtr = new JsonTextReader(reader);
                binderator_json_array = (JArray)JToken.ReadFrom(jtr);
            }

            Warning("config.json fixing missing folder strucutre ...");
            foreach(JObject jo in binderator_json_array[0]["artifacts"])
            {
                string groupId      = (string) jo["groupId"];
                string artifactId   = (string) jo["artifactId"];

                Information($"		Verifying files for     :");
                Information($"              group       : {groupId}");
                Information($"              artifact    : {artifactId}");

                bool? dependency_only = (bool?) jo["dependencyOnly"];
                if ( dependency_only == true)
                {
                    continue;
                }


                string dir_group = $"source/{groupId}";
                if ( ! DirectoryExists(dir_group) )
                {
                    Warning($"  		Creating {dir_group}");
                    CreateDirectory(dir_group);
                }

                string dir_artifact = $"{dir_group}/{artifactId}";
                if ( ! DirectoryExists(dir_artifact) )
                {
                    Warning($"     			Creating artifact folder : {dir_artifact}");
                    CreateDirectory(dir_artifact);
                    CreateDirectory($"{dir_artifact}/Transforms/");
                    CreateDirectory($"{dir_artifact}/Additions/");
                }
				else
				{
					continue;
				}

				if ( ! FileExists($"{dir_artifact}/Transforms/Metadata.xml"))
				{
					Warning($"     				Creating file : {dir_artifact}/Metadata.xml");
					CopyFile
					(
						$"./source/template-group-id/template-artifact/Transforms/Metadata.xml",
						$"{dir_artifact}/Transforms/Metadata.xml"
					);
				}
				if ( ! FileExists($"{dir_artifact}/Transforms/Metadata.Namespaces.xml"))
				{
					Warning($"     				Creating file : {dir_artifact}/Metadata.Namespaces.xml");
					CopyFile
					(
						$"./source/template-group-id/template-artifact/Transforms/Metadata.Namespaces.xml",
						$"{dir_artifact}/Transforms/Metadata.Namespaces.xml"
					);
				}
				if ( ! FileExists($"{dir_artifact}/Transforms/Metadata.ParameterNames.xml"))
				{
					Warning($"     				Creating file : {dir_artifact}/Metadata.ParameterNames.xml");
					CopyFile
					(
						$"./source/template-group-id/template-artifact/Transforms/Metadata.ParameterNames.xml",
						$"{dir_artifact}/Transforms/Metadata.ParameterNames.xml"
					);
				}
				if ( ! FileExists($"{dir_artifact}/Transforms/EnumFields.xml"))
				{
					Warning($"     				Creating file : {dir_artifact}/EnumFields.xml");
					CopyFile
					(
						$"./source/template-group-id/template-artifact/Transforms/EnumFields.xml",
						$"{dir_artifact}/Transforms/EnumFields.xml"
					);
				}
				if ( ! FileExists($"{dir_artifact}/Transforms/EnumMethods.xml"))
				{
					Warning($"     				Creating file : {dir_artifact}/EnumMethods.xml");
					CopyFile
					(
						$"./source/template-group-id/template-artifact/Transforms/EnumMethods.xml",
						$"{dir_artifact}/Transforms/EnumMethods.xml"
					);
				}

				if ( ! FileExists($"{dir_artifact}/Additions/Additions.cs"))
				{
					Warning($"     				Creating file : {dir_artifact}/Additions/Additions.cs");
					CopyFile
					(
						$"./source/template-group-id/template-artifact/Additions/Additions.cs",
						$"{dir_artifact}/Additions/Additions.cs"
					);
				}
            }

            return;
        }
    );

Task("mergetargets")
	.Does(() =>
{
	/*****************************
	* BEGIN: Merge all the .targets together into one for the sake of compiling samples
	******************************/
	var generatedTargets = GetFiles("./generated/*/Xamarin.*.targets");

	// Load the doc to append to, and the doc to append
	var xFileRoot = System.Xml.Linq.XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n</Project>");
	System.Xml.Linq.XNamespace nsRoot = xFileRoot.Root.Name.Namespace;

	foreach (var generatedTarget in generatedTargets) {
		var xFileChild = System.Xml.Linq.XDocument.Load (MakeAbsolute (generatedTarget).FullPath);
		System.Xml.Linq.XNamespace nsChild = xFileRoot.Root.Name.Namespace;

		// Add all the elements under <Project> into the existing file's <Project> node
		foreach (var xItemToAdd in xFileChild.Element (nsChild + "Project").Elements ())
			xFileRoot.Element (nsRoot + "Project").Add (xItemToAdd);
	}

	// Inject a property to prevent errors from missing assemblies in .targets
	// this allows us to use one big .targets file in all the projects and not have to figure out which specific
	// ones each project needs to reference for development purposes
	if (!xFileRoot.Descendants (nsRoot + "XamarinBuildResourceMergeThrowOnMissingAssembly").Any ()) {
		xFileRoot.Element (nsRoot + "Project")
			.AddFirst (new System.Xml.Linq.XElement (nsRoot + "PropertyGroup",
				new System.Xml.Linq.XElement (nsRoot + "XamarinBuildResourceMergeThrowOnMissingAssembly", false)));
	}

	xFileRoot.Save ("./generated/generated.targets");
	/*****************************
	* END: Merge all the .targets together into one for the sake of compiling samples
	******************************/
});

Task("libs-native")
	.Does(() =>
{
	string root = "./source/com.google.android.play/core.extensions/";

	RunGradle(root, "build");

	string outputDir = "./externals/com.xamarin.google.android.play.core.extensions/";
	EnsureDirectoryExists(outputDir);
	CleanDirectories(outputDir);

	CopyFileToDirectory($"{root}/extensions-aar/build/outputs/aar/extensions-aar-release.aar", outputDir);
	Unzip($"{outputDir}/extensions-aar-release.aar", outputDir);
	MoveFile($"{outputDir}/classes.jar", $"{outputDir}/extensions.jar");
});


Task("libs")
	.IsDependentOn("libs-native")
	.Does(() =>
{
	Configs = new string[] { "Release" };

	foreach(string config in Configs)
	{
		var settings = new DotNetMSBuildSettings()
							.SetConfiguration(config)
							.SetMaxCpuCount(MAX_CPU_COUNT)
							.EnableBinaryLogger("./output/libs.binlog")
							.WithProperty("NodeReuse", "false");

		settings.Properties.Add("DesignTimeBuild", new [] { "false" });
		settings.Properties.Add("AndroidSdkBuildToolsVersion", new [] { AndroidSdkBuildTools });
		
		if (!string.IsNullOrEmpty(ANDROID_HOME))
		{
			settings.Properties.Add("AndroidSdkDirectory", new [] { $"{ANDROID_HOME}" } );
		}

		DotNetRestore("./generated/GooglePlayServices.sln", new DotNetRestoreSettings
		{ 
			MSBuildSettings = settings.EnableBinaryLogger("./output/restore.binlog")
		});
		
		DotNetMSBuild("./generated/GooglePlayServices.sln", settings);
	}
});

Task("samples-directory-build-targets")
	.Does
	(
		() =>
		{
			Information("samples Director.Build.targets from config.json ...");
			using (StreamReader reader = System.IO.File.OpenText(@"./config.json"))
			{
				JsonTextReader jtr = new JsonTextReader(reader);
				binderator_json_array = (JArray)JToken.ReadFrom(jtr);
			}

			foreach(JObject jo in binderator_json_array[0]["artifacts"])
			{
				string version       = (string) jo["version"];
				string nuget_version = (string) jo["nugetVersion"];
				Information($"groupId       = {jo["groupId"]}");
				Information($"artifactId    = {jo["artifactId"]}");
				Information($"version       = {version}");
				Information($"nuget_version = {nuget_version}");
				Information($"nugetId       = {jo["nugetId"]}");
			}

	        XmlDocument doc = new XmlDocument();
	        XmlElement element_p = doc.CreateElement( string.Empty, "Project", string.Empty );
        	doc.AppendChild( element_p );
	       	XmlElement element_ig = doc.CreateElement( string.Empty, "ItemGroup", string.Empty );
        	element_p.AppendChild(element_ig);

			foreach(JObject jo in binderator_json_array[0]["artifacts"])
			{
				string version       = (string) jo["version"];
				string nuget_version = (string) jo["nugetVersion"];
				Information($"groupId       = {jo["groupId"]}");
				Information($"artifactId    = {jo["artifactId"]}");
				Information($"version       = {version}");
				Information($"nuget_version = {nuget_version}");
				Information($"nugetId       = {jo["nugetId"]}");

				XmlElement element_pr = doc.CreateElement( string.Empty, "PackageReference", string.Empty );
	        	element_ig.AppendChild(element_pr);
				XmlAttribute attr_update = doc.CreateAttribute("Update");
				attr_update.Value = (string) jo["nugetId"];
				element_pr.Attributes.Append(attr_update);
				XmlAttribute attr_version = doc.CreateAttribute("Version");
				attr_version.Value = nuget_version;
				element_pr.Attributes.Append(attr_version);
			}

			XmlElement xbd_pr = doc.CreateElement( string.Empty, "PackageReference", string.Empty );
			element_ig.AppendChild(xbd_pr);
			XmlAttribute xbd_attr_update = doc.CreateAttribute("Update");
			xbd_attr_update.Value = "Xamarin.Build.Download";
			xbd_pr.Attributes.Append(xbd_attr_update);
			XmlAttribute xbd_attr_version = doc.CreateAttribute("Version");
			xbd_attr_version.Value = "0.11.4";
			xbd_pr.Attributes.Append(xbd_attr_version);

			doc.Save( System.IO.Path.Combine("samples", "Directory.Build.targets" ));

			return;
		}
	);

Task("samples")
	.IsDependentOn("libs")
	.IsDependentOn("samples-only");

Task("samples-only")
	.IsDependentOn("samples-directory-build-targets")
	.IsDependentOn("mergetargets")
	.IsDependentOn("allbindingprojectrefs")
	.Does
(
	() =>
	{
		Configs = new string[] { "Debug", "Release" };

		DeleteDirectories(GetDirectories("./samples/**/bin/"), new DeleteDirectorySettings() { Force = true, Recursive = true });
		DeleteDirectories(GetDirectories("./samples/**/obj/"), new DeleteDirectorySettings() { Force = true, Recursive = true });

		EnsureDirectoryExists($@"./output/failed/");

		var sampleSlns = GetFiles("./samples/all/**/*.sln")
							.Concat(GetFiles("./samples/com.google.android.gms/**/*.sln"))
							.Concat(GetFiles("./samples/com.google.firebase/**/*.sln"))
							;

		foreach(string config in Configs)
		{
			foreach (var sampleSln in sampleSlns)
			{
				string filename_sln = sampleSln.GetFilenameWithoutExtension().ToString();

				if ( ! filename_sln.Contains("BuildAll") )
				{
					NuGetRestore(sampleSln, new NuGetRestoreSettings { }); // R8 errors
				}
				if
				(
					sampleSln.ToString().Contains("com.google.android.gms/play-services-cast/CastingCall.sln")
					||
					sampleSln.ToString().Contains("com.google.android.gms/play-services-games/BeGenerous.sln")
					||
					sampleSln.ToString().Contains("com.google.android.gms/play-services-wallet/AndroidPayQuickstart.sln")
					||
					sampleSln.ToString().Contains("com.google.firebase/firebase-analytics/FirebaseAnalyticsQuickstart.sln")
					||
					sampleSln.ToString().Contains("com.google.firebase/firebase-storage/FirebaseStorageQuickstart.sln")
				)
				{
					// skip problematic samples for now
					continue;
				}
				Information($"Solution: {filename_sln}");
				string bl = MakeAbsolute(new FilePath($"./output/{filename_sln}{config}.sample.binlog")).FullPath;
				try
				{
					MSBuild
						(
							sampleSln,
							c =>
							{
								c.Configuration = config;
								c.Properties.Add("DesignTimeBuild", new [] { "false" });
								c.BinaryLogger = new MSBuildBinaryLogSettings
														{
															Enabled = true,
															FileName = bl
														};
								if (! string.IsNullOrEmpty(ANDROID_HOME))
								{
									c.Properties.Add("AndroidSdkDirectory", new [] { $"{ANDROID_HOME}" } );
								}
							}
						);
				}
				catch (Exception exc)
				{
					Error($"Error: 	{exc}");
					Error($"   bl:	{bl}");
					Error($"   bl:	{bl.Replace($@"output", $@"output/failed")}");
					if ( FileExists(bl) )
					{
						DeleteFile(bl);
					}
					MoveFile(bl, bl.Replace($@"output", $@"output/failed"));
				}
			}
		}

		DeleteFiles(".output/system.*/nupkg");
		DeleteFiles(".output/microsoft.*/nupkg");
		DeleteFiles(".output/xamarin.android.support.*/nupkg");
		DeleteFiles(".output/xamarin.android.arch.*/nupkg");
		DeleteFiles(".output/xamarin.build.download.*/nupkg");
});

Task("samples-dotnet")
    .IsDependentOn("nuget")
    .IsDependentOn("samples-only")
    .IsDependentOn("samples-only-dotnet")
	;

Task("samples-only-dotnet")
	.IsDependentOn("samples-directory-build-targets")
	.IsDependentOn("mergetargets")
	.IsDependentOn("allbindingprojectrefs")
    .Does(() =>
{
    // clear the packages folder so we always use the latest
    var packagesPath = MakeAbsolute((DirectoryPath)"./samples/packages-dotnet").FullPath;
    EnsureDirectoryExists(packagesPath);
    CleanDirectories(packagesPath);

    var settings = new DotNetMSBuildSettings()
        .SetConfiguration("Debug") // We don't need to run linking
        .WithProperty("RestorePackagesPath", packagesPath)
        .WithProperty("AndroidSdkBuildToolsVersion", $"{AndroidSdkBuildTools}");

    if (!string.IsNullOrEmpty(ANDROID_HOME))
        settings.WithProperty("AndroidSdkDirectory", $"{ANDROID_HOME}");

    string[] solutions = new string[]
    {
        "./samples/dotnet/BuildAllDotNet.sln",
        "./samples/dotnet/BuildAllMauiApp.sln",
        "./samples/dotnet/BuildAllXamarinForms.sln",
    };

    foreach(string solution in solutions)
    {
        FilePath fp_solution = new FilePath(solution);
        string filename = fp_solution.GetFilenameWithoutExtension().ToString();
        Information($"=====================================================================================================");
        Information($"DotNetMSBuild        {solution} / {filename}");    
        DotNetBuild(solution, new DotNetBuildSettings
        {
            MSBuildSettings = settings.EnableBinaryLogger($"./output/samples-dotnet-dotnet-msbuild-{filename}.binlog")
        });
    }
});


Task("allbindingprojectrefs")
	.Does(() =>
{
	Action<string,string> generateTargets = (string pattern, string file) => {
		var xmlns = (XNamespace)"http://schemas.microsoft.com/developer/msbuild/2003";
		var itemGroup = new XElement(xmlns + "ItemGroup");
		foreach (var nupkg in GetFiles(pattern)) {
			var filename = nupkg.GetFilenameWithoutExtension();
		var match = Regex.Match(filename.ToString(), @"(.+?)\.(\d+[\.0-9\-a-zA-Z]+)");
		itemGroup.Add(new XElement(xmlns + "PackageReference",
			new XAttribute("Include", match.Groups[1]),
			new XAttribute("Version", match.Groups[2])));
		}
		var xdoc = new XDocument(new XElement(xmlns + "Project", itemGroup));
		xdoc.Save(file);

	};

	generateTargets("./output/Xamarin.Firebase.*.nupkg", "./output/FirebasePackages.targets");
	generateTargets("./output/Xamarin.GooglePlayServices.*.nupkg", "./output/PlayServicesPackages.targets");
	generateTargets("./output/Xamarin.Google.MLKit.*.nupkg", "./output/Google.MLKit.targets");
	generateTargets("./output/Xamarin.Google.Play.*.nupkg", "./output/Google.Play.targets");
});


Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	var outputPath = new DirectoryPath("./output");

	var settings = new DotNetMSBuildSettings()
		.SetConfiguration("Release")
		.SetMaxCpuCount(MAX_CPU_COUNT)
		.EnableBinaryLogger ("./output/nuget.binlog");
	settings.Targets.Clear();
	settings.Targets.Add("Pack");
	settings.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(outputPath).FullPath });
	settings.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
	settings.Properties.Add("DesignTimeBuild", new [] { "false" });
	settings.Properties.Add("AndroidSdkBuildToolsVersion", new [] { $"{AndroidSdkBuildTools}" });

	if (! string.IsNullOrEmpty(ANDROID_HOME))
	{
		settings.Properties.Add("AndroidSdkDirectory", new[] { $"{ANDROID_HOME}" });
	}

	DotNetMSBuild ("./generated/GooglePlayServices.sln", settings);
});

Task ("merge")
	.IsDependentOn ("libs")
	.Does (() =>
{
	var allDlls =
		GetFiles ($"./generated/*/bin/Release/monoandroid*/Xamarin.GooglePlayServices.*.dll") +
		GetFiles ($"./generated/*/bin/Release/monoandroid*/Xamarin.Firebase.*.dll");
	var mergeDlls = allDlls
		.GroupBy(d => new FileInfo(d.FullPath).Name)
		.Select(g => g.FirstOrDefault())
		.ToList();

	EnsureDirectoryExists("./output/");
	RunProcess("androidx-migrator",
		$"merge" +
		$"  --assembly " + string.Join(" --assembly ", mergeDlls) +
		$"  --output ./output/GooglePlayServices.Merged.dll" +
		$"  --search \"{XAMARIN_ANDROID_PATH}/{ANDROID_SDK_VERSION}\" " +
		$"  --search \"{XAMARIN_ANDROID_PATH}/{ANDROID_SDK_BASE_VERSION}\" " +
		$"  --inject-assemblyname");
});

Task ("ci-setup")
	.WithCriteria (!BuildSystem.IsLocalBuild)
	.Does (() =>
{
	var glob = "./source/AssemblyInfo.cs";

	ReplaceTextInFiles(glob, "{BUILD_COMMIT}", BUILD_COMMIT);
	ReplaceTextInFiles(glob, "{BUILD_NUMBER}", BUILD_NUMBER);
	ReplaceTextInFiles(glob, "{BUILD_TIMESTAMP}", BUILD_TIMESTAMP);
});

Task("nuget-dependecies")
	.Does
	(
		() =>
		{
			string icanhasdotnet = "https://icanhasdot.net/Downloads/ICanHasDotnetCore.zip";
		}
	);

Task("tools-executive-order")
    .Does
    (
        () =>
        {
            CakeExecuteScript
                        (
                            "./utilities.cake",
                            new CakeSettings
                            { 
                                Arguments = new Dictionary<string, string>() 
                                { 
                                    { "target", "tools-executive-order" } 
                                } 
                            }
                        );        
        }
    );

// Task ("genapi")
// 	.IsDependentOn ("libs")
// 	.Does (() =>
// {
// 	var GenApiToolPath = GetFiles ("./tools/**/GenAPI.exe").FirstOrDefault ();

// 	// For some reason GenAPI.exe can't handle absolute paths on mac/unix properly, so always make them relative
// 	// GenAPI.exe -libPath:$(MONOANDROID) -out:Some.generated.cs -w:TypeForwards ./relative/path/to/Assembly.dll
// 	var libDirPrefix = IsRunningOnWindows () ? "output/" : "";

// 	var libs = new FilePath [] {
// 		"./" + libDirPrefix + "Xamarin.Android.Support.Compat.dll",
// 		"./" + libDirPrefix + "Xamarin.Android.Support.Core.UI.dll",
// 		"./" + libDirPrefix + "Xamarin.Android.Support.Core.Utils.dll",
// 		"./" + libDirPrefix + "Xamarin.Android.Support.Fragment.dll",
// 		"./" + libDirPrefix + "Xamarin.Android.Support.Media.Compat.dll",
// 	};

// 	foreach (var lib in libs) {
// 		var genName = lib.GetFilename () + ".generated.cs";

// 		var libPath = IsRunningOnWindows () ? MakeAbsolute (lib).FullPath : lib.FullPath;
// 		var monoDroidPath = IsRunningOnWindows () ? "\"" + MONODROID_PATH + "\"" : MONODROID_PATH;

// 		Information ("GenAPI: {0}", lib.FullPath);

// 		StartProcess (GenApiToolPath, new ProcessSettings {
// 			Arguments = string.Format("-libPath:{0} -out:{1}{2} -w:TypeForwards {3}",
// 							monoDroidPath + "," + MSCORLIB_PATH,
// 							IsRunningOnWindows () ? "" : "./",
// 							genName,
// 							libPath),
// 			WorkingDirectory = "./output/",
// 		});
// 	}

// 	MSBuild ("./GooglePlayServices.TypeForwarders.sln", c => c.Configuration = BUILD_CONFIG);

// 	CopyFile ("./support-v4/source/bin/" + BUILD_CONFIG + "/Xamarin.Android.Support.v4.dll", "./output/Xamarin.Android.Support.v4.dll");
// });

// Task ("docs-api-diff")
//     .Does (async () =>
// {
// 	var nupkgFiles = GetFiles ("./**/output/*.nupkg"); //get all of the nugets in the output

// 	Information ("Found ({0}) Nuget's to Diff", nupkgFiles.Count ());

// 	foreach (var nupkgFile in nupkgFiles)  //loop through each nuget that is found
// 	{
// 		Information("Diffing: {0}", nupkgFile);
// 		await BuildApiDiff(nupkgFile);
// 	}
// });

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings { Recursive = true, Force = true });

	if (DirectoryExists ("./generated"))
		DeleteDirectory ("./generated", new DeleteDirectorySettings { Recursive = true, Force = true });

	CleanDirectories ("./**/packages");
	CleanDirectories("./**/bin");
	CleanDirectories("./**/obj");
});

Task ("ci")
    .IsDependentOn ("ci-build")
    .IsDependentOn ("ci-samples")
    ;
    
Task ("ci-build")
	.IsDependentOn ("ci-setup")
	//.IsDependentOn ("tools-check")
	//.IsDependentOn ("inject-variables")
	.IsDependentOn ("binderate")
	.IsDependentOn ("nuget")
	//.IsDependentOn ("merge")
  .IsDependentOn ("tools-executive-order")
  ;

Task ("ci-samples")
	.IsDependentOn ("samples-only")
  ;

RunTarget (TARGET);
