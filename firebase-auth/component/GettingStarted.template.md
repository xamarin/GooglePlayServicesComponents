Get Started with Firebase Authentication for Android


{FIREBASE-CONFIGURE}



## Manage Users in Firebase


### Create a User

You create a new user in your Firebase project by calling the `CreateUserWithEmailAndPassword` method or by signing in a user for the first time using a federated identity provider, such as Google Sign-In or Facebook Login.

You can also create new password-authenticated users from the Authentication section of the Firebase console, on the Users page.


### Get the currently signed-in user

The recommended way to get the current user is by subscribing to the `AuthState` event:

```csharp
FirebaseAuth.Instance.AuthState += (sender, e) => {
    var user = e?.Auth?.CurrentUser;

    if (user != null) {
        // User is signed in
    } else {
        // User is signed out
    }
};
```

By subscribing to the event, you ensure the Auth object isn't in an intermediate state - such as initialization - when you get the current user.

You can also get the currently signed-in user by accessing the  `FirebaseAuth.Instance.CurrentUser` property.  If a user isn't signed in, the result will be `null`.

```csharp
var user = FirebaseAuth.Instance.CurrentUser;
var signedIn = user != null;
```


### Get a user's profile

To get a user's profile information, use the accessor methods of an instance of FirebaseUser. For example:

```csharp
var user = FirebaseAuth.Instance.CurrentUser;

if (user != null) {
    // Name, email address, and profile photo Url
    var name = user.DisplayName;
    var email = user.Email;
    var photoUrl = user.PhotoUrl;

    // The user's ID, unique to the Firebase project. Do NOT use this value to
    // authenticate with your backend server, if you have one. Use
    // FirebaseUser.Token instead.
    var uid = user.Uid;
}
```


### Get a user's provider-specific profile information

To get the profile information retrieved from the sign-in providers linked to a user, use the `ProviderData` property. For example:

```csharp
var user = FirebaseAuth.Instance.CurrentUser;
if (user != null) {
    foreach (IUserInfo profile in user.ProviderData) {
        // Id of the provider (ex: google.com)
        var providerId = profile.ProviderId;

        // UID specific to the provider
        var uid = profile.Uid;

        // Name, email address, and profile photo Url
        var name = profile.DisplayName;
        var email = profile.Email;
        var photoUrl = profile.PhotoUrl;
    };
}
```



### Update a user's profile

You can update a user's basic profile information—the user's display name and profile photo URL—with the `UpdateProfile` method. For example:

```csharp
var user = FirebaseAuth.Instance.CurrentUser;

var profileUpdates = new UserProfileChangeRequest.Builder()
        .SetDisplayName ("Jane Q. User")
        .SetPhotoUri (Uri.Parse ("https://example.com/user/profile.jpg"))
        .Build();

try {
    await user.UpdateProfileAsync (profileUpdates);
} catch (Exception ex) {
    // Failed to update user profile
}
```



### Set a user's email address

You can set a user's email address with the `UpdateEmail` method. For example:

```csharp
var user = FirebaseAuth.Instance.gCurrentUser;

try {
    await user.UpdateEmailAsync ("user@example.com");
} catch (Exception ex) {
    // Failed to update user email
}
```





## Password Authentication

You will need to enable Email/Password sign-in in your Firebase console if you haven't already:

 1. In the Firebase console, open the Auth section.
 2. On the Sign in method tab, enable the ***Email/password*** sign-in method and click ***Save***.


### Create a password-based account

To create a new user account with a password, complete the following steps in your app's sign-in activity:

Set up an AuthState event handler that responds to changes in the user's sign-in state:

```csharp
public void AuthStateChanged (object sender, FirebaseAuth.AuthStateEventArgs e)
{
	var user = e.Auth.CurrentUser;
	
    if (user != null) {
        // User is signed in
    } else {
        // User is signed out
    }
}

// ...

public override void OnStart ()
{
    base.OnStart ();

    FirebaseAuth.Instance.AuthState += AuthStateChanged;
}

public override void OnStop ()
{
    base.OnStop ();
    
    FirebaseAuth.Instance.AuthState -= AuthStateChanged;
}
```

When a new user signs up using your app's sign-up form, complete any new account validation steps that your app requires, such as verifying that the new account's password was correctly typed and meets your complexity requirements.

Create a new account by passing the new user's email address and password to `CreateUserWithEmailAndPasswordAsync`:

```csharp
try {
    await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync (email, password);
} catch (Exception ex) {
    // Sign-up failed, display a message to the user
    // If sign in succeeds, the AuthState event handler will
    //  be notified and logic to handle the signed in user can happen there
    Toast.MakeText (this, "Sign In failed", ToastLength.Short).Show ();
}
```

If the new account was created, the user is also signed in, and the AuthState event is fired. In the event handler, you can use the `CurrentUser` property to get the user's account data.




### Sign in a user with an email address and password

The steps for signing in a user with a password are similar to the steps for creating a new account. In your app's sign-in activity, do the following:


Set up the AuthState event handler just like in the previous example, that responds to changes in the user's sign-in state:

When a user signs in to your app, pass the user's email address and password to `SignInWithEmailAndPasswordAsync`:

```csharp
try {
    await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync (email, password);
} catch (Exception ex) {
    // Sign-in failed, display a message to the user
    // If sign in succeeds, the AuthState event handler will
    //  be notified and logic to handle the signed in user can happen there
    Toast.MakeText (this, "Sign In failed", ToastLength.Short).Show ();
}
```

If sign-in succeeded, the AuthState event will fire. In the  event handler, you can use the `CurrentUser` property to get the user's account data.


{FIREBASE-SAMPLES}



{FIREBASE-LEARNMORE}



{LINKS}
