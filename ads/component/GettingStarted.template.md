The Google Mobile Ads SDK is the latest generation in Google mobile advertising, featuring refined ad formats and streamlined APIs for access to mobile ad networks and advertising solutions. The SDK enables mobile app developers to maximize their monetization in native mobile apps.

{APILEVELS}

{ANDROIDMANIFEST}

The Google Mobile Ads SDK requires the *Internet* and *Access Network State* permissions to work correctly.  You can add these with the following assembly level attributes:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
```

You must also declare an activity that exists in the SDK by manually adding the following element to your *AndroidManifest.xml* file, inside of the `<application>` `</application>` tags:

```xml
<activity android:name="com.google.android.gms.ads.AdActivity"
            android:configChanges="keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize"
            android:theme="@android:style/Theme.Translucent" />
```





{SAMPLES}

The AdMobSample in this component demonstrates how to use various ad types from code and xml layouts.



{LEARNMORE}

You can learn more about the Google Play Services Ad by visiting the official [AdMob for Android](https://developers.google.com/admob/android/quick-start) documentation.



{LINKS}
