
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
    [Activity (Label = "Ground Overlay Demo")]			
    public class GroundOverlayDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        const int TRANSPARENCY_MAX = 100;
        static readonly LatLng NEWARK = new LatLng (40.714086, -74.228697);

        List<BitmapDescriptor> images = new List<BitmapDescriptor> ();

        GroundOverlay groundOverlay;
        SeekBar transparencyBar;

        int currentEntry = 0;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.ground_overlay_demo);

            transparencyBar = FindViewById<SeekBar> (Resource.Id.transparencySeekBar);
            transparencyBar.Max = TRANSPARENCY_MAX;
            transparencyBar.Progress = 0;

            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById (Resource.Id.map);

            mapFragment.GetMapAsync (this);
        }

        public void OnMapReady (GoogleMap googleMap)
        {
            googleMap.MoveCamera (CameraUpdateFactory.NewLatLngZoom (NEWARK, 11));

            images.Clear ();
            images.Add (BitmapDescriptorFactory.FromResource (Resource.Drawable.newark_nj_1922));
            images.Add (BitmapDescriptorFactory.FromResource (Resource.Drawable.newark_prudential_sunny));

            currentEntry = 0;

            groundOverlay = googleMap.AddGroundOverlay (new GroundOverlayOptions ()
                .InvokeImage (images [currentEntry])
                .Anchor (0, 1)
                .Position (NEWARK, 8600f, 6500f));

            transparencyBar.ProgressChanged += (sender, e) => {
                if (groundOverlay != null)
                    groundOverlay.Transparency = (float)e.Progress / (float)TRANSPARENCY_MAX;                    
            };

            googleMap.SetContentDescription ("Google Map with ground overlay.");
        }

        [Export ("switchImage")]
        public void SwitchImage (View view)
        {
            currentEntry = (currentEntry + 1) % images.Count;
            groundOverlay.SetImage (images [currentEntry]);
        }
    }
}

