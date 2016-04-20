
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
    [Activity (Label = "Retain Map Demo")]			
    public class RetainMapDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.basic_demo);

            var mapFragment =
                (SupportMapFragment) SupportFragmentManager.FindFragmentById (Resource.Id.map);

            if (savedInstanceState == null) {
                // First incarnation of this activity.
                mapFragment.RetainInstance = true;
            }

            mapFragment.GetMapAsync (this);
        }
            
        public void OnMapReady(GoogleMap map) 
        {
            map.AddMarker(new MarkerOptions().SetPosition (new LatLng(0, 0)).SetTitle ("Marker"));
        }
    }
}

