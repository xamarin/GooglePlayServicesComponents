using System;
using Android.Runtime;
using Android.App;

[assembly: UsesPermission (Android.Manifest.Permission.Internet)]

namespace Android.Gms.AppInvite
{
    // PACKAGE: com.google.android.gms.appinvite

//    <activity
//    android:name=".PreviewActivity"
//        android:exported="true"
//        android:theme="@style/Theme.AppInvite.Preview">
//        <intent-filter>
//        <action android:name="com.google.android.gms.appinvite.ACTION_PREVIEW"/>
//        <category android:name="android.intent.category.DEFAULT"/>
//        </intent-filter>
//        </activity>
    [Activity (
        Name="com.google.android.gms.appinvite.PreviewActivity",
        Exported=true,
        Theme="@style/Theme.AppInvite.Preview")]
    [IntentFilter (new [] { "com.google.android.gms.appinvite.ACTION_PREVIEW" },
        Categories = new [] { global::Android.Content.Intent.CategoryDefault })]
    partial class PreviewActivity { }
}

