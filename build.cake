// Tools needed by cake addins
#tool nuget:?package=vswhere&version=2.7.1

// Cake Addins
#addin nuget:?package=Cake.FileHelpers&version=3.2.1
#addin nuget:?package=Newtonsoft.Json&version=11.0.2

using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var TARGET = Argument ("t", Argument ("target", "Default"));
var BUILD_CONFIG = Argument ("config", "Release");
var MAX_CPU_COUNT = Argument("maxcpucount", 0);

// Lists all the artifacts and their versions for com.android.support.*
// https://dl.google.com/dl/android/maven2/com/android/support/group-index.xml
// Master list of all the packages in the repo:
// https://dl.google.com/dl/android/maven2/master-index.xml

var REF_DOCS_URL = "https://bosstoragemirror.blob.core.windows.net/android-docs-scraper/a7/a712886a8b4ee709f32d51823223039883d38734/play-services-firebase.zip";
var REF_METADATA_URL = "https://bosstoragemirror.blob.core.windows.net/android-docs-scraper/a7/a712886a8b4ee709f32d51823223039883d38734/play-services-firebase-metadata.xml";

// These are a bunch of parameter names in the txt format which binding projects can use
var REF_PARAMNAMES_URL = "https://bosstoragemirror.blob.core.windows.net/android-docs-scraper/a7/a712886a8b4ee709f32d51823223039883d38734/play-services-firebase-paramnames.txt";

// Resolve Xamarin.Android installation
var XAMARIN_ANDROID_PATH = EnvironmentVariable ("XAMARIN_ANDROID_PATH");
var ANDROID_SDK_BASE_VERSION = "v1.0";
var ANDROID_SDK_VERSION = "v9.0";
if (string.IsNullOrEmpty(XAMARIN_ANDROID_PATH)) {
	if (IsRunningOnWindows()) {
		var vsInstallPath = VSWhereLatest(new VSWhereLatestSettings { Requires = "Component.Xamarin" });
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

// Log some variables
Information ("XAMARIN_ANDROID_PATH: {0}", XAMARIN_ANDROID_PATH);
Information ("ANDROID_SDK_VERSION:  {0}", ANDROID_SDK_VERSION);
Information ("BUILD_COMMIT:         {0}", BUILD_COMMIT);
Information ("BUILD_NUMBER:         {0}", BUILD_NUMBER);
Information ("BUILD_TIMESTAMP:      {0}", BUILD_TIMESTAMP);

// You shouldn't have to configure anything below here
// ######################################################

void RunProcess(FilePath fileName, string processArguments)
{
	var exitCode = StartProcess(fileName, processArguments);
	if (exitCode != 0)
		throw new Exception ($"Process {fileName} exited with code {exitCode}.");
}

string[] RunProcessWithOutput(FilePath fileName, string processArguments)
{
	var exitCode = StartProcess(fileName, new ProcessSettings {
		Arguments = processArguments,
		RedirectStandardOutput = true,
		RedirectStandardError = true
	}, out var procOut);
	if (exitCode != 0)
		throw new Exception ($"Process {fileName} exited with code {exitCode}.");
	return procOut.ToArray();;
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

Task("check-tools")
	.Does(() =>
{
	var installedTools = RunProcessWithOutput("dotnet", "tool list -g");
	foreach (var toolName in REQUIRED_DOTNET_TOOLS) {
		if (installedTools.All(l => l.IndexOf(toolName, StringComparison.OrdinalIgnoreCase) == -1))
			throw new Exception ($"Missing dotnet tool: {toolName}");
	}
});

Task("binderate")
	.IsDependentOn("javadocs")
	.IsDependentOn("binderate-config-verify")
	.Does(() =>
{
	var configFile = MakeAbsolute(new FilePath("./config.json")).FullPath;
	var basePath = MakeAbsolute(new DirectoryPath ("./")).FullPath;

	RunProcess("xamarin-android-binderator",
		$"--config=\"{configFile}\" --basepath=\"{basePath}\"");
});

string nuget_version_template = "71.vvvv.0-preview3";

Task("binderate-config-verify")
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
					string version       = (string) jo["version"];
					string nuget_version = (string) jo["nugetVersion"];
					Information($"groupId       = {jo["groupId"]}");
					Information($"artifactId    = {jo["artifactId"]}");
					Information($"version       = {version}");
					Information($"nuget_version = {nuget_version}");
					Information($"nugetId       = {jo["nugetId"]}");
					
					string version_compressed = version.Replace(".", "");
					if( nuget_version?.Contains(version_compressed) == false)
					{
						Error("check config.json for nuget id");
						Error  ($"		groupId       = {jo["groupId"]}");
						Error  ($"		artifactId    = {jo["artifactId"]}");
						Error  ($"		version       = {version}");
						Error  ($"		nuget_version = {nuget_version}");
						Error  ($"		nugetId       = {jo["nugetId"]}");

						string nuget_version_new = nuget_version_template.Replace("vvvv", version_compressed);
						Warning($"	expected : ");
						Warning($"		nuget_version = {nuget_version_new}");
						throw new Exception("check config.json for nuget id");
					}
				}
			}
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


Task("libs")
	.IsDependentOn("nuget-restore")
	.Does(() =>
{
	NuGetRestore("./generated/GooglePlayServices.sln", new NuGetRestoreSettings { });

	MSBuild("./generated/GooglePlayServices.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = MAX_CPU_COUNT;
		c.BinaryLogger = new MSBuildBinaryLogSettings { Enabled = true, FileName = MakeAbsolute(new FilePath("./output/libs.binlog")).FullPath };
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { "28.0.3" });
	});
});


