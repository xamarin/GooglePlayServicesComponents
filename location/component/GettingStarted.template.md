One of the unique features of mobile applications is location awareness. Mobile users take their devices with them everywhere, and adding location awareness to your app offers users a more contextual experience. The location APIs available in Google Play services facilitate adding location awareness to your app with automated location tracking, geofencing, and activity recognition.



{APILEVELS}



{ANDROIDMANIFEST}

The SDK requires the *CourseLocation* or *FineLocation* permission(s) to work correctly.  If you specify *CourseLocation* location accuracy will be lower than if you choose *FineLocation*.  Pick the appropriate one for your application.  You can have these automatically added to your *AndroidManifest.xml* file by including the following assembly level attributes:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.FineLocation)]
// Or
[assembly: UsesPermission (Android.Manifest.Permission.CourseLocation)]
```



{SAMPLES}



{LEARNMORE}

You can learn more about Google Play Services Location by visiting the official [Location](https://developer.android.com/training/location/index.html) documentation.



{LINKS}
