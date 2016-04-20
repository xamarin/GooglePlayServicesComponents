using System;
using Android.Support.V4.App;
using Android.Content;
using Android.Gms.Wallet;
using Android.Widget;

namespace AndroidPayQuickstart
{
    public abstract class BikestoreFragmentActivity : FragmentActivity
    {
        /**
     *  Request code used to launch LoginActivity
     */
        const int REQUEST_USER_LOGIN = 1000;

        /**
     * When calling {@link Wallet#loadFullWallet(GoogleApiClient, FullWalletRequest, int)} or
     * resolving connection errors with
     * {@link ConnectionResult#startResolutionForResult(android.app.Activity, int)},
     * the given {@link Activity}'s callback is called. Since in this case, the caller is a
     * {@link Fragment}, and not {@link Activity} that is passed in, this callback is forwarded to
     * {@link FullWalletConfirmationButtonFragment}, {@link PromoAddressLookupFragment} or
     * {@link LoginFragment}.
     * If the requestCode is one of the predefined codes to handle
     * the API calls, pass it to the fragment or else treat it normally.
     */
        protected override void OnActivityResult (int requestCode, Android.App.Result resultCode, Intent data)
        {            
            switch (requestCode) {
            case FullWalletConfirmationButtonFragment.REQUEST_CODE_RESOLVE_LOAD_FULL_WALLET:
            case FullWalletConfirmationButtonFragment.REQUEST_CODE_RESOLVE_ERR:
            case PromoAddressLookupFragment.REQUEST_CODE_RESOLVE_ADDRESS_LOOKUP:
            case PromoAddressLookupFragment.REQUEST_CODE_RESOLVE_ERR:
            case LoginFragment.REQUEST_CODE_RESOLVE_ERR:
                var fragment = ResultTargetFragment;
                if (fragment != null)
                    fragment.OnActivityResult (requestCode, (int)resultCode, data);
                break;
            case REQUEST_USER_LOGIN:
                if (resultCode == Android.App.Result.Ok)
                    ActivityCompat.InvalidateOptionsMenu (this);
                break;
            default:
                base.OnActivityResult (requestCode, resultCode, data);
                break;
            }
        }

        public override bool OnCreateOptionsMenu (Android.Views.IMenu menu)
        {            
            base.OnCreateOptionsMenu (menu);
            var inflater = MenuInflater;
            if (((BikestoreApplication) Application).IsLoggedIn)
                inflater.Inflate (Resource.Menu.menu_logout, menu);
            else
                inflater.Inflate (Resource.Menu.menu_login, menu);
            
            return true;
        }

        public override bool OnOptionsItemSelected (Android.Views.IMenuItem item)
        {            
            switch (item.ItemId) {
            case Resource.Id.login:
                var loginIntent = new Intent(this, typeof(LoginActivity));
                loginIntent.PutExtra (LoginActivity.EXTRA_ACTION, LoginActivity.Action.LOGIN);
                StartActivityForResult (loginIntent, REQUEST_USER_LOGIN);
                return true;
            case Resource.Id.logout:
                var logoutIntent = new Intent (this, typeof (LoginActivity));
                logoutIntent.PutExtra (LoginActivity.EXTRA_ACTION, LoginActivity.Action.LOGOUT);
                StartActivityForResult (logoutIntent, REQUEST_USER_LOGIN);
                return true;
            default:
                return false;
            }
        }

        protected void HandleError(int errorCode) {
            switch (errorCode) {
            case WalletConstants.ErrorCodeSpendingLimitExceeded:
                Toast.MakeText (this, GetString (Resource.String.spending_limit_exceeded, errorCode),
                    ToastLength.Long).Show ();
                break;
            case WalletConstants.ErrorCodeInvalidParameters:
            case WalletConstants.ErrorCodeAuthenticationFailure:
            case WalletConstants.ErrorCodeBuyerAccountError:
            case WalletConstants.ErrorCodeMerchantAccountError:
            case WalletConstants.ErrorCodeServiceUnavailable:
            case WalletConstants.ErrorCodeUnsupportedApiVersion:
            case WalletConstants.ErrorCodeUnknown:
            default:
                // unrecoverable error
                String errorMessage = GetString (Resource.String.google_wallet_unavailable) + "\n" +
                    GetString (Resource.String.error_code, errorCode);
                Toast.MakeText (this, errorMessage, ToastLength.Long).Show ();
                break;
            }
        }
        /**
     * Implemented by Activities like {@link ConfirmationActivity}, {@link LoginActivity},
     * {@link ItemListActivity}
     * This is called from {@link BikestoreFragmentActivity#onActivityResult(int, int, Intent)}
     * to forward the callback to the appropriate {@link Fragment}
     *
     * @return The Fragment that should handle result. Some implementations can return null.
     */
        protected abstract Fragment ResultTargetFragment { get; }
    }
}

