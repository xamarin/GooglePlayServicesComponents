// Tools needed by cake addins
#tool nuget:?package=ILRepack&version=2.0.13
#tool nuget:?package=Cake.MonoApiTools&version=3.0.1
//#tool nuget:?package=Microsoft.DotNet.BuildTools.GenAPI&version=1.0.0-beta-00081
#tool nuget:?package=vswhere

// Cake Addins
#addin nuget:?package=Cake.FileHelpers&version=3.1.0
#addin nuget:?package=Cake.Compression&version=0.1.6
#addin nuget:?package=Cake.MonoApiTools&version=3.0.1
#addin nuget:?package=Xamarin.Nuget.Validator&version=1.1.1
#addin nuget:?package=Mono.ApiTools.NuGetDiff&version=1.0.1&loaddependencies=true

// From Cake.Xamarin.Build, dumps out versions of things
//LogSystemInfo ();
using Mono.ApiTools;
using NuGet.Packaging;
using NuGet.Versioning;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var TARGET = Argument ("t", Argument ("target", "Default"));
var BUILD_CONFIG = Argument ("config", "Release");

// Lists all the artifacts and their versions for com.android.support.*
// https://dl.google.com/dl/android/maven2/com/android/support/group-index.xml
// Master list of all the packages in the repo:
// https://dl.google.com/dl/android/maven2/master-index.xml

var NUGET_PRE = "";

// FROM: https://dl.google.com/android/repository/addon2-1.xml
var BUILD_TOOLS_URL = "https://dl-ssl.google.com/android/repository/build-tools_r28-macosx.zip";
var ANDROID_SDK_VERSION = IsRunningOnWindows () ? "v9.0" : "android-28";
var RENDERSCRIPT_FOLDER = "android-8.1.0";
var TF_MONIKER = "monoandroid90";
var DOCAPI_CACHEPATH = "./externals/package_cache";

var REF_DOCS_URL = "https://bosstoragemirror.blob.core.windows.net/android-docs-scraper/a7/a712886a8b4ee709f32d51823223039883d38734/play-services-firebase.zip";
var REF_METADATA_URL = "https://bosstoragemirror.blob.core.windows.net/android-docs-scraper/a7/a712886a8b4ee709f32d51823223039883d38734/play-services-firebase-metadata.xml";

// These are a bunch of parameter names in the txt format which binding projects can use
var REF_PARAMNAMES_URL = "https://bosstoragemirror.blob.core.windows.net/android-docs-scraper/a7/a712886a8b4ee709f32d51823223039883d38734/play-services-firebase-paramnames.txt";

// We grab the previous release's api-info.xml to use as a comparison for this build's generated info to make an api-diff
var BASE_API_INFO_URL = EnvironmentVariable("MONO_API_INFO_XML_URL") ?? "https://github.com/xamarin/GooglePlayServicesComponents/releases/download/60.1142.0/api-info.xml";

