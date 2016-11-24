
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Extensions;
using Android.Gms.Tasks;

using Firebase.Auth;
using Android.Text;

namespace FirebaseAuthQuickstart
{
    [Activity (Label = "Anonymous Auth")]
    public class AnonymousAuthActivity : BaseActivity
    {
        const string TAG = "AnonymousAuth";

        // [START declare_auth]
        FirebaseAuth mAuth;
        // [END declare_auth]

        EditText mEmailField;
        EditText mPasswordField;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.activity_anonymous_auth);

            // [START initialize_auth]
            mAuth = FirebaseAuth.Instance;
            // [END initialize_auth]

            // Fields
            mEmailField = FindViewById<EditText> (Resource.Id.field_email);
            mPasswordField = FindViewById<EditText> (Resource.Id.field_password);

            // Click listeners
            FindViewById<Button> (Resource.Id.button_anonymous_sign_in).Click += delegate {
                SignInAnonymously ();
            };
            FindViewById<Button> (Resource.Id.button_anonymous_sign_out).Click += delegate {
                SignOut ();
            };
            FindViewById<Button> (Resource.Id.button_link_account).Click += delegate {
                LinkAccount ();
            };
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

        async System.Threading.Tasks.Task SignInAnonymously ()
        {
            ShowProgressDialog ();

            // [START signin_anonymously]

            try {
                var result = await mAuth.SignInAnonymouslyAsync ();
            } catch (Exception ex) {
                Toast.MakeText (this, "Authentication failed.", ToastLength.Short).Show ();
            }

            // [START_EXCLUDE]
            HideProgressDialog ();
            // [END_EXCLUDE]
            // [END signin_anonymously]
        }


        void SignOut ()
        {
            mAuth.SignOut ();
            UpdateUI (null);
        }

        async System.Threading.Tasks.Task LinkAccount ()
        {
            // Make sure form is valid
            if (!ValidateLinkForm ()) {
                return;
            }

            // Get email and password from form
            var email = mEmailField.Text;
            var password = mPasswordField.Text;

            // Create EmailAuthCredential with email and password
            AuthCredential credential = EmailAuthProvider.GetCredential (email, password);

            // Link the anonymous user to the email credential
            ShowProgressDialog ();


            // [START link_credential]
            try {
                var result = await mAuth.CurrentUser.LinkWithCredentialAsync (credential);

            } catch (Exception ex) {
                Toast.MakeText (this, "Authentication failed.", ToastLength.Short).Show ();
            }

            // [START_EXCLUDE]
            HideProgressDialog ();
            // [END_EXCLUDE]
            // [END link_credential]
        }

        bool ValidateLinkForm ()
        {
            var valid = true;

            var email = mEmailField.Text;
            if (TextUtils.IsEmpty (email)) {
                mEmailField.Error = "Required.";
                valid = false;
            } else {
                mEmailField.Error = null;
            }

            var password = mPasswordField.Text;
            if (TextUtils.IsEmpty (password)) {
                mPasswordField.Error = "Required.";
                valid = false;
            } else {
                mPasswordField.Error = null;
            }

            return valid;
        }

        void UpdateUI (FirebaseUser user)
        {
            HideProgressDialog ();

            var idView = FindViewById<TextView> (Resource.Id.anonymous_status_id);
            var emailView = FindViewById<TextView> (Resource.Id.anonymous_status_email);
            var isSignedIn = (user != null);

            // Status text
            if (isSignedIn) {
                idView.Text = GetString (Resource.String.id_fmt, user.Uid);
                emailView.Text = GetString (Resource.String.email_fmt, user.Email);
            } else {
                idView.SetText (Resource.String.signed_out);
                emailView.Text = null;
            }

            // Button visibility
            FindViewById (Resource.Id.button_anonymous_sign_in).Enabled = !isSignedIn;
            FindViewById (Resource.Id.button_anonymous_sign_out).Enabled = isSignedIn;
            FindViewById (Resource.Id.button_link_account).Enabled = isSignedIn;
        }
    }
}

