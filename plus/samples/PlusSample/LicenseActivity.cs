
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

namespace PlusSample
{
    [Activity (Label = "License")]			
    public class LicenseActivity : Activity
    {
        protected override void OnCreate (Bundle savedState) 
        {
            base.OnCreate (savedState);

            var scroll = new ScrollView (this);

            var license = new TextView (this);

            license.Text = GooglePlayServicesUtil.GetOpenSourceSoftwareLicenseInfo (this);
            scroll.AddView (license);

            SetContentView (scroll);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb) {
                ActionBar.SetDisplayHomeAsUpEnabled (true);
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
    }
}

