using System;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Auth;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Android.Widget;
using Android.App;

namespace FirebaseAuthQuickstart
{
    [Activity (Label = "Custom Auth")]
    public class CustomAuthActivity : AppCompatActivity
    {
        const string TAG = "CustomAuthActivity";

        // [START declare_auth]
        FirebaseAuth auth;
        // [END declare_auth]

        string customToken;
        TokenBroadcastReceiver tokenReceiver;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.activity_custom);

            // Button click listeners
            FindViewById (Resource.Id.button_sign_in).Click += async (sender, e) => {
                await StartSignIn ();
            };

            // Create token receiver (for demo purposes only)
            tokenReceiver = new TokenBroadcastReceiver {
                NewTokenHandler = token => {
                    Android.Util.Log.Debug (TAG, "onNewToken:" + token);
                    SetCustomToken (token);
                }
            };

            // [START initialize_auth]
            auth = FirebaseAuth.Instance;
            // [END initialize_auth]
        }


        void AuthStateChanged (object sender, FirebaseAuth.AuthStateEventArgs e)
        {
            var user = e.Auth.CurrentUser;
            if (user != null) {
                // User is signed in
                Android.Util.Log.Debug (TAG, "onAuthStateChanged:signed_in:" + user.Uid);
            } else {
                // User is signed out
                Android.Util.Log.Debug (TAG, "onAuthStateChanged:signed_out");
            }
            // [START_EXCLUDE]
            UpdateUI (user);
            // [END_EXCLUDE]
        }


        // [START on_start_add_listener]
        protected override void OnStart ()
        {
            base.OnStart ();
            auth.AuthState += AuthStateChanged;
            // [START_EXCLUDE]
            RegisterReceiver (tokenReceiver, TokenBroadcastReceiver.Filter);
            // [END_EXCLUDE]
        }
        // [END on_start_add_listener]

        // [START on_stop_remove_listener]
        protected override void OnStop ()
        {
            base.OnStop ();
            auth.AuthState -= AuthStateChanged;

            // [START_EXCLUDE]
            UnregisterReceiver (tokenReceiver);
            // [END_EXCLUDE]
        }
        // [END on_stop_remove_listener]

        async Task StartSignIn ()
        {
            try {
                await auth.SignInWithCustomTokenAsync (customToken);
            } catch {
                Toast.MakeText (this, "Authentication failed.", ToastLength.Short).Show ();
            }
        }

        void UpdateUI (FirebaseUser user)
        {
            if (user != null)
                FindViewById<TextView> (Resource.Id.text_sign_in_status).Text = "User ID: " + user.Uid;
            else
                FindViewById<TextView> (Resource.Id.text_sign_in_status).Text = "Error: sign in failed";
        }

        void SetCustomToken (string token)
        {
            customToken = token;

            string status;
            if (customToken != null) {
                status = "Token:" + customToken;
            } else {
                status = "Token: null";
            }

            // Enable/disable sign-in button and show the token
            FindViewById<Button> (Resource.Id.button_sign_in).Enabled = customToken != null;
            FindViewById<TextView> (Resource.Id.text_token_status).Text = status;
        }
    }
}

