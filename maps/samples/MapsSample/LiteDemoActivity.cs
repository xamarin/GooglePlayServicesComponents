
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
using Java.Interop;

namespace MapsSample
{
    [Activity (Label = "Lite Demo")]			
    public class LiteDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        static readonly LatLng BRISBANE = new LatLng(-27.47093, 153.0235);
        static readonly LatLng MELBOURNE = new LatLng(-37.81319, 144.96298);
        static readonly LatLng SYDNEY = new LatLng(-33.87365, 151.20689);
        static readonly LatLng ADELAIDE = new LatLng(-34.92873, 138.59995);
        static readonly LatLng PERTH = new LatLng(-31.952854, 115.857342);
        static readonly LatLng DARWIN = new LatLng(-12.459501, 130.839915);

        static readonly LatLng[] POLYGON = new [] { 
            new LatLng(-18.000328, 130.473633), new LatLng(-16.173880, 135.087891),
            new LatLng(-19.663970, 137.724609), new LatLng(-23.202307, 135.395508),
            new LatLng(-19.705347, 129.550781)};

        GoogleMap map;

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            // Set the layout
            SetContentView (Resource.Layout.lite_demo);

            // Get the map and register for the ready callback
            var mapFragment =
                (SupportMapFragment) SupportFragmentManager.FindFragmentById (Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }
            
        [Export ("showDarwin")]
        public void ShowDarwin (View v) 
        {
            // Wait until map is ready
            if (map == null)
                return;

            // Center camera on Adelaide marker
            map.MoveCamera (CameraUpdateFactory.NewLatLngZoom (DARWIN, 10f));
        }
            
        [Export ("showAdelaide")]
        public void ShowAdelaide(View v) 
        {
            // Wait until map is ready
            if (map == null)
                return;

            // Center camera on Adelaide marker
            map.MoveCamera (CameraUpdateFactory.NewLatLngZoom (ADELAIDE, 10f));
        }

        [Export ("showAustralia")]
        public void ShowAustralia (View v) 
        {
            // Wait until map is ready
            if (map == null)
                return;

            // Create bounds that include all locations of the map
            var boundsBuilder = new LatLngBounds.Builder ()
                .Include(PERTH)
                .Include(ADELAIDE)
                .Include(MELBOURNE)
                .Include(SYDNEY)
                .Include(DARWIN)
                .Include(BRISBANE);

            // Move camera to show all markers and locations
            map.MoveCamera (CameraUpdateFactory.NewLatLngBounds (boundsBuilder.Build (), 20));
        }
            
        public void OnMapReady(GoogleMap googleMap) 
        {
            map = googleMap;
            addMarkers();
            addPolyobjects();

            var mapView = SupportFragmentManager.FindFragmentById (Resource.Id.map).View;
            if (mapView.ViewTreeObserver.IsAlive)
                mapView.ViewTreeObserver.GlobalLayout += MapView_ViewTreeObserver_GlobalLayout;
        }

        void MapView_ViewTreeObserver_GlobalLayout (object sender, EventArgs e)
        {
            var mapView = SupportFragmentManager.FindFragmentById (Resource.Id.map).View;
            mapView.ViewTreeObserver.GlobalLayout -= MapView_ViewTreeObserver_GlobalLayout;

            ShowAustralia(null);
        }
            
        void addPolyobjects() 
        {
            map.AddPolyline (new PolylineOptions()
                .Add (MELBOURNE, ADELAIDE, PERTH)
                .InvokeColor (Android.Graphics.Color.Green)
                .InvokeWidth (5f));

            map.AddPolygon (new PolygonOptions()
                .Add (POLYGON)
                .InvokeFillColor (Android.Graphics.Color.Cyan)
                .InvokeStrokeColor (Android.Graphics.Color.Blue)
                .InvokeStrokeWidth (5));
        }
            
        void addMarkers () 
        {
            map.AddMarker(new MarkerOptions()
                .SetPosition (BRISBANE)
                .SetTitle("Brisbane"));

            map.AddMarker(new MarkerOptions()
                .SetPosition(MELBOURNE)
                .SetTitle("Melbourne")
                .SetIcon(BitmapDescriptorFactory.DefaultMarker (BitmapDescriptorFactory.HueAzure)));

            map.AddMarker(new MarkerOptions()
                .SetPosition(SYDNEY)
                .SetTitle("Sydney")
                .SetIcon (BitmapDescriptorFactory.DefaultMarker (BitmapDescriptorFactory.HueRed)));

            map.AddMarker(new MarkerOptions()
                .SetPosition(ADELAIDE)
                .SetTitle("Adelaide")
                .SetIcon (BitmapDescriptorFactory.DefaultMarker (BitmapDescriptorFactory.HueYellow)));

            map.AddMarker(new MarkerOptions()
                .SetPosition(PERTH)
                .SetTitle("Perth")
                .SetIcon(BitmapDescriptorFactory.DefaultMarker (BitmapDescriptorFactory.HueMagenta)));
        }
    }
}

