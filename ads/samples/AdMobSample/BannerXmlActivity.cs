
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
using Android.Gms.Ads;

namespace AdMob
{
    [Activity (Label = "@string/banner_in_xml")]			
    public class BannerXmlActivity : Activity
    {
        AdView adView;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_banner_xml);

            adView = FindViewById<AdView> (Resource.Id.adView);
            adView.AdListener = new ToastAdListener (this);

            adView.LoadAd (GoogleAdsSampleActivity.GetAdRequest (true));
        }

        protected override void OnPause ()
        {
            adView.Pause ();
            base.OnPause ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            adView.Resume ();
        }

        protected override void OnDestroy ()
        {
            adView.Destroy ();
            base.OnDestroy ();
        }       
    }
}

