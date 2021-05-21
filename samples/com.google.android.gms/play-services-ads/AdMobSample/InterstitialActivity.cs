
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
using Android.Gms.Ads.Interstitial;
using Java.Interop;

namespace AdMob
{
    // needs porting
    // https://stackoverflow.com/questions/65875325/android-admob-interstitialad-deprecated
    // https://stackoverflow.com/questions/65880741/after-updating-google-ads-sdk-interstitialad-is-deprecated-how-to-resolve
    // https://developers.google.com/admob/android/interstitial
    // https://developers.google.com/admob/android/migration
    // https://stackoverflow.com/questions/66164874/interstitial-ad
    // https://stackoverflow.com/questions/11106322/how-to-create-android-interstitial-ads

    [Activity (Label = "@string/interstitial")]			
    public class InterstitialActivity : Activity
    {
        InterstitialAd interstitial;
        Button showButton;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.activity_interstitial);

            //interstitial = ;
            Android.Gms.Ads.Interstitial.InterstitialAd.Load(this,
                Resources.GetString(Resource.String.ad_unit_id),
                //interstitial,
                new AdRequest.Builder().Build(),
                new InterstitialAdLoadCallback(showButton)
                );
 
            showButton = FindViewById<Button> (Resource.Id.showButton);
            showButton.Enabled = false;
        }
            
        [Export ("loadInterstitial")]
        public void loadInterstitial (View unusedView) 
        {
            showButton.Text = "Loading Interstitial...";
            showButton.Enabled = false;
            // interstitial.LoadAd (GoogleAdsSampleActivity.GetAdRequest (true));
        }

        [Export ("showInterstitial")]
        public void showInterstitial(View unusedView) {
            //if (interstitial.IsLoaded) {
            //    interstitial.Show (this);
            //}

            showButton.Text = "Interstitial Not Ready";
            showButton.Enabled = false;
        }

    }

}

