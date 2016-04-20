using System;
using Android.Gms.Wallet.Fragment;
using Android.Gms.Wallet;
using Android.Runtime;

namespace AndroidPayQuickstart
{
    [Android.App.Activity (Label = "Confirmation")]
    public class ConfirmationActivity : BikestoreFragmentActivity
    {
        const int REQUEST_CODE_CHANGE_MASKED_WALLET = 1002;

        SupportWalletFragment mWalletFragment;
        MaskedWallet mMaskedWallet;

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        
            mMaskedWallet = Intent.GetParcelableExtra (Constants.EXTRA_MASKED_WALLET).JavaCast<MaskedWallet> ();
            SetContentView (Resource.Layout.activity_confirmation);

            CreateAndAddWalletFragment();
        }

        public override bool OnCreateOptionsMenu (Android.Views.IMenu menu)
        {
            // no need to show login menu on confirmation screen
            return false;
        }

        private void CreateAndAddWalletFragment() {
            var walletFragmentStyle = new WalletFragmentStyle()
                .SetMaskedWalletDetailsTextAppearance (
                    Resource.Style.BikestoreWalletFragmentDetailsTextAppearance)
                .SetMaskedWalletDetailsHeaderTextAppearance (
                    Resource.Style.BikestoreWalletFragmentDetailsHeaderTextAppearance)
                .SetMaskedWalletDetailsBackgroundColor(
                    Resources.GetColor (Resource.Color.bikestore_white))
                .SetMaskedWalletDetailsButtonBackgroundResource(
                    Resource.Drawable.bikestore_btn_default_holo_light);

            var walletFragmentOptions = WalletFragmentOptions.NewBuilder ()
                .SetEnvironment (Constants.WALLET_ENVIRONMENT)
                .SetFragmentStyle (walletFragmentStyle)
                .SetTheme (WalletConstants.ThemeLight)
                .SetMode (WalletFragmentMode.SelectionDetails)
                .Build ();
            mWalletFragment = SupportWalletFragment.NewInstance (walletFragmentOptions);

            // Now initialize the Wallet Fragment
            var accountName = ((BikestoreApplication) Application).AccountName;

            var startParamsBuilder = WalletFragmentInitParams.NewBuilder ()
                .SetMaskedWallet (mMaskedWallet)
                .SetMaskedWalletRequestCode (REQUEST_CODE_CHANGE_MASKED_WALLET)
                .SetAccountName (accountName);
            mWalletFragment.Initialize (startParamsBuilder.Build ());

            // add Wallet fragment to the UI
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.dynamic_wallet_masked_wallet_fragment, mWalletFragment)
                .Commit ();
        }

        protected override void OnActivityResult (int requestCode, Android.App.Result resultCode, Android.Content.Intent data)
        {
            switch (requestCode) {
            case REQUEST_CODE_CHANGE_MASKED_WALLET:
                if (resultCode == Android.App.Result.Ok &&
                    data.HasExtra (WalletConstants.ExtraMaskedWallet)) {
                    mMaskedWallet = data.GetParcelableExtra (WalletConstants.ExtraMaskedWallet).JavaCast<MaskedWallet> ();
                    ((FullWalletConfirmationButtonFragment) ResultTargetFragment)
                        .UpdateMaskedWallet (mMaskedWallet);
                }
                // you may also want to use the new masked wallet data here, say to recalculate
                // shipping or taxes if shipping address changed
                break;
            case WalletConstants.ResultError:
                int errorCode = data.GetIntExtra (WalletConstants.ExtraErrorCode, 0);
                HandleError (errorCode);
                break;
            default:
                base.OnActivityResult (requestCode, resultCode, data);
                break;
            }
        }

        protected override Android.Support.V4.App.Fragment ResultTargetFragment {
            get {
                return SupportFragmentManager.FindFragmentById (Resource.Id.full_wallet_confirmation_button_fragment);
            }
        }
    }
}

