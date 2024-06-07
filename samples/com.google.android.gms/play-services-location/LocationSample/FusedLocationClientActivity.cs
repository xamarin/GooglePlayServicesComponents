using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Java.Lang;

namespace LocationSample
{
	[Activity(Label = "Fused Location Client")]
	public class FusedLocationClientActivity : Activity
	{
		TextView textLastLocation;
		TextView textLocationUpdates;

        Android.Gms.Location.LocationCallback locationCallback;
        Android.Gms.Location.FusedLocationProviderClient fusedClient;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			textLastLocation = FindViewById<TextView>(Resource.Id.textLastLocation);
			textLocationUpdates = FindViewById<TextView>(Resource.Id.textLocationUpdates);

			// Create our client and location callback
			fusedClient = null; //new Android.Gms.Location.FusedLocationProviderClient(this);
			locationCallback = new Android.Gms.Location.LocationCallback();
		}

		protected override async void OnResume()
		{
			base.OnResume();

			// Subscribe for location updates
			locationCallback.LocationResult += LocationCallback_LocationResult;

			// Build our location request setting interval, priority, etc
			var locationRequest = new Android.Gms.Location.LocationRequest()
				.SetInterval(10000)
				.SetFastestInterval(1000)
				.SetPriority(Android.Gms.Location.LocationRequest.PriorityHighAccuracy);

			// Start receiving location updates
			// await fusedClient.RequestLocationUpdatesAsync(locationRequest, locationCallback);

			// Get the last known location so we can show the user something immediately
			// var lastLocation = await fusedClient.GetLastLocationAsync();
			// if (lastLocation != null)
			// 	textLastLocation.Text = DescribeLocation(lastLocation);
		}

		protected override async void OnPause()
		{
			// Unsubscribe from the event
			locationCallback.LocationResult -= LocationCallback_LocationResult;

			// TODO: We should await this call but there appears to be a bug
			// in Google Play Services where the first time removeLocationUpdates is called,
			// the returned Android.Gms.Tasks.Task never actually completes, even though
			// location updates do seem to be removed and stop happening.
			// For now we'll just fire and forget as a workaround.
			// fusedClient.RemoveLocationUpdatesAsync(locationCallback);

			base.OnPause();
		}

		void LocationCallback_LocationResult(object sender, Android.Gms.Location.LocationCallbackResultEventArgs e)
		{
			textLocationUpdates.Text = DescribeLocation(e.Result.LastLocation);
		}

		string DescribeLocation(Android.Locations.Location location)
		{
			return string.Format("{0}: {1}, {2} @ {3}",
				location.Provider,
				location.Latitude,
				location.Longitude,
				new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(location.Time));
		}
	}
}
