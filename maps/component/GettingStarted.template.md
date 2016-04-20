With the Google Maps Android API, you can add maps based on Google Maps data to your application. The API automatically handles access to Google Maps servers, data downloading, map display, and response to map gestures. You can also use API calls to add markers, polygons, and overlays to a basic map, and to change the user's view of a particular map area. These objects provide additional information for map locations, and allow user interaction with the map.



{APILEVELS}



{DEVELOPERSCONSOLE}

The *Google Maps Android API v2* API must be enabled in the Developers Console to use the Maps APIs in your app.

If you are using the Places APIs, the *Google Places API for Android* API must also be enabled in the Developers Console.



{CREDENTIALS}



{CREDENTIALS-APIKEY}

Once you have created your API key, you must add it as a metadata value in your *AndroidManifest.xml* file.  You can add it by including the following assembly level attribute in your app:

```csharp
[assembly: MetaData ("com.google.android.maps.v2.API_KEY", Value="YOUR-API-KEY")]
```



{ANDROIDMANIFEST}

The SDK requires the *Internet*, *AccessNetworkState*, *AccessCourseLocation*, *AccessFineLocation*, *WriteExternalStorage* and *AccessMockLocation* permissions to work correctly.  You can have these automatically added to your *AndroidManifest.xml* file by including the following assembly level attributes:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessCourseLocation)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessMockLocation)]
[assembly: UsesPermission (Android.Manifest.Permission.WriteExternalStorage)]
```



{SAMPLES}



Attribution Requirements
========================

If you use the Google Maps Android API in your application, you must include the Google Play Services attribution text as part of a "Legal Notices" section in your application. Including legal notices as an independent menu item, or as part of an "About" menu item, is recommended.

The attribution text is available by making a call to `GoogleApiAvailability.OpenSourceSoftwareLicenseInfo`



{LEARNMORE}

You can learn more about Google Play Services Maps by visiting the official [Google Maps for Android API](https://developers.google.com/maps/documentation/android-api/) documentation.



{LINKS}
