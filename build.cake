// Tools needed by cake addins
#tool nuget:?package=XamarinComponent&version=1.1.0.65
#tool nuget:?package=ILRepack&version=2.0.15
#tool nuget:?package=Cake.MonoApiTools&version=2.0.0
#tool nuget:?package=Microsoft.DotNet.BuildTools.GenAPI&version=1.0.0-beta-00081
#tool nuget:?package=NUnit.Runners&version=2.6.6
#tool nuget:?package=Paket
#tool nuget:?package=vswhere

// Dependencies of Cake Addins - this should be removed once 
// Cake 0.23 is out
// #addin nuget:?package=SharpZipLib&version=0.86.0
// #addin nuget:?package=Newtonsoft.Json&version=9.0.1
// #addin nuget:?package=semver&version=2.0.4
// #addin nuget:?package=YamlDotNet&version=4.2.1
// #addin nuget:?package=NuGet.Core&version=2.14.0

// Cake Addins
#addin nuget:?package=Cake.FileHelpers&version=3.0.0
#addin nuget:?package=Cake.Json&version=3.0.1
#addin nuget:?package=Cake.Yaml&version=2.1.0
#addin nuget:?package=Cake.Xamarin&version=3.0.0
#addin nuget:?package=Cake.XCode&version=4.0.0
#addin nuget:?package=Cake.Xamarin.Build&version=4.0.1
#addin nuget:?package=Cake.Compression&version=0.1.6
#addin nuget:?package=Cake.Android.SdkManager&version=3.0.0
#addin nuget:?package=Cake.Android.Adb&version=3.0.0

// Not yet cake 0.22+ compatible (requires --settings_skipverification=true)
#addin nuget:?package=Cake.MonoApiTools&version=2.0.0
//#addin nuget:?package=Cake.Xamarin.Binding.Util&version=1.0.2

#addin nuget:?package=MavenNet&version=2.0.2

// Lists all the artifacts and their versions for com.google.android.gms.*
// https://dl.google.com/dl/android/maven2/com/google/android/gms/group-index.xml
// Lists all the artifacts and their versions for com.google.firebase.*
// https://dl.google.com/dl/android/maven2/com/google/firebase/group-index.xml

// Master list of all the packages in the repo:
// https://dl.google.com/dl/android/maven2/master-index.xml

// To find new URL: https://dl-ssl.google.com/android/repository/addon.xml and search for 
//		google_play_services_*.zip
//		google_play_services_v
// FROM: https://dl.google.com/android/repository/addon2-1.xml
var DOCS_URL = "https://dl-ssl.google.com/android/repository/google_play_services_v16_1_rc09.zip";

// We grab the previous release's api-info.xml to use as a comparison for this build's generated info to make an api-diff
var BASE_API_INFO_URL = "https://github.com/xamarin/GooglePlayServicesComponents/releases/download/60.1142.0/api-info.xml";

// The common suffix for nuget version
// Sometimes might be "-beta1" for a prerelease, or ".1" if we have a point release for the same actual aar's
// will be blank for a stable release that has no point release fixes
const string COMMON_NUGET_VERSION = "-preview2";
const string NUGET_VERSION_PREFIX = "70.";

var SUPPORT_VERSION = "26.1.0.1";

var ANDROID_API_LEVEL = "26";
var ANDROID_SDK_VERSION = "8.0";

var MAVEN_REPO_BASE_URL = "https://dl.google.com/dl/android/maven2/com/google/";

var TARGET = Argument ("t", Argument ("target", "Default"));
var BUILD_CONFIG = Argument ("config", "Release");

var CPU_COUNT = 1;
var ALWAYS_MSBUILD = true;

LogSystemInfo ();

