using System;
using Android.Support.V4.App;
using Android.Gms.Common;
using Android.App;
using Android.Widget;
using Android.Gms.Wallet;
using Android.Content;
using Android.Runtime;
using Android.Gms.Common.Apis;
using Android.OS;

namespace AndroidPayQuickstart
{
    public class FullWalletConfirmationButtonFragment : Android.Support.V4.App.Fragment, IDialogInterfaceOnCancelListener, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        const string TAG = "FullWallet";

        /**
     * Request code used when attempting to resolve issues with connecting to Google Play Services.
     * Only use this request code when calling {@link ConnectionResult#startResolutionForResult(
     * android.app.Activity, int)}.
     */
        public const int REQUEST_CODE_RESOLVE_ERR = 1003;

        /**
     * Request code used when loading a full wallet. Only use this request code when calling
     * {@link Wallet#loadFullWallet(GoogleApiClient, FullWalletRequest, int)}.
     */
        public const int REQUEST_CODE_RESOLVE_LOAD_FULL_WALLET = 1004;

        // Maximum number of times to try to connect to GoogleApiClient if the connection is failing
        const int MAX_RETRIES = 3;
        const long INITIAL_RETRY_DELAY_MILLISECONDS = 3000;
        const int MESSAGE_RETRY_CONNECTION = 1010;
        const string KEY_RETRY_COUNTER = "KEY_RETRY_COUNTER";
        const string KEY_HANDLE_FULL_WALLET_WHEN_READY = "KEY_HANDLE_FULL_WALLET_WHEN_READY";

        // No. of times to retry loadFullWallet on receiving a ConnectionResult.INTERNAL_ERROR
        const int MAX_FULL_WALLET_RETRIES = 1;
        const string KEY_RETRY_FULL_WALLET_COUNTER = "KEY_RETRY_FULL_WALLET_COUNTER";

        int mRetryCounter = 0;
        // handler for processing retry attempts
        RetryHandler mRetryHandler;

        protected GoogleApiClient mGoogleApiClient;
        protected ProgressDialog mProgressDialog;
        // whether the user tried to do an action that requires a full wallet (i.e.: loadFullWallet)
        // before a full wallet was acquired (i.e.: still waiting for mGoogleApiClient to connect)
        protected bool mHandleFullWalletWhenReady = false;
        protected int mItemId;

        // Cached connection result for resolving connection failures on user action.
        protected ConnectionResult mConnectionResult;

        private ItemInfo mItemInfo;
        private Button mConfirmButton;
        private MaskedWallet mMaskedWallet;
        private int mRetryLoadFullWalletCount = 0;
        private Intent mActivityLaunchIntent;

        public override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        
            if (savedInstanceState != null) {
                mRetryCounter = savedInstanceState.GetInt (KEY_RETRY_COUNTER);
                mRetryLoadFullWalletCount = savedInstanceState.GetInt (KEY_RETRY_FULL_WALLET_COUNTER);
                mHandleFullWalletWhenReady =
                    savedInstanceState.GetBoolean (KEY_HANDLE_FULL_WALLET_WHEN_READY);
            }
            mActivityLaunchIntent = Activity.Intent;
            mItemId = mActivityLaunchIntent.GetIntExtra (Constants.EXTRA_ITEM_ID, 0);
            mMaskedWallet = mActivityLaunchIntent.GetParcelableExtra (Constants.EXTRA_MASKED_WALLET).JavaCast<MaskedWallet> ();

            var accountName = ((BikestoreApplication) Activity.Application).AccountName;