var MONODROID_PATH = "/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mandroid/platforms/" + ANDROID_SDK_VERSION + "/";
if (IsRunningOnWindows ()) {
	var vsInstallPath = VSWhereLatest (new VSWhereLatestSettings { Requires = "Component.Xamarin" });
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

if (Argument("localTestPkg", "false") == "true")
	Environment.SetEnvironmentVariable("LOCAL_TEST_PKG", "true");

Information ("MONODROID_PATH: {0}", MONODROID_PATH);
Information ("MSCORLIB_PATH: {0}", MSCORLIB_PATH);

//set the function to download and compare the previous version of the nugets
async Task BuildApiDiff (FilePath nupkg)
{
	var baseDir = nupkg.GetDirectory(); //get the parent directory of the packge file

	using (var reader = new PackageArchiveReader (nupkg.FullPath)) 
	{
		//get the id from the package and the version number
		 var packageId = reader.GetIdentity ().Id;
		var currentVersionNo = reader.GetIdentity ().Version.ToNormalizedString();

		//calculate the diff storage path from the location of the nuget
		var diffRoot = $"{baseDir}/api-diff/{packageId}";
		CleanDirectories (diffRoot);

		// get the latest version of this package - if any
		var latestVersion = (await NuGetVersions.GetLatestAsync (packageId))?.ToNormalizedString ();

		// log what is going to happen
		if (string.IsNullOrEmpty (latestVersion))
			Information ($"Running a diff on a new package '{packageId}'...");
		else
			Information ($"Running a diff on '{latestVersion}' vs '{currentVersionNo}' of '{packageId}'...");

		// create comparer
		var comparer = new NuGetDiff ();
		comparer.PackageCache = DOCAPI_CACHEPATH;  // Cache path
		comparer.SaveAssemblyApiInfo = true;       // we don't keep this, but it lets us know if there were no changes
		comparer.SaveAssemblyMarkdownDiff = true;  // we want markdown
		comparer.IgnoreResolutionErrors = true;    // we don't care if frameowrk/platform types can't be found

		await comparer.SaveCompleteDiffToDirectoryAsync (packageId, latestVersion, reader, diffRoot);

		// run the diff with just the breaking changes
		comparer.MarkdownDiffFileExtension = ".breaking.md";
		comparer.IgnoreNonBreakingChanges = true;
		await comparer.SaveCompleteDiffToDirectoryAsync (packageId, latestVersion, reader, diffRoot);

		// TODO: there are two bugs in this version of mono-api-html
		var mdFiles = $"{diffRoot}/*.*.md";
		// 1. the <h4> doesn't look pretty in the markdown
		ReplaceTextInFiles (mdFiles, "<h4>", "> ");
		ReplaceTextInFiles (mdFiles, "</h4>", Environment.NewLine); 
		// 2. newlines are inccorect on Windows: https://github.com/mono/mono/pull/9918
		ReplaceTextInFiles (mdFiles, "\r\r", "\r");

		// we are done
		Information ($"Diff complete of '{packageId}'.");
	}
}

// You shouldn't have to configure anything below here
// ######################################################

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


Task("binderate")
	.IsDependentOn("javadocs")
	.IsDependentOn("binderate-config-verify")
	.Does(() =>
{
	if (!DirectoryExists("./util/binderator"))
	{
		EnsureDirectoryExists("./util/binderator");
		Unzip ("./util/binderator.zip", "./util/binderator");
	}

	var configFile = new FilePath("./config.json");
	var basePath = new DirectoryPath ("./");

	StartProcess("dotnet", "./util/binderator/android-binderator.dll --config=\""
		+ MakeAbsolute(configFile).FullPath + "\" --basepath=\"" + MakeAbsolute(basePath).FullPath + "\"");
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
		c.BinaryLogger = new MSBuildBinaryLogSettings { Enabled = true, FileName = MakeAbsolute(new FilePath("./output/nuget.binlog")).FullPath };
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(outputPath).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { "28.0.3" });
    });
});

Task("nuget-validation")
    .IsDependentOn("nuget")
	.Does(()=>
{
	//setup validation options
	var options = new Xamarin.Nuget.Validator.NugetValidatorOptions()
	{
		Copyright = "© Microsoft Corporation. All rights reserved.",
		Author = "Microsoft",
		Owner = "Microsoft",
		NeedsProjectUrl = true,
		NeedsLicenseUrl = true,
		ValidateRequireLicenseAcceptance = true,
		ValidPackageNamespace = new [] { "Xamarin" },
	};

	var nupkgFiles = GetFiles ("./output/*.nupkg");

	Information ("Found ({0}) Nuget's to validate", nupkgFiles.Count ());

	foreach (var nupkgFile in nupkgFiles)
	{
		Information ("Verifiying Metadata of {0}", nupkgFile.GetFilename ());

		var result = Xamarin.Nuget.Validator.NugetValidator.Validate(MakeAbsolute(nupkgFile).FullPath, options);

		if (!result.Success)
		{
			Information ("Metadata validation failed for: {0} \n\n", nupkgFile.GetFilename ());
			Information (string.Join("\n    ", result.ErrorMessages));
			throw new Exception ($"Invalid Metadata for: {nupkgFile.GetFilename ()}");
		}
		else
		{
			Information ("Metadata validation passed for: {0}", nupkgFile.GetFilename ());
		}
	}
});

Task ("diff")
	.WithCriteria (!IsRunningOnWindows ())
	.IsDependentOn ("merge")
	.Does (() =>
{
	if (DirectoryExists("./output/dependencies"))
		DeleteDirectory("./output/dependencies", true);
	EnsureDirectoryExists("./output/dependencies/");

	// Copy all the dependencies into one spot to reference from MonoApiTools
	CopyFiles("./generated/**/bin/Release/" + TF_MONIKER + "/*.dll", "./output/dependencies/");

	var SEARCH_DIRS = new DirectoryPath [] {
		MONODROID_PATH,
		"/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/xbuild-frameworks/MonoAndroid/v1.0/",
		"/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mono/2.1/",
		"./output/dependencies/"
	};

	MonoApiInfo ("./output/GooglePlayServices.Merged.dll",
		"./output/GooglePlayServices.api-info.xml",
		new MonoApiInfoToolSettings { SearchPaths = SEARCH_DIRS });

	// Grab the last public release's api-info.xml to use as a base to compare and make an API diff
	DownloadFile (BASE_API_INFO_URL, "./output/GooglePlayServices.api-info.previous.xml");

	// Now diff against current release'd api info
	// eg: mono mono-api-diff.exe ./gps.r26.xml ./gps.r27.xml > gps.diff.xml
	MonoApiDiff ("./output/GooglePlayServices.api-info.previous.xml",
		"./output/GooglePlayServices.api-info.xml",
		"./output/GooglePlayServices.api-diff.xml");

	// Now let's make a purty html file
	// eg: mono mono-api-html.exe -c -x ./gps.previous.info.xml ./gps.current.info.xml > gps.diff.html
	MonoApiHtml ("./output/GooglePlayServices.api-info.previous.xml",
		"./output/GooglePlayServices.api-info.xml",
		"./output/GooglePlayServices.api-diff.html");

	if (DirectoryExists("./output/dependencies"))
		DeleteDirectory("./output/dependencies", true);
});