var AAR_INFOS = new [] {
	new AarInfo ("ads", "play-services-ads", "android/gms/play-services-ads", "Xamarin.GooglePlayServices.Ads", "15.0.1"),
	new AarInfo ("ads-lite", "play-services-ads-lite", "android/gms/play-services-ads-lite", "Xamarin.GooglePlayServices.Ads.Lite", "15.0.1"),
	new AarInfo ("analytics", "play-services-analytics", "android/gms/play-services-analytics", "Xamarin.GooglePlayServices.Analytics", "16.0.1"),
	new AarInfo ("analytics-impl", "play-services-analytics-impl", "android/gms/play-services-analytics-impl", "Xamarin.GooglePlayServices.Analytics.Impl", "16.0.1"),
	new AarInfo ("audience", "play-services-audience", "android/gms/play-services-audience", "Xamarin.GooglePlayServices.Audience", "15.0.1"),
	new AarInfo ("auth", "play-services-auth", "android/gms/play-services-auth", "Xamarin.GooglePlayServices.Auth", "15.0.1"),
	new AarInfo ("auth-base", "play-services-auth-base", "android/gms/play-services-auth-base", "Xamarin.GooglePlayServices.Auth.Base", "15.0.1"),
	new AarInfo ("auth-api-phone", "play-services-auth-api-phone", "android/gms/play-services-auth-api-phone", "Xamarin.GooglePlayServices.Auth.Api.Phone", "15.0.1"),
	new AarInfo ("awareness", "play-services-awareness", "android/gms/play-services-awareness", "Xamarin.GooglePlayServices.Awareness", "15.0.1"),
	new AarInfo ("base", "play-services-base", "android/gms/play-services-base", "Xamarin.GooglePlayServices.Base", "15.0.1"),
	new AarInfo ("basement", "play-services-basement", "android/gms/play-services-basement", "Xamarin.GooglePlayServices.Basement", "15.0.1"),
	new AarInfo ("appinvite", "play-services-appinvite", "android/gms/play-services-appinvite", "Xamarin.GooglePlayServices.AppInvite", "16.0.1"),
	new AarInfo ("cast", "play-services-cast", "android/gms/play-services-cast", "Xamarin.GooglePlayServices.Cast", "15.0.1"),
	new AarInfo ("cast-framework", "play-services-cast-framework", "android/gms/play-services-cast-framework", "Xamarin.GooglePlayServices.Cast.Framework", "15.0.1"),
	new AarInfo ("clearcut", "play-services-clearcut", "android/gms/play-services-clearcut", "Xamarin.GooglePlayServices.Clearcut", "15.0.1"),
	new AarInfo ("drive", "play-services-drive", "android/gms/play-services-drive", "Xamarin.GooglePlayServices.Drive", "15.0.1"),
	new AarInfo ("fido", "play-services-fido", "android/gms/play-services-fido", "Xamarin.GooglePlayServices.Fido", "15.0.1"),
	new AarInfo ("flags", "play-services-flags", "android/gms/play-services-flags", "Xamarin.GooglePlayServices.Flags", "15.0.1"),
	new AarInfo ("fitness", "play-services-fitness", "android/gms/play-services-fitness", "Xamarin.GooglePlayServices.Fitness", "15.0.1"),
	new AarInfo ("games", "play-services-games", "android/gms/play-services-games", "Xamarin.GooglePlayServices.Games", "15.0.1"),
	new AarInfo ("gass", "play-services-gass", "android/gms/play-services-gass", "Xamarin.GooglePlayServices.Gass", "15.0.1"),
	new AarInfo ("gcm", "play-services-gcm", "android/gms/play-services-gcm", "Xamarin.GooglePlayServices.Gcm", "15.0.1"),
	new AarInfo ("identity", "play-services-identity", "android/gms/play-services-identity", "Xamarin.GooglePlayServices.Identity", "15.0.1"),
	new AarInfo ("iid", "play-services-iid", "android/gms/play-services-iid", "Xamarin.GooglePlayServices.Iid", "15.0.1"),
	new AarInfo ("instantapps", "play-services-instantapps", "android/gms/play-services-instantapps", "Xamarin.GooglePlayServices.InstantApps", "15.0.1"),
	new AarInfo ("location", "play-services-location", "android/gms/play-services-location", "Xamarin.GooglePlayServices.Location", "15.0.1"),
	new AarInfo ("maps", "play-services-maps", "android/gms/play-services-maps", "Xamarin.GooglePlayServices.Maps", "15.0.1"),
	new AarInfo ("measurement-base", "play-services-measurement-base", "android/gms/play-services-measurement-base", "Xamarin.GooglePlayServices.Measurement.Base", "16.0.0"),
	new AarInfo ("nearby", "play-services-nearby", "android/gms/play-services-nearby", "Xamarin.GooglePlayServices.Nearby", "15.0.1"),
	new AarInfo ("oss-licenses", "play-services-oss-licenses", "android/gms/play-services-oss-licenses", "Xamarin.GooglePlayServices.Oss.Licenses", "15.0.1"),
	new AarInfo ("panorama", "play-services-panorama", "android/gms/play-services-panorama", "Xamarin.GooglePlayServices.Panorama", "15.0.1"),
	new AarInfo ("places", "play-services-places", "android/gms/play-services-places", "Xamarin.GooglePlayServices.Places", "15.0.1"),
	new AarInfo ("plus", "play-services-plus", "android/gms/play-services-plus", "Xamarin.GooglePlayServices.Plus", "15.0.1"),
	new AarInfo ("safetynet", "play-services-safetynet", "android/gms/play-services-safetynet", "Xamarin.GooglePlayServices.SafetyNet", "15.0.1"),
	new AarInfo ("tasks", "play-services-tasks", "android/gms/play-services-tasks", "Xamarin.GooglePlayServices.Tasks", "15.0.1"),
	new AarInfo ("tagmanager", "play-services-tagmanager", "android/gms/play-services-tagmanager", "Xamarin.GooglePlayServices.TagManager", "16.0.1"),
	new AarInfo ("tagmanager-api", "play-services-tagmanager-api", "android/gms/play-services-tagmanager-api", "Xamarin.GooglePlayServices.TagManager.Api", "16.0.1"),
	new AarInfo ("tagmanager-v4-impl", "play-services-tagmanager-v4-impl", "android/gms/play-services-tagmanager-v4-impl", "Xamarin.GooglePlayServices.TagManager.V4.Impl", "16.0.1"),
	new AarInfo ("vision", "play-services-vision", "android/gms/play-services-vision", "Xamarin.GooglePlayServices.Vision", "15.0.2"),
	new AarInfo ("vision-common", "play-services-vision-common", "android/gms/play-services-vision-common", "Xamarin.GooglePlayServices.Vision.Common", "15.0.2"),
	new AarInfo ("wallet", "play-services-wallet", "android/gms/play-services-wallet", "Xamarin.GooglePlayServices.Wallet", "15.0.1"),
	new AarInfo ("wearable", "play-services-wearable", "android/gms/play-services-wearable", "Xamarin.GooglePlayServices.Wearable", "15.0.1"),

	new AarInfo ("phenotype", "play-services-phenotype", "android/gms/play-services-phenotype", "Xamarin.GooglePlayServices.Phenotype", "15.0.1"),
	new AarInfo ("places-placereport", "play-services-places-placereport", "android/gms/play-services-places-placereport", "Xamarin.GooglePlayServices.Places.PlaceReport", "15.0.1"),
	new AarInfo ("stats", "play-services-stats", "android/gms/play-services-stats", "Xamarin.GooglePlayServices.Stats", "15.0.1"),
	new AarInfo ("vision-image-label", "play-services-vision-image-label", "android/gms/play-services-vision-image-label", "Xamarin.GooglePlayServices.Vision.ImageLabel", "15.0.0"),
	new AarInfo ("ads-base", "play-services-ads-base", "android/gms/play-services-ads-base", "Xamarin.GooglePlayServices.Ads.Base", "15.0.1"),
	new AarInfo ("ads-identifier", "play-services-ads-identifier", "android/gms/play-services-ads-identifier", "Xamarin.GooglePlayServices.Ads.Identifier", "15.0.1"),

	new AarInfo ("firebase-ads", "firebase-ads", "firebase/firebase-ads", "Xamarin.Firebase.Ads", "15.0.1"),
	new AarInfo ("firebase-analytics", "firebase-analytics", "firebase/firebase-analytics", "Xamarin.Firebase.Analytics", "16.0.1"),
	new AarInfo ("firebase-analytics-impl", "firebase-analytics-impl", "firebase/firebase-analytics-impl", "Xamarin.Firebase.Analytics.Impl", "16.1.1"),
	new AarInfo ("firebase-appindexing", "firebase-appindexing", "firebase/firebase-appindexing", "Xamarin.Firebase.AppIndexing", "16.0.1"),
	new AarInfo ("firebase-auth", "firebase-auth", "firebase/firebase-auth", "Xamarin.Firebase.Auth", "16.0.2"),
	new AarInfo ("firebase-common", "firebase-common", "firebase/firebase-common", "Xamarin.Firebase.Common", "16.0.0"),
	new AarInfo ("firebase-config", "firebase-config", "firebase/firebase-config", "Xamarin.Firebase.Config", "16.0.0"),
	new AarInfo ("firebase-core", "firebase-core", "firebase/firebase-core", "Xamarin.Firebase.Core", "16.0.1"),
	new AarInfo ("firebase-crash", "firebase-crash", "firebase/firebase-crash", "Xamarin.Firebase.Crash", "16.0.1"),
	new AarInfo ("firebase-database", "firebase-database", "firebase/firebase-database", "Xamarin.Firebase.Database", "16.0.1"),
	new AarInfo ("firebase-database-connection", "firebase-database-connection", "firebase/firebase-database-connection", "Xamarin.Firebase.Database.Connection", "16.0.1"),
	new AarInfo ("firebase-dynamic-links", "firebase-dynamic-links", "firebase/firebase-dynamic-links", "Xamarin.Firebase.Dynamic.Links", "16.0.1"),
	new AarInfo ("firebase-firestore", "firebase-firestore", "firebase/firebase-firestore", "Xamarin.Firebase.Firestore", "17.0.2"),
	new AarInfo ("firebase-iid", "firebase-iid", "firebase/firebase-iid", "Xamarin.Firebase.Iid", "16.2.0"),
	new AarInfo ("firebase-invites", "firebase-invites", "firebase/firebase-invites", "Xamarin.Firebase.Invites", "16.0.1"),
	new AarInfo ("firebase-messaging", "firebase-messaging", "firebase/firebase-messaging", "Xamarin.Firebase.Messaging", "17.1.0"),
	new AarInfo ("firebase-perf", "firebase-perf", "firebase/firebase-perf", "Xamarin.Firebase.Perf", "16.0.0"),
	new AarInfo ("firebase-storage", "firebase-storage", "firebase/firebase-storage", "Xamarin.Firebase.Storage", "16.0.1"),
	new AarInfo ("firebase-storage-common", "firebase-storage-common", "firebase/firebase-storage-common", "Xamarin.Firebase.Storage.Common", "16.0.1"),

	new AarInfo ("firebase-abt", "firebase-abt", "firebase/firebase-abt", "Xamarin.Firebase.Abt", "16.0.0"),
	new AarInfo ("firebase-ads-lite", "firebase-ads-lite", "firebase/firebase-ads-lite", "Xamarin.Firebase.Ads.Lite", "15.0.1"),
	new AarInfo ("firebase-auth-interop", "firebase-auth-interop", "firebase/firebase-auth-interop", "Xamarin.Firebase.Auth.Interop", "16.0.0"),
	new AarInfo ("firebase-database-collection", "firebase-database-collection", "firebase/firebase-database-collection", "Xamarin.Firebase.Database.Collection", "15.0.1"),
	new AarInfo ("firebase-functions", "firebase-functions", "firebase/firebase-functions", "Xamarin.Firebase.Functions", "16.0.1"),
	new AarInfo ("firebase-iid-interop", "firebase-iid-interop", "firebase/firebase-iid-interop", "Xamarin.Firebase.Iid.Interop", "16.0.0"),
	new AarInfo ("firebase-invites", "firebase-invites", "firebase/firebase-invites", "Xamarin.Firebase.Invites", "16.0.1"),
	new AarInfo ("firebase-measurement-connector", "firebase-measurement-connector", "firebase/firebase-measurement-connector", "Xamarin.Firebase.Measurement.Connector", "17.0.0"),
	new AarInfo ("firebase-measurement-connector-impl", "firebase-measurement-connector-impl", "firebase/firebase-measurement-connector-impl", "Xamarin.Firebase.Measurement.Connector.Impl", "16.0.1"),
	new AarInfo ("firebase-ml-common", "firebase-ml-common", "firebase/firebase-ml-common", "Xamarin.Firebase.ML.Common", "16.0.0"),
	new AarInfo ("firebase-ml-model-interpreter", "firebase-ml-model-interpreter", "firebase/firebase-ml-model-interpreter", "Xamarin.Firebase.ML.Model.Interpreter", "16.0.0"),
	new AarInfo ("firebase-ml-vision", "firebase-ml-vision", "firebase/firebase-ml-vision", "Xamarin.Firebase.ML.Vision", "16.0.0"),
	new AarInfo ("firebase-ml-vision-image-label-model", "firebase-ml-vision-image-label-model", "firebase/firebase-ml-vision-image-label-model", "Xamarin.Firebase.ML.Vision.Image.Label.Model", "15.0.0"),
	new AarInfo ("protolite-well-known-types", "protolite-well-known-types", "firebase/protolite-well-known-types", "Xamarin.Firebase.ProtoliteWellKnownTypes", "15.0.0"),

};

