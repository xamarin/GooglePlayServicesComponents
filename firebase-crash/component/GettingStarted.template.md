
Firebase Crash Reporting creates detailed reports of the errors in your app. Errors are grouped into clusters of similar stack traces, and triaged by the severity of impact on your users. In addition to automatic reports, you can log custom events to help capture the steps leading up to a crash.

{FIREBASE-CONFIGURE}


## Create your first error

Firebase Crash Reporting automatically generates reports for fatal errors (or uncaught exceptions). However, you can also generate reports in instances where you catch an exception but still want to report the occurrence. To report such an error, you can follow these steps:

Add a call to the static report method in the main activity:

```csharp
FirebaseCrash.Report (new Exception ("My first Android non-fatal error"));
```

Launch the app.

In adb logcat, look for the message confirming that Crash Reporting is enabled.

Check the Crash Reporting section of the Firebase console to see the error. Note that it takes 1-2 minutes for errors to show there.


## Create custom logs

You can use Crash Reporting to log custom events in your error reports and optionally the logcat. If you wish to log an event and don't want logcat output, you only need to pass a string as the argument, as shown in this example:

```csharp
FirebaseCrash.Log("Activity created");
```

If you want to create logcat output, you must also supply the log level and a tag.



{FIREBASE-SAMPLES}



{FIREBASE-LEARNMORE}



{LINKS}
