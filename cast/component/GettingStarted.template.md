The Google Cast SDK lets you extend your Android app to control a TV or sound system. It supports many media formats, protocols and codecs to ease integration.  Continuous playback is enabled by autoplay and queuing APIs and Game Manager APIs make Cast enabled gaming a breeze. 



{APILEVELS}

You must also ensure you have installed *Android 6.0 (API 23)*'s *SDK Platform* in the *Android SDK Manager* or you will have compiler errors.  You do not need to *use* this API level in your app, however *AAPT* requires it to be installed anyway.




{ANDROIDMANIFEST}

Since the Cast SDK is implemented using AppCompat V7, your Activities using this API must use a theme that derives from `Theme.AppCompat`.  You can update the theme per activity, or you can set it globally in your *AndroidManifest.xml* file by adding the `android:theme="@style/Theme.AppCompat"` attribute to your `<application>` element like this:

```xml
<application android:theme="@style/Theme.AppCompat">
```



{SAMPLES}



{LEARNMORE}

You can learn more about Google Play Services Cast by visiting the official [Cast SDK for Android](https://developers.google.com/cast/docs/android_sender) documentation.



{LINKS}
