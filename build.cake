#tool nuget:?package=ILRepack&version=2.0.10
#tool nuget:?package=XamarinComponent
#tool nuget:?package=Cake.MonoApiTools

#addin nuget:?package=Cake.Json
#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.Xamarin.Build&version=1.1.13
#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=Cake.MonoApiTools

// To find new URL: https://dl-ssl.google.com/android/repository/addon.xml and search for google_play_services_*.zip\
// FROM: https://dl.google.com/android/repository/addon2-1.xml
var DOCS_URL = "https://dl-ssl.google.com/android/repository/google_play_services_v9_rc41.zip";
var M2_REPOSITORY = "https://dl-ssl.google.com/android/repository/google_m2repository_gms_v9_rc41_wear_2_0_rc6.zip";

// We grab the previous release's api-info.xml to use as a comparison for this build's generated info to make an api-diff
var BASE_API_INFO_URL = "https://github.com/xamarin/GooglePlayServicesComponents/releases/download/42.1001.0/api-info.xml";

var PLAY_COMPONENT_VERSION = "42.1020.0.0";
var PLAY_NUGET_VERSION = "42.1020.0";
var PLAY_AAR_VERSION = "10.2.0";
var VERSION_DESC = "10.2.0";

var WEAR_COMPONENT_VERSION = "2.0.0.0";
var WEAR_NUGET_VERSION = "2.0.0";
var WEAR_AAR_VERSION = "2.0.0";
var WEARABLE_SUPPORT_VERSION = "2.0.0";

var FIREBASE_COMPONENT_VERSION = PLAY_COMPONENT_VERSION;
var FIREBASE_NUGET_VERSION = PLAY_NUGET_VERSION;
var FIREBASE_AAR_VERSION = PLAY_AAR_VERSION;

var TARGET = Argument ("t", Argument ("target", "Default"));

var CPU_COUNT = System.Environment.ProcessorCount;
var ALWAYS_MSBUILD = false;

LogSystemInfo ();

