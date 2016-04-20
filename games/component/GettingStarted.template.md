The Play Games SDK provides cross-platform Google Play games services that lets you easily integrate popular gaming features such as achievements, leaderboards, Saved Games, and real-time multiplayer in your tablet and mobile games.



{APILEVELS}



Google Play Developer Console Setup
===================================

Follow Google's instructions for [Setting Up Google Play Games Services](https://developers.google.com/games/services/console/enabling).

You will need to know your app's package name (from the *AndroidManifest.xml* file) as well as the SHA1 fingerprint for both your debug keystore and the keystore file you will use to sign your release builds.  If you are not sure how to find your SHA1 fingerprints, visit the documentation on [Finding your SHA-1 Fingerprints][2].




{ANDROIDMANIFEST}

For Play Games you will need to add the *App ID* you generated previously as metadata to your *AndroidManifest.xml* file.  You can add this by including the following assembly level attribute in your app:

```csharp
[assembly: MetaData ("com.google.android.gms.games.APP_ID", Value="YOUR-APP-ID")]
```



{SAMPLES}



{LEARNMORE}

You can learn more about Google Play Services Games by visiting the official [Play Games Services SDK for Android](https://developers.google.com/games/services/) documentation.



{LINKS}
