using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.
[assembly: AssemblyTitle ("Xamarin.Android.Wearable")]
[assembly: AssemblyDescription ("")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("")]
[assembly: AssemblyProduct ("")]
[assembly: AssemblyCopyright ("Xamarin Inc.")]
[assembly: AssemblyTrademark ("")]
[assembly: AssemblyCulture ("")]
// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.
[assembly: AssemblyVersion ("1.0.0")]
// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.
//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]


// Wearable Support lib
[assembly: Java.Interop.JavaLibraryReference ("classes.jar",
	PackageName = __WearableConsts.PackageName,
	SourceUrl = __GpsConsts.Url,
	EmbeddedArchive = __WearableConsts.AarPath,
    Version     = __GpsConsts.WearVersion,
    Sha1sum = __GpsConsts.Sha1sum)]
// Wearable Support Resources
[assembly: Android.IncludeAndroidResourcesFromAttribute ("./",
	PackageName = __WearableConsts.PackageName,
	SourceUrl   = __GpsConsts.Url,
	EmbeddedArchive = __WearableConsts.AarPath,
    Version     = __GpsConsts.WearVersion,
    Sha1sum = __GpsConsts.Sha1sum)]

static class __WearableConsts {    
	public const string PackageName = "Android Wear";
    public const string AarPath = "m2repository/com/google/android/support/wearable/" + __GpsConsts.WearVersion + "/wearable-" + __GpsConsts.WearVersion + ".aar";
}