var AAR_INFOS = new [] {
	new AarInfo ("ads", "play-services-ads", "android/gms/play-services-ads", "Xamarin.GooglePlayServices.Ads", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("ads-lite", "play-services-ads-lite", "android/gms/play-services-ads-lite", "Xamarin.GooglePlayServices.Ads.Lite", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("analytics", "play-services-analytics", "android/gms/play-services-analytics", "Xamarin.GooglePlayServices.Analytics", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("analytics-impl", "play-services-analytics-impl", "android/gms/play-services-analytics-impl", "Xamarin.GooglePlayServices.Analytics.Impl", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("auth", "play-services-auth", "android/gms/play-services-auth", "Xamarin.GooglePlayServices.Auth", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("auth-base", "play-services-auth-base", "android/gms/play-services-auth-base", "Xamarin.GooglePlayServices.Auth.Base", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("awareness", "play-services-awareness", "android/gms/play-services-awareness", "Xamarin.GooglePlayServices.Awareness", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("base", "play-services-base", "android/gms/play-services-base", "Xamarin.GooglePlayServices.Base", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("basement", "play-services-basement", "android/gms/play-services-basement", "Xamarin.GooglePlayServices.Basement", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("appinvite", "play-services-appinvite", "android/gms/play-services-appinvite", "Xamarin.GooglePlayServices.AppInvite", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("cast", "play-services-cast", "android/gms/play-services-cast", "Xamarin.GooglePlayServices.Cast", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("cast-framework", "play-services-cast-framework", "android/gms/play-services-cast-framework", "Xamarin.GooglePlayServices.Cast.Framework", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("clearcut", "play-services-clearcut", "android/gms/play-services-clearcut", "Xamarin.GooglePlayServices.Clearcut", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("drive", "play-services-drive", "android/gms/play-services-drive", "Xamarin.GooglePlayServices.Drive", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("fitness", "play-services-fitness", "android/gms/play-services-fitness", "Xamarin.GooglePlayServices.Fitness", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("games", "play-services-games", "android/gms/play-services-games", "Xamarin.GooglePlayServices.Games", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("gass", "play-services-gass", "android/gms/play-services-gass", "Xamarin.GooglePlayServices.Gass", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("gcm", "play-services-gcm", "android/gms/play-services-gcm", "Xamarin.GooglePlayServices.Gcm", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("identity", "play-services-identity", "android/gms/play-services-identity", "Xamarin.GooglePlayServices.Identity", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("iid", "play-services-iid", "android/gms/play-services-iid", "Xamarin.GooglePlayServices.Iid", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("instantapps", "play-services-instantapps", "android/gms/play-services-instantapps", "Xamarin.GooglePlayServices.InstantApps", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("location", "play-services-location", "android/gms/play-services-location", "Xamarin.GooglePlayServices.Location", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("maps", "play-services-maps", "android/gms/play-services-maps", "Xamarin.GooglePlayServices.Maps", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("nearby", "play-services-nearby", "android/gms/play-services-nearby", "Xamarin.GooglePlayServices.Nearby", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("panorama", "play-services-panorama", "android/gms/play-services-panorama", "Xamarin.GooglePlayServices.Panorama", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("places", "play-services-places", "android/gms/play-services-places", "Xamarin.GooglePlayServices.Places", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("plus", "play-services-plus", "android/gms/play-services-plus", "Xamarin.GooglePlayServices.Plus", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("safetynet", "play-services-safetynet", "android/gms/play-services-safetynet", "Xamarin.GooglePlayServices.SafetyNet", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("tasks", "play-services-tasks", "android/gms/play-services-tasks", "Xamarin.GooglePlayServices.Tasks", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("tagmanager", "play-services-tagmanager", "android/gms/play-services-tagmanager", "Xamarin.GooglePlayServices.TagManager", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("tagmanager-api", "play-services-tagmanager-api", "android/gms/play-services-tagmanager-api", "Xamarin.GooglePlayServices.TagManager.Api", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("tagmanager-v4-impl", "play-services-tagmanager-v4-impl", "android/gms/play-services-tagmanager-v4-impl", "Xamarin.GooglePlayServices.TagManager.V4.Impl", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("vision", "play-services-vision", "android/gms/play-services-vision", "Xamarin.GooglePlayServices.Vision", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("wallet", "play-services-wallet", "android/gms/play-services-wallet", "Xamarin.GooglePlayServices.Wallet", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("wearable", "play-services-wearable", "android/gms/play-services-wearable", "Xamarin.GooglePlayServices.Wearable", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),

	new AarInfo ("support-wearable", "wearable", "android/support/wearable", "Xamarin.Android.Wear", WEAR_AAR_VERSION, WEAR_NUGET_VERSION, WEAR_COMPONENT_VERSION),

	new AarInfo ("firebase-ads", "firebase-ads", "firebase/firebase-ads", "Xamarin.Firebase.Ads", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-analytics", "firebase-analytics", "firebase/firebase-analytics", "Xamarin.Firebase.Analytics", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-analytics-impl", "firebase-analytics-impl", "firebase/firebase-analytics-impl", "Xamarin.Firebase.Analytics.Impl", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-appindexing", "firebase-appindexing", "firebase/firebase-appindexing", "Xamarin.Firebase.AppIndexing", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-auth", "firebase-auth", "firebase/firebase-auth", "Xamarin.Firebase.Auth", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-common", "firebase-common", "firebase/firebase-common", "Xamarin.Firebase.Common", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-config", "firebase-config", "firebase/firebase-config", "Xamarin.Firebase.Config", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-core", "firebase-core", "firebase/firebase-core", "Xamarin.Firebase.Core", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-crash", "firebase-crash", "firebase/firebase-crash", "Xamarin.Firebase.Crash", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-database", "firebase-database", "firebase/firebase-database", "Xamarin.Firebase.Database", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-database-connection", "firebase-database-connection", "firebase/firebase-database-connection", "Xamarin.Firebase.Database.Connection", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-iid", "firebase-iid", "firebase/firebase-iid", "Xamarin.Firebase.Iid", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-invites", "firebase-invites", "firebase/firebase-invites", "Xamarin.Firebase.Invites", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-messaging", "firebase-messaging", "firebase/firebase-messaging", "Xamarin.Firebase.Messaging", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-storage", "firebase-storage", "firebase/firebase-storage", "Xamarin.Firebase.Storage", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-storage-common", "firebase-storage-common", "firebase/firebase-storage-common", "Xamarin.Firebase.Storage.Common", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
};

class PartialZipInfo {
	public string Url { get;set; }
	public string LocalPath { get;set; }
	public string Md5 { get;set; }
	public long RangeStart { get;set; }
	public long RangeEnd { get;set; }
}

class AarInfo
{
	public AarInfo (string bindingDir, string dir, string path, string nugetId, string aarVersion, string nugetVersion, string componentVersion)
	{
		BindingDir = bindingDir;
		Dir = dir;
		Path = path;
		NugetId = nugetId;
		AarVersion = aarVersion;
		NuGetVersion = nugetVersion;
		ComponentVersion = componentVersion;
		Extension = ".aar";
	}

	public string BindingDir { get;set; }
	public string Dir { get; set; }
	public string Path { get; set; }
	public string NugetId { get;set; }
	public string AarVersion { get; set; }
	public string NuGetVersion { get; set; }
	public string ComponentVersion { get; set; }
	public string Extension { get;set; }
}

var MONODROID_PATH = "/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mandroid/platforms/android-23/";
if (IsRunningOnWindows ())
	MONODROID_PATH = new DirectoryPath (Environment.GetFolderPath (Environment.SpecialFolder.ProgramFilesX86)).Combine ("Reference Assemblies/Microsoft/Framework/MonoAndroid/v6.0/").FullPath;

var MONO_PATH = "/Library/Frameworks/Mono.framework/Versions/Current";

var buildsOnWinMac = BuildPlatforms.Windows | BuildPlatforms.Mac;

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./GooglePlayServices.sln",
			BuildsOn = BuildPlatforms.Windows | BuildPlatforms.Mac,
			MaxCpuCount = CPU_COUNT,
			AlwaysUseMSBuild = ALWAYS_MSBUILD,
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./base/source/bin/Release/Xamarin.GooglePlayServices.Base.dll" },
				new OutputFileCopy { FromFile = "./basement/source/bin/Release/Xamarin.GooglePlayServices.Basement.dll" },
				new OutputFileCopy { FromFile = "./ads/source/bin/Release/Xamarin.GooglePlayServices.Ads.dll" },
				new OutputFileCopy { FromFile = "./ads-lite/source/bin/Release/Xamarin.GooglePlayServices.Ads.Lite.dll" },
				new OutputFileCopy { FromFile = "./analytics/source/bin/Release/Xamarin.GooglePlayServices.Analytics.dll" },
				new OutputFileCopy { FromFile = "./analytics-impl/source/bin/Release/Xamarin.GooglePlayServices.Analytics.Impl.dll" },
				new OutputFileCopy { FromFile = "./appinvite/source/bin/Release/Xamarin.GooglePlayServices.AppInvite.dll" },
				new OutputFileCopy { FromFile = "./auth/source/bin/Release/Xamarin.GooglePlayServices.Auth.dll" },
				new OutputFileCopy { FromFile = "./auth-base/source/bin/Release/Xamarin.GooglePlayServices.Auth.Base.dll" },
				new OutputFileCopy { FromFile = "./awareness/source/bin/Release/Xamarin.GooglePlayServices.Awareness.dll" },
				new OutputFileCopy { FromFile = "./cast/source/bin/Release/Xamarin.GooglePlayServices.Cast.dll" },
				new OutputFileCopy { FromFile = "./cast-framework/source/bin/Release/Xamarin.GooglePlayServices.Cast.Framework.dll" },
				new OutputFileCopy { FromFile = "./clearcut/source/bin/Release/Xamarin.GooglePlayServices.Clearcut.dll" },
				new OutputFileCopy { FromFile = "./drive/source/bin/Release/Xamarin.GooglePlayServices.Drive.dll" },
				new OutputFileCopy { FromFile = "./fitness/source/bin/Release/Xamarin.GooglePlayServices.Fitness.dll" },
				new OutputFileCopy { FromFile = "./games/source/bin/Release/Xamarin.GooglePlayServices.Games.dll" },
				new OutputFileCopy { FromFile = "./gass/source/bin/Release/Xamarin.GooglePlayServices.Gass.dll" },
				new OutputFileCopy { FromFile = "./gcm/source/bin/Release/Xamarin.GooglePlayServices.Gcm.dll" },
				new OutputFileCopy { FromFile = "./identity/source/bin/Release/Xamarin.GooglePlayServices.Identity.dll" },
				new OutputFileCopy { FromFile = "./iid/source/bin/Release/Xamarin.GooglePlayServices.Iid.dll" },
				new OutputFileCopy { FromFile = "./instantapps/source/bin/Release/Xamarin.GooglePlayServices.InstantApps.dll" },
				new OutputFileCopy { FromFile = "./location/source/bin/Release/Xamarin.GooglePlayServices.Location.dll" },
				new OutputFileCopy { FromFile = "./maps/source/bin/Release/Xamarin.GooglePlayServices.Maps.dll" },
				new OutputFileCopy { FromFile = "./nearby/source/bin/Release/Xamarin.GooglePlayServices.Nearby.dll" },
				new OutputFileCopy { FromFile = "./panorama/source/bin/Release/Xamarin.GooglePlayServices.Panorama.dll" },
				new OutputFileCopy { FromFile = "./places/source/bin/Release/Xamarin.GooglePlayServices.Places.dll" },
				new OutputFileCopy { FromFile = "./plus/source/bin/Release/Xamarin.GooglePlayServices.Plus.dll" },
				new OutputFileCopy { FromFile = "./safetynet/source/bin/Release/Xamarin.GooglePlayServices.SafetyNet.dll" },
				new OutputFileCopy { FromFile = "./vision/source/bin/Release/Xamarin.GooglePlayServices.Vision.dll" },
				new OutputFileCopy { FromFile = "./wallet/source/bin/Release/Xamarin.GooglePlayServices.Wallet.dll" },
				new OutputFileCopy { FromFile = "./wearable/source/bin/Release/Xamarin.GooglePlayServices.Wearable.dll" },
				new OutputFileCopy { FromFile = "./support-wearable/source/bin/Release/Xamarin.Android.Wear.dll" },
				new OutputFileCopy { FromFile = "./tagmanager/source/bin/Release/Xamarin.GooglePlayServices.TagManager.dll" },
				new OutputFileCopy { FromFile = "./tagmanager-api/source/bin/Release/Xamarin.GooglePlayServices.TagManager.Api.dll" },
				new OutputFileCopy { FromFile = "./tagmanager-v4-impl/source/bin/Release/Xamarin.GooglePlayServices.TagManager.V4.Impl.dll" },
				new OutputFileCopy { FromFile = "./tasks/source/bin/Release/Xamarin.GooglePlayServices.Tasks.dll" },

				new OutputFileCopy { FromFile = "./firebase-analytics/source/bin/Release/Xamarin.Firebase.Analytics.dll" },
				new OutputFileCopy { FromFile = "./firebase-analytics-impl/source/bin/Release/Xamarin.Firebase.Analytics.Impl.dll" },
				new OutputFileCopy { FromFile = "./firebase-appindexing/source/bin/Release/Xamarin.Firebase.AppIndexing.dll" },
				new OutputFileCopy { FromFile = "./firebase-auth/source/bin/Release/Xamarin.Firebase.Auth.dll" },
				new OutputFileCopy { FromFile = "./firebase-common/source/bin/Release/Xamarin.Firebase.Common.dll" },
				new OutputFileCopy { FromFile = "./firebase-config/source/bin/Release/Xamarin.Firebase.Config.dll" },
				new OutputFileCopy { FromFile = "./firebase-crash/source/bin/Release/Xamarin.Firebase.Crash.dll" },
				new OutputFileCopy { FromFile = "./firebase-database/source/bin/Release/Xamarin.Firebase.Database.dll" },
				new OutputFileCopy { FromFile = "./firebase-database-connection/source/bin/Release/Xamarin.Firebase.Database.Connection.dll" },
				new OutputFileCopy { FromFile = "./firebase-iid/source/bin/Release/Xamarin.Firebase.Iid.dll" },
				new OutputFileCopy { FromFile = "./firebase-messaging/source/bin/Release/Xamarin.Firebase.Messaging.dll" },
				new OutputFileCopy { FromFile = "./firebase-storage/source/bin/Release/Xamarin.Firebase.Storage.dll" },
				new OutputFileCopy { FromFile = "./firebase-storage-common/source/bin/Release/Xamarin.Firebase.Storage.Common.dll" },
			}
		},
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./ads/samples/AdMobSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./analytics/samples/AnalyticsSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
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
		new DefaultSolutionBuilder { SolutionPath = "./support-wearable/samples/MultiPageSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./support-wearable/samples/WatchFaceSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },

		new DefaultSolutionBuilder { SolutionPath = "./firebase-ads/samples/FirebaseAdmobQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-analytics/samples/FirebaseAnalyticsQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-appindexing/samples/AppIndexingSample.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-auth/samples/FirebaseAuthQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-config/samples/FirebaseConfigQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-crash/samples/FirebaseCrashReportingQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-invites/samples/FirebaseInvitesQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-messaging/samples/FirebaseMessagingQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-storage/samples/FirebaseStorageQuickstart.sln", BuildsOn = buildsOnWinMac, MaxCpuCount = CPU_COUNT, AlwaysUseMSBuild = ALWAYS_MSBUILD },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./base/nuget/Xamarin.GooglePlayServices.Base.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./basement/nuget/Xamarin.GooglePlayServices.Basement.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./ads/nuget/Xamarin.GooglePlayServices.Ads.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./ads-lite/nuget/Xamarin.GooglePlayServices.Ads.Lite.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./analytics/nuget/Xamarin.GooglePlayServices.Analytics.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./analytics-impl/nuget/Xamarin.GooglePlayServices.Analytics.Impl.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./appinvite/nuget/Xamarin.GooglePlayServices.AppInvite.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./auth/nuget/Xamarin.GooglePlayServices.Auth.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./auth-base/nuget/Xamarin.GooglePlayServices.Auth.Base.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./awareness/nuget/Xamarin.GooglePlayServices.Awareness.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./cast/nuget/Xamarin.GooglePlayServices.Cast.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./cast-framework/nuget/Xamarin.GooglePlayServices.Cast.Framework.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./clearcut/nuget/Xamarin.GooglePlayServices.Clearcut.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./drive/nuget/Xamarin.GooglePlayServices.Drive.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./fitness/nuget/Xamarin.GooglePlayServices.Fitness.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./games/nuget/Xamarin.GooglePlayServices.Games.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./gass/nuget/Xamarin.GooglePlayServices.Gass.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./gcm/nuget/Xamarin.GooglePlayServices.Gcm.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./identity/nuget/Xamarin.GooglePlayServices.Identity.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./iid/nuget/Xamarin.GooglePlayServices.Iid.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./instantapps/nuget/Xamarin.GooglePlayServices.InstantApps.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./location/nuget/Xamarin.GooglePlayServices.Location.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./maps/nuget/Xamarin.GooglePlayServices.Maps.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./nearby/nuget/Xamarin.GooglePlayServices.Nearby.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./panorama/nuget/Xamarin.GooglePlayServices.Panorama.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./places/nuget/Xamarin.GooglePlayServices.Places.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./plus/nuget/Xamarin.GooglePlayServices.Plus.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./safetynet/nuget/Xamarin.GooglePlayServices.SafetyNet.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./vision/nuget/Xamarin.GooglePlayServices.Vision.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./wallet/nuget/Xamarin.GooglePlayServices.Wallet.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./wearable/nuget/Xamarin.GooglePlayServices.Wearable.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./support-wearable/nuget/Xamarin.Android.Wear.nuspec", Version = WEAR_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./tagmanager/nuget/Xamarin.GooglePlayServices.TagManager.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./tagmanager-api/nuget/Xamarin.GooglePlayServices.TagManager.Api.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./tagmanager-v4-impl/nuget/Xamarin.GooglePlayServices.TagManager.V4.Impl.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./tasks/nuget/Xamarin.GooglePlayServices.Tasks.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },

		new NuGetInfo { NuSpec = "./firebase-ads/nuget/Xamarin.Firebase.Ads.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-analytics/nuget/Xamarin.Firebase.Analytics.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-analytics-impl/nuget/Xamarin.Firebase.Analytics.Impl.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-appindexing/nuget/Xamarin.Firebase.AppIndexing.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-auth/nuget/Xamarin.Firebase.Auth.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-common/nuget/Xamarin.Firebase.Common.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-config/nuget/Xamarin.Firebase.Config.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-crash/nuget/Xamarin.Firebase.Crash.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-database/nuget/Xamarin.Firebase.Database.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-database-connection/nuget/Xamarin.Firebase.Database.Connection.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-iid/nuget/Xamarin.Firebase.Iid.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-invites/nuget/Xamarin.Firebase.Invites.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-messaging/nuget/Xamarin.Firebase.Messaging.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-storage/nuget/Xamarin.Firebase.Storage.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-storage-common/nuget/Xamarin.Firebase.Storage.Common.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },

		// These are empty packages that depend on others
		new NuGetInfo { NuSpec = "./firebase-core/nuget/Xamarin.Firebase.Core.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-ads/nuget/Xamarin.Firebase.Ads.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./ads/component" },
		new Component { ManifestDirectory = "./analytics/component" },
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

		new Component { ManifestDirectory = "./firebase-ads/component" },
		new Component { ManifestDirectory = "./firebase-analytics/component" },
		new Component { ManifestDirectory = "./firebase-appindexing/component" },
		new Component { ManifestDirectory = "./firebase-auth/component" },
		new Component { ManifestDirectory = "./firebase-config/component" },
		new Component { ManifestDirectory = "./firebase-crash/component" },
		new Component { ManifestDirectory = "./firebase-invites/component" },
		new Component { ManifestDirectory = "./firebase-messaging/component" },
		new Component { ManifestDirectory = "./firebase-storage/component" },
	}
};

var NUGET_SOURCES = EnvironmentVariable ("NUGET_SOURCES") ?? Argument ("nugetsources", string.Empty);;
if (!string.IsNullOrEmpty (NUGET_SOURCES)) {
	buildSpec.NuGetSources = NUGET_SOURCES.Split (';',',').Select (ns => new NuGetSource { Url = ns }).ToArray ();
	Information ("Using Nuget Sources:");
	foreach (var nsrc in buildSpec.NuGetSources)
		Information ("  {0}", nsrc.Url);
}

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
	foreach (var aar in AAR_INFOS) {

		CopyFile (
			string.Format (path + "m2repository/com/google/{0}/{1}/{2}-{3}.aar", aar.Path, aar.AarVersion, aar.Dir, aar.AarVersion),
			string.Format (path + "{0}.aar", aar.Dir));
		Unzip (path + aar.Dir + ".aar", path + aar.Dir);
	}

	// Get Wearable stuff
	Unzip (path + "m2repository/com/google/android/support/wearable/" + WEARABLE_SUPPORT_VERSION + "/wearable-" + WEARABLE_SUPPORT_VERSION + ".aar", path + "wearable-support");
	CopyFile (path + "m2repository/com/google/android/wearable/wearable/" + WEAR_AAR_VERSION + "/wearable-" + WEAR_AAR_VERSION + ".jar", path + "wearable.jar");

	// Get the GPS Docs
	if (!FileExists (path + "docs.zip"))
		DownloadFile (DOCS_URL, path + "docs.zip");
	// Move the docs around
	if (!DirectoryExists (path + "docs")) {
		Unzip (path + "docs.zip", path);
		CopyDirectory (path + "google-play-services/docs", path + "docs/");
		DeleteDirectory (path + "google-play-services", true);
	}

	// Put the uitest.keystore in our artifacts for downstream jobs to use
	EnsureDirectoryExists ("./output");
	if (FileExists ("./uitest.keystore"))
		CopyFile ("./uitest.keystore", "./output/uitest.keystore");
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
});

Task ("merge").IsDependentOn ("libs").Does (() =>
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
		Libs = new List<FilePath> {
			MONODROID_PATH
		},
	});
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (FileExists ("./generated.targets"))
		DeleteFile ("./generated.targets");

	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);

	CleanDirectories ("./**/packages");
});

Task ("cleanwin").Does (() =>
{
	if (FileExists ("./generated.targets"))
		DeleteFile ("./generated.targets");

	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);

	CleanDirectories ("./**/packages");
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
		var NUGET_VERSION = PLAY_NUGET_VERSION;
		var COMPONENT_VERSION = PLAY_COMPONENT_VERSION;

		if (yaml.FullPath.Contains ("firebase-")) {
			NUGET_VERSION = FIREBASE_NUGET_VERSION;
			COMPONENT_VERSION = FIREBASE_COMPONENT_VERSION;
		}

		var manifestTxt = FileReadText (yaml)
			.Replace ("$nuget-version$", NUGET_VERSION)
			.Replace ("$version$", COMPONENT_VERSION)
			.Replace ("$wear-version$", WEAR_COMPONENT_VERSION)
			.Replace ("$wear-nuget-version$", WEAR_NUGET_VERSION);

		var newYaml = yaml.GetDirectory ().CombineWithFilePath ("component.yaml");
		FileWriteText (newYaml, manifestTxt);
	}

	if (DirectoryExists ("./output"))
		DeleteFiles ("./output/*.xam");
});

Task ("component").IsDependentOn ("component-docs").IsDependentOn ("component-setup").IsDependentOn ("component-base");

Task ("nuget-setup").IsDependentOn ("buildtasks").Does (() => {

	var templateText = FileReadText ("./template.targets");

	if (FileExists ("./generated.targets"))
		DeleteFile ("./generated.targets");

	// Get the zip file offsets for the relevant aar's
	var downloadParts = FindZipEntries ("./externals/m2repository.zip")
		.Where (e => (e.EntryName.Contains (PLAY_AAR_VERSION) || e.EntryName.Contains (WEAR_AAR_VERSION))
			&& (e.EntryName.Contains (".aar") || e.EntryName.Contains (".jar")))
		.Select (e => new PartialZipInfo {
			RangeStart = e.RangeStart,
			RangeEnd = e.RangeEnd,
			Url = M2_REPOSITORY,
			LocalPath = e.EntryName,
			Md5 = ReadZipEntryText ("./externals/m2repository.zip", e.EntryName + ".md5", readBinaryAsHex: !e.EntryName.Contains ("wearable-" + WEAR_AAR_VERSION + ".aar"))
		}).ToList ();

	foreach (var aar in AAR_INFOS) {

		// Write out the nuspec from template
		var nuspec = new FilePath ("./" + aar.BindingDir + "/nuget/" + aar.NugetId + ".template.nuspec");
		var nuspecTxt = FileReadText (nuspec).Replace ("$aar-version$", VERSION_DESC);
		var newNuspec = nuspec.FullPath.Replace (".template.nuspec", ".nuspec");
		FileWriteText (newNuspec, nuspecTxt);

		// Write out the .targets
		var part = downloadParts.FirstOrDefault (p => p.LocalPath.EndsWith ("/" + aar.Dir + "-" + aar.AarVersion + ".aar"));

		if (part == null)
			throw new Exception ("No matching part found for '" + aar.Dir + "-" + aar.AarVersion + "' in partial-download-info.json ");

		var msName = aar.Dir.Replace("-", "");

		var xbdKey = "playservices-" + PLAY_AAR_VERSION + "/" + msName;
		if (aar.Dir.StartsWith ("firebase-"))
			xbdKey = "firebase-" + FIREBASE_AAR_VERSION + "/" + msName;

		var items = new Dictionary<string, string> {
			{ "_XbdUrl_", "_XbdUrl_" + msName },
			{ "_XbdKey_", "_XbdKey_" + msName },
			{ "_XbdAarFile_", "_XbdAarFile_" + msName },
			{ "_XbdAarFileInSdk_", "_XbdAarFileInSdk_" + msName },
			{ "_XbdAssemblyName_", "_XbdAssemblyName_" + msName },
			{ "_XbdAarFileFullPath_", "_XbdAarFileFullPath_" + msName },
			{ "_XbdRestoreItems_", "_XbdRestoreItems_" + msName },
			{ "$XbdUrl$", M2_REPOSITORY },
			{ "$XbdMd5$", part.Md5 },
			{ "$XbdKey$", xbdKey },
			{ "$XbdAssemblyName$", aar.NugetId },
			{ "$XbdRangeStart$", part.RangeStart.ToString() },
			{ "$XbdRangeEnd$", part.RangeEnd.ToString() },
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

Task ("nuget").IsDependentOn ("nuget-setup").IsDependentOn ("nuget-base").IsDependentOn ("libs");

Task ("libs").IsDependentOn ("nuget-setup").IsDependentOn ("libs-base");

Task ("buildtasks").Does (() =>
{
	NuGetRestore ("./basement/buildtasks/Basement-BuildTasks.csproj");

	DotNetBuild ("./basement/buildtasks/Basement-BuildTasks.csproj", c => c.Configuration = "Release");

	CopyFile ("./basement/buildtasks/bin/Release/Xamarin.GooglePlayServices.Basement.targets", "./basement/nuget/merge.targets");
});


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
