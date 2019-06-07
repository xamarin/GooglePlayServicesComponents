using Android.App;
using Android.Gms.Analytics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace GoogleAnalyticsApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Tracker _tracker;

        public Tracker Tracker
        {
            get
            {
                if (_tracker == null)
                {
                    var analytics = GoogleAnalytics.GetInstance(this);
                    _tracker = analytics.NewTracker("UA-XXXXXXXX-X");
                }
                return _tracker;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            Button sendScreenViewButton = FindViewById<Button>(Resource.Id.sendScreenViewButton);
            sendScreenViewButton.Click += (object sender, EventArgs eventArgs) =>
            {
                Tracker.SetScreenName("HOME SCREEN");
                Tracker.Send(new HitBuilders.ScreenViewBuilder().Build());
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}