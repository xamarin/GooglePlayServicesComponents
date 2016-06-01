
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
//using CheckResult = Android.Gms.Common.Apis.GoogleApiClient.ServerAuthCodeCallbacksCheckResult;

namespace PlusSample
{
    [Activity (Label = "Sign In")]			
    public class SignInActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener //, GoogleApiClient.IServerAuthCodeCallbacks
    {
        const string TAG = "SignInActivity";

        const int DIALOG_GET_GOOGLE_PLAY_SERVICES = 1;

        const int REQUEST_CODE_SIGN_IN = 1;
        const int REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES = 2;

        const string KEY_NEW_CODE_REQUIRED = "codeRequired";

        TextView mSignInStatus;
        GoogleApiClient mGoogleApiClient;
        SignInButton mSignInButton;
        View mSignOutButton;
        View mServerAuthCodeResetButton;
        TextView mServerAuthCodeDisabledLabel;
        View mRevokeAccessButton;
        ToggleButton mScopeSelector;

        /*
     * Stores the connection result from onConnectionFailed callbacks so that we can resolve them
     * when the user clicks sign-in.
     */
        ConnectionResult mConnectionResult;

        /*
     * Tracks whether the sign-in button has been clicked so that we know to resolve all issues
     * preventing sign-in without waiting.
     */
        bool mSignInClicked;

        /*
     * Tracks whether a resolution Intent is in progress.
     */
        bool mIntentInProgress;

        /**
     * Tracks the emulated state of whether a new server auth code is required.
     */
        Java.Util.Concurrent.Atomic.AtomicBoolean mServerAuthCodeRequired = new Java.Util.Concurrent.Atomic.AtomicBoolean (false);

        /**
     * Whether Verbose is loggable.
     */
        bool mIsLogVerbose;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            // If you want to understand the life cycle more, you can use below command to turn on
            // verbose logging for this Activity on your testing device:
            // adb shell setprop log.tag.SignInActivity VERBOSE
            mIsLogVerbose = Android.Util.Log.IsLoggable (TAG, Android.Util.LogPriority.Verbose);

            SetContentView (Resource.Layout.sign_in_activity);

            restoreState (savedInstanceState);

            logVerbose ("Activity onCreate, creating new GoogleApiClient");

            mGoogleApiClient = buildGoogleApiClient (false);

            mSignInStatus = FindViewById<TextView> (Resource.Id.sign_in_status);
            mSignInButton = FindViewById<SignInButton> (Resource.Id.sign_in_button);
            mSignInButton.Click += (sender, e) => {
                if (!mGoogleApiClient.IsConnecting) {
                    int available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
                    if (available != ConnectionResult.Success) {
                        ShowDialog (DIALOG_GET_GOOGLE_PLAY_SERVICES);
                        return;
                    }

                    mSignInClicked = true;
                    mSignInStatus.Text = GetString (Resource.String.signing_in_status);
                    resolveSignInError ();
                }
            };

            mServerAuthCodeDisabledLabel = FindViewById<TextView> (Resource.Id.server_auth_code_disabled);
            mServerAuthCodeResetButton = FindViewById<View> (Resource.Id.server_auth_code_reset_button);
            mServerAuthCodeResetButton.Click += (sender, e) => {
                mServerAuthCodeRequired.Set (true);
            };
            if (!isUsingOfflineAccess ()) {
                mServerAuthCodeDisabledLabel.Visibility = ViewStates.Visible;
                mServerAuthCodeResetButton.Visibility = ViewStates.Gone;
            } else {
                mServerAuthCodeDisabledLabel.Visibility = ViewStates.Gone;
                mServerAuthCodeResetButton.Visibility = ViewStates.Visible;
            }

            mSignOutButton = FindViewById<View> (Resource.Id.sign_out_button);
            mSignOutButton.Click += (sender, e) => {
                if (mGoogleApiClient.IsConnected)
                    mGoogleApiClient.ClearDefaultAccountAndReconnect ();
            };
            mRevokeAccessButton = FindViewById (Resource.Id.revoke_access_button);
            mRevokeAccessButton.Click += async delegate {
                mServerAuthCodeRequired.Set (true);
                if (mGoogleApiClient.IsConnected) {
                    var result = await PlusClass.AccountApi.RevokeAccessAndDisconnectAsync (mGoogleApiClient);

                    if (result.IsSuccess) {
                        mSignInStatus.SetText (Resource.String.revoke_access_status);
                    } else {
                        mSignInStatus.SetText (Resource.String.revoke_access_error_status);
                    }
                    mGoogleApiClient.Reconnect ();
                       
                    updateButtons (false /* isSignedIn */);
                }
            };

