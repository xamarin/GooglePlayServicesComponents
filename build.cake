#tool nuget:?package=ILRepack&version=2.0.10
#tool nuget:?package=XamarinComponent&version=1.1.0.29

#addin nuget:?package=Cake.XCode&version=1.0.4.0
#addin nuget:?package=Cake.Xamarin&version=1.3.0.1
#addin nuget:?package=Cake.Xamarin.Build&version=1.0.11.0
#addin nuget:?package=Cake.FileHelpers&version=1.0.3.2

var TARGET = Argument ("t", Argument ("target", "Default"));

var COMPONENT_VERSION = "29.0.0.2";
var NUGET_VERSION = "29.0.0.2-beta2";
var COMPONENT_WEAR_VERSION = "1.3.0.4";
var NUGET_WEAR_VERSION = "1.3.0.4-beta2";
var AAR_VERSION = "8.4.0";
var WEARABLE_SUPPORT_VERSION = "1.3.0";
var WEARABLE_VERSION = "1.0.0";

var M2_REPOSITORY = "https://dl-ssl.google.com/android/repository/google_m2repository_r24.zip";
// https://dl-ssl.google.com/android/repository/addon.xml to find url
var DOCS_URL = "https://dl-ssl.google.com/android/repository/google_play_services_8487000_r29.zip";

var AAR_DIRS = new [] {
	"play-services", "play-services-all-wear", "play-services-base", "play-services-basement", "play-services-ads", "play-services-analytics", "play-services-appindexing",
	"play-services-appinvite", "play-services-appstate", "play-services-cast", "play-services-drive", "play-services-fitness", "play-services-games",
	"play-services-gcm", "play-services-identity", "play-services-location", "play-services-maps", "play-services-measurement", "play-services-nearby", "play-services-panorama",
	"play-services-plus", "play-services-safetynet", "play-services-vision", "play-services-wallet", "play-services-wearable", "play-services-auth"
};

var MONODROID_PATH = "/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mandroid/platforms/android-23/";
if (IsRunningOnWindows ())
	MONODROID_PATH = new DirectoryPath (Environment.GetFolderPath (Environment.SpecialFolder.ProgramFilesX86)).Combine ("Reference Assemblies/Microsoft/Framework/MonoAndroid/v6.0/").FullPath;

