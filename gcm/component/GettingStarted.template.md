Google Cloud Messaging (GCM) is a free service that enables developers to send messages between servers and client apps. This includes downstream messages from servers to client apps, and upstream messages from client apps to servers.

For example, a lightweight downstream message could inform a client app that there is new data to be fetched from the server, as in the case of a "new email" notification. For use cases such as instant messaging, a GCM message can transfer up to 4kb of payload to the client app. The GCM service handles all aspects of queueing of messages and delivery to and from the target client app.



{APILEVELS}



{DEVELOPERSCONSOLE}

To use the Google Cloud Messaging in your Android app, you must enable the *Cloud Messaging for Android* in the Google Developers Console, under *APIs and Auth*.



{ANDROIDMANIFEST}

It is **VERY** important that your application's package name does **NOT** start with an uppercase letter.  This will cause an error in deploying your application to a simulator or device.

Cloud Messaging requires the *Internet*, *WakeLock*, and *com.google.android.c2dm.permission.RECEIVE* permissions.  You can add these by adding the following code to your app (or manually adding them to your AndroidManifest.xml file):

```csharp
[assembly: UsesPermission ("com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.WakeLock)]
```

Cloud messaging also requires you to declare and use a special permission (`@PACKAGE_NAME@.permission.C2D_MESSAGE`) in your manifest file.  Again, you can either manually update your manifest file or add this code to your app:

```csharp
[assembly: Permission (Name="@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission ("@PACKAGE_NAME@.permission.C2D_MESSAGE")]
```

Note that the `@PACKAGE_NAME@` is a special token that will be replaced with the actual package name of your application at build time.

Finally, you will need to declare the built-in GCM `BroadcastReceiver` in your manifest.  The easiest way to do this is to add a new .cs file to your project and paste the following code into it:

```csharp
using System;
using Android.Runtime;
using Android.App;
using Android.Content;

namespace Android.Gms.Gcm
{
    [BroadcastReceiver (
        Name="com.google.android.gms.gcm.GcmReceiver",
        Exported=true,
        Permission="com.google.android.c2dm.permission.SEND")]
    [IntentFilter (new [] { "com.google.android.c2dm.intent.RECEIVE", "com.google.android.c2dm.intent.REGISTRATION" }, 
        Categories = new [] { "@PACKAGE_NAME@" })]
    partial class GcmReceiver { }
}
```



{SAMPLES}



{LEARNMORE}

You can learn more about Google Play Services GCM by visiting the official [Google Cloud Messaging for Android](https://developers.google.com/cloud-messaging/android/start) documentation.



{LINKS}