            mScopeSelector = FindViewById<ToggleButton> (Resource.Id.scope_selection_toggle);
            mScopeSelector.CheckedChange += (sender, e) => {
                mGoogleApiClient.Disconnect ();
                // Since we changed the configuration, the cached connection result is no longer
                // valid.
                mConnectionResult = null;
                mGoogleApiClient = buildGoogleApiClient (e.IsChecked);
                mGoogleApiClient.Connect ();
            };


            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb) {
                ActionBar.SetDisplayHomeAsUpEnabled (true);
            }
        }

        void restoreState (Bundle savedInstanceState)
        {
            if (savedInstanceState == null) {
                mServerAuthCodeRequired.Set (isUsingOfflineAccess ());
            } else {
                mServerAuthCodeRequired.Set (
                    savedInstanceState.GetBoolean (KEY_NEW_CODE_REQUIRED, false));
            }
        }

        GoogleApiClient buildGoogleApiClient (bool useProfileScope)
        {
            var builder = new GoogleApiClient.Builder (this)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this);

            var serverClientId = GetString (Resource.String.server_client_id);

            //if (!string.IsNullOrEmpty (serverClientId))
            //    builder.RequestServerAuthCode (serverClientId, this);

            if (useProfileScope) {
                builder.AddApi (PlusClass.API)
                    .AddScope (PlusClass.ScopePlusProfile);
            } else {
                //var options = new PlusClass.PlusOptions.Builder ().AddActivityTypes (MomentUtil.ACTIONS).Build ();
                builder.AddApi (PlusClass.API) //, options)
                    .AddScope (PlusClass.ScopePlusLogin);
            }

            return builder.Build ();
        }

        bool isUsingOfflineAccess ()
        {
            // the emulation of offline access negotiation is enabled/disabled by
            // specifying the server client ID of the app in strings.xml - if no
            // value is present, we do not request offline access.
            return !string.IsNullOrEmpty (GetString (Resource.String.server_client_id));
        }

        protected override void OnStart ()
        {
            base.OnStart ();

            logVerbose ("Activity onStart, starting connecting GoogleApiClient");
            mGoogleApiClient.Connect ();
        }

        protected override void OnStop ()
        {
            logVerbose ("Activity onStop, disconnecting GoogleApiClient");

            mGoogleApiClient.Disconnect ();
            base.OnStop ();
        }

        protected override void OnSaveInstanceState (Bundle outState)
        {
            outState.PutBoolean (KEY_NEW_CODE_REQUIRED, mServerAuthCodeRequired.Get ());
        }

        protected override Dialog OnCreateDialog (int id)
        {
            if (id != DIALOG_GET_GOOGLE_PLAY_SERVICES) {
                return base.OnCreateDialog (id);
            }

            var available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
            if (available == ConnectionResult.Success) {
                return null;
            }
            if (GooglePlayServicesUtil.IsUserRecoverableError (available)) {
                return GooglePlayServicesUtil.GetErrorDialog (
                    available, this, REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES);
            }
            return new AlertDialog.Builder (this)
                .SetMessage (Resource.String.plus_generic_error)
                .SetCancelable (true)
                .Create ();
        }


        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
        {
            logVerbose (String.Format ("onActivityResult - requestCode:{0} resultCode:{1}", requestCode,
                resultCode));

            if (requestCode == REQUEST_CODE_SIGN_IN
                || requestCode == REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES) {
                mIntentInProgress = false; //Previous resolution intent no longer in progress.

                if (resultCode == Result.Ok) {
                    // After resolving a recoverable error, now retry connect(). Note that it's possible
                    // mGoogleApiClient is already connected or connecting due to rotation / Activity
                    // restart while user is walking through the (possibly full screen) resolution
                    // Activities. We should always reconnect() and ignore earlier connection attempts
                    // started before completion of the resolution. (With only one exception, a
                    // connect() attempt started late enough in the resolution flow and it actually
                    // succeeded)
                    if (!mGoogleApiClient.IsConnected) {
                        logVerbose ("Previous resolution completed successfully, try connecting again");
                        mGoogleApiClient.Reconnect ();
                    }
                } else {
                    mSignInClicked = false; // No longer in the middle of resolving sign-in errors.

                    if (resultCode == Result.Canceled) {
                        mSignInStatus.Text = GetString (Resource.String.signed_out_status);
                    } else {
                        mSignInStatus.Text = GetString (Resource.String.sign_in_error_status);
                        Console.WriteLine ("Error during resolving recoverable error.");
                    }
                }
            }
        }

