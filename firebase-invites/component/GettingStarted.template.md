Get Started with Firebase Invites for Android

Firebase Invites are an out-of-the-box solution for app referrals and sharing via email or SMS.


{FIREBASE-CONFIGURE}


### Send Invitations

Start by building an `Intent` using the `AppInviteInvitation.IntentBuilder` class:

```csharp
var intent = new AppInviteInvitation.IntentBuilder ("Title")
        .SetMessage ("Your Message")
        .SetDeepLink (Uri.Parse ("yourapp://deeplink"))
        .SetCallToActionText ("Call to Action")
        .Build();

StartActivityForResult (intent, REQUEST_INVITE);
```

### Customize the invitation

When you build the invitation Intent, you must specify the title of the invitation dialog and the invitation message to send. You can also customize the image and deep link URL that get sent in the invitation, as in the example above, and you can specify HTML to send rich email invitations, which is recommended. 


If you have an iOS version of your app and you want to send an invitation that can be opened on iOS in addition to Android, pass the OAuth 2.0 client ID of your iOS app to `SetOtherPlatformsTargetApplication` when you build the app invitation intent. You can find your iOS app's client ID in the `GoogleService-Info.plist` file you downloaded from the Firebase console. For example:

```csharp
var intent = new AppInviteInvitation.IntentBuilder ("Title)
    ...
    .SetOtherPlatformsTargetApplication(
       AppInviteInvitation.IntentBuilder.PlatformMode.PROJECT_PLATFORM_IOS,
        "IOS_APP_CLIENT_ID")
    ...
    .Build();
```



Launching the `AppInviteInvitation` intent opens the contact chooser where the user selects the contacts to invite. Invites are sent via email or SMS. After the user chooses contacts and sends the invite, your app receives a callback to `OnActivityResult`:

```csharp
protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
{
    base.OnActivityResult (requestCode, resultCode, data);

    if (requestCode == REQUEST_INVITE) {
        if (resultCode == Result.Ok) {
            // Get the invitation IDs of all sent messages
            var ids = AppInviteInvitation.GetInvitationIds (resultCode, data);
            foreach (var id in ids) {
                Android.Util.Log.Debug (TAG, "onActivityResult: sent invitation " + id);
            }
        } else {
            // Sending failed or it was canceled, show failure message to the user
            // ...
        }
    }
}
```


### Receive invitations

When a user receives an invitation, if the user has not yet installed the app, they can choose to install the app from the Google Play Store. Then, after the app is installed, or if the app was already installed, the app starts and receives the URL to its content, if you sent one. To receive the URL to your app's content, call the `GetInvitation` method:

```csharp
protected override void onCreate(Bundle savedInstanceState)
{
    // ...

    // Create an auto-managed GoogleApiClient with access to App Invites.
    googleApiClient = new GoogleApiClient.Builder (this)
            .AddApi (AppInvite.API)
            .EnableAutoManage (this, this)
            .Build();

    // Check for App Invite invitations and launch deep-link activity if possible.
    // Requires that an Activity is registered in AndroidManifest.xml to handle
    // deep-link URLs.
    var autoLaunchDeepLink = true;

    var result = await AppInvite.AppInviteApi.GetInvitationAsync (googleApiClient, this, autoLaunchDeepLink);

    Android.Util.Log.Debug (TAG, "getInvitation:onResult:" + result.Status);
    
    if (result.Status.IsSuccess) {
        // Extract information from the intent
        var intent = result.GetInvitationIntent ();
        var deepLink = AppInviteReferral.GetDeepLink (intent);
        var invitationId = AppInviteReferral.GetInvitationId (intent);

        // Because autoLaunchDeepLink = true we don't have to do anything
        // here, but we could set that to false and manually choose
        // an Activity to launch to handle the deep link here.
        // ...
    }
}
```

{FIREBASE-SAMPLES}



{FIREBASE-LEARNMORE}



{LINKS}
