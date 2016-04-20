The Google Analytics SDK for Android makes it easy for developers to collect user engagement data form their apps. 

{APILEVELS}

{ANDROIDMANIFEST}

The Google Analytics SDK requires the *Internet* and *Access Network State* permissions to work correctly.  You can include these by adding the following assembly level attributes to your app:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
```

If you want to add Campaign tracking you must manually add the following element to your *AndroidManifest.xml* file, inside of the `<application>` `</application>` tags:

```xml
<service android:name="com.google.analytics.tracking.android.CampaignTrackingService" />
<receiver android:name="com.google.analytics.tracking.android.CampaignTrackingReceiver"
          android:exported="true" >
  <intent-filter>
    <action android:name="com.android.vending.INSTALL_REFERRER" />
  </intent-filter>
</receiver>
```



{SAMPLES}


{LEARNMORE}

You can learn more about Google Play Services Analytics by visiting the official [Analytics SDK for Android](https://developers.google.com/analytics/devguides/collection/android/v4/) documentation.

{LINKS}
