using System;
using Android.App;

[assembly: UsesPermission (Android.Manifest.Permission.Internet)]

namespace Firebase.Crash.Internal.Service
{
    //<service android:name="com.google.firebase.crash.internal.service.FirebaseCrashReceiverService"
    //    android:process=":background_crash"/>
    [Service (Name="com.google.firebase.crash.internal.service.FirebaseCrashReceiverService", 
        Process=":background_crash")]
    public partial class FirebaseCrashReceiverService
    {
    }

    //<service android:name="com.google.firebase.crash.internal.service.FirebaseCrashSenderService"
    //    android:process=":background_crash"/>
    [Service (Name="com.google.firebase.crash.internal.service.FirebaseCrashSenderService", 
        Process=":background_crash")]
    public partial class FirebaseCrashSenderService
    {
    }
}

