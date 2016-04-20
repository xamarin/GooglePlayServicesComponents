using System;
using Android.Gms.Wallet;
using Android.Widget;
using Android.Content;
using Android.Runtime;

namespace AndroidPayQuickstart
{
    [Android.App.Activity (Label = "Order Complete")]
    public class OrderCompleteActivity : Android.App.Activity
    {
        FullWallet mFullWallet;

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.activity_order_complete);
            mFullWallet = Intent.GetParcelableExtra (Constants.EXTRA_FULL_WALLET).JavaCast<FullWallet> ();
            var continueButton = FindViewById<Button> (Resource.Id.button_continue_shopping);
            continueButton.Click += delegate {
                var intent = new Intent (this, typeof (ItemListActivity));
                intent.AddFlags (ActivityFlags.ClearTask);
                intent.AddFlags (ActivityFlags.NewTask);
                StartActivity (intent);  
            };
        }
    }
}