            // Set up an API client;
            mGoogleApiClient = new GoogleApiClient.Builder (Activity)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this)
                .SetAccountName (accountName)
                .AddApi (WalletClass.API, new WalletClass.WalletOptions.Builder ()
                    .SetEnvironment (Constants.WALLET_ENVIRONMENT)
                    .SetTheme (WalletConstants.ThemeLight)
                    .Build ())
                .Build ();

            mRetryHandler = new RetryHandler (this);
        }

        public override void OnStart ()
        {
            base.OnStart ();
        
            // Connect to Google Play Services
            mGoogleApiClient.Connect ();
        }

        public override void OnSaveInstanceState (Android.OS.Bundle outState)
        {
            base.OnSaveInstanceState (outState);
            outState.PutInt (KEY_RETRY_COUNTER, mRetryCounter);
            outState.PutBoolean (KEY_HANDLE_FULL_WALLET_WHEN_READY, mHandleFullWalletWhenReady);
            outState.PutInt (KEY_RETRY_FULL_WALLET_COUNTER, mRetryLoadFullWalletCount);
        }

        public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {            
            initializeProgressDialog();
            var view = inflater.Inflate (Resource.Layout.fragment_full_wallet_confirmation_button, container, false);
            mItemInfo = Constants.ITEMS_FOR_SALE[mItemId];

            mConfirmButton = view.FindViewById<Button> (Resource.Id.button_place_order);
            mConfirmButton.Click += delegate {
                confirmPurchase();    
            };
            return view;
        }

        public override void OnStop ()
        {
            base.OnStop ();
        
            // Disconnect from Google Play Services
            mGoogleApiClient.Disconnect ();

            if (mProgressDialog != null)
                mProgressDialog.Dismiss ();

            mRetryHandler.RemoveMessages (MESSAGE_RETRY_CONNECTION);
        }
            
        public void OnConnected (Bundle connectionHint) 
        {
            // don't need to do anything here
        }

        public void OnConnectionSuspended (int cause)
        {
            // don't need to do anything here
        }

        public void OnConnectionFailed (ConnectionResult result)
        {
            mConnectionResult = result;

            // Handle the user's tap by dismissing the progress dialog and attempting to resolve the
            // connection result.
            if (mHandleFullWalletWhenReady) {
                mProgressDialog.Dismiss ();
                resolveUnsuccessfulConnectionResult ();
            }
        }

        /**
     * Helper to try to resolve a user recoverable error (i.e. the user has an out of date version
     * of Google Play Services installed), via an error dialog provided by
     * {@link GooglePlayServicesUtil#getErrorDialog(int, Activity, int, OnCancelListener)}. If an,
     * error is not user recoverable then the error will be handled in {@link #handleError(int)}.
     */
        protected void resolveUnsuccessfulConnectionResult ()
        {
            // Additional user input is needed
            if (mConnectionResult.HasResolution) {
                try {
                    mConnectionResult.StartResolutionForResult (Activity, REQUEST_CODE_RESOLVE_ERR);
                } catch (IntentSender.SendIntentException) {
                    reconnect();
                }
            } else {
                int errorCode = mConnectionResult.ErrorCode;
                if (GooglePlayServicesUtil.IsUserRecoverableError (errorCode)) {

                    var dialog = GooglePlayServicesUtil.GetErrorDialog (errorCode, Activity, REQUEST_CODE_RESOLVE_ERR, this);

                    // the dialog will either be dismissed, which will invoke the OnCancelListener, or
                    // the dialog will be addressed, which will result in a callback to
                    // OnActivityResult()
                    dialog.Show ();
                } else {
                    switch (errorCode) {
                    case ConnectionResult.InternalError:
                    case ConnectionResult.NetworkError:
                        reconnect();
                        break;
                    default:
                        handleError (errorCode);
                        break;
                    }
                }
            }

            mConnectionResult = null;
        }

        public void OnCancel (IDialogInterface dialog)
        {
            mGoogleApiClient.Connect ();
        }
            
        public override void OnActivityResult (int requestCode, int resultCode, Android.Content.Intent data)
        {            
            mProgressDialog.Hide ();

            // retrieve the error code, if available
            int errorCode = -1;
            if (data != null)
                errorCode = data.GetIntExtra (WalletConstants.ExtraErrorCode, -1);

            switch (requestCode) {
            case REQUEST_CODE_RESOLVE_ERR:
                if (resultCode == (int)Result.Ok) {
                    mGoogleApiClient.Connect();
                } else {
                    handleUnrecoverableGoogleWalletError(errorCode);
                }
                break;
            case REQUEST_CODE_RESOLVE_LOAD_FULL_WALLET:
                switch (resultCode) {
                case (int)Result.Ok:
                    if (data.HasExtra(WalletConstants.ExtraFullWallet)) {
                        FullWallet fullWallet =
                            data.GetParcelableExtra (WalletConstants.ExtraFullWallet).JavaCast<FullWallet> ();
                        // the full wallet can now be used to process the customer's payment
                        // send the wallet info up to server to process, and to get the result
                        // for sending a transaction status
                        fetchTransactionStatus(fullWallet);
                    } else if (data.HasExtra(WalletConstants.ExtraMaskedWallet)) {
                        // re-launch the activity with new masked wallet information
                        mMaskedWallet = data.GetParcelableExtra (WalletConstants.ExtraMaskedWallet).JavaCast<MaskedWallet> ();
                        mActivityLaunchIntent.PutExtra (Constants.EXTRA_MASKED_WALLET, mMaskedWallet);

                        StartActivity(mActivityLaunchIntent);
                    }
                    break;
                case (int)Result.Canceled:
                    // nothing to do here
                    break;
                default:
                    handleError(errorCode);
                    break;
                }
                break;
            }
        }

        public void UpdateMaskedWallet (MaskedWallet maskedWallet)
        {
            this.mMaskedWallet = maskedWallet;
        }

        void reconnect() 
        {
            if (mRetryCounter < MAX_RETRIES) {
                mProgressDialog.Show ();
                var m = mRetryHandler.ObtainMessage (MESSAGE_RETRY_CONNECTION);
                // back off exponentially
                long delay = (long) (INITIAL_RETRY_DELAY_MILLISECONDS * Math.Pow (2, mRetryCounter));
                mRetryHandler.SendMessageDelayed (m, delay);
                mRetryCounter++;
            } else {
                handleError(WalletConstants.ErrorCodeServiceUnavailable);
            }
        }

        /**
     * For unrecoverable Google Wallet errors, send the user back to the checkout page to handle the
     * problem.
     *
     * @param errorCode
     */
        protected void handleUnrecoverableGoogleWalletError(int errorCode)
        {
            Intent intent = new Intent (Activity, typeof (CheckoutActivity));
            intent.AddFlags (ActivityFlags.ClearTop);
            intent.PutExtra (WalletConstants.ExtraErrorCode, errorCode);
            intent.PutExtra (Constants.EXTRA_ITEM_ID, mItemId);
            StartActivity (intent);
        }

        void handleError(int errorCode)
        {
            if (checkAndRetryFullWallet(errorCode)) {
                // handled by retrying
                return;
            }
            switch (errorCode) {
            case WalletConstants.ErrorCodeSpendingLimitExceeded:
                // may be recoverable if the user tries to lower their charge
                // take the user back to the checkout page to try to handle
            case WalletConstants.ErrorCodeInvalidParameters:
            case WalletConstants.ErrorCodeAuthenticationFailure:
            case WalletConstants.ErrorCodeBuyerAccountError:
            case WalletConstants.ErrorCodeMerchantAccountError:
            case WalletConstants.ErrorCodeServiceUnavailable:
            case WalletConstants.ErrorCodeUnsupportedApiVersion:
            case WalletConstants.ErrorCodeUnknown:
            default:
                // unrecoverable error
                // take the user back to the checkout page to handle these errors
                handleUnrecoverableGoogleWalletError (errorCode);
                break;
            }
        }

        void confirmPurchase() 
        {
            if (mConnectionResult != null) {
                // The user needs to resolve an issue before GoogleApiClient can connect
                resolveUnsuccessfulConnectionResult();
            } else {
                getFullWallet();
                mProgressDialog.SetCancelable (false);
                mProgressDialog.Show ();
                mHandleFullWalletWhenReady = true;
            }
        }

        void getFullWallet() 
        {
            WalletClass.Payments.LoadFullWallet (mGoogleApiClient,
                WalletUtil.CreateFullWalletRequest (mItemInfo, mMaskedWallet.GoogleTransactionId),
                REQUEST_CODE_RESOLVE_LOAD_FULL_WALLET);
        }

        /**
     * Here the client should connect to their server, process the credit card/instrument
     * and get back a status indicating whether charging the card was successful or not
     */
        void fetchTransactionStatus (FullWallet fullWallet) 
        {
            if (mProgressDialog.IsShowing)
                mProgressDialog.Dismiss();

            // Log Stripe payment method token, if it exists
            PaymentMethodToken token = fullWallet.PaymentMethodToken;
            if (token != null) {
                // getToken returns a JSON object as a String.  Replace newlines to make LogCat output
                // nicer.  The 'id' field of the object contains the Stripe token we are interested in.
                Android.Util.Log.Debug (TAG, "PaymentMethodToken:" + token.Token.Replace ('\n', ' '));
            }

            // NOTE: Send details such as fullWallet.getProxyCard() or fullWallet.getBillingAddress()
            // to your server and get back success or failure. If you used Stripe for processing,
            // you can get the token from fullWallet.getPaymentMethodToken()
            // The following code assumes a successful response and calls notifyTransactionStatus
            WalletClass.Payments.NotifyTransactionStatus (mGoogleApiClient,
                WalletUtil.CreateNotifyTransactionStatusRequest (fullWallet.GoogleTransactionId,
                    NotifyTransactionStatusRequest.Status.Success));

            Intent intent = new Intent(Activity, typeof (OrderCompleteActivity));
            intent.SetFlags (ActivityFlags.ClearTask | ActivityFlags.NewTask);
            intent.PutExtra (Constants.EXTRA_FULL_WALLET, fullWallet);

            StartActivity(intent);
        }

        /**
     * Retries {@link Wallet#loadFullWallet(GoogleApiClient, FullWalletRequest, int)} if
     * {@link #MAX_FULL_WALLET_RETRIES} has not been reached.
     *
     * @return {@code true} if {@link FullWalletConfirmationButtonFragment#getFullWallet()} is retried,
     *         {@code false} otherwise.
     */
        bool checkAndRetryFullWallet (int errorCode) 
        {
            if ((errorCode == WalletConstants.ErrorCodeServiceUnavailable ||
                errorCode == WalletConstants.ErrorCodeUnknown) &&
                mRetryLoadFullWalletCount < MAX_FULL_WALLET_RETRIES) {
                mRetryLoadFullWalletCount++;
                getFullWallet();
                return true;
            }
            return false;
        }

        protected void initializeProgressDialog() 
        {
            mProgressDialog = new ProgressDialog (Activity);
            mProgressDialog.Indeterminate = true;
            mProgressDialog.SetMessage (GetString (Resource.String.loading));
            mProgressDialog.DismissEvent += delegate {
                mHandleFullWalletWhenReady = false;  
            };                
        }

        class RetryHandler : Android.OS.Handler
        {            
            WeakReference<FullWalletConfirmationButtonFragment> mWeakReference;

            public RetryHandler(FullWalletConfirmationButtonFragment fragment) {
                mWeakReference = new WeakReference<FullWalletConfirmationButtonFragment>(fragment);
            }

            public override void HandleMessage (Android.OS.Message msg)
            {
                switch (msg.What) {
                case MESSAGE_RETRY_CONNECTION:
                    
                    FullWalletConfirmationButtonFragment fragment = null;
                    mWeakReference.TryGetTarget (out fragment);

                    if (fragment != null)
                        fragment.mGoogleApiClient.Connect ();

                    break;
                }
            }
        }
    }
}