class AarInfo
{
	public AarInfo (string bindingDir, string dir, string path, string nugetId, string aarVersion, string nugetVersionPostfix = null)
	{
		BindingDir = bindingDir;
		Dir = dir;
		Path = path;
		NugetId = nugetId;
		AarVersion = aarVersion;
		NuGetVersion = NUGET_VERSION_PREFIX + aarVersion.Replace(".", "") + (nugetVersionPostfix ?? "") + (COMMON_NUGET_VERSION ?? "");
		Extension = ".aar";
	}

	public string BindingDir { get;set; }
	public string Dir { get; set; }
	public string Path { get; set; }
	public string NugetId { get;set; }
	public string AarVersion { get; set; }
	public string NuGetVersion { get; set; }
	public string Extension { get;set; }
}

var MONODROID_PATH = "/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mandroid/platforms/android-" + ANDROID_API_LEVEL + "/";
if (IsRunningOnWindows ()) {
	var vsInstallPath = VSWhereLatest (new VSWhereLatestSettings {
		Requires = "Component.Xamarin"
	});

	MONODROID_PATH = vsInstallPath.Combine ("Common7/IDE/ReferenceAssemblies/Microsoft/Framework/MonoAndroid/v" + ANDROID_SDK_VERSION).FullPath;
}