//        public CheckResult OnCheckServerAuthorization (string idToken, ICollection<Scope> scopeSet)
//        {
//            if (mServerAuthCodeRequired.Get ()) {
//                var scopes = new List<Scope> ();
//                if (mScopeSelector.Checked) {
//                    scopes.Add (PlusClass.ScopePlusProfile);
//                } else {
//                    scopes.Add (PlusClass.ScopePlusLogin);
//                }
//
//                // also emulate the server asking for an additional Drive scope.
//                scopes.Add (new Scope (Scopes.DriveAppfolder));
//                return CheckResult.NewAuthRequiredResult (scopes);
//            } else {
//                return CheckResult.NewAuthNotRequiredResult ();
//            }
//        }
//
//        public bool OnUploadServerAuthCode (string idToken, string serverAuthCode)
//        {
//            Console.WriteLine ("upload server auth code " + serverAuthCode + " requested, faking success");
//            mServerAuthCodeRequired.Set (false);
//            return true;
//        }

        public void OnConnected (Bundle connectionHint)
        {
            logVerbose ("GoogleApiClient onConnected");
            var person = PlusClass.PeopleApi.GetCurrentPerson (mGoogleApiClient);
            var currentPersonName = person != null
                ? person.DisplayName
                : GetString (Resource.String.unknown_person);
            mSignInStatus.Text = GetString (Resource.String.signed_in_status, currentPersonName);
            updateButtons (true /* isSignedIn */);
            mSignInClicked = false;
        }

        public void OnConnectionSuspended (int cause)
        {
            logVerbose ("GoogleApiClient onConnectionSuspended");
            mSignInStatus.SetText (Resource.String.loading_status);
            mGoogleApiClient.Connect ();
            updateButtons (false /* isSignedIn */);
        }

        public void OnConnectionFailed (ConnectionResult result)
        {
            logVerbose (string.Format ("GoogleApiClient onConnectionFailed, error code: {0}, with " +
            "resolution: {1}", result.ErrorCode, result.HasResolution));
            if (!mIntentInProgress) {
                logVerbose ("Caching the failure result");
                mConnectionResult = result;

                if (mSignInClicked) {
                    resolveSignInError ();
                }
                updateButtons (false /* isSignedIn */);
            } else {
                logVerbose ("Intent already in progress, ignore the new failure");
            }
        }

        private void updateButtons (bool isSignedIn)
        {
            if (isSignedIn) {
                mSignInButton.Visibility = ViewStates.Invisible;
                mSignOutButton.Enabled = true;
                mRevokeAccessButton.Enabled = true;
            } else {
                if (mConnectionResult == null) {
                    // Disable the sign-in button until onConnectionFailed is called with result.
                    mSignInButton.Visibility = ViewStates.Invisible;
                    mSignInStatus.Text = GetString (Resource.String.loading_status);
                } else {
                    // Enable the sign-in button since a connection result is available.
                    mSignInButton.Visibility = ViewStates.Visible;
                    mSignInStatus.Text = GetString (Resource.String.signed_out_status);
                }

                mSignOutButton.Enabled = false;
                mRevokeAccessButton.Enabled = false;
            }
        }

        void resolveSignInError ()
        {
            if (mConnectionResult.HasResolution) {
                try {
                    mIntentInProgress = true;
                    logVerbose ("Start the resolution intent, flipping the intent-in-progress bit.");
                    mConnectionResult.StartResolutionForResult (this, REQUEST_CODE_SIGN_IN);
                } catch (IntentSender.SendIntentException e) {
                    // The intent was canceled before it was sent.  Return to the default state and
                    // attempt to connect to get an updated ConnectionResult.
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect ();
                    Console.WriteLine ("Error sending the resolution Intent, connect() again.");
                }
            }
        }

        public override bool OnOptionsItemSelected (IMenuItem item)
        {
            switch (item.ItemId) {
            case Android.Resource.Id.Home:
                var intent = new Intent (this, typeof(PlusSampleActivity));
                intent.AddFlags (ActivityFlags.ClearTop);
                StartActivity (intent);
                Finish ();
                return true;

            default:
                return base.OnOptionsItemSelected (item);
            }
        }

        void logVerbose (string message)
        {
            if (mIsLogVerbose) {
                Console.WriteLine (message);
            }
        }
    }
}

