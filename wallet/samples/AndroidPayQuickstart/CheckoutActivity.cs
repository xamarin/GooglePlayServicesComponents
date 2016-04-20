using System;
using Android.Gms.Wallet;
using Android.Widget;
using Android.Gms.Wallet.Fragment;
using Android.Content;
using Android.Runtime;

namespace AndroidPayQuickstart
{
    [Android.App.Activity (Label = "Checkout")]
    public class CheckoutActivity : BikestoreFragmentActivity
    {
        const int REQUEST_CODE_MASKED_WALLET = 1001;

        SupportWalletFragment mWalletFragment;
        int mItemId;
        Button mReturnToShopping;
        Button mContinueCheckout;
        CheckBox mStripeCheckbox;
        PaymentMethodTokenizationParameters mPaymentMethodParameters;

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        
            SetContentView (Resource.Layout.activity_checkout);

            mItemId = Intent.GetIntExtra (Constants.EXTRA_ITEM_ID, 0);

            createAndAddWalletFragment();

            mReturnToShopping = FindViewById<Button> (Resource.Id.button_return_to_shopping);
            mReturnToShopping.Click += delegate {
                GoToItemListActivity ();
            };
            mContinueCheckout = FindViewById<Button> (Resource.Id.button_regular_checkout);
            mContinueCheckout.Click += delegate {
                ContinueCheckout ();
            };

            mStripeCheckbox = FindViewById<CheckBox> (Resource.Id.checkbox_stripe);
            mStripeCheckbox.CheckedChange += (sender, e) => {
                if (e.IsChecked && ValidateStripeConfiguration ()) {
                    
                    mPaymentMethodParameters = PaymentMethodTokenizationParameters.NewBuilder ()
                        .SetPaymentMethodTokenizationType (PaymentMethodTokenizationType.PaymentGateway)
                        .AddParameter ("gateway", "stripe")
                        .AddParameter ("stripe:publishableKey", GetString (Resource.String.stripe_publishable_key))
                        .AddParameter ("stripe:version", GetString (Resource.String.stripe_version))
                        .Build();
                    
                } else {
                    mPaymentMethodParameters = null;
                }

                // Re-create the buy-button with the proper processor
                createAndAddWalletFragment();
            };
        }

        protected override void OnActivityResult (int requestCode, Android.App.Result resultCode, Android.Content.Intent data)
        {
            // retrieve the error code, if available
            int errorCode = -1;
            if (data != null) {
                errorCode = data.GetIntExtra (WalletConstants.ExtraErrorCode, -1);
            }
            switch (requestCode) {
            case REQUEST_CODE_MASKED_WALLET:
                switch (resultCode) {
                case Android.App.Result.Ok:
                    var maskedWallet = data.GetParcelableExtra (WalletConstants.ExtraMaskedWallet).JavaCast<MaskedWallet> ();
                    launchConfirmationPage(maskedWallet);
                    break;
                case Android.App.Result.Canceled:
                    break;
                default:
                    HandleError (errorCode);
                    break;
                }
                break;
            case WalletConstants.ResultError:
                HandleError (errorCode);
                break;
            default:
                base.OnActivityResult (requestCode, resultCode, data);
                break;
            }
        }

        /**
     * If the confirmation page encounters an error it can't handle, it will send the customer back
     * to this page.  The intent should include the error code as an {@code int} in the field
     * {@link WalletConstants#EXTRA_ERROR_CODE}.
     */
        protected override void OnNewIntent (Android.Content.Intent intent)
        {            
            if (intent.HasExtra (WalletConstants.ExtraErrorCode)) {
                int errorCode = intent.GetIntExtra (WalletConstants.ExtraErrorCode, 0);
                HandleError(errorCode);
            }
        }

        private void GoToItemListActivity ()
        {
            var intent = new Intent (this, typeof (ItemListActivity));
            intent.AddFlags (ActivityFlags.ClearTop);
            StartActivity (intent);
        }

        private void ContinueCheckout () 
        {
            Toast.MakeText(this, Resource.String.checkout_bikestore_message, ToastLength.Long).Show ();
        }


        private bool ValidateStripeConfiguration ()
        {
            var publishableKey = GetString (Resource.String.stripe_publishable_key);
            var version = GetString(Resource.String.stripe_version);

            if ("REPLACE_ME".Equals (publishableKey) || "REPLACE_ME".Equals (version)) {
                Toast.MakeText (this, GetString (Resource.String.stripe_config_error), ToastLength.Long).Show ();
                mStripeCheckbox.Checked = false;
                return false;
            } else {
                return true;
            }
        }

        private void createAndAddWalletFragment() {
            var walletFragmentStyle = new WalletFragmentStyle ()
                .SetBuyButtonText (BuyButtonText.BuyWithGoogle)
                .SetBuyButtonAppearance (BuyButtonAppearance.Classic)
                .SetBuyButtonWidth(Dimension.MatchParent);

            var walletFragmentOptions = WalletFragmentOptions.NewBuilder ()
                .SetEnvironment (Constants.WALLET_ENVIRONMENT)
                .SetFragmentStyle (walletFragmentStyle)
                .SetTheme (WalletConstants.ThemeLight)
                .SetMode (WalletFragmentMode.BuyButton)
                .Build();
            mWalletFragment = SupportWalletFragment.NewInstance (walletFragmentOptions);

            // Now initialize the Wallet Fragment
            var accountName = ((BikestoreApplication) Application).AccountName;
            MaskedWalletRequest maskedWalletRequest;
            if (mPaymentMethodParameters != null) {
                maskedWalletRequest = WalletUtil.CreateStripeMaskedWalletRequest (Constants.ITEMS_FOR_SALE[mItemId],
                    mPaymentMethodParameters);
            } else {
                maskedWalletRequest = WalletUtil.CreateMaskedWalletRequest (Constants.ITEMS_FOR_SALE[mItemId]);
            }

            var startParamsBuilder = WalletFragmentInitParams.NewBuilder ()
                .SetMaskedWalletRequest (maskedWalletRequest)
                .SetMaskedWalletRequestCode(REQUEST_CODE_MASKED_WALLET)
                .SetAccountName(accountName);

            mWalletFragment.Initialize (startParamsBuilder.Build ());

            // add Wallet fragment to the UI
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.dynamic_wallet_button_fragment, mWalletFragment)
                .Commit();
        }

        private void launchConfirmationPage (MaskedWallet maskedWallet) {
            var intent = new Intent(this, typeof (ConfirmationActivity));
            intent.PutExtra (Constants.EXTRA_ITEM_ID, mItemId);
            intent.PutExtra (Constants.EXTRA_MASKED_WALLET, maskedWallet);
            StartActivity (intent);
        }

        protected override Android.Support.V4.App.Fragment ResultTargetFragment {
            get { return null; }
        }
    }
}

