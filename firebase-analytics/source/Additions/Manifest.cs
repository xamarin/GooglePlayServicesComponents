using Android.App;
using Android.Content;

[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.WakeLock)]

namespace Android.Gms.Measurement
{
    //<receiver
    //android:name="com.google.android.gms.measurement.AppMeasurementReceiver"
    //android:enabled="true">
    //<intent-filter>
    //<action android:name="com.google.android.gms.measurement.UPLOAD"/>
    //</intent-filter>
    //</receiver>
    [BroadcastReceiver (Name="com.google.android.gms.measurement.AppMeasurementReceiver", Enabled=true)]
    [IntentFilter (new [] { "com.google.android.gms.measurement.UPLOAD" })]
    public partial class AppMeasurementReceiver
    {
    }

    //<service
    //android:name="com.google.android.gms.measurement.AppMeasurementService"
    //android:enabled="true"
    //android:exported="false"/>
    [Service (Name="com.google.android.gms.measurement.AppMeasurementService", Enabled=true, Exported=false)]
    public partial class AppMeasurementService
    {
    }
}