var MONO_PATH = "/Library/Frameworks/Mono.framework/Versions/Current";

var MSCORLIB_PATH = "/Library/Frameworks/Xamarin.Android.framework/Libraries/mono/2.1/";
if (IsRunningOnWindows ()) {

	var DOTNETDIR = new DirectoryPath (Environment.GetFolderPath (Environment.SpecialFolder.Windows)).Combine ("Microsoft.NET/");

	if (DirectoryExists (DOTNETDIR.Combine ("Framework64")))
		MSCORLIB_PATH = MakeAbsolute (DOTNETDIR.Combine("Framework64/v4.0.30319/")).FullPath;
	else
		MSCORLIB_PATH = MakeAbsolute (DOTNETDIR.Combine("Framework/v4.0.30319/")).FullPath;
}

Information("MONODROID_PATH: {0}", MONODROID_PATH);
Information("MSCORLIB_PATH: {0}", MSCORLIB_PATH);

var buildsOnWinMac = BuildPlatforms.Windows | BuildPlatforms.Mac;

var nugetInfos = AAR_INFOS.Select (a => new NuGetInfo { NuSpec = "./" + a.BindingDir + "/nuget/" + a.NugetId + ".nuspec", Version = a.NuGetVersion, RequireLicenseAcceptance = true }).ToList ();
nugetInfos.Add(new NuGetInfo { NuSpec = "./firebase-core/nuget/Xamarin.Firebase.Core.nuspec", Version = NUGET_VERSION_PREFIX + "1601" + COMMON_NUGET_VERSION, RequireLicenseAcceptance = true });
nugetInfos.Add(new NuGetInfo { NuSpec = "./firebase-ads/nuget/Xamarin.Firebase.Ads.nuspec", Version = NUGET_VERSION_PREFIX + "1501" + COMMON_NUGET_VERSION, RequireLicenseAcceptance = true });

