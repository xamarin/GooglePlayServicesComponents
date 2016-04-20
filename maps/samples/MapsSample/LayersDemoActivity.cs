
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
using Java.Interop;

namespace MapsSample
{
    [Activity (Label = "Layers Demo")]			
    public class LayersDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        GoogleMap map;

        CheckBox trafficCheckbox;
        CheckBox myLocationCheckbox;
        CheckBox buildingsCheckbox;
        CheckBox indoorCheckbox;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.layers_demo);

            var spinner = FindViewById<Spinner> (Resource.Id.layers_spinner);
            var adapter = ArrayAdapter.CreateFromResource (this, Resource.Array.layers_array,
                              Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += (sender, e) => {
                if (map != null)
                    setLayer (spinner.GetItemAtPosition (e.Position).ToString ());
            };

            trafficCheckbox = FindViewById<CheckBox> (Resource.Id.traffic);
            myLocationCheckbox = FindViewById<CheckBox> (Resource.Id.my_location);
            buildingsCheckbox = FindViewById<CheckBox> (Resource.Id.buildings);
            indoorCheckbox = FindViewById<CheckBox> (Resource.Id.indoor);

            var mapFragment = (SupportMapFragment) SupportFragmentManager.FindFragmentById (Resource.Id.map);
            mapFragment.GetMapAsync (this);
        }

        public void OnMapReady (GoogleMap googleMap)
        {
            map = googleMap;
            updateTraffic ();
            updateMyLocation ();
            updateBuildings ();
            updateIndoor ();
        }

        public bool CheckReady ()
        {
            if (map == null) {
                Toast.MakeText (this, Resource.String.map_not_ready, ToastLength.Short).Show ();
                return false;
            }
            return true;
        }

        [Export ("onTrafficToggled")]
        public void OnTrafficToggled (View view) 
        {
            updateTraffic ();
        }
         
        void updateTraffic ()
        {
            if (!CheckReady ())
                return;
            map.TrafficEnabled = trafficCheckbox.Checked;
        }

        [Export ("onMyLocationToggled")]
        public void OnMyLocationToggled (View view) 
        {
            updateMyLocation ();
        }

        void updateMyLocation ()
        {
            if (!CheckReady ())
                return;

            map.MyLocationEnabled = myLocationCheckbox.Checked;
        }

        [Export ("onBuildingsToggled")]
        public void OnBuildingsToggled (View view)
        {
            updateBuildings ();
        }

        void updateBuildings ()
        {
            if (!CheckReady ())
                return;
            map.BuildingsEnabled = buildingsCheckbox.Checked;
        }

        [Export ("onIndoorToggled")]
        public void OnIndoorToggled (View view)
        {
            updateIndoor ();
        }

        void updateIndoor ()
        {
            if (!CheckReady ())
                return;
            map.SetIndoorEnabled (indoorCheckbox.Checked);
        }

        void setLayer (string layerName)
        {
            if (layerName == GetString (Resource.String.normal))
                map.MapType = GoogleMap.MapTypeNormal;
            else if (layerName == GetString (Resource.String.hybrid))
                map.MapType = GoogleMap.MapTypeHybrid;
            else if (layerName == GetString (Resource.String.satellite))
                map.MapType = GoogleMap.MapTypeSatellite;
            else if (layerName == GetString (Resource.String.terrain))
                map.MapType = GoogleMap.MapTypeTerrain;
            else if (layerName == GetString (Resource.String.none_map))
                map.MapType = GoogleMap.MapTypeNone;
            else
                Console.WriteLine ("Error setting layer with name: " + layerName);
        }
    }
}

