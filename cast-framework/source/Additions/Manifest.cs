using System;
using Android.App;
using Android.Content;

// <manifest xmlns:android="http://schemas.android.com/apk/res/android"
//           package="com.google.android.gms.cast.framework">
//     <uses-sdk android:minSdkVersion="9"/>
//     <application>
//         <receiver android:name="com.google.android.gms.cast.framework.media.MediaIntentReceiver" />
//         <service android:name="com.google.android.gms.cast.framework.media.MediaNotificationService" />
//         <service android:name="com.google.android.gms.cast.framework.ReconnectionService"/>
//     </application>
// </manifest>

namespace Android.Gms.Cast.Framework.Media
{
    [BroadcastReceiver (Name="com.google.android.gms.cast.framework.media.MediaIntentReceiver")]
    partial class MediaIntentReceiver { }

    [Service(Name = "com.google.android.gms.cast.framework.media.MediaNotificationService")]
    partial class MediaNotificationService { }
}

namespace Android.Gms.Cast.Framework
{
    [Service(Name = "com.google.android.gms.cast.framework.ReconnectionService")]
    partial class MediaNotificationService { }
}