var buildsOnWinMac = BuildPlatforms.Windows | BuildPlatforms.Mac;

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./GooglePlayServices.sln",
			BuildsOn = BuildPlatforms.Windows | BuildPlatforms.Mac,
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./base/source/bin/Release/Xamarin.GooglePlayServices.Base.dll" },
				new OutputFileCopy { FromFile = "./basement/source/bin/Release/Xamarin.GooglePlayServices.Basement.dll" },
				new OutputFileCopy { FromFile = "./ads/source/bin/Release/Xamarin.GooglePlayServices.Ads.dll" },
				new OutputFileCopy { FromFile = "./analytics/source/bin/Release/Xamarin.GooglePlayServices.Analytics.dll" },
				new OutputFileCopy { FromFile = "./appinvite/source/bin/Release/Xamarin.GooglePlayServices.AppInvite.dll" },
				new OutputFileCopy { FromFile = "./appindexing/source/bin/Release/Xamarin.GooglePlayServices.AppIndexing.dll" },
				new OutputFileCopy { FromFile = "./appstate/source/bin/Release/Xamarin.GooglePlayServices.AppState.dll" },
				new OutputFileCopy { FromFile = "./auth/source/bin/Release/Xamarin.GooglePlayServices.Auth.dll" },
				new OutputFileCopy { FromFile = "./cast/source/bin/Release/Xamarin.GooglePlayServices.Cast.dll" },
				new OutputFileCopy { FromFile = "./drive/source/bin/Release/Xamarin.GooglePlayServices.Drive.dll" },
				new OutputFileCopy { FromFile = "./fitness/source/bin/Release/Xamarin.GooglePlayServices.Fitness.dll" },
				new OutputFileCopy { FromFile = "./games/source/bin/Release/Xamarin.GooglePlayServices.Games.dll" },
				new OutputFileCopy { FromFile = "./gcm/source/bin/Release/Xamarin.GooglePlayServices.Gcm.dll" },
				new OutputFileCopy { FromFile = "./identity/source/bin/Release/Xamarin.GooglePlayServices.Identity.dll" },
				new OutputFileCopy { FromFile = "./location/source/bin/Release/Xamarin.GooglePlayServices.Location.dll" },
				new OutputFileCopy { FromFile = "./maps/source/bin/Release/Xamarin.GooglePlayServices.Maps.dll" },
				new OutputFileCopy { FromFile = "./measurement/source/bin/Release/Xamarin.GooglePlayServices.Measurement.dll" },
				new OutputFileCopy { FromFile = "./nearby/source/bin/Release/Xamarin.GooglePlayServices.Nearby.dll" },
				new OutputFileCopy { FromFile = "./panorama/source/bin/Release/Xamarin.GooglePlayServices.Panorama.dll" },
				new OutputFileCopy { FromFile = "./plus/source/bin/Release/Xamarin.GooglePlayServices.Plus.dll" },
				new OutputFileCopy { FromFile = "./safetynet/source/bin/Release/Xamarin.GooglePlayServices.SafetyNet.dll" },
				new OutputFileCopy { FromFile = "./vision/source/bin/Release/Xamarin.GooglePlayServices.Vision.dll" },
				new OutputFileCopy { FromFile = "./wallet/source/bin/Release/Xamarin.GooglePlayServices.Wallet.dll" },
				new OutputFileCopy { FromFile = "./wearable/source/bin/Release/Xamarin.GooglePlayServices.Wearable.dll" },
				new OutputFileCopy { FromFile = "./support-wearable/source/bin/Release/Xamarin.Android.Wearable.dll" },
			}
		},
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./ads/samples/AdMobSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./analytics/samples/AnalyticsSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./appinvite/samples/AppInviteSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./appindexing/samples/AppIndexingSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./cast/samples/CastingCall.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./drive/samples/DriveSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./fitness/samples/BasicSensorsApi.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./games/samples/BeGenerous.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./gcm/samples/GCMSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./location/samples/LocationSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./location/samples/PlacesAsync.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./maps/samples/MapsSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./nearby/samples/NearbySample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./panorama/samples/PanoramaSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./plus/samples/PlusSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./safetynet/samples/SafetyNetSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./vision/samples/VisionSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./wallet/samples/AndroidPayQuickstart.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./support-wearable/samples/MultiPageSample.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./support-wearable/samples/WatchFaceSample.sln", BuildsOn = buildsOnWinMac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./base/nuget/Xamarin.GooglePlayServices.Base.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./basement/nuget/Xamarin.GooglePlayServices.Basement.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./ads/nuget/Xamarin.GooglePlayServices.Ads.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./analytics/nuget/Xamarin.GooglePlayServices.Analytics.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./appindexing/nuget/Xamarin.GooglePlayServices.AppIndexing.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./appinvite/nuget/Xamarin.GooglePlayServices.AppInvite.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./appstate/nuget/Xamarin.GooglePlayServices.AppState.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./auth/nuget/Xamarin.GooglePlayServices.Auth.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./cast/nuget/Xamarin.GooglePlayServices.Cast.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./drive/nuget/Xamarin.GooglePlayServices.Drive.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./fitness/nuget/Xamarin.GooglePlayServices.Fitness.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./games/nuget/Xamarin.GooglePlayServices.Games.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./gcm/nuget/Xamarin.GooglePlayServices.Gcm.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./identity/nuget/Xamarin.GooglePlayServices.Identity.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./location/nuget/Xamarin.GooglePlayServices.Location.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./maps/nuget/Xamarin.GooglePlayServices.Maps.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./measurement/nuget/Xamarin.GooglePlayServices.Measurement.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./nearby/nuget/Xamarin.GooglePlayServices.Nearby.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./panorama/nuget/Xamarin.GooglePlayServices.Panorama.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./plus/nuget/Xamarin.GooglePlayServices.Plus.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./safetynet/nuget/Xamarin.GooglePlayServices.SafetyNet.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./vision/nuget/Xamarin.GooglePlayServices.Vision.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./wallet/nuget/Xamarin.GooglePlayServices.Wallet.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./wearable/nuget/Xamarin.GooglePlayServices.Wearable.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./support-wearable/nuget/Xamarin.Android.Wear.nuspec", Version = NUGET_WEAR_VERSION, RequireLicenseAcceptance = true },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./ads/component" },
		new Component { ManifestDirectory = "./analytics/component" },
		new Component { ManifestDirectory = "./appindexing/component" },
		new Component { ManifestDirectory = "./appinvite/component" },
		new Component { ManifestDirectory = "./cast/component" },
		new Component { ManifestDirectory = "./drive/component" },
		new Component { ManifestDirectory = "./fitness/component" },
		new Component { ManifestDirectory = "./games/component" },
		new Component { ManifestDirectory = "./gcm/component" },
		new Component { ManifestDirectory = "./location/component" },
		new Component { ManifestDirectory = "./maps/component" },
		new Component { ManifestDirectory = "./nearby/component" },
		new Component { ManifestDirectory = "./panorama/component" },
		new Component { ManifestDirectory = "./plus/component" },
		new Component { ManifestDirectory = "./safetynet/component" },
		new Component { ManifestDirectory = "./vision/component" },
		new Component { ManifestDirectory = "./wallet/component" },
		new Component { ManifestDirectory = "./support-wearable/component" },
	}
};



