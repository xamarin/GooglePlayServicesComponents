
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
using Android.Support.V4.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace MapsSample
{
    [Activity (Label = "Basic Demo")]			
    public class BasicMapDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.basic_demo);

            var mapFragment =
                (SupportMapFragment) SupportFragmentManager.FindFragmentById (Resource.Id.map);
            mapFragment.GetMapAsync (this);
        }

        public void OnMapReady (GoogleMap googleMap)
        {
            googleMap.AddMarker (new MarkerOptions ()
                .SetPosition (new LatLng (0, 0))
                .SetTitle ("Marker"));
        }
    }
}

