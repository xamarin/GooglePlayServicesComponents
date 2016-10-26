Get Started with Firebase Ads for Android

AdMob by Google is an easy way to monetize mobile apps with targeted, in-app advertising.


{FIREBASE-CONFIGURE}


### Add your Ad Unit ID's to your app's resources

An ad unit ID is a unique identifier given to the places in your app where ads are displayed. Create an ad unit for each activity your app will perform. If you have an app with two activities, for example, each displaying a banner, you need two ad units, each with its own ID. AdMob ad unit IDs have the form `ca-app-pub-XXXXXXXXXXXXXXXX/NNNNNNNNNN`.

For your new app to display an ad, it needs to include an ad unit ID. Open your app's string resource file, which is found at `YourAppSource/Resources/values/strings.xml`.

```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <string name="banner_ad_unit_id">ca-app-pub-3940256099942544/6300978111</string>
</resources>
```

### Place an AdView in your Layout

In your Layout xml file, add an AdView:

```xml
<com.google.android.gms.ads.AdView
        android:id="@+id/adView"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:layout_alignParentBottom="true"
        android:layout_centerHorizontal="true"
        ads:adSize="BANNER"
        ads:adUnitId="@string/banner_ad_unit_id" />
```

You will need to add the `ads` namespace as well:

```xml
<RelativeLayout
	xmlns:ads="http://schemas.android.com/apk/res-auto"
	/ >
```


### Initialize the Google Mobile Ads SDK

To initialize the Google Mobile Ads SDK at app launch, call MobileAds.Initialize() in the onCreate() method of the MainActivity class.

Open your MainActivity.java file. It's in the BannerExample/app/src/main/java/ folder, though the exact subdirectory path varies based on the domain you used when creating your project above. Once it's open in the editor, look for the onCreate() method in the MainActivity class:

   


{FIREBASE-SAMPLES}



{FIREBASE-LEARNMORE}



{LINKS}
