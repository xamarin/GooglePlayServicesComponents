
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
using Android.Gms.Maps;
using Android.Support.V4.App;
using Android.Gms.Maps.Model;

namespace MapsSample
{
    [Activity (Label = "Programmatic Demo")]			
    public class ProgrammaticDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        const string MAP_FRAGMENT_TAG = "map";

        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            // It isn't possible to set a fragment's id programmatically so we set a tag instead and
            // search for it using that.
            var mapFragment = (SupportMapFragment)
                SupportFragmentManager.FindFragmentByTag (MAP_FRAGMENT_TAG);

            // We only create a fragment if it doesn't already exist.
            if (mapFragment == null) {
                // To programmatically add the map, we first create a SupportMapFragment.
                mapFragment = SupportMapFragment.NewInstance ();

                // Then we add it using a FragmentTransaction.
                var fragmentTransaction =
                    SupportFragmentManager.BeginTransaction ();
                fragmentTransaction.Add (Android.Resource.Id.Content, mapFragment, MAP_FRAGMENT_TAG);
                fragmentTransaction.Commit();
            }
            mapFragment.GetMapAsync (this);
        }
            
        public void OnMapReady (GoogleMap map) 
        {
            map.AddMarker (new MarkerOptions().SetPosition (new LatLng(0, 0)).SetTitle("Marker"));
        }
    }
}

