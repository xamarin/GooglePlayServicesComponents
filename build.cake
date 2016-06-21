#tool nuget:?package=ILRepack&version=2.0.10
#tool nuget:?package=XamarinComponent
#tool nuget:?package=Cake.MonoApiTools

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=Cake.MonoApiTools

// To find new URL: https://dl-ssl.google.com/android/repository/addon.xml and search for google_play_services_*.zip
var DOCS_URL = "https://dl-ssl.google.com/android/repository/google_play_services_9080000_r30.zip";
var M2_REPOSITORY = "https://dl-ssl.google.com/android/repository/google_m2repository_r28.zip";

var PLAY_COMPONENT_VERSION = "30.0.2.0";
var PLAY_NUGET_VERSION = "30.0.2-alpha1";
var PLAY_AAR_VERSION = "9.0.2";

var WEAR_COMPONENT_VERSION = "1.4.0.0";
var WEAR_NUGET_VERSION = "1.4.0.0-alpha6";
var WEAR_AAR_VERSION = PLAY_AAR_VERSION;

var WEARABLE_SUPPORT_VERSION = "1.4.0";
var WEARABLE_VERSION = "1.0.0";

var FIREBASE_COMPONENT_VERSION = PLAY_COMPONENT_VERSION;
var FIREBASE_NUGET_VERSION = PLAY_NUGET_VERSION;
var FIREBASE_AAR_VERSION = PLAY_AAR_VERSION;

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_INFOS = new [] {
	new AarInfo ("play-services-auth", "android/gms/play-services-auth", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-auth-base", "android/gms/play-services-auth-base", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-base", "android/gms/play-services-base", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-basement", "android/gms/play-services-basement", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-ads", "android/gms/play-services-ads", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-ads-lite", "android/gms/play-services-ads-lite", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-analytics", "android/gms/play-services-analytics", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-analytics-impl", "android/gms/play-services-analytics-impl", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-appindexing", "android/gms/play-services-appindexing", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-appinvite", "android/gms/play-services-appinvite", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-cast", "android/gms/play-services-cast", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-drive", "android/gms/play-services-drive", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-fitness", "android/gms/play-services-fitness", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-games", "android/gms/play-services-games", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-gcm", "android/gms/play-services-gcm", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-identity", "android/gms/play-services-identity", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-iid", "android/gms/play-services-iid", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-location", "android/gms/play-services-location", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-maps", "android/gms/play-services-maps", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-nearby", "android/gms/play-services-nearby", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-panorama", "android/gms/play-services-panorama", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-plus", "android/gms/play-services-plus", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-safetynet", "android/gms/play-services-safetynet", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-tasks", "android/gms/play-services-tasks", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-tagmanager", "android/gms/play-services-tagmanager", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-tagmanager-api", "android/gms/play-services-tagmanager-api", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-vision", "android/gms/play-services-vision", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-wallet", "android/gms/play-services-wallet", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),
	new AarInfo ("play-services-wearable", "android/gms/play-services-wearable", PLAY_AAR_VERSION, PLAY_NUGET_VERSION, PLAY_COMPONENT_VERSION),

	new AarInfo ("firebase-ads", "firebase/firebase-ads", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-analytics", "firebase/firebase-analytics", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-analytics-impl", "firebase/firebase-analytics-impl", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-auth", "firebase/firebase-auth", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-auth-common", "firebase/firebase-auth-common", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-auth-module", "firebase/firebase-auth-module", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-common", "firebase/firebase-common", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-config", "firebase/firebase-config", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-core", "firebase/firebase-core", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-crash", "firebase/firebase-crash", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-database", "firebase/firebase-database", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-database-connection", "firebase/firebase-database-connection", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-iid", "firebase/firebase-iid", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-invites", "firebase/firebase-invites", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-messaging", "firebase/firebase-messaging", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-storage", "firebase/firebase-storage", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
	new AarInfo ("firebase-storage-common", "firebase/firebase-storage-common", FIREBASE_AAR_VERSION, FIREBASE_NUGET_VERSION, FIREBASE_COMPONENT_VERSION),
};

class AarInfo
{
	public AarInfo (string name, string path, string aarVersion, string nugetVersion, string componentVersion)
	{
		Name = name;
		Path = path;
		AarVersion = aarVersion;
		NuGetVersion = nugetVersion;
		ComponentVersion = componentVersion;
	}

	public string Name { get; set; }
	public string Path { get; set; }
	public string AarVersion { get; set; }
	public string NuGetVersion { get; set; }
	public string ComponentVersion { get; set; }
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
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./base/source/bin/Release/Xamarin.GooglePlayServices.Base.dll" },
				new OutputFileCopy { FromFile = "./basement/source/bin/Release/Xamarin.GooglePlayServices.Basement.dll" },
				new OutputFileCopy { FromFile = "./ads/source/bin/Release/Xamarin.GooglePlayServices.Ads.dll" },
				new OutputFileCopy { FromFile = "./ads-lite/source/bin/Release/Xamarin.GooglePlayServices.Ads.Lite.dll" },
				new OutputFileCopy { FromFile = "./analytics/source/bin/Release/Xamarin.GooglePlayServices.Analytics.dll" },
				new OutputFileCopy { FromFile = "./analytics-impl/source/bin/Release/Xamarin.GooglePlayServices.Analytics.Impl.dll" },
				new OutputFileCopy { FromFile = "./appinvite/source/bin/Release/Xamarin.GooglePlayServices.AppInvite.dll" },
				new OutputFileCopy { FromFile = "./appindexing/source/bin/Release/Xamarin.GooglePlayServices.AppIndexing.dll" },
				new OutputFileCopy { FromFile = "./auth/source/bin/Release/Xamarin.GooglePlayServices.Auth.dll" },
				new OutputFileCopy { FromFile = "./auth-base/source/bin/Release/Xamarin.GooglePlayServices.Auth.Base.dll" },
				new OutputFileCopy { FromFile = "./cast/source/bin/Release/Xamarin.GooglePlayServices.Cast.dll" },
				new OutputFileCopy { FromFile = "./drive/source/bin/Release/Xamarin.GooglePlayServices.Drive.dll" },
				new OutputFileCopy { FromFile = "./fitness/source/bin/Release/Xamarin.GooglePlayServices.Fitness.dll" },
				new OutputFileCopy { FromFile = "./games/source/bin/Release/Xamarin.GooglePlayServices.Games.dll" },
				new OutputFileCopy { FromFile = "./gcm/source/bin/Release/Xamarin.GooglePlayServices.Gcm.dll" },
				new OutputFileCopy { FromFile = "./identity/source/bin/Release/Xamarin.GooglePlayServices.Identity.dll" },
				new OutputFileCopy { FromFile = "./iid/source/bin/Release/Xamarin.GooglePlayServices.Iid.dll" },
				new OutputFileCopy { FromFile = "./location/source/bin/Release/Xamarin.GooglePlayServices.Location.dll" },
				new OutputFileCopy { FromFile = "./maps/source/bin/Release/Xamarin.GooglePlayServices.Maps.dll" },
				new OutputFileCopy { FromFile = "./nearby/source/bin/Release/Xamarin.GooglePlayServices.Nearby.dll" },
				new OutputFileCopy { FromFile = "./panorama/source/bin/Release/Xamarin.GooglePlayServices.Panorama.dll" },
				new OutputFileCopy { FromFile = "./plus/source/bin/Release/Xamarin.GooglePlayServices.Plus.dll" },
				new OutputFileCopy { FromFile = "./safetynet/source/bin/Release/Xamarin.GooglePlayServices.SafetyNet.dll" },
				new OutputFileCopy { FromFile = "./vision/source/bin/Release/Xamarin.GooglePlayServices.Vision.dll" },
				new OutputFileCopy { FromFile = "./wallet/source/bin/Release/Xamarin.GooglePlayServices.Wallet.dll" },
				new OutputFileCopy { FromFile = "./wearable/source/bin/Release/Xamarin.GooglePlayServices.Wearable.dll" },
				new OutputFileCopy { FromFile = "./support-wearable/source/bin/Release/Xamarin.Android.Wearable.dll" },
				new OutputFileCopy { FromFile = "./tagmanager/source/bin/Release/Xamarin.GooglePlayServices.TagManager.dll" },
				new OutputFileCopy { FromFile = "./tagmanager-api/source/bin/Release/Xamarin.GooglePlayServices.TagManager.Api.dll" },
				new OutputFileCopy { FromFile = "./tasks/source/bin/Release/Xamarin.GooglePlayServices.Tasks.dll" },

				new OutputFileCopy { FromFile = "./firebase-analytics/source/bin/Release/Xamarin.Firebase.Analytics.dll" },
				new OutputFileCopy { FromFile = "./firebase-analytics-impl/source/bin/Release/Xamarin.Firebase.Analytics.Impl.dll" },
				new OutputFileCopy { FromFile = "./firebase-auth/source/bin/Release/Xamarin.Firebase.Auth.dll" },
				new OutputFileCopy { FromFile = "./firebase-auth-common/source/bin/Release/Xamarin.Firebase.Auth.Common.dll" },
				new OutputFileCopy { FromFile = "./firebase-auth-module/source/bin/Release/Xamarin.Firebase.Auth.Module.dll" },
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

		new DefaultSolutionBuilder { SolutionPath = "./firebase-analytics/samples/FirebaseAnalyticsQuickstart.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-auth/samples/FirebaseAuthQuickstart.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-crash/samples/FirebaseCrashReportingQuickstart.sln", BuildsOn = buildsOnWinMac },
		new DefaultSolutionBuilder { SolutionPath = "./firebase-messaging/samples/FirebaseMessagingQuickstart.sln", BuildsOn = buildsOnWinMac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./base/nuget/Xamarin.GooglePlayServices.Base.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./basement/nuget/Xamarin.GooglePlayServices.Basement.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./ads/nuget/Xamarin.GooglePlayServices.Ads.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./ads-lite/nuget/Xamarin.GooglePlayServices.Ads.Lite.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./analytics/nuget/Xamarin.GooglePlayServices.Analytics.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./analytics-impl/nuget/Xamarin.GooglePlayServices.Analytics.Impl.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./appindexing/nuget/Xamarin.GooglePlayServices.AppIndexing.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./appinvite/nuget/Xamarin.GooglePlayServices.AppInvite.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./auth/nuget/Xamarin.GooglePlayServices.Auth.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./auth-base/nuget/Xamarin.GooglePlayServices.Auth.Base.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./cast/nuget/Xamarin.GooglePlayServices.Cast.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./drive/nuget/Xamarin.GooglePlayServices.Drive.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./fitness/nuget/Xamarin.GooglePlayServices.Fitness.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./games/nuget/Xamarin.GooglePlayServices.Games.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./gcm/nuget/Xamarin.GooglePlayServices.Gcm.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./identity/nuget/Xamarin.GooglePlayServices.Identity.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./iid/nuget/Xamarin.GooglePlayServices.Iid.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./location/nuget/Xamarin.GooglePlayServices.Location.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./maps/nuget/Xamarin.GooglePlayServices.Maps.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./nearby/nuget/Xamarin.GooglePlayServices.Nearby.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./panorama/nuget/Xamarin.GooglePlayServices.Panorama.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./plus/nuget/Xamarin.GooglePlayServices.Plus.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./safetynet/nuget/Xamarin.GooglePlayServices.SafetyNet.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./vision/nuget/Xamarin.GooglePlayServices.Vision.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./wallet/nuget/Xamarin.GooglePlayServices.Wallet.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./wearable/nuget/Xamarin.GooglePlayServices.Wearable.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./support-wearable/nuget/Xamarin.Android.Wear.nuspec", Version = WEAR_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./tagmanager/nuget/Xamarin.GooglePlayServices.TagManager.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./tagmanager-api/nuget/Xamarin.GooglePlayServices.TagManager.Api.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./tasks/nuget/Xamarin.GooglePlayServices.Tasks.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },

		new NuGetInfo { NuSpec = "./firebase-analytics/nuget/Xamarin.Firebase.Analytics.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-analytics-impl/nuget/Xamarin.Firebase.Analytics.Impl.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-auth/nuget/Xamarin.Firebase.Auth.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-auth-common/nuget/Xamarin.Firebase.Auth.Common.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-auth-module/nuget/Xamarin.Firebase.Auth.Module.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-common/nuget/Xamarin.Firebase.Common.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-config/nuget/Xamarin.Firebase.Config.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-crash/nuget/Xamarin.Firebase.Crash.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-database/nuget/Xamarin.Firebase.Database.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-database-connection/nuget/Xamarin.Firebase.Database.Connection.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./firebase-iid/nuget/Xamarin.Firebase.Iid.nuspec", Version = PLAY_NUGET_VERSION, RequireLicenseAcceptance = true },
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
	foreach (var aar in AAR_INFOS) {

		CopyFile (
			string.Format (path + "m2repository/com/google/{0}/{1}/{2}-{3}.aar", aar.Path, aar.AarVersion, aar.Name, aar.AarVersion),
			string.Format (path + "{0}.aar", aar.Name));
		Unzip (path + aar.Name + ".aar", path + aar.Name);
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

Task ("diff")
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

	// Grab the latest published api info from S3
	var latestReleasedApiInfoUrl = "http://xamarin-components-apiinfo.s3.amazonaws.com/GooglePlayServices.Android-Latest.xml";
	DownloadFile (latestReleasedApiInfoUrl, "./output/GooglePlayServices.api-info.previous.xml");

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

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
