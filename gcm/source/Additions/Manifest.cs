﻿using System;
using Android.App;

[assembly: UsesPermission ("com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]

// [assembly: Permission (Name="@PACKAGE_NAME@.permission.C2D_MESSAGE")]
// [assembly: UsesPermission ("@PACKAGE_NAME@.permission.C2D_MESSAGE")]

// namespace Android.Gms.Gcm
// {
//     [Register ("com/google/android/gms/gcm/GcmReceiver", DoNotGenerateAcw=true)]
//     [BroadcastReceiver (
//         Name="com.google.android.gms.gcm.GcmReceiver",
//         Exported=true,
//         Permission="com.google.android.c2dm.permission.SEND")]
//     [IntentFilter (new [] { "com.google.android.c2dm.intent.RECEIVE", "com.google.android.c2dm.intent.REGISTRATION" }, 
//         Categories = new [] { "@PACKAGE_NAME@" })]
//     internal class _InternalGcmReceiver : BroadcastReceiver
//     {
//         public override void OnReceive (Context context, Intent intent)
//         { // stub
//         }
//     }
// }