Task ("merge")
	.IsDependentOn ("libs")
	.Does (() =>
{
	EnsureDirectoryExists("./output/");
	if (DirectoryExists("./output/dependencies"))
		DeleteDirectory("./output/dependencies", true);
	EnsureDirectoryExists("./output/dependencies/");

	// Copy all the dependencies into one spot to reference from ILRepack
	CopyFiles("./generated/**/bin/Release/" + TF_MONIKER + "/*.dll", "./output/dependencies/");

	if (FileExists ("./output/GooglePlayServices.Merged.dll"))
		DeleteFile ("./output/GooglePlayServices.Merged.dll");

	var allDlls = GetFiles ("./generated/**/bin/Release/" + TF_MONIKER + "/Xamarin.GooglePlayServices*.dll")
					.Concat(GetFiles ("./generated/**/bin/Release/" + TF_MONIKER + "/Xamarin.Firebase*.dll"));

	var mergeDlls = allDlls
		.GroupBy(d => new FileInfo(d.FullPath).Name)
		.Select(g => g.FirstOrDefault())
		.ToList();

	Information("Merging: \n {0}", string.Join("\n", mergeDlls));

	// Wait for ILRepack support in cake-0.5.2
	ILRepack ("./output/GooglePlayServices.Merged.dll", mergeDlls.First(), mergeDlls.Skip(1), new ILRepackSettings {
		CopyAttrs = true,
		AllowMultiple = true,
		//TargetKind = ILRepack.TargetKind.Dll,
		Libs = new List<DirectoryPath> {
			MONODROID_PATH,
			"./output/dependencies/"
		},
	});

	if (DirectoryExists("./output/dependencies"))
		DeleteDirectory("./output/dependencies", true);
});

Task ("ci-setup")
	.WithCriteria (!BuildSystem.IsLocalBuild)
	.Does (() => 
{
	var buildCommit = "DEV";
	var buildNumber = "DEBUG";
	var buildTimestamp = DateTime.UtcNow.ToString ();

	if (BuildSystem.IsRunningOnJenkins) {
		buildNumber = BuildSystem.Jenkins.Environment.Build.BuildTag;
		buildCommit = EnvironmentVariable("GIT_COMMIT") ?? buildCommit;
	} else if (BuildSystem.IsRunningOnVSTS) {
		buildNumber = BuildSystem.TFBuild.Environment.Build.Number;
		buildCommit = BuildSystem.TFBuild.Environment.Repository.SourceVersion;
	}

	var glob = "./source/AssemblyInfo.cs";

	ReplaceTextInFiles(glob, "{BUILD_COMMIT}", buildCommit);
	ReplaceTextInFiles(glob, "{BUILD_NUMBER}", buildNumber);
	ReplaceTextInFiles(glob, "{BUILD_TIMESTAMP}", buildTimestamp);
});

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

Task ("docs-api-diff")
    .Does (async () =>
{
	var nupkgFiles = GetFiles ("./**/output/*.nupkg"); //get all of the nugets in the output

	Information ("Found ({0}) Nuget's to Diff", nupkgFiles.Count ());

	foreach (var nupkgFile in nupkgFiles)  //loop through each nuget that is found
	{
		Information("Diffing: {0}", nupkgFile);
		await BuildApiDiff(nupkgFile);
	}
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);

	if (DirectoryExists ("./generated"))
		DeleteDirectory ("./generated", true);

	if (DirectoryExists ("./util/binderator"))
		DeleteDirectory ("./util/binderator", true);

	CleanDirectories ("./**/packages");
	CleanDirectories("./**/bin");
	CleanDirectories("./**/obj");
});

Task ("ci")
	.IsDependentOn ("ci-setup")
	.IsDependentOn ("binderate")
	.IsDependentOn ("diff")
	.IsDependentOn ("docs-api-diff")
	.IsDependentOn ("nuget-validation")
	.IsDependentOn ("samples");

RunTarget (TARGET);
