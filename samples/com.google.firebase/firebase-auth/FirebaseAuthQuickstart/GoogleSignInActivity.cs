using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Extensions;
using Android.Gms.Auth.Api;
using Android.OS;
using Android.Widget;
using Firebase.Auth;
using Android.Views;
using Android.App;

namespace FirebaseAuthQuickstart
{
    [Activity (Label = "Google Signin")]
    public class GoogleSignInActivity : BaseActivity, GoogleApiClient.IOnConnectionFailedListener
    {
        const string TAG = "GoogleActivity";
        const int RC_SIGN_IN = 9001;

        // [START declare_auth]
        FirebaseAuth mAuth;
        // [END declare_auth]

        GoogleApiClient mGoogleApiClient;
        TextView mStatusTextView;
        TextView mDetailTextView;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.activity_google);

            // Views
            mStatusTextView = FindViewById<TextView> (Resource.Id.status);
            mDetailTextView = FindViewById<TextView> (Resource.Id.detail);

            // Button listeners
            FindViewById (Resource.Id.sign_in_button).Click += delegate {
                SignIn ();
            };
            FindViewById (Resource.Id.sign_out_button).Click += async delegate {
                await SignOut ();
            };
            FindViewById (Resource.Id.disconnect_button).Click += async delegate {
                await RevokeAccess ();
            };

            // [START config_signin]
            // Configure Google Sign In
            var gso = new GoogleSignInOptions.Builder (GoogleSignInOptions.DefaultSignIn)
                    .RequestIdToken (GetString (Resource.String.default_web_client_id))
                    .RequestEmail ()
                    .Build ();
            // [END config_signin]

            mGoogleApiClient = new GoogleApiClient.Builder (this)
                    .EnableAutoManage (this /* FragmentActivity */, this /* OnConnectionFailedListener */)
                    .AddApi (Android.Gms.Auth.Api.Auth.GOOGLE_SIGN_IN_API, gso)
                    .Build ();

            // [START initialize_auth]
            mAuth = FirebaseAuth.Instance;
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
            mAuth.AuthState += AuthStateChanged;
        }
        // [END on_start_add_listener]

        // [START on_stop_remove_listener]
        protected override void OnStop ()
        {
            base.OnStop ();
            mAuth.AuthState -= AuthStateChanged;
        }
        // [END on_stop_remove_listener]

        protected override async void OnActivityResult (int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult (requestCode, resultCode, data);
       
            // Result returned from launching the Intent from GoogleSignInApi.getSignInIntent(...);
            if (requestCode == RC_SIGN_IN) {
                var result = Android.Gms.Auth.Api.Auth.GoogleSignInApi.GetSignInResultFromIntent (data);
                if (result.IsSuccess) {
                    // Google Sign In was successful, authenticate with Firebase
                    await FirebaseAuthWithGoogle (result.SignInAccount);
                } else {
                    // Google Sign In failed, update UI appropriately
                    // [START_EXCLUDE]
                    UpdateUI (null);
                    // [END_EXCLUDE]
                }
            }
        }
        // [END onactivityresult]

        // [START auth_with_google]
        async Task FirebaseAuthWithGoogle (GoogleSignInAccount acct)
        {
            Android.Util.Log.Debug (TAG, "firebaseAuthWithGoogle:" + acct.Id);
            // [START_EXCLUDE silent]
            ShowProgressDialog ();
            // [END_EXCLUDE]

            AuthCredential credential = GoogleAuthProvider.GetCredential (acct.IdToken, null);

            try {
                await mAuth.SignInWithCredentialAsync (credential);
            } catch {
                Toast.MakeText (this, "Authentication failed.", ToastLength.Short).Show ();
            }
            // [START_EXCLUDE]
            HideProgressDialog ();
            // [END_EXCLUDE]
        }

        // [START signin]
        void SignIn ()
        {
            Intent signInIntent = Android.Gms.Auth.Api.Auth.GoogleSignInApi.GetSignInIntent (mGoogleApiClient);
            StartActivityForResult (signInIntent, RC_SIGN_IN);
        }
        // [END signin]

        async Task SignOut ()
        {
            // Firebase sign out
            mAuth.SignOut ();

            await Auth.GoogleSignInApi.SignOut (mGoogleApiClient);

            UpdateUI (null);
        }

        async Task RevokeAccess ()
        {
            // Firebase sign out
            mAuth.SignOut ();

            // Google revoke access
            await Auth.GoogleSignInApi.RevokeAccess (mGoogleApiClient);

            UpdateUI (null);
        }

        void UpdateUI (FirebaseUser user)
        {
            HideProgressDialog ();
            if (user != null) {
                mStatusTextView.Text = GetString (Resource.String.google_status_fmt, user.Email);
                mDetailTextView.Text = GetString (Resource.String.firebase_status_fmt, user.Uid);

                FindViewById (Resource.Id.sign_in_button).Visibility = ViewStates.Gone;
                FindViewById (Resource.Id.sign_out_and_disconnect).Visibility = ViewStates.Visible;
            } else {
                mStatusTextView.SetText (Resource.String.signed_out);
                mDetailTextView.Text = null;

                FindViewById (Resource.Id.sign_in_button).Visibility = ViewStates.Visible;
                FindViewById (Resource.Id.sign_out_and_disconnect).Visibility = ViewStates.Gone;
            }
        }

        public void OnConnectionFailed (ConnectionResult connectionResult)
        {
            // An unresolvable error has occurred and Google APIs (including Sign-In) will not
            // be available.
            Android.Util.Log.Debug (TAG, "onConnectionFailed:" + connectionResult);
            Toast.MakeText (this, "Google Play Services error.", ToastLength.Short).Show ();
        }
    }
}

