using System;
using Android.App;

namespace Firebase.Messaging
{
    // <!-- FirebaseMessagingService performs security checks at runtime,
    // no need for explicit permissions despite exported="true" -->
    // <service
    //    android:name="com.google.firebase.messaging.FirebaseMessagingService"
    //    android:exported="true">
    //    <intent-filter android:priority="-500">
    //        <action android:name="com.google.firebase.MESSAGING_EVENT" />
    //    </intent-filter>
    // </service>
    [Service (Name="com.google.firebase.messaging.FirebaseMessagingService",
        Exported=true)]
    [IntentFilter (new [] { "com.google.firebase.MESSAGING_EVENT" }, Priority=-500)]
    public partial class FirebaseMessagingService
    {
    }
}
