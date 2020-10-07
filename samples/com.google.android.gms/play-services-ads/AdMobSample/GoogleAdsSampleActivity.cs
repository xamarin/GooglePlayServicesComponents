using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Ads;

namespace AdMob
{
    [Activity (Label = "AdMob", MainLauncher = true, Icon = "@drawable/icon")]
    public class GoogleAdsSampleActivity : ListActivity
    {       
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            var samples = new [] {
                new Sample (Resources.GetString (Resource.String.banner_in_xml), typeof (BannerXmlActivity)),
                new Sample (Resources.GetString (Resource.String.banner_in_code), typeof (BannerCodeActivity)),
                new Sample (Resources.GetString (Resource.String.interstitial), typeof (InterstitialActivity))
            };

            ListAdapter = new ArrayAdapter<Sample>(this, Android.Resource.Layout.SimpleListItem1, samples);

            ListView.ItemClick += (sender, e) => {
                var sample = ListView.GetItemAtPosition (e.Position) as Sample;

                StartActivity (sample.ActivityType);
            };
        }

        public static AdRequest GetAdRequest (bool addTestDevice)
        {
            var testDeviceId = "BC2508B19A2078B6AC72133BB7E6E177";
            var builder = new AdRequest.Builder ();

            if (addTestDevice)
                builder.AddTestDevice (testDeviceId);

            return builder.Build ();
        }
    }

    public class Sample : Java.Lang.Object
    {
        public Sample (string title, Type activityType) 
        {          
            Title = title;
            ActivityType = activityType;
        }

        public string Title { get;set; }
        public Type ActivityType { get;set; }

        public override string ToString ()
        {
            return Title;
        }
    }
}


