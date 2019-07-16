using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace RemoveLocationUpdatesHanging
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private FusedLocationProviderClient _fusedLocationClient;
        private LocationCallback _locationCallback;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _fusedLocationClient = new FusedLocationProviderClient(this);
            _locationCallback = new LocationCallback();
            _locationCallback.LocationResult += OnLocationResult;

            Button startButton = FindViewById<Button>(Resource.Id.startTrackingButton);
            startButton.Click += StartButtonClicked;

            Button stopButton = FindViewById<Button>(Resource.Id.stopTrackingButton);
            stopButton.Click += StopButtonClicked;
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

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == 555)
            {
                for (int i = 0; i < permissions.Length; i++)
                {
                    string permission = permissions[i];
                    if (permission == Android.Manifest.Permission.AccessFineLocation &&
                        grantResults[i] == Permission.Granted)
                    {
                        await StartLocationUpdates();
                    }
                }
            }
        }

        private async void StartButtonClicked(object sender, EventArgs e)
        {
            if (CheckSelfPermission(Android.Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                await StartLocationUpdates();
            }
            else
            {
                RequestPermissions(new [] { Android.Manifest.Permission.AccessFineLocation }, 555);    
            }
        }

        private async Task StartLocationUpdates()
        {
            var request = new LocationRequest()
                .SetSmallestDisplacement(10)
                .SetInterval(10000)
                .SetPriority(LocationRequest.PriorityHighAccuracy);

            await _fusedLocationClient.RequestLocationUpdatesAsync(request, _locationCallback, null);
            Toast.MakeText(this, "Location Updates Began", ToastLength.Long).Show();
        }

      
        private async void StopButtonClicked(object sender, EventArgs e)
        {
            try
            {
                # if SHOW_BUG
                await
                #endif
                    _fusedLocationClient.RemoveLocationUpdatesAsync(_locationCallback);
                Toast.MakeText(this, "Location Updates Ended", ToastLength.Long).Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void OnLocationResult(object sender, LocationCallbackResultEventArgs e)
        {
            foreach (var location in e.Result.Locations)
            {
                Toast.MakeText(this, $"Location Update Received: ({location.Latitude}, {location.Longitude})", ToastLength.Short).Show();
            }
            
        }
    }
}

