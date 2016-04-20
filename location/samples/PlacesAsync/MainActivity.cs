using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Location.Places;
using Android.Gms.Maps.Model;

// USE YOUR OWN KEY HERE, THIS ONE WILL NOT WORK
[assembly: MetaData ("com.google.android.geo.API_KEY", Value="AIzaSyB9GNnotNlDk0PtxI6LhPCwAs-k4aN-gpA")]

namespace PlacesAsync
{
    [Activity (Label = "Places Async", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Android.Support.V4.App.FragmentActivity, GoogleApiClient.IOnConnectionFailedListener
    {   

        static readonly LatLngBounds BOUNDS_GREATER_SYDNEY = new LatLngBounds(
            new LatLng(-34.041458, 150.790100), new LatLng(-33.682247, 151.383362));
        
        GoogleApiClient googleApiClient;


        PlacesAutocompleteAdapter adapter;
        ListView listView;
        AutoCompleteTextView textSearch;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            listView = FindViewById<ListView> (Resource.Id.listView);
            textSearch = FindViewById<AutoCompleteTextView> (Resource.Id.textSearch);

            googleApiClient = new GoogleApiClient.Builder (this)
                .EnableAutoManage (this, 0, this)
                .AddApi (PlacesClass.GEO_DATA_API)
                .Build ();

            adapter = new PlacesAutocompleteAdapter (this, googleApiClient, BOUNDS_GREATER_SYDNEY, null);
            textSearch.Adapter = adapter;


            textSearch.ItemClick += (sender, e) => {
                var item = adapter [e.Position];

                // Go to details of place:
                var intent = new Intent (this, typeof (PlaceDetailsActivity));
                intent.PutExtra ("PLACE_ID", item.PlaceId);
                StartActivity (intent);
            };
        }

        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
        {
            Toast.MakeText (this, "Failed to Connect to Google Services", ToastLength.Long).Show ();
        }
    }
}


