using System;
using Android.Runtime;
using Android.App;
using Android.Content;

[assembly: UsesPermission (Android.Manifest.Permission.WakeLock)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]

namespace Android.Gms.TagManager
{
    // <service android:name="com.google.android.gms.tagmanager.TagManagerService"
    //    android:exported="false"
    //    android:enabled="true" />
    [Service (Name="com.google.android.gms.tagmanager.TagManagerService", Exported=false, Enabled=true)]
    public partial class TagManagerService
    {
    }

    //    <activity
    //        android:name="com.google.android.gms.tagmanager.TagManagerPreviewActivity"
    //        android:noHistory="true">  <!-- optional, removes the previewActivity from the activity stack. -->
    //        <intent-filter>
    //            <data android:scheme="tagmanager.c.${applicationId}" />
    //            <action android:name="android.intent.action.VIEW" />
    //            <category android:name="android.intent.category.DEFAULT" />
    //            <category android:name="android.intent.category.BROWSABLE"/>
    //        </intent-filter>
    //    </activity>
    [Activity (Name="com.google.android.gms.tagmanager.TagManagerPreviewActivity", NoHistory=true)]
    [IntentFilter (new [] { Intent.ActionView },
        DataScheme="tagmanager.c.${applicationId}",
        Categories=new [] { Intent.CategoryDefault, Intent.CategoryBrowsable })]
    public partial class TagManagerPreviewActivity
    {
    }
}

