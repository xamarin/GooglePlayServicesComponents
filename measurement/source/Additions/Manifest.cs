using System;
using Android.Runtime;
using Android.App;
using Android.Content;

[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.WakeLock)]

namespace Android.Gms.Measurement
{

//    <provider
//    android:authorities="${applicationId}.google_measurement_service"
//        android:name="com.google.android.gms.measurement.AppMeasurementContentProvider"
//        android:exported="false"/>
    [ContentProvider (new [] { "${applicationId}.google_measurement_service" },
        Name="com.google.android.gms.measurement.AppMeasurementContentProvider",
        Exported=false)]
    partial class AppMeasurementContentProvider {}

    [Service (
        Name="com.google.android.gms.measurement.AppMeasurementService", 
        Enabled=true, 
        Exported =false)]
    partial class AppMeasurementService { }

//    <receiver
//    android:name="com.google.android.gms.measurement.AppMeasurementReceiver"
//        android:enabled="true">
//        <intent-filter>
//        <action android:name="com.google.android.gms.measurement.UPLOAD"/>
//        </intent-filter>
//        </receiver>
    [BroadcastReceiver (
        Name="com.google.android.gms.measurement.AppMeasurementReceiver",
        Enabled=true)]
    [IntentFilter (new [] { "com.google.android.gms.measurement.UPLOAD" })]
    partial class AppMeasurementReceiver { }
}
