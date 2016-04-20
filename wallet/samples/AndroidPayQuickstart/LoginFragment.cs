using System;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Support.V4.App;
using Android.OS;
using Android.App;
using Android.Gms.Plus;
using Android.Widget;

namespace AndroidPayQuickstart
{
    public class LoginFragment : Android.Support.V4.App.Fragment, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public const int REQUEST_CODE_RESOLVE_ERR = 1005;
        const string KEY_SIGNIN_BUTTON_CLICKED = "KEY_SIGNIN_BUTTON_CLICKED";
        const string WALLET_SCOPE = "https://www.googleapis.com/auth/payments.make_payments";

        ProgressDialog mProgressDialog;
        bool mSignInButtonClicked = false;
        bool mIsResolving = false;
        GoogleApiClient mGoogleApiClient;
        ConnectionResult mConnectionResult;
        int mLoginAction;

        public static LoginFragment NewInstance (int loginAction)
        {
            var fragment = new LoginFragment ();
            var bundle = new Bundle ();
            bundle.PutInt (LoginActivity.EXTRA_ACTION, loginAction);
            fragment.Arguments = bundle;
            return fragment;
        }

        public override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            if (savedInstanceState != null)
                mSignInButtonClicked = savedInstanceState.GetBoolean (KEY_SIGNIN_BUTTON_CLICKED);
            
            var args = Arguments;
            if (args != null)
                mLoginAction = args.GetInt (LoginActivity.EXTRA_ACTION);
            
            var options = new PlusClass.PlusOptions.Builder ().Build ();
            mGoogleApiClient = new GoogleApiClient.Builder (Activity)
                .AddApi (PlusClass.API, options)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener(this)
                .AddScope (PlusClass.ScopePlusProfile)
                .AddScope (new Scope (WALLET_SCOPE))
                .Build ();
        }

        public override void OnSaveInstanceState (Bundle outState)
        {
            base.OnSaveInstanceState (outState);
            outState.PutBoolean (KEY_SIGNIN_BUTTON_CLICKED, mSignInButtonClicked);
        }

        public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate (Resource.Layout.fragment_login, container, false);
            mProgressDialog = initializeProgressDialog();

            var signInButton = view.FindViewById<SignInButton> (Resource.Id.sign_in_button);
            signInButton.SetSize (SignInButton.SizeWide);
            signInButton.Click += delegate {
                showProgressDialog ();
                mSignInButtonClicked = true;
                mGoogleApiClient.Connect ();
            };

            view.FindViewById (Resource.Id.button_login_bikestore).Click += delegate {
                Toast.MakeText (Activity, Resource.String.login_bikestore_message, ToastLength.Long).Show ();
            };
            return view;
        }

        public override void OnStart ()
        {
            base.OnStart ();
            mGoogleApiClient.Connect ();
        }

        public override void OnStop ()
        {
            base.OnStop ();
        
            if (mGoogleApiClient.IsConnecting || mGoogleApiClient.IsConnected)
                mGoogleApiClient.Disconnect ();
        }

        public override void OnPause ()
        {
            base.OnPause ();
            dismissProgressDialog ();
        }

        public override void OnActivityResult (int requestCode, int resultCode, Android.Content.Intent data)
        {
            switch (requestCode) {
            case REQUEST_CODE_RESOLVE_ERR:
                if (resultCode != (int)Result.Ok)
                    mSignInButtonClicked = false;
                
                mIsResolving = false;
                showProgressDialog ();
                mGoogleApiClient.Connect ();
                break;
            default:
                base.OnActivityResult (requestCode, resultCode, data);
                dismissProgressDialog ();
                break;
            }
        }

        public void OnConnected (Bundle connectionHint) 
        {
            dismissProgressDialog ();
            if (mLoginAction == LoginActivity.Action.LOGOUT) {
                logOut ();
            } else {
                mSignInButtonClicked = false;
                logIn ();
            }
        }

        public void OnConnectionFailed (ConnectionResult result) 
        {
            // Save the intent so that we can start an activity when the user clicks
            // the sign-in button.
            mConnectionResult = result;
            if (mSignInButtonClicked)
                resolveConnection ();
        }

        public void OnConnectionSuspended (int cause)
        {
            // nothing specifically required here, onConnected will be called when connection resumes
        }

        void resolveConnection ()
        {
            if (mIsResolving)
                return;

            try {
                if (mConnectionResult != null && mConnectionResult.HasResolution) {
                    mConnectionResult.StartResolutionForResult (Activity, REQUEST_CODE_RESOLVE_ERR);
                    mIsResolving = true;
                } else {
                    dismissProgressDialog ();
                }
            } catch (Android.Content.IntentSender.SendIntentException) {
                mConnectionResult = null;
                mIsResolving = false;

                dismissProgressDialog ();
                mGoogleApiClient.Connect ();
            }
        }

        void logIn ()
        {
            if (mGoogleApiClient.IsConnected) {
                var user = PlusClass.PeopleApi.GetCurrentPerson (mGoogleApiClient);
                var accountName = PlusClass.AccountApi.GetAccountName (mGoogleApiClient);
                if (user == null) {
                    Toast.MakeText (Activity, Resource.String.network_error, ToastLength.Long).Show ();
                } else {
                    Toast.MakeText (Activity, GetString (Resource.String.welcome_user, user.DisplayName), ToastLength.Long).Show ();

                    ((BikestoreApplication) Activity.Application).Login (accountName);
                    Activity.SetResult (Android.App.Result.Ok);
                    Activity.Finish ();
                }
            }
        }

        void logOut ()
        {
            if (mGoogleApiClient.IsConnected) {
                PlusClass.AccountApi.ClearDefaultAccount (mGoogleApiClient);
                ((BikestoreApplication) Activity.Application).Logout ();
                mGoogleApiClient.Disconnect ();
                Toast.MakeText (Activity, GetString (Resource.String.logged_out), ToastLength.Long).Show();
                Activity.SetResult (Result.Ok);
                Activity.Finish ();
            }
        }

        ProgressDialog initializeProgressDialog ()
        {
            var dialog = new ProgressDialog (Activity);
            dialog.Indeterminate = true;
            dialog.SetMessage (GetString (Resource.String.loading));
            return dialog;
        }

        void showProgressDialog ()
        {
            if (mProgressDialog != null && !mProgressDialog.IsShowing)
                mProgressDialog.Show ();
        }

        void dismissProgressDialog ()
        {
            if (mProgressDialog != null && mProgressDialog.IsShowing)
                mProgressDialog.Dismiss();
        }
    }
}

