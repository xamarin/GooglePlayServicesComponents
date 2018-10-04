### API Key

If the Google Play Services API you are using requires an API Key:

  1. *Add credentials* button and then *API key*
  2. Choose *Android key*
  3. Click *Add package name and fingerprint*
  4. Enter your android app's package name as found in your *AndroidManifest.xml* file
  5. [Find your SHA-1 fingerprints][2]
  6. Enter your SHA-1 fingerprint of your app's debug keystore
  7. Repeat steps 4-6 with the package name and SHA-1 of the keystore file you will be signing your app's Release builds with
  8. Click *Create*
  9. Note the *API key* value you generated

Once you have your API key value, you will need to add this to your *AndroidManifest.xml* as a metadata value either by directly editing the manifest file, or using an assembly level attribute which will generate the value in the manifest file for you.  The metadata key will be different for each Google Play Services API.  For example, if you are adding it for Maps, you could add this assembly level attribute to your project:

```csharp
[assembly: MetaData ("com.google.android.maps.v2.API_KEY", Value="YOUR-API-KEY")]
```
