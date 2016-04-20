
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
    [Activity (Label = "Raw MapView Demo")]			
    public class RawMapViewDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        private MapView mapView;

        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);

            SetContentView (Resource.Layout.raw_mapview_demo);

            mapView = FindViewById<MapView> (Resource.Id.map);
            mapView.OnCreate (savedInstanceState);

            mapView.GetMapAsync(this);
        }
            
        protected override void OnResume () 
        {
            base.OnResume ();
            mapView.OnResume ();
        }
            
        public void OnMapReady (GoogleMap map) 
        {
            map.AddMarker (new MarkerOptions().SetPosition (new LatLng(0, 0)).SetTitle("Marker"));
        }
            
        protected override void OnPause () 
        {
            mapView.OnPause ();
            base.OnPause ();
        }
            
        protected override void OnDestroy () 
        {
            mapView.OnDestroy ();
            base.OnDestroy();
        }
                   
        public override void OnLowMemory () 
        {
            base.OnLowMemory ();
            mapView.OnLowMemory ();
        }
            
        protected override void OnSaveInstanceState(Bundle outState) 
        {
            base.OnSaveInstanceState (outState);
            mapView.OnSaveInstanceState (outState);
        }
    }
}

