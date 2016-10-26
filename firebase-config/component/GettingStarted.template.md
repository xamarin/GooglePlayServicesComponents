Get Started with Firebase Remote Config for Android

Change the behavior and appearance of your app without publishing an app update.


{FIREBASE-CONFIGURE}


### Create a singleton instance

You can use Firebase Remote Config from the singleton instance:

```xml
var fbRemoteConfig = FirebaseRemoteConfig.Instance;
```


### Setup your default parameter values

You can set in-app default parameter values in the Remote Config object, so that your app behaves as intended before it connects to the Remote Config Server, and so that default values are available if none are set on the server.

Define a set of parameter names, and default parameter values using a Dictionary object:



```csharp
FirebaseRemoteConfig.Instance.SetDefaults (new Dictionary<string, Java.Lang.Object>
{
	{ "price_prefix", "Your price is $" },
	{ "loading_phrase", "Checking your price..." },
	{ "is_promotion_on", new Java.Lang.Boolean (false) },
	{ "price", new Java.Lang.Long (100) },
	{ "discount", new Java.Lang.Long (0) },
});
```

Or you can define your default values in an XML resource file stored in your app's `Resources/xml` folder:

```xml
<?xml version="1.0" encoding="utf-8"?>
<defaultsMap>
    <entry>
        <key>price_prefix</key>
        <value>Your price is $</value>
    </entry>
    <entry>
        <key>loading_phrase</key>
        <value>Checking your price…</value>
    </entry>
    <entry>
        <key>is_promotion_on</key>
        <value>false</value>
    </entry>
    <entry>
        <key>price</key>
        <value>100</value>
    </entry>
    <entry>
        <key>discount</key>
        <value>0</value>
    </entry>
</defaultsMap>
```

You can assign the XML default values by calling:

```csharp
FirebaseRemoteConfig.Instance.SetDefaults (Resource.Xml.remote_config_defaults);
```

### Get values

Now you can get parameter values from the Remote Config object. If you set values on the Remote Config server, fetched them, and then activated them, those values are available to your app. Otherwise, you will get the in-app parameter values configured using `SetDefaults()`. To get these values, call the method listed below that maps to the data type expected by your app, providing the parameter key as an argument:

 - `GetBoolean()`
 - `GetByteArray()`
 - `GetDouble()`
 - `GetLong()`
 - `GetString()`

For example:

```csharp
var price = FirebaseRemoteConfig.Instance.GetLong ("price");
```

### Fetch and activate values from the server

To fetch parameter values from the Remote Config Server, call the `Fetch()` method. Any values that you set on the Remote Config Server are fetched and cached in the Remote Config object.

To make fetched parameter values available to your app, call the `ActivateFetched()` method.

Because these updated parameter values affect the behavior and appearance of your app, you should activate the fetched values at a time that ensures a smooth experience for your user, such as the next time that the user opens your app.



{FIREBASE-SAMPLES}



{FIREBASE-LEARNMORE}



{LINKS}
