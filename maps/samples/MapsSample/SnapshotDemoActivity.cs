
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
    [Activity (Label = "SnapshotDemoActivity")]			
    public class SnapshotDemoActivity : FragmentActivity, IOnMapReadyCallback, GoogleMap.IOnMapLoadedCallback, GoogleMap.ISnapshotReadyCallback
    {
        GoogleMap map;

        CheckBox waitForMapLoadCheckBox;

        protected void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.snapshot_demo);

            waitForMapLoadCheckBox = FindViewById<CheckBox> (Resource.Id.wait_for_map_load);

            var mapFragment =
                (SupportMapFragment) SupportFragmentManager.FindFragmentById (Resource.Id.map);
            mapFragment.GetMapAsync (this);
        }
            
        public void OnMapReady (GoogleMap map) 
        {
            map = map;
        }

        [Export ("onScreenshot")]
        public void OnScreenshot(View view) 
        {
            takeSnapshot ();
        }

        private void takeSnapshot() 
        {
            if (map == null)
                return;

            if (waitForMapLoadCheckBox.Checked) {
                map.SetOnMapLoadedCallback (this);
            } else {
                map.Snapshot (this);
            }
        }
            
        [Export ("onClearScreenshot")]
        public void OnClearScreenshot (View view) 
        {
            var snapshotHolder = FindViewById<ImageView>(Resource.Id.snapshot_holder);
            snapshotHolder.SetImageDrawable (null);
        }

        public void OnMapLoaded ()
        {
            map.Snapshot (this);
        }

        public void OnSnapshotReady (Android.Graphics.Bitmap snapshot)
        {
            var snapshotHolder = FindViewById<ImageView>(Resource.Id.snapshot_holder);
            snapshotHolder.SetImageBitmap (snapshot);
        }
    }
}

