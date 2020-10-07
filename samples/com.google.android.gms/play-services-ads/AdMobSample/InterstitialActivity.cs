
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
using Java.Interop;

namespace AdMob
{
    [Activity (Label = "@string/interstitial")]			
    public class InterstitialActivity : Activity
    {
        InterstitialAd interstitial;
        Button showButton;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.activity_interstitial);

            interstitial = new InterstitialAd (this) {
                AdUnitId = Resources.GetString (Resource.String.ad_unit_id),
                AdListener = new ToastAdListener (this) {
                    AdLoadedAction = () => {
                        showButton.Text = "Show Interstitial";
                        showButton.Enabled = true;
                    },
                    AdFailedToLoadAction = () => {
                        showButton.Text = "Ad Failed to Load";
                        showButton.Enabled = false;
                    }
                }
            };

            showButton = FindViewById<Button> (Resource.Id.showButton);
            showButton.Enabled = false;
        }
            
        [Export ("loadInterstitial")]
        public void loadInterstitial (View unusedView) 
        {
            showButton.Text = "Loading Interstitial...";
            showButton.Enabled = false;
            interstitial.LoadAd (GoogleAdsSampleActivity.GetAdRequest (true));
        }

        [Export ("showInterstitial")]
        public void showInterstitial(View unusedView) {
            if (interstitial.IsLoaded) {
                interstitial.Show ();
            }

            showButton.Text = "Interstitial Not Ready";
            showButton.Enabled = false;
        }
    }
}

