
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
    [Activity (Label = "Split StreetView Panorama And Map Demo")]			
    public class SplitStreetViewPanoramaAndMapDemoActivity : FragmentActivity, IOnStreetViewPanoramaReadyCallback, IOnMapReadyCallback
    {
        const string MARKER_POSITION_KEY = "MarkerPosition";

        // George St, Sydney
        static readonly LatLng SYDNEY = new LatLng(-33.87365, 151.20689);

        StreetViewPanorama mStreetViewPanorama;
        Marker mMarker;
        LatLng markerPosition;

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.split_street_view_panorama_and_map_demo);

            if (savedInstanceState == null)
                markerPosition = SYDNEY;
            else
                markerPosition = savedInstanceState.GetParcelable (MARKER_POSITION_KEY).JavaCast<LatLng> ();
            
            var streetViewPanoramaFragment =
                (SupportStreetViewPanoramaFragment)
                SupportFragmentManager.FindFragmentById (Resource.Id.streetviewpanorama);
            
            streetViewPanoramaFragment.GetStreetViewPanoramaAsync (this);

            SupportMapFragment mapFragment =
                (SupportMapFragment) SupportFragmentManager.FindFragmentById (Resource.Id.map);
            mapFragment.GetMapAsync (this);
//                @Override
//                public void onMapReady(GoogleMap map) {
//                    map.setOnMarkerDragListener(SplitStreetViewPanoramaAndMapDemoActivity.this);
//                    // Creates a draggable marker. Long press to drag.
//                    mMarker = map.addMarker(new MarkerOptions()
//                        .position(markerPosition)
//                        .icon(BitmapDescriptorFactory.fromResource(R.drawable.pegman))
//                        .draggable(true));
//                }
//            });
        }

        public void OnStreetViewPanoramaReady (StreetViewPanorama panorama)
        {
            mStreetViewPanorama = panorama;
            mStreetViewPanorama.SetPosition (markerPosition);
            mStreetViewPanorama.StreetViewPanoramaChange += (sender, e) => {
                if (e.Location != null)
                    mMarker.Position = e.Location.Position;
            };
        }

        public void OnMapReady (GoogleMap googleMap)
        {
            googleMap.MarkerDragEnd += (sender, e) => {
                mStreetViewPanorama.SetPosition (e.Marker.Position, 150);
            };
                                
            // Creates a draggable marker. Long press to drag.
            mMarker = googleMap.AddMarker (new MarkerOptions()
                .SetPosition (markerPosition)
                .SetIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.pegman))
                .Draggable(true));
        }

        protected override void OnSaveInstanceState (Bundle outState) 
        {
            base.OnSaveInstanceState(outState);
            outState.PutParcelable (MARKER_POSITION_KEY, mMarker.Position);
        }
    }
}

