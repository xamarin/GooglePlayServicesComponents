using System;
using Android.Widget;
using Android.Content;
using Android.Runtime;
using Android.OS;

namespace AndroidPayQuickstart
{
    [Android.App.Activity (Label = "Checkout")]
    public class CheckoutActivity : BikestoreFragmentActivity
    {
        const int REQUEST_CODE_MASKED_WALLET = 1001;

        Android.Gms.Wallet.Fragment.SupportWalletFragment mWalletFragment;
        int mItemId;
        Button mReturnToShopping;
        Button mContinueCheckout;
        CheckBox mStripeCheckbox;
        Android.Gms.Wallet.PaymentMethodTokenizationParameters mPaymentMethodParameters;

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
                    
                    mPaymentMethodParameters = Android.Gms.Wallet.PaymentMethodTokenizationParameters.NewBuilder ()
                        .SetPaymentMethodTokenizationType (Android.Gms.Wallet.PaymentMethodTokenizationType.PaymentGateway)
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
                errorCode = data.GetIntExtra (Android.Gms.Wallet.WalletConstants.ExtraErrorCode, -1);
            }
            switch (requestCode) {
            case REQUEST_CODE_MASKED_WALLET:
                switch (resultCode) {
                case Android.App.Result.Ok:
                    var maskedWallet = data.GetParcelableExtra (Android.Gms.Wallet.WalletConstants.ExtraMaskedWallet).JavaCast<Android.Gms.Wallet.MaskedWallet> ();
                    launchConfirmationPage(maskedWallet);
                    break;
                case Android.App.Result.Canceled:
                    break;
                default:
                    HandleError (errorCode);
                    break;
                }
                break;
            case Android.Gms.Wallet.WalletConstants.ResultError:
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
            if (intent.HasExtra (Android.Gms.Wallet.WalletConstants.ExtraErrorCode)) {
                int errorCode = intent.GetIntExtra (Android.Gms.Wallet.WalletConstants.ExtraErrorCode, 0);
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
            var walletFragmentStyle = new Android.Gms.Wallet.Fragment.WalletFragmentStyle()
                .SetBuyButtonText (Android.Gms.Wallet.Fragment.BuyButtonText.BuyWithGoogle)
                .SetBuyButtonAppearance (Android.Gms.Wallet.Fragment.BuyButtonAppearance.Classic)
                .SetBuyButtonWidth(Android.Gms.Wallet.Fragment.Dimension.MatchParent);

            var walletFragmentOptions = Android.Gms.Wallet.Fragment.WalletFragmentOptions.NewBuilder ()
                .SetEnvironment (Constants.WALLET_ENVIRONMENT)
                .SetFragmentStyle (walletFragmentStyle)
                .SetTheme (Android.Gms.Wallet.WalletConstants.ThemeLight)
                .SetMode (Android.Gms.Wallet.Fragment.WalletFragmentMode.BuyButton)
                .Build();
            mWalletFragment = Android.Gms.Wallet.Fragment.SupportWalletFragment.NewInstance (walletFragmentOptions);

            // Now initialize the Wallet Fragment
            var accountName = ((BikestoreApplication) Application).AccountName;
            Android.Gms.Wallet.MaskedWalletRequest maskedWalletRequest;
            if (mPaymentMethodParameters != null) {
                maskedWalletRequest = WalletUtil.CreateStripeMaskedWalletRequest (Constants.ITEMS_FOR_SALE[mItemId],
                    mPaymentMethodParameters);
            } else {
                maskedWalletRequest = WalletUtil.CreateMaskedWalletRequest (Constants.ITEMS_FOR_SALE[mItemId]);
            }

            var startParamsBuilder = Android.Gms.Wallet.Fragment.WalletFragmentInitParams.NewBuilder ()
                .SetMaskedWalletRequest (maskedWalletRequest)
                .SetMaskedWalletRequestCode(REQUEST_CODE_MASKED_WALLET)
                .SetAccountName(accountName);

            mWalletFragment.Initialize (startParamsBuilder.Build ());

            // add Wallet fragment to the UI
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.dynamic_wallet_button_fragment, mWalletFragment)
                .Commit();
        }

        private void launchConfirmationPage (Android.Gms.Wallet.MaskedWallet maskedWallet) {
            var intent = new Intent(this, typeof (ConfirmationActivity));
            intent.PutExtra (Constants.EXTRA_ITEM_ID, mItemId);
            intent.PutExtra (Constants.EXTRA_MASKED_WALLET, maskedWallet.JavaCast<IParcelable>());
            StartActivity (intent);
        }

        protected override AndroidX.Fragment.App.Fragment ResultTargetFragment {
            get { return null; }
        }
    }
}

