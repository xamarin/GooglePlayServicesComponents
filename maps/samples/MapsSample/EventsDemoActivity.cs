
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

namespace MapsSample
{
    [Activity (Label = "Events Demo")]			
    public class EventsDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        TextView tapTextView;
        TextView cameraTextView;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.events_demo);

            tapTextView = FindViewById<TextView> (Resource.Id.tap_text);
            cameraTextView = FindViewById<TextView> (Resource.Id.camera_text);

            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById (Resource.Id.map);

            mapFragment.GetMapAsync (this);
        }

        public void OnMapReady (GoogleMap googleMap)
        {
            googleMap.MapClick += (sender, e) => {
                tapTextView.Text = "Tapped: Point=" + e.Point;
            };
            googleMap.MapLongClick += (sender, e) => {
                tapTextView.Text = "Long Pressed: Point=" + e.Point;
            };
            googleMap.CameraChange += (sender, e) => {
                cameraTextView.Text = e.Position.ToString ();
            };
        }
    }
}

