using System;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.App;
using Android.Gms.Wallet;
using Android.Gms.Identity.Intents.Model;
using Android.Widget;
using Android.OS;
using Android.Gms.Identity.Intents;
using System.Text;

namespace AndroidPayQuickstart
{
    public class PromoAddressLookupFragment : Android.Support.V4.App.Fragment, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public const int REQUEST_CODE_RESOLVE_ADDRESS_LOOKUP = 1006;
        public const int REQUEST_CODE_RESOLVE_ERR = 1007;
        const string KEY_PROMO_CLICKED = "KEY_PROMO_CLICKED";

        ProgressDialog mProgressDialog;
        GoogleApiClient mGoogleApiClient;
        ConnectionResult mConnectionResult;
        bool mPromoWasSelected = false;

        public override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            if (savedInstanceState != null)
                mPromoWasSelected = savedInstanceState.GetBoolean (KEY_PROMO_CLICKED);
            
            var accountName = ((BikestoreApplication) Activity.Application).AccountName;
            var options = new Android.Gms.Identity.Intents.Address.AddressOptions (WalletConstants.ThemeLight);
            mGoogleApiClient = new GoogleApiClient.Builder (Activity)
                .AddApi (Android.Gms.Identity.Intents.Address.Api, options)
                .SetAccountName (accountName)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this)
                .Build();
        }

        public override void OnSaveInstanceState (Android.OS.Bundle outState)
        {
            base.OnSaveInstanceState (outState);
            outState.PutBoolean (KEY_PROMO_CLICKED, mPromoWasSelected);
        }

        public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {            
            var view = inflater.Inflate (Resource.Layout.fragment_promo_address_lookup, container, false);
            // Styling the header with HTML elements in TextView
            var promoTitle = view.FindViewById<TextView> (Resource.Id.promo_title);
            promoTitle.Text = Android.Text.Html.FromHtml (GetString (Resource.String.promo)).ToString ();
            mProgressDialog = initializeProgressDialog();
            view.Click += delegate {
                if (mConnectionResult != null) {
                    // If there was a connection failure, attempt to resolve the ConnectionResult
                    // when the user taps the button
                    resolveConnection ();
                } else {
                    lookupAddress ();
                }
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
            dismissProgressDialog();
        }

        public override void OnActivityResult (int requestCode, int resultCode, Android.Content.Intent data)
        {
            switch (requestCode) {
            case REQUEST_CODE_RESOLVE_ERR:
                // call connect regardless of success or failure
                // if the result was success, the connect should succeed
                // if the result was not success, this should get a new connection result
                mGoogleApiClient.Connect ();
                break;
            case REQUEST_CODE_RESOLVE_ADDRESS_LOOKUP:
                dismissProgressDialog ();
                mPromoWasSelected = false;
                switch (resultCode) {
                case (int)Result.Ok:
                    ((BikestoreApplication)Activity.Application).IsAddressValidForPromo = true;
                    var userAddress = UserAddress.FromIntent (data);
                    
                    Toast.MakeText (Activity, GetString (Resource.String.promo_eligible, FormatUsAddress (userAddress)), ToastLength.Long).Show ();
                    break;
                case (int)Result.Canceled:
                    break;
                default:
                    Toast.MakeText (Activity, GetString (Resource.String.no_address), ToastLength.Long).Show ();
                    break;
                }
                break;
            default:
                base.OnActivityResult (requestCode, resultCode, data);
                dismissProgressDialog ();
                break;
            }
        }

        public void OnConnected (Bundle connectionHint)
        {
            if (mPromoWasSelected)
                lookupAddress();
        }
        
        public void OnConnectionFailed (ConnectionResult result)
        {
            // Save the intent so that we can start an lookup when the user clicks the promo.
            mConnectionResult = result;
            if (mPromoWasSelected)
                resolveConnection ();
        }

        public void OnConnectionSuspended (int cause)
        {
            // nothing specifically required here, onConnected will be called when connection resumes
        }

        void resolveConnection ()
        {
            try {
                if (mConnectionResult != null && mConnectionResult.HasResolution) {
                    mConnectionResult.StartResolutionForResult (Activity, REQUEST_CODE_RESOLVE_ERR);
                } else {
                    mGoogleApiClient.Connect ();
                }
            }  catch (Android.Content.IntentSender.SendIntentException e) {
                mConnectionResult = null;
                mGoogleApiClient.Connect ();
            }
        }

        void lookupAddress ()
        {
            if (mGoogleApiClient.IsConnected ) {
                showProgressDialog();
                var request = UserAddressRequest.NewBuilder ().Build();
                Android.Gms.Identity.Intents.Address.RequestUserAddress (mGoogleApiClient, request, REQUEST_CODE_RESOLVE_ADDRESS_LOOKUP);
            } else {
                if (!mGoogleApiClient.IsConnecting)
                    mGoogleApiClient.Connect ();
            
                mPromoWasSelected = true;
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

        private void dismissProgressDialog () 
        {
            if (mProgressDialog != null && mProgressDialog.IsShowing)
                mProgressDialog.Dismiss ();
        }

        // Address formatting specific to the US, depending upon the countries supported you may
        // have different address formatting
        static string FormatUsAddress (UserAddress address)
        {
            var builder = new StringBuilder ();
            builder.Append ("\n");
            
            if (!string.IsNullOrEmpty (address.Address1))
                builder.Append (address.Address1 + ", ");
            if (!string.IsNullOrEmpty (address.Locality))
                builder.Append (address.Locality + ", ");
            if (!string.IsNullOrEmpty (address.AdministrativeArea))
                builder.Append (address.AdministrativeArea + ", ");
            
            if (!string.IsNullOrEmpty (address.CountryCode))
                builder.Append (address.CountryCode);
        
            return builder.ToString ();
        }
    }
}

