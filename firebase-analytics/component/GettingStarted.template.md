Get Started with Firebase Analytics for Android

Firebase Analytics collects usage and behavior data for your app. The SDK logs two primary types of information:

 - **Events**: What is happening in your app, such as user actions, system events, or errors.

 - **User properties**: Attributes you define to describe segments of your userbase, such as language preference or geographic location.

Analytics automatically logs some events and user properties; you don't need to add any code to enable them.



{FIREBASE-CONFIGURE}




### Add Analytics to your App

Declare the `FirebaseAnalytics` object at the top of your activity:

```csharp
FirebaseAnalytics firebaseAnalytics;
```

Then initialize it in the `OnCreate ()` method:

```csharp
// Obtain the FirebaseAnalytics instance.
firebaseAnalytics = FirebaseAnalytics.GetInstance (this);
```

### Log events

Once you have created a `FirebaseAnalytics` instance, you can use it to log either predefined or custom events with the `LogEvent ()` method. You can explore the predefined events and parameters in the `FirebaseAnalytics.Event` and `FirebaseAnalytics.Param` reference documentation.

The following code logs a `SelectContent` Event when a user clicks on a specific element in your app.

```csharp
var bundle = new Bundle();
bundle.PutString (FirebaseAnalytics.Param.ItemId, id);
bundle.PutString (FirebaseAnalytics.Param.ItemName, name);
bundle.PutString (FirebaseAnalytics.Param.ContentType, "image");

firebaseAnalytics.LogEvent (FirebaseAnalytics.Event.SelectContent, bundle);
```

### Confirm Events

You can enable verbose logging to monitor logging of events by the SDK to help verify that events are being logged properly. This includes both automatically and manually logged events.

You can enable verbose logging with a series of adb commands:

```
adb shell setprop log.tag.FA VERBOSE
adb shell setprop log.tag.FA-SVC VERBOSE
adb logcat -v time -s FA FA-SVC
```

This command displays your events helping you immediately verify that events are being sent.




{FIREBASE-SAMPLES}



{FIREBASE-LEARNMORE}



{LINKS}
