Android Pay enables simple and secure purchases of physical goods and services in Android apps, and eliminates the need to manually enter payment and shipping information. Integrate Android Pay to reach millions of signed-in Android users and drive higher conversions.



{APILEVELS}



{DEVELOPERSCONSOLE}



{CREDENTIALS}



{CREDENTIALS-OAUTH}



{ANDROIDMANIFEST}

The SDK requires the *Internet* permission(s) to work correctly.  You can have these automatically added to your *AndroidManifest.xml* file by including the following assembly level attributes:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.GetAccounts)]
[assembly: UsesPermission (Android.Manifest.Permission.UseCredentials)]
```

You also need to declare metadata in your *AndroidManifest.xml* file to enable the wallet API.  You can do this manually, or using an assembly level attribute like this:

```csharp
[assembly: MetaData ("com.google.android.gms.wallet.api.enabled", Value = "true" )]
```

You must also manually add the following element to your *AndroidManifest.xml*  inside of the `<application>` `</application>` tag:

```xml
<receiver 
    android:name="com.google.android.gms.wallet.EnableWalletOptimizationReceiver"
    android:exported="false">
    <intent-filter>
        <action android:name="com.google.android.gms.wallet.ENABLE_WALLET_OPTIMIZATION" />
    </intent-filter>
</receiver>
```




{SAMPLES}



{LEARNMORE}

You can learn more about Google Play Services Wallet by visiting the official [Android Pay SDK for Android](https://developers.google.com/android-pay/) documentation.



{LINKS}