// There are no actual bindings for these, they are just nuget packages that depend on others
var redirectArtifacts = new List<string> { "firebase-ads", "firebase-core" };

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./GooglePlayServices.sln",
			BuildsOn = BuildPlatforms.Windows | BuildPlatforms.Mac,
			MaxCpuCount = CPU_COUNT,
			//AlwaysUseMSBuild = ALWAYS_MSBUILD,
			OutputFiles = AAR_INFOS
				.Where(a => !redirectArtifacts.Contains(a.Dir))
				.Select (a => new OutputFileCopy { FromFile = "./" + a.BindingDir + "/source/bin/" + BUILD_CONFIG + "/" + a.NugetId + ".dll" })
				.ToArray (),
		},
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./ads/samples/AdMobSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		//new DefaultSolutionBuilder { SolutionPath = "./analytics/samples/AnalyticsSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./appinvite/samples/AppInviteSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./cast/samples/CastingCall.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./drive/samples/DriveSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./fitness/samples/BasicSensorsApi.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./games/samples/BeGenerous.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./gcm/samples/GCMSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./location/samples/LocationSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./maps/samples/MapsSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./nearby/samples/NearbySample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./panorama/samples/PanoramaSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./places/samples/PlacesAsync.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./plus/samples/PlusSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./safetynet/samples/SafetyNetSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./vision/samples/VisionSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./wallet/samples/AndroidPayQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		
		new DefaultSolutionBuilder { SolutionPath = "./firebase-ads/samples/FirebaseAdmobQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-analytics/samples/FirebaseAnalyticsQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-appindexing/samples/AppIndexingSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		//new DefaultSolutionBuilder { SolutionPath = "./firebase-auth/samples/FirebaseAuthQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-config/samples/FirebaseConfigQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-crash/samples/FirebaseCrashReportingQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-invites/samples/FirebaseInvitesQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-messaging/samples/FirebaseMessagingQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		//new DefaultSolutionBuilder { SolutionPath = "./firebase-storage/samples/FirebaseStorageQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
	},

	NuGets = nugetInfos.ToArray()
};

var NUGET_SOURCES = EnvironmentVariable ("NUGET_SOURCES") ?? Argument ("nugetsources", string.Empty);;
if (!string.IsNullOrEmpty (NUGET_SOURCES)) {
	buildSpec.NuGetSources = NUGET_SOURCES.Split (';',',').Select (ns => new NuGetSource { Url = ns }).ToArray ();
	Information ("Using Nuget Sources:");
	foreach (var nsrc in buildSpec.NuGetSources)
		Information ("  {0}", nsrc.Url);
}

Task ("sysinfo");

Task ("externals")
	.WithCriteria (() => !FileExists ("./externals/play-services-base.aar")).Does (() =>
{
	var path = "./externals/";

	EnsureDirectoryExists (path);
	
	// Copy the .aar's to a better location
	foreach (var aar in AAR_INFOS) {
		var aarUrl = MAVEN_REPO_BASE_URL + aar.Path + "/" + aar.AarVersion + "/" + aar.Dir + "-" + aar.AarVersion + ".aar";
		var aarFile = path + aar.Dir + ".aar";

		// Download the .aar
		if (!FileExists (aarFile))
			DownloadFile (aarUrl, aarFile);

		// Download the .md5 for the .aar
		if (!FileExists (aarFile + ".md5"))
			DownloadFile (aarUrl + ".md5", aarFile + ".md5");
		
		// Unzip the .aar
		if (!DirectoryExists (path + aar.Dir))
			Unzip (aarFile, path + aar.Dir);
	}

	RunTarget("docs");

	// Put the uitest.keystore in our artifacts for downstream jobs to use
	EnsureDirectoryExists ("./output");
	if (FileExists ("./uitest.keystore"))
		CopyFile ("./uitest.keystore", "./output/uitest.keystore");
});

Task ("docs")
	.Does (() =>
{
	var path = "./externals/";

	// Get the GPS Docs
	if (!FileExists (path + "docs.zip"))
		DownloadFile (DOCS_URL, path + "docs.zip");
	// Move the docs around
	if (!DirectoryExists (path + "docs")) {
		EnsureDirectoryExists (path + "google-play-services/docs");
		Unzip (path + "docs.zip", path);
		CopyDirectory (path + "google-play-services/docs", path + "docs/");
		DeleteDirectory (path + "google-play-services", new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
	}
});

Task ("diff")
	.IsDependentOn ("merge")
	.WithCriteria (!IsRunningOnWindows ())
	.Does (() =>
{
	var SEARCH_DIRS = new FilePath [] {
		MONODROID_PATH,
		"/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/xbuild-frameworks/MonoAndroid/v1.0/",
		"./cast/source/bin/Release/",
		"./wearable/source/bin/Release/"
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

	// // Find obfuscated types/members and log them to a file
	// var obfuscations = FindObfuscations ("./output/GooglePlayServices.Merged.dll", false, false, false, null);

	// var obfLog = "Obfuscated Types:" + System.Environment.NewLine;
	// foreach (var t in obfuscations.Types)
	// 	obfLog += t.NetType.FullName + " -> " + t.JavaType + System.Environment.NewLine;
	// obfLog += System.Environment.NewLine + "Obfuscated Members:" + System.Environment.NewLine;
	// foreach (var t in obfuscations.Members)
	// 	obfLog += t.NetMember.FullName + " -> " + t.JavaMember + System.Environment.NewLine;
	// FileWriteText ("./output/obfuscations.txt", obfLog);

	// var missingMetadata = FindMissingMetadata ("./output/GooglePlayServices.Merged.dll");
	// var metaLog = string.Empty;
	// foreach (var t in missingMetadata)
	// 	metaLog += t.NetMember.FullName + System.Environment.NewLine;
	// FileWriteText ("./output/missing-metadata.txt", metaLog);
});

Task ("merge")
	.IsDependentOn ("libs") 
	.Does (() =>
{
	if (FileExists ("./output/GooglePlayServices.Merged.dll"))
		DeleteFile ("./output/GooglePlayServices.Merged.dll");

	// Clean up any existing old merge files
	DeleteFiles ("./output/GooglePlayServices.Merged.dll");

	var mergeDlls = GetFiles ("./output/*.dll");

	ILRepack ("./output/GooglePlayServices.Merged.dll", mergeDlls.First(), mergeDlls.Skip(1), new ILRepackSettings {
		CopyAttrs = true,
		AllowMultiple = true,
		//TargetKind = ILRepack.TargetKind.Dll,
		Libs = new List<DirectoryPath> {
			MONODROID_PATH
		},
	});
});

Task ("clean")
	.IsDependentOn ("clean-base")
	.Does (() =>
{
	if (FileExists ("./generated.targets"))
		DeleteFile ("./generated.targets");

	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});

	CleanDirectories ("./**/packages");
});