Task ("externals")
	.WithCriteria (() => !FileExists ("./externals/play-services-base.aar")).Does (() =>
{
	var path = "./externals/";

	if (!DirectoryExists (path))
		CreateDirectory (path);

	// Get the actual GPS .aar files and extract
	if (!FileExists (path + "m2repository.zip"))
		DownloadFile (M2_REPOSITORY, path + "m2repository.zip");
	if (!FileExists (path + "m2repository/source.properties"))
		Unzip (path + "m2repository.zip", path);

	// Copy the .aar's to a better location
	foreach (var aar in AAR_DIRS) {
		CopyFile (
			string.Format (path + "m2repository/com/google/android/gms/{0}/{1}/{2}-{3}.aar", aar, AAR_VERSION, aar, AAR_VERSION),
			string.Format (path + "{0}.aar", aar));
		Unzip (path + aar + ".aar", path + aar);
	}

	// Get Wearable stuff
	Unzip (path + "m2repository/com/google/android/support/wearable/" + WEARABLE_SUPPORT_VERSION + "/wearable-" + WEARABLE_SUPPORT_VERSION + ".aar", path + "wearable-support");
	CopyFile (path + "m2repository/com/google/android/wearable/wearable/" + WEARABLE_VERSION + "/wearable-" + WEARABLE_VERSION + ".jar", path + "wearable.jar");

	// Get the GPS Docs
	if (!FileExists (path + "docs.zip"))
		DownloadFile (DOCS_URL, path + "docs.zip");
	// Move the docs around
	if (!DirectoryExists (path + "docs")) {
		Unzip (path + "docs.zip", path);
		CopyDirectory (path + "google-play-services/docs", path + "docs/");
		DeleteDirectory (path + "google-play-services", true);
	}
});


Task ("merge").IsDependentOn ("libs").Does (() =>
{
	// Clean up any existing old merge files
	DeleteFiles ("./output/GooglePlayServices.Merged.dll");

	var mergeDlls = GetFiles ("./output/*.dll");

	// Wait for ILRepack support in cake-0.5.2
	ILRepack ("./output/GooglePlayServices.Merged.dll", mergeDlls.First(), mergeDlls.Skip(1), new ILRepackSettings {
		CopyAttrs = true,
		AllowMultiple = true,
		//TargetKind = ILRepack.TargetKind.Dll,
		Libs = new List<FilePath> {
			MONODROID_PATH
		},
	});

	// Don't want to think about what the paths will do to this on windows right now
	if (!IsRunningOnWindows ()) {
		// Next run the mono-api-info.exe to generate xml api info we can later diff with
		var monoApiInfoExe = GetFiles ("../../**/mono-api-info.exe").FirstOrDefault ();
		var monoApiDiffExe = GetFiles ("../../**/mono-api-diff.exe").FirstOrDefault ();
		var monoApiHtmlExe = GetFiles ("../../**/mono-api-html.exe").FirstOrDefault ();

		//eg: mono mono-api-info.exe --search-directory=/Library/Frameworks/Xamarin.Android.framework/Libraries/mandroid/platforms/android-23 ./Xamarin.GooglePlayServices.r27.dll > gps.r27.xml
		using(var process = StartAndReturnProcess (monoApiInfoExe, new ProcessSettings {
			Arguments = "--search-directory=" + MONODROID_PATH + " ./output/GooglePlayServices.Merged.dll",
			RedirectStandardOutput = true,
		})) {
		    process.WaitForExit();
		    FileWriteLines ("./output/GooglePlayServices.api-info.xml", process.GetStandardOutput ().ToArray ());
		}

		// Grab the latest published api info from S3
		var latestReleasedApiInfoUrl = "http://xamarin-components-apiinfo.s3.amazonaws.com/GooglePlayServices.Android-Latest.xml";
		DownloadFile (latestReleasedApiInfoUrl, "./output/GooglePlayServices.api-info.previous.xml");

		// Now diff against current release'd api info
		// eg: mono mono-api-diff.exe ./gps.r26.xml ./gps.r27.xml > gps.diff.xml
		using(var process = StartAndReturnProcess (monoApiDiffExe, new ProcessSettings {
			Arguments = "./output/GooglePlayServices.api-info.previous.xml ./output/GooglePlayServices.api-info.xml",
			RedirectStandardOutput = true,
		})) {
		    process.WaitForExit();
		    FileWriteLines ("./output/GooglePlayServices.api-diff.xml", process.GetStandardOutput ().ToArray ());
		}

		// Now let's make a purty html file
		// eg: mono mono-api-html.exe -c -x ./gps.previous.info.xml ./gps.current.info.xml > gps.diff.html
		using(var process = StartAndReturnProcess (monoApiHtmlExe, new ProcessSettings {
			Arguments = "-c -x ./output/GooglePlayServices.api-info.previous.xml ./output/GooglePlayServices.api-info.xml",
			RedirectStandardOutput = true,
		})) {
			process.WaitForExit();
			FileWriteLines ("./output/GooglePlayServices.api-diff.html", process.GetStandardOutput ().ToArray ());
		}
	}
});


Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

