using System;
using Android.Runtime;

namespace AndroidPayQuickstart
{
    [Android.App.Activity (Label = "Confirmation")]
    public class ConfirmationActivity : BikestoreFragmentActivity
    {
        const int REQUEST_CODE_CHANGE_MASKED_WALLET = 1002;

        //mc++ Android.Gms.Wallet.Fragment.SupportWalletFragment mWalletFragment;
        Android.Gms.Wallet.MaskedWallet mMaskedWallet;

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        
            mMaskedWallet = Intent.GetParcelableExtra (Constants.EXTRA_MASKED_WALLET).JavaCast<Android.Gms.Wallet.MaskedWallet> ();
            SetContentView (Resource.Layout.activity_confirmation);

            CreateAndAddWalletFragment();
        }

        public override bool OnCreateOptionsMenu (Android.Views.IMenu menu)
        {
            // no need to show login menu on confirmation screen
            return false;
        }

        private void CreateAndAddWalletFragment() {
            // removed https://developers.google.com/android/guides/releases#august_27_2019
            /*
            var walletFragmentStyle = new Android.Gms.Wallet.Fragment.WalletFragmentStyle()
                .SetMaskedWalletDetailsTextAppearance (
                    Resource.Style.BikestoreWalletFragmentDetailsTextAppearance)
                .SetMaskedWalletDetailsHeaderTextAppearance (
                    Resource.Style.BikestoreWalletFragmentDetailsHeaderTextAppearance)
                .SetMaskedWalletDetailsBackgroundColor(
                    Resources.GetColor (Resource.Color.bikestore_white))
                .SetMaskedWalletDetailsButtonBackgroundResource(
                    Resource.Drawable.bikestore_btn_default_holo_light);
            
            // removed https://developers.google.com/android/guides/releases#august_27_2019
            var walletFragmentOptions = Android.Gms.Wallet.Fragment.WalletFragmentOptions.NewBuilder ()
                .SetEnvironment (Constants.WALLET_ENVIRONMENT)
                .SetFragmentStyle (walletFragmentStyle)
                .SetTheme (Android.Gms.Wallet.WalletConstants.ThemeLight)
                // removed https://developers.google.com/android/guides/releases#august_27_2019
                .SetMode (Android.Gms.Wallet.Fragment.WalletFragmentMode.SelectionDetails)
                .Build ();
            // removed https://developers.google.com/android/guides/releases#august_27_2019
            mWalletFragment = Android.Gms.Wallet.Fragment.SupportWalletFragment.NewInstance (walletFragmentOptions);
            
            // Now initialize the Wallet Fragment
            var accountName = ((BikestoreApplication) Application).AccountName;

            // removed https://developers.google.com/android/guides/releases#august_27_2019
            var startParamsBuilder = Android.Gms.Wallet.Fragment.WalletFragmentInitParams.NewBuilder ()
                .SetMaskedWallet (mMaskedWallet)
                .SetMaskedWalletRequestCode (REQUEST_CODE_CHANGE_MASKED_WALLET)
                .SetAccountName (accountName);
            mWalletFragment.Initialize (startParamsBuilder.Build ());
            
            // add Wallet fragment to the UI
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.dynamic_wallet_masked_wallet_fragment, mWalletFragment)
                .Commit ();
            */
        }

        protected override void OnActivityResult (int requestCode, Android.App.Result resultCode, Android.Content.Intent data)
        {
            switch (requestCode) {
            case REQUEST_CODE_CHANGE_MASKED_WALLET:
                    /*
                if (resultCode == Android.App.Result.Ok &&
                    // removed https://developers.google.com/android/guides/releases#august_27_2019
                    data.HasExtra (Android.Gms.Wallet.WalletConstants.ExtraMaskedWallet)) {
                        // removed https://developers.google.com/android/guides/releases#august_27_2019
                        mMaskedWallet = data.GetParcelableExtra (Android.Gms.Wallet.WalletConstants.ExtraMaskedWallet).JavaCast<Android.Gms.Wallet.MaskedWallet> ();
                    ((FullWalletConfirmationButtonFragment) ResultTargetFragment)
                        .UpdateMaskedWallet (mMaskedWallet);
                }
                    */
                // you may also want to use the new masked wallet data here, say to recalculate
                // shipping or taxes if shipping address changed
                break;
            case Android.Gms.Wallet.WalletConstants.ResultError:
                int errorCode = data.GetIntExtra (Android.Gms.Wallet.WalletConstants.ExtraErrorCode, 0);
                HandleError (errorCode);
                break;
            default:
                base.OnActivityResult (requestCode, resultCode, data);
                break;
            }
        }

        protected override AndroidX.Fragment.App.Fragment ResultTargetFragment {
            get {
                return null; // mc++ SupportFragmentManager.FindFragmentById (Resource.Id.full_wallet_confirmation_button_fragment);
            }
        }
    }
}

