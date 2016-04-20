using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using System.Threading.Tasks;

namespace LocationSample
{
    [Activity (Label = "Location Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, ILocationListener
    {
        TextView textLastLocation;
        TextView textLocationUpdates;

        GoogleApiClient googleApiClient;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            textLastLocation = FindViewById<TextView> (Resource.Id.textLastLocation);
            textLocationUpdates = FindViewById<TextView> (Resource.Id.textLocationUpdates);

            googleApiClient = new GoogleApiClient.Builder (this)
                .AddApi (Android.Gms.Location.LocationServices.API)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this)
                .Build ();
            googleApiClient.Connect ();
        }

        public async void OnConnected (Bundle connectionHint)
        {           
            // Get Last known location
            var lastLocation = LocationServices.FusedLocationApi.GetLastLocation (googleApiClient);

            textLastLocation.Text = lastLocation == null ? "NULL" : DescribeLocation (lastLocation);

            await RequestLocationUpdates ();
        }

        async Task RequestLocationUpdates ()
        {
            // Describe our location request
            var locationRequest = new LocationRequest ()
                .SetInterval (10000)
                .SetFastestInterval (1000)
                .SetPriority (LocationRequest.PriorityHighAccuracy);

            // Check to see if we can request updates first
            if (await CheckLocationAvailability (locationRequest)) {

                // Request updates
                await LocationServices.FusedLocationApi.RequestLocationUpdates (googleApiClient,
                    locationRequest, 
                    this);
            }
        }

        async Task<bool> CheckLocationAvailability (LocationRequest locationRequest)
        {
            // Build a new request with the given location request
            var locationSettingsRequest = new LocationSettingsRequest.Builder ()
                .AddLocationRequest (locationRequest)
                .Build ();

            // Ask the Settings API if we can fulfill this request
            var locationSettingsResult = await LocationServices.SettingsApi.CheckLocationSettingsAsync (googleApiClient, locationSettingsRequest);


            // If false, we might be able to resolve it by showing the location settings 
            // to the user and allowing them to change the settings
            if (!locationSettingsResult.Status.IsSuccess) {
                
                if (locationSettingsResult.Status.StatusCode == LocationSettingsStatusCodes.ResolutionRequired)
                    locationSettingsResult.Status.StartResolutionForResult (this, 101);
                else 
                    Toast.MakeText (this, "Location Services Not Available for the given request.", ToastLength.Long).Show ();

                return false;
            }

            return true;
        }

        public void OnConnectionSuspended (int cause)
        {
            Console.WriteLine ("GooglePlayServices Connection Suspended: {0}", cause);
        }

        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
        {
            Console.WriteLine ("GooglePlayServices Connection Failed: {0}", result);
        }

        public void OnLocationChanged (Android.Locations.Location location)
        {
            // Show latest location
            var l = DescribeLocation (location);

            textLocationUpdates.Text = l + System.Environment.NewLine + textLocationUpdates.Text;
        }

        protected override async void OnActivityResult (int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult (requestCode, resultCode, data);

            // See if we returned from a location settings dialog 
            // and if succesfully, we can try location updates again
            if (requestCode == 101) {
                if (resultCode == Result.Ok)
                    await RequestLocationUpdates ();
                else
                    Toast.MakeText (this, "Failed to resolve Location Settings changes", ToastLength.Long).Show ();
            }
        }

        string DescribeLocation (Android.Locations.Location location)
        {
            return string.Format ("{0}: {1}, {2} @ {3}",
                location.Provider,
                location.Latitude,
                location.Longitude,
                new DateTime (1970, 1, 1, 0, 0, 0).AddMilliseconds (location.Time));
        }
    }
}
