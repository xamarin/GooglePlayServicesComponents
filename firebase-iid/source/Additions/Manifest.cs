using System;
using Android.App;
using Android.Content;

[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.WakeLock)]
[assembly: UsesPermission ("com.google.android.c2dm.permission.RECEIVE")]

//<permission android:name="${applicationId}.permission.C2D_MESSAGE" android:protectionLevel="signature" />
//<uses-permission android:name="${applicationId}.permission.C2D_MESSAGE" />
[assembly: Permission (Name="${applicationId}.permission.C2D_MESSAGE", ProtectionLevel=Android.Content.PM.Protection.Signature)]
[assembly: UsesPermission ("${applicationId}.permission.C2D_MESSAGE")]

namespace Firebase.Iid
{
    //<receiver
    //    android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver"
    //    android:exported="true"
    //    android:permission="com.google.android.c2dm.permission.SEND" >
    //    <intent-filter>
    //        <action android:name="com.google.android.c2dm.intent.RECEIVE" />
    //        <action android:name="com.google.android.c2dm.intent.REGISTRATION" />
    //        <category android:name="${applicationId}" />
    //    </intent-filter>
    //</receiver>
    [BroadcastReceiver (Name="com.google.firebase.iid.FirebaseInstanceIdReceiver", 
        Exported=true,
        Permission="com.google.android.c2dm.permission.SEND")]
    [IntentFilter (new [] { "com.google.android.c2dm.intent.RECEIVE", "com.google.android.c2dm.intent.REGISTRATION" },
        Categories = new [] { "${applicationId}" })]
    public partial class FirebaseInstanceIdReceiver
    {
    }

    // <!-- Internal (not exported) receiver used by the app to start its own exported services
    // without risk of being spoofed. -->
    // <receiver
    //    android:name="com.google.firebase.iid.FirebaseInstanceIdInternalReceiver"
    //    android:exported="false" />
    [BroadcastReceiver (Name="com.google.firebase.iid.FirebaseInstanceIdInternalReceiver", 
        Exported=false)]
    public partial class FirebaseInstanceIdInternalReceiver
    {
    }

    // <!-- FirebaseInstanceIdService performs security checks at runtime,
    // no need for explicit permissions despite exported="true" -->
    // <service
    //    android:name="com.google.firebase.iid.FirebaseInstanceIdService"
    //    android:exported="true">
    //    <intent-filter android:priority="-500">
    //        <action android:name="com.google.firebase.INSTANCE_ID_EVENT" />
    //    </intent-filter>
    // </service>
    [Service (Name="com.google.firebase.iid.FirebaseInstanceIdService",
        Exported=true)]
    [IntentFilter (new [] { "com.google.firebase.INSTANCE_ID_EVENT" }, Priority=-500)]
    public partial class FirebaseInstanceIdService
    {
    }
}