Task ("component-docs").Does (() =>
{
	var gettingStartedTemplates = new Dictionary<string, string> ();

	foreach (var f in GetFiles ("./component/GettingStarted.*.md")) {

		var key = f.GetFilenameWithoutExtension ().FullPath.Replace ("GettingStarted.", "");
		var val = TransformTextFile (f).ToString ();

		gettingStartedTemplates.Add (key, val);
	}

	var componentDirs = GetDirectories ("./*");

	foreach (var compDir in componentDirs)
		Information ("Found: {0}", compDir);

	foreach (var compDir in componentDirs) {

		var f = compDir.CombineWithFilePath ("./component/GettingStarted.template.md");

		if (!FileExists (f))
			continue;

		Information ("Transforming: {0}", compDir);

		var apiLevel = compDir.FullPath.Contains (".Cast") ? "Android 4.2 (API Level 17)" : "Android 4.1 (API Level 16)";

		var t = TransformTextFile (f, "{", "}");

		foreach (var kvp in gettingStartedTemplates) {
			var v = TransformText (kvp.Value, "{", "}").WithToken ("APILEVEL", apiLevel).ToString ();
			t = t.WithToken (kvp.Key, v);
		}

		FileWriteText (compDir.CombineWithFilePath ("./component/GettingStarted.md"), t.ToString ());
	}


	var detailsTemplates = new Dictionary<string, string> ();

	foreach (var f in GetFiles ("./component/Details.*.md")) {

		var key = f.GetFilenameWithoutExtension ().FullPath.Replace ("Details.", "");
		var val = TransformTextFile (f).ToString ();

		detailsTemplates.Add (key, val);
	}

	foreach (var compDir in componentDirs) {

		var f = compDir.CombineWithFilePath ("./component/Details.template.md");

		if (!FileExists (f))
			continue;

		Information ("Transforming: {0}", compDir);

		var t = TransformTextFile (f, "{", "}");

		foreach (var kvp in detailsTemplates)
			t = t.WithToken (kvp.Key, kvp.Value);

		FileWriteText (compDir.CombineWithFilePath ("./component/Details.md"), t.ToString ());
	}
});

Task ("component-setup").Does (() =>
{
	var yamls = GetFiles ("./**/component/component.template.yaml");
	foreach (var yaml in yamls) {
		var manifestTxt = FileReadText (yaml)
			.Replace ("$nuget-version$", NUGET_VERSION)
			.Replace ("$version$", COMPONENT_VERSION)
			.Replace ("$wear-version$", COMPONENT_WEAR_VERSION)
			.Replace ("$wear-nuget-version$", NUGET_WEAR_VERSION);

		var newYaml = yaml.GetDirectory ().CombineWithFilePath ("component.yaml");
		FileWriteText (newYaml, manifestTxt);
    }
});

Task ("component").IsDependentOn ("component-docs").IsDependentOn ("component-setup").IsDependentOn ("component-base");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
