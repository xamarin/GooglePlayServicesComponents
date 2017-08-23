using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Android.Gms.Location;

namespace LocationSample
{
	[Activity(Label = "Fused Location Client")]
	public class FusedLocationClientActivity : AppCompatActivity
	{
		TextView textLastLocation;
		TextView textLocationUpdates;

		LocationCallback locationCallback;
		FusedLocationProviderClient fusedClient;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			textLastLocation = FindViewById<TextView>(Resource.Id.textLastLocation);
			textLocationUpdates = FindViewById<TextView>(Resource.Id.textLocationUpdates);

			// Create our client and location callback
			fusedClient = new FusedLocationProviderClient(this);
			locationCallback = new LocationCallback();
		}

		protected override async void OnResume()
		{
			base.OnResume();

			// Subscribe for location updates
			locationCallback.LocationResult += LocationCallback_LocationResult;

			// Build our location request setting interval, priority, etc
			var locationRequest = new LocationRequest()
				.SetInterval(10000)
				.SetFastestInterval(1000)
				.SetPriority(LocationRequest.PriorityHighAccuracy);

			// Start receiving location updates
			await fusedClient.RequestLocationUpdatesAsync(locationRequest, locationCallback);

			// Get the last known location so we can show the user something immediately
			var lastLocation = await fusedClient.GetLastLocationAsync();
			if (lastLocation != null)
				DescribeLocation(lastLocation);
		}

		protected override async void OnStop()
		{
			// Remove ourselves from getting location updates
			await fusedClient.RemoveLocationUpdatesAsync(locationCallback);

			// Unsubscribe from the event
			locationCallback.LocationResult -= LocationCallback_LocationResult;

			base.OnStop();
		}

		void LocationCallback_LocationResult(object sender, LocationCallbackResultEventArgs e)
		{
			DescribeLocation(e.Result.LastLocation);
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
