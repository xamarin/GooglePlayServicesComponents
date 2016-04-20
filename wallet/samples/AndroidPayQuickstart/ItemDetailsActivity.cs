using System;
using Android.Support.V4.App;

namespace AndroidPayQuickstart
{
    [Android.App.Activity (Label = "Details")]
    public class ItemDetailsActivity : BikestoreFragmentActivity
    {

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.activity_item_details);
        }

        protected override void OnResume ()
        {
            base.OnResume ();
        
            ActivityCompat.InvalidateOptionsMenu (this);
        }

        protected override Fragment ResultTargetFragment {
            get { return null; }
        }
    }
}

