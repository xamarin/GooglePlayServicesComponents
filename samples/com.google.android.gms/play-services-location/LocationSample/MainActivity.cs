using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace LocationSample
{
    [Activity (Label = "Location Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Menu);

			FindViewById<Button>(Resource.Id.buttonOld).Click += (sender, e) => {
				StartActivity(typeof(FusedLocationProviderActivity));
			};

			FindViewById<Button>(Resource.Id.buttonNew).Click += (sender, e) => {
				StartActivity(typeof(FusedLocationClientActivity));
			};
        }
    }
}
