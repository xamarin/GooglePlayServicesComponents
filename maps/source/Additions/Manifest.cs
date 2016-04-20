using System;
using Android.App;

[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]

[assembly: UsesFeature (GLESVersion=0x00020000, Required=true)]
