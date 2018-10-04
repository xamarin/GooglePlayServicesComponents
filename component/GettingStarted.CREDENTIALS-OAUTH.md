### OAuth 2.0 Client ID

This SDK/API requires an OAuth 2.0 Client ID to be created:

  1. *Add credentials* button and then *OAuth 2.0 client ID*
  2. Choose *Android* for the Application type.
  3. [Find your SHA-1 fingerprints][2]
  4. Enter the SHA-1 fingerprint of your app's debug keystore
  5. Enter your android app's package name as found in your *AndroidManifest.xml* file
  6. Check *Enable Deep Linking*
  7. Click *Create*
  8. Repeat this process with the package name and SHA-1 of the keystore file you will be signing your app's Release builds with

NOTE: Once you have created the OAuth 2.0 Client ID, you typically do not need to do anything with the generated *Client ID* value since your keystore SHA-1 fingerprints will identify the client id for you.