Task("samples")
	.IsDependentOn("libs")
	.IsDependentOn("mergetargets")
	.IsDependentOn("allbindingprojectrefs")
	.Does(() =>
{
	var sampleSlns = GetFiles("./samples/**/*.sln");

	foreach (var sampleSln in sampleSlns) {
		NuGetRestore(sampleSln, new NuGetRestoreSettings { });
		string filename_sln = sampleSln.GetFilenameWithoutExtension().ToString();
		Information($"Solution: {filename_sln}");
		MSBuild(sampleSln, c => {
			c.Configuration = "Release";
			c.Properties.Add("DesignTimeBuild", new [] { "false" });
			c.BinaryLogger = new MSBuildBinaryLogSettings 
			{
				Enabled = true, 
				FileName = MakeAbsolute(new FilePath($"./output/{filename_sln}.sample.binlog")).FullPath 
			};
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

	generateTargets("./output/*firebase*.nupkg", "./output/FirebasePackages.targets");
	generateTargets("./output/*play-services*.nupkg", "./output/PlayServicesPackages.targets");
});

Task("nuget-restore")
	.Does(() =>
{
	NuGetRestore("./generated/GooglePlayServices.sln", new NuGetRestoreSettings { });
});


Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	var outputPath = new DirectoryPath("./output");

	MSBuild ("./generated/GooglePlayServices.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = MAX_CPU_COUNT;
		c.BinaryLogger = new MSBuildBinaryLogSettings { Enabled = true, FileName = MakeAbsolute(new FilePath("./output/nuget.binlog")).FullPath };
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(outputPath).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { "28.0.3" });
    });
});

Task ("merge")
	.IsDependentOn ("libs")
	.Does (() =>
{
	var allDlls = 
		GetFiles ($"./generated/*/bin/{BUILD_CONFIG}/monoandroid*/Xamarin.GooglePlayServices.*.dll") +
		GetFiles ($"./generated/*/bin/{BUILD_CONFIG}/monoandroid*/Xamarin.Firebase.*.dll");
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

Task("inject-variables")
	.WithCriteria(!BuildSystem.IsLocalBuild)
	.Does(() =>
{
	var glob = "./source/AssemblyInfo.cs";

	ReplaceTextInFiles(glob, "{BUILD_COMMIT}", BUILD_COMMIT);
	ReplaceTextInFiles(glob, "{BUILD_NUMBER}", BUILD_NUMBER);
	ReplaceTextInFiles(glob, "{BUILD_TIMESTAMP}", BUILD_TIMESTAMP);
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);

	if (DirectoryExists ("./generated"))
		DeleteDirectory ("./generated", true);

	CleanDirectories ("./**/packages");
	CleanDirectories("./**/bin");
	CleanDirectories("./**/obj");
});

Task ("ci")
	.IsDependentOn ("check-tools")
	.IsDependentOn ("inject-variables")
	.IsDependentOn ("binderate")
	.IsDependentOn ("nuget")
	.IsDependentOn ("merge")
	.IsDependentOn ("samples");

RunTarget (TARGET);
