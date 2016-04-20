The Face API finds human faces in photos, videos, or live streams. It also finds and tracks positions of facial landmarks such as the eyes, nose, and mouth.

The API also provides information about the state of facial features -- are the subject's eyes open? Are they smiling? With these technologies, you can edit photos and video, enhance video feeds with effects and decorations, create hands-free controls for games and apps, or react when a person winks or smiles.

The Barcode Scanner API detects barcodes in real time in any orientation. You can also detect and parse several barcodes in different formats at the same time.



{APILEVELS}



{ANDROIDMANIFEST}

If you want to use the Live camera feed with the Vision APIs, you will need to declare the *Camera* permission to work correctly.  You can have this automatically added to your *AndroidManifest.xml* file by including the following assembly level attribute:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.Camera)]
```



{SAMPLES}



{LEARNMORE}

You can learn more about Google Play Services Vision by visiting the official [Mobile Vision API for Android](https://developers.google.com/vision/) documentation.



{LINKS}
