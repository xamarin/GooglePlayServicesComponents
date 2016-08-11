using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle ("Xamarin.Firebase.Analytics")]
[assembly: AssemblyDescription ("")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("Xamarin")]
[assembly: AssemblyProduct ("")]
[assembly: AssemblyCopyright ("Xamarin")]
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

[assembly: Java.Interop.JavaLibraryReference ("classes.jar",
    PackageName = __Consts.PackageName,
    SourceUrl = __FbConsts.Url,
    EmbeddedArchive = __Consts.AarPath,
    Version = __FbConsts.Version,
    Sha1sum = __FbConsts.Sha1sum)]
// AppCompat-v7 resources
[assembly: Android.IncludeAndroidResourcesFromAttribute ("./",
    PackageName = __Consts.PackageName,
    SourceUrl   = __FbConsts.Url,
    EmbeddedArchive = __Consts.AarPath,
    Version     = __FbConsts.Version,
    Sha1sum = __FbConsts.Sha1sum)]

// Google Addon feed with GPS in it:
//      https://dl-ssl.google.com/android/repository/addon.xml

static class __Consts {
    public const string PackageName = "Firebase Analytics";
    public const string AarPath = "m2repository/com/google/firebase/firebase-analytics/" + __FbConsts.Version + "/firebase-analytics-" + __FbConsts.Version + ".aar";
}
    