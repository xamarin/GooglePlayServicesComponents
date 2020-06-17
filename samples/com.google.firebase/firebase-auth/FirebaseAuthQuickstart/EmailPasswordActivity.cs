using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Firebase.Auth;

namespace FirebaseAuthQuickstart
{
    [Activity (Label = "Email / Pwd Auth")]
    public class EmailPasswordActivity : BaseActivity
    {
        const string TAG = "EmailPassword";

        TextView mStatusTextView;
        TextView mDetailTextView;
        EditText mEmailField;
        EditText mPasswordField;

        // [START declare_auth]
        FirebaseAuth mAuth;
        // [END declare_auth]


        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.activity_emailpassword);

            // Views
            mStatusTextView = FindViewById<TextView> (Resource.Id.status);
            mDetailTextView = FindViewById<TextView> (Resource.Id.detail);
            mEmailField = FindViewById<EditText> (Resource.Id.field_email);
            mPasswordField = FindViewById<EditText> (Resource.Id.field_password);

            // Buttons
            FindViewById (Resource.Id.email_sign_in_button).Click += async delegate {
                await SignIn (mEmailField.Text , mPasswordField.Text);
            };
            FindViewById (Resource.Id.email_create_account_button).Click += async delegate {
                await CreateAccount (mEmailField.Text, mPasswordField.Text);
            };
            FindViewById (Resource.Id.sign_out_button).Click += delegate {
                SignOut ();
            };

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

        async Task CreateAccount (string email, string password)
        {
            Android.Util.Log.Debug (TAG, "createAccount:" + email);
            if (!ValidateForm ())
                return;
            ShowProgressDialog ();

            try {
                await mAuth.CreateUserWithEmailAndPasswordAsync (email, password);
            } catch {
                Toast.MakeText (this, "Authentication failed.", ToastLength.Short).Show ();
            }

            // [START_EXCLUDE]
            HideProgressDialog ();
            // [END_EXCLUDE]
        }

        async Task SignIn (string email, string password)
        {
            Android.Util.Log.Debug (TAG, "signIn:" + email);
            if (!ValidateForm ())
                return;

            ShowProgressDialog ();

            try {
                await mAuth.SignInWithEmailAndPasswordAsync (email, password);
            } catch {
                Toast.MakeText (this, "Authentication failed.", ToastLength.Short).Show ();
            }


            // [START_EXCLUDE]
            HideProgressDialog ();
            // [END_EXCLUDE]
        }
         
        void SignOut ()
        {
            mAuth.SignOut ();
            UpdateUI (null);
        }

        bool ValidateForm ()
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
            if (user != null) {
                mStatusTextView.Text = GetString (Resource.String.emailpassword_status_fmt, user.Email);
                mDetailTextView.Text = GetString (Resource.String.firebase_status_fmt, user.Uid);

                FindViewById (Resource.Id.email_password_buttons).Visibility = ViewStates.Gone;
                FindViewById (Resource.Id.email_password_fields).Visibility = ViewStates.Gone;
                FindViewById (Resource.Id.sign_out_button).Visibility = ViewStates.Visible;
            } else {
                mStatusTextView.SetText (Resource.String.signed_out);
                mDetailTextView.Text = null;

                FindViewById (Resource.Id.email_password_buttons).Visibility = ViewStates.Visible;
                FindViewById (Resource.Id.email_password_fields).Visibility = ViewStates.Visible;
                FindViewById (Resource.Id.sign_out_button).Visibility = ViewStates.Gone;
            }
        }
    }
}

