Nearby exposes simple publish and subscribe methods that rely on proximity. Your app publishes a payload that can be received by nearby subscribers. On top of this foundation, you can build a variety of user experiences to share messages and create real-time connections between nearby devices.



{APILEVELS}



{DEVELOPERSCONSOLE}

To use Nearby you must enable the *Nearby Messages API* in the Developers Console.



{CREDENTIALS}



{CREDENTIALS-APIKEY}

Once you have created your API key, you must add it as a metadata value in your *AndroidManifest.xml* file either manually or by including an assembly level attribute in your app:

```csharp
[assembly: MetaData ("com.google.android.nearby.messages.API_KEY", Value="YOUR-API-KEY")]
```



{SAMPLES}



{LEARNMORE}

You can learn more about Google Play Services Nearby by visiting the official [Nearby API](https://developers.google.com/nearby/) documentation.



{LINKS}