Task ("nuget-setup")
	.IsDependentOn ("buildtasks")
	.Does (() =>
{
	var templateText = FileReadText ("./template.targets");

	if (FileExists ("./generated.targets"))
		DeleteFile ("./generated.targets");

	var mavenRepo = MavenNet.MavenRepository.FromGoogle();
	Information ("Loading Maven Repo...");
	mavenRepo.Refresh("com.google.android.gms", "com.google.firebase").Wait();
	Information ("Loaded Maven Repo.");

	foreach (var aar in AAR_INFOS) {

		var aarMd5File = "./externals/" + aar.Dir + ".aar.md5";

		// Write out the nuspec from template
		var nuspec = new FilePath ("./" + aar.BindingDir + "/nuget/" + aar.NugetId + ".template.nuspec");
		var nuspecTxt = FileReadText (nuspec)
							.Replace ("$aar-version$", aar.AarVersion)
							.Replace ("$support-version$", SUPPORT_VERSION);
		var newNuspec = nuspec.FullPath.Replace (".template.nuspec", ".nuspec");
		
		var mavenGroupId = "com.google." + aar.Path.Substring(0, aar.Path.LastIndexOf("/")).Replace("/", ".").Trim('.');
		var mavenArtifactId = aar.Path.Substring(aar.Path.LastIndexOf("/") + 1).Trim('/');

		Information("Maven GroupId: {0}, ArtifactId: {1}, Version: {2}", mavenGroupId, mavenArtifactId, aar.AarVersion);

		var mavenProject = mavenRepo.GetProjectAsync(mavenGroupId, mavenArtifactId, aar.AarVersion).Result;

		var depXml = string.Empty;

		foreach (var mavenDep in mavenProject.Dependencies) {

			if (!mavenDep.GroupId.StartsWith("com.google.android.gms") && !mavenDep.GroupId.StartsWith("com.google.firebase"))
				continue;

			var mdepGid = mavenDep.GroupId.Replace(".", "/");

			if (mdepGid.StartsWith("com/google/"))
				mdepGid = mdepGid.Replace("com/google/", "");

			var mdepAid = mavenDep.ArtifactId;
			var mpath = mdepGid.Trim('/') + "/" + mdepAid;
			Information("  Depends on ArtifactId: {0}, Version: {1} ({2})", mdepAid, mavenDep.Version, mpath);
			var depAarInfo = AAR_INFOS.FirstOrDefault(ai => ai.Path == mpath);

			depXml += "        <dependency id=\"" + depAarInfo.NugetId + "\" version=\"" + depAarInfo.NuGetVersion + "\" />\r\n";
		}

		nuspecTxt = nuspecTxt.Replace("<!-- dependencies -->", depXml.TrimEnd());
		
		FileWriteText (newNuspec, nuspecTxt);

		var msName = aar.Dir.Replace("-", "");
		var xbdKey = "playservices-" + aar.AarVersion + "/" + msName;
		
		if (aar.Dir.StartsWith ("firebase-"))
			xbdKey = "firebase-" + aar.AarVersion + "/" + msName;
		
		var items = new Dictionary<string, string> {
			{ "_XbdUrl_", "_XbdUrl_" + msName },
			{ "_XbdKey_", "_XbdKey_" + msName },
			{ "_XbdAarFile_", "_XbdAarFile_" + msName },
			{ "_XbdAarFileInSdk_", "_XbdAarFileInSdk_" + msName },
			{ "_XbdAssemblyName_", "_XbdAssemblyName_" + msName },
			{ "_XbdAarFileFullPath_", "_XbdAarFileFullPath_" + msName },
			{ "_XbdRestoreItems_", "_XbdRestoreItems_" + msName },
			{ "$XbdUrl$", MAVEN_REPO_BASE_URL + aar.Path + "/" + aar.AarVersion + "/" + aar.Dir + "-" + aar.AarVersion + ".aar" },
			{ "$XbdMd5$", FileReadText (aarMd5File) },
			{ "$XbdKey$", xbdKey },
			{ "$XbdAssemblyName$", aar.NugetId },
			// { "$XbdRangeStart$", part.RangeStart.ToString() },
			// { "$XbdRangeEnd$", part.RangeEnd.ToString() },
			{ "$AarKey$", aar.Dir },
			{ "$AarVersion$", aar.AarVersion },
			{ "$AarInnerPath$", aar.Path.Replace ("/", "\\") },
		};

		var targetsText = templateText;

		foreach (var kvp in items)
			targetsText = targetsText.Replace (kvp.Key, kvp.Value);

		var targetsFile = new FilePath (string.Format ("{0}/nuget/{1}.targets", aar.BindingDir, aar.NugetId));
		FileWriteText (targetsFile, targetsText);

		// Merge each generated targets file into one main one
		// this makes one file to import into our actual binding projects
		// which is much easier/less maintenance
		if (!FileExists ("./generated.targets"))
			FileWriteText ("./generated.targets", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n</Project>");

		// Load the doc to append to, and the doc to append
		var xFileRoot = System.Xml.Linq.XDocument.Load ("./generated.targets");
		System.Xml.Linq.XNamespace nsRoot = xFileRoot.Root.Name.Namespace;
		var xFileChild = System.Xml.Linq.XDocument.Load (MakeAbsolute (targetsFile).FullPath);
		System.Xml.Linq.XNamespace nsChild = xFileRoot.Root.Name.Namespace;

		// Add all the elements under <Project> into the existing file's <Project> node
		foreach (var xItemToAdd in xFileChild.Element (nsChild + "Project").Elements ())
			xFileRoot.Element (nsRoot + "Project").Add (xItemToAdd);

		// Inject a property to prevent errors from missing assemblies in .targets
		// this allows us to use one big .targets file in all the projects and not have to figure out which specific
		// ones each project needs to reference for development purposes
		if (!xFileRoot.Descendants (nsRoot + "XamarinBuildResourceMergeThrowOnMissingAssembly").Any ()) {
			xFileRoot.Element (nsRoot + "Project")
				.AddFirst (new System.Xml.Linq.XElement (nsRoot + "PropertyGroup",
					new System.Xml.Linq.XElement (nsRoot + "XamarinBuildResourceMergeThrowOnMissingAssembly", false)));
		}

		xFileRoot.Save ("./generated.targets");

		// Check for an existing .targets file in this nuget package
		// we need to merge the generated one with it if it exists
		// nuget only allows one automatic .targets file in the build/ folder
		// of the nuget package, which must be named {nuget-package-id}.targets
		// so we need to merge them all into one
		var mergeFile = new FilePath (aar.BindingDir + "/nuget/merge.targets");

		if (FileExists (mergeFile)) {
			Information ("merge.targets found, merging into generated file...");

			// Load the doc to append to, and the doc to append
			var xOrig = System.Xml.Linq.XDocument.Load (MakeAbsolute(targetsFile).FullPath);
			System.Xml.Linq.XNamespace nsOrig = xOrig.Root.Name.Namespace;
			var xMerge = System.Xml.Linq.XDocument.Load (MakeAbsolute(mergeFile).FullPath);
			System.Xml.Linq.XNamespace nsMerge = xMerge.Root.Name.Namespace;
			// Add all the elements under <Project> into the existing file's <Project> node
			foreach (var xItemToAdd in xMerge.Element (nsMerge + "Project").Elements ())
				xOrig.Element (nsOrig + "Project").Add (xItemToAdd);

			xOrig.Save (MakeAbsolute (targetsFile).FullPath);
		}
	}
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

	foreach (var art in AAR_INFOS) {
		var glob = "./" + art.BindingDir + "/**/source/**/AssemblyInfo.cs";

		ReplaceTextInFiles(glob, "{NUGET_VERSION}", art.NuGetVersion);
		ReplaceTextInFiles(glob, "{BUILD_COMMIT}", buildCommit);
		ReplaceTextInFiles(glob, "{BUILD_NUMBER}", buildNumber);
		ReplaceTextInFiles(glob, "{BUILD_TIMESTAMP}", buildTimestamp);
	}
});

Task ("dependency-list")
	.Does(() =>
{
	var mavenRepo = MavenNet.MavenRepository.FromGoogle();
	
	Information ("Loading Maven Repo...");
	mavenRepo.Refresh("com.google.android.gms", "com.google.firebase").Wait();
	Information ("Loaded Maven Repo.");

	var depText = string.Empty;

	foreach (var aar in AAR_INFOS) {
		var mavenGroupId = aar.Path.Substring(0, aar.Path.LastIndexOf("/")).Replace("/", ".").Trim('.');
		var mavenArtifactId = aar.Path.Substring(aar.Path.LastIndexOf("/") + 1).Trim('/');

		MavenNet.Models.Project mavenProject = null;

		try {
			mavenProject = mavenRepo.GetProjectAsync(mavenGroupId, mavenArtifactId, aar.AarVersion).Result;
		} catch {
			mavenGroupId = "com.google." + mavenGroupId;
			mavenProject = mavenRepo.GetProjectAsync(mavenGroupId, mavenArtifactId, aar.AarVersion).Result;
		}

		depText += mavenGroupId + " -> " + mavenArtifactId + Environment.NewLine;

		foreach (var mavenDep in mavenProject.Dependencies)
			depText += "    " + mavenDep.GroupId + " -> " + mavenDep.ArtifactId + ": " + mavenDep.Version + Environment.NewLine;

		depText += Environment.NewLine;
	}

	FileWriteText("./output/dependency-list.txt", depText);
});

Task ("genapi")
	.IsDependentOn ("libs-base")
	.Does (() =>
{

	var GenApiToolPath = GetFiles ("./tools/**/GenAPI.exe").FirstOrDefault ();

	// For some reason GenAPI.exe can't handle absolute paths on mac/unix properly, so always make them relative
	// GenAPI.exe -libPath:$(MONOANDROID) -out:Some.generated.cs -w:TypeForwards ./relative/path/to/Assembly.dll
	var libDirPrefix = IsRunningOnWindows () ? "output/" : "";

	var libs = new FilePath [] {
		"./" + libDirPrefix + "Xamarin.Firebase.AppIndexing.dll",
	};

	foreach (var lib in libs) {
		var genName = lib.GetFilename () + ".generated.cs";

		var libPath = IsRunningOnWindows () ? MakeAbsolute (lib).FullPath : lib.FullPath;
		var monoDroidPath = IsRunningOnWindows () ? "\"" + MONODROID_PATH + "\"" : MONODROID_PATH;

		Information ("GenAPI: {0}", lib.FullPath);

		StartProcess (GenApiToolPath, new ProcessSettings {
			Arguments = string.Format("-libPath:{0} -out:{1}{2} -w:TypeForwards {3}",
							monoDroidPath + "," + MSCORLIB_PATH,
							IsRunningOnWindows () ? "" : "./",
							genName,
							libPath),
			WorkingDirectory = "./output/",
		});
	}

	MSBuild ("./GooglePlayServices.TypeForwarders.sln", c => c.Configuration = BUILD_CONFIG);

	CopyFile ("./appindexing/source/bin/" + BUILD_CONFIG + "/Xamarin.GooglePlayServices.AppIndexing.dll", "./output/Xamarin.GooglePlayServices.AppIndexing.dll");
});

Task ("buildtasks")
	.Does (() =>
{
	NuGetRestore ("./basement/buildtasks/Basement-BuildTasks.csproj");

	MSBuild ("./basement/buildtasks/Basement-BuildTasks.csproj", c => c.Configuration = "Release");

	CopyFile ("./basement/buildtasks/bin/Release/Xamarin.GooglePlayServices.Basement.targets", "./basement/nuget/merge.targets");
});

Task ("package-samples")
	.IsDependentOn ("nuget")
	.IsDependentOn ("samples")
	.Does (() => 
{
	EnsureDirectoryExists ("./output/samples/");

	foreach (var sampleSln in buildSpec.Samples) {

		var slnPath = new FilePath ((sampleSln as DefaultSolutionBuilder).SolutionPath);
		Information ("Packing sample: {0}", slnPath);
		
		var tempPath = new DirectoryPath ("./output/samples/tmp/");
		if (DirectoryExists (tempPath))
			DeleteDirectory (tempPath, new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
		EnsureDirectoryExists (tempPath);

		var sampleDir = slnPath.GetDirectory ();
		
		CleanDirectories (sampleDir.FullPath.TrimEnd ('/') + "/**/bin");
		CleanDirectories (sampleDir.FullPath.TrimEnd ('/') + "/**/obj");

		CopyDirectory (sampleDir, tempPath);

		var csprojGlobPattern = tempPath.FullPath.TrimEnd ('/') + "/**/*.csproj";
		var csprojs = GetFiles (csprojGlobPattern);

		
		foreach (var csproj in csprojs) {

			Information ("Fixing CSPROJ: {0}", csproj);

			var xcsproj = System.Xml.Linq.XDocument.Load (MakeAbsolute (csproj).FullPath);
			System.Xml.Linq.XNamespace nsRoot = xcsproj.Root.Name.Namespace;

			var prItemGroup = System.Xml.XPath.Extensions.XPathSelectElements (xcsproj, "//ItemGroup/PackageReference/..").FirstOrDefault();
			var prElems = System.Xml.XPath.Extensions.XPathSelectElements (xcsproj, "//ProjectReference[@NugetId!='']");

			foreach (var prElem in prElems) {
				Information ("Found element");
				var nugetId = prElem.Attribute (nsRoot + "NugetId")?.Value;
				var aarInfo = AAR_INFOS.First (a => a.NugetId == nugetId);
				var nugetVersion = aarInfo.NuGetVersion;

				Information ("Adding PackageReference: {0}, {1}", nugetId, nugetVersion);

				prItemGroup.Add (new System.Xml.Linq.XElement (nsRoot + "PackageReference",
					new System.Xml.Linq.XAttribute (nsRoot + "Include", nugetId),
					new System.Xml.Linq.XAttribute (nsRoot + "Version", nugetVersion)));

				prElem.Remove ();
			}

			xcsproj.Save (MakeAbsolute (csproj).FullPath);
		}

		ZipCompress(tempPath, "./output/samples/" + slnPath.GetFilenameWithoutExtension() + ".zip");

		DeleteDirectory (tempPath, new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
	}
});

Task ("nuget")
	.IsDependentOn ("libs")
	.IsDependentOn ("nuget-setup")
	.IsDependentOn ("nuget-base");

Task ("libs")
	.IsDependentOn ("externals")
	.IsDependentOn ("nuget-setup")
	.IsDependentOn ("libs-base")
	.IsDependentOn ("genapi");

Task ("ci")
	.IsDependentOn ("ci-setup")
	.IsDependentOn ("diff")
	.IsDependentOn ("dependency-list")
	.IsDependentOn ("package-samples");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
