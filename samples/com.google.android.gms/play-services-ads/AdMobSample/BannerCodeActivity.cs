
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
    [Activity (Label = "@string/banner_in_code")]			
    public class BannerCodeActivity : Activity
    {
        AdView adView;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_banner_code_ad_listener);

            adView = new AdView (this) {
                AdUnitId = Resources.GetString (Resource.String.ad_unit_id),
                AdSize = AdSize.Banner,
                AdListener = new ToastAdListener (this)
            };

            var layout = FindViewById<RelativeLayout> (Resource.Id.mainLayout);
            var lp = new RelativeLayout.LayoutParams (
                ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

            layout.AddView (adView, lp);

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

