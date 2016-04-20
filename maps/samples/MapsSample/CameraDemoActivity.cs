
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
using Java.Interop;

namespace MapsSample
{
    [Activity (Label = "Camera Demo")]			
    public class CameraDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        const int SCROLL_BY_PX = 100;

        public static readonly CameraPosition BONDI =
            new CameraPosition.Builder().Target(new LatLng(-33.891614, 151.276417))
                .Zoom(15.5f)
                .Bearing(300)
                .Tilt(50)
                .Build();

        public static readonly CameraPosition SYDNEY =
            new CameraPosition.Builder().Target(new LatLng(-33.87365, 151.20689))
                .Zoom(15.5f)
                .Bearing(0)
                .Tilt(25)
                .Build();

        private GoogleMap map;

        private CompoundButton animateToggle;
        private CompoundButton customDurationToggle;
        private SeekBar customDurationBar;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.camera_demo);

            animateToggle = FindViewById<CompoundButton> (Resource.Id.animate);
            customDurationToggle = FindViewById<CompoundButton> (Resource.Id.duration_toggle);
            customDurationBar = FindViewById<SeekBar> (Resource.Id.duration_bar);

            updateEnabledState ();

            var mapFragment =
                (SupportMapFragment) SupportFragmentManager.FindFragmentById (Resource.Id.map);
            mapFragment.GetMapAsync (this);
        }

        public void OnMapReady (GoogleMap googleMap)
        {
            map = googleMap;

            map.UiSettings.ZoomControlsEnabled = false;

            map.MoveCamera (CameraUpdateFactory.NewLatLngZoom (new LatLng (-33.87365, 151.20689), 10));
        }

        bool CheckReady ()
        {
            if (map == null) {
                Toast.MakeText (this, Resource.String.map_not_ready, ToastLength.Short).Show ();
                return false;
            }

            return true;
        }

        [Export ("onGoToBondi")]
        public void OnGoToBondi (View view) 
        {
            if (!CheckReady ())
                return;

            ChangeCamera (CameraUpdateFactory.NewCameraPosition (BONDI));
        }

        [Export ("onGoToSydney")]
        public void OnGoToSydney (View view)
        {
            if (!CheckReady ())
                return;

            ChangeCamera (CameraUpdateFactory.NewCameraPosition (SYDNEY), 
                new MyCancelableCallback (() => {
                    Toast.MakeText (BaseContext, "Animation to Sydney canceled", ToastLength.Short).Show ();
                }, () => {
                    Toast.MakeText (BaseContext, "Animation to Sydney complete", ToastLength.Short).Show ();
                }));

        }

        [Export ("onStopAnimation")]
        public void OnStopAnimation (View view)
        {
            if (!CheckReady ())
                return;

            map.StopAnimation ();
        }

        [Export ("onZoomIn")]
        public void OnZoomIn (View view)
        {
            if (!CheckReady ())
                return;

            ChangeCamera (CameraUpdateFactory.ZoomIn ());
        }

        [Export ("onZoomOut")]
        public void OnZoomOut (View view)
        {
            if (!CheckReady ())
                return;

            ChangeCamera (CameraUpdateFactory.ZoomOut ());
        }

        [Export ("onTiltMore")]
        public void OnTiltMore (View view) 
        {
            if (!CheckReady ())
                return;

            var currentCameraPosition = map.CameraPosition;
            var currentTilt = currentCameraPosition.Tilt;
            var newTilt = currentTilt + 10;

            newTilt = (newTilt > 90) ? 90 : newTilt;

            var cameraPosition = new CameraPosition.Builder (currentCameraPosition)
                .Tilt (newTilt).Build ();

            ChangeCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
        }

        [Export ("onTiltLess")]
        public void OnTiltLess (View view) 
        {
            if (!CheckReady ())
                return;

            var currentCameraPosition = map.CameraPosition;
            var currentTilt = currentCameraPosition.Tilt;
            var newTilt = currentTilt - 10;
            newTilt = (newTilt > 0) ? newTilt : 0;

            var cameraPosition = new CameraPosition.Builder (currentCameraPosition)
                .Tilt (newTilt).Build ();

            ChangeCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
        }
                   
        [Export ("onScrollLeft")]
        public void OnScrollLeft (View view) 
        {
            if (!CheckReady ())
                return;

            ChangeCamera (CameraUpdateFactory.ScrollBy (-SCROLL_BY_PX, 0));
        }

        [Export ("onScrollRight")]
        public void OnScrollRight (View view) 
        {
            if (!CheckReady ())
                return;

            ChangeCamera (CameraUpdateFactory.ScrollBy (SCROLL_BY_PX, 0));
        }

        [Export ("onScrollUp")]
        public void OnScrollUp (View view) 
        {
            if (!CheckReady ())
                return;

            ChangeCamera (CameraUpdateFactory.ScrollBy (0, -SCROLL_BY_PX));
        }

        [Export ("onScrollDown")]
        public void OnScrollDown (View view) 
        {
            if (!CheckReady())
                return;

            ChangeCamera (CameraUpdateFactory.ScrollBy (0, SCROLL_BY_PX));
        }

        [Export ("onToggleAnimate")]
        public void OnToggleAnimate (View view) 
        {
            updateEnabledState();
        }

        [Export ("onToggleCustomDuration")]
        public void OnToggleCustomDuration (View view) 
        {
            updateEnabledState();
        }

        void updateEnabledState() 
        {
            customDurationToggle.Enabled = animateToggle.Checked;
            customDurationBar.Enabled = animateToggle.Checked && customDurationToggle.Checked;
        }

        void ChangeCamera (CameraUpdate update, GoogleMap.ICancelableCallback callback = null) 
        {
            if (animateToggle.Checked) {
                if (customDurationToggle.Checked) {
                    var duration = customDurationBar.Progress;

                    map.AnimateCamera (update, Math.Max (duration, 1), callback);
                } else {
                    map.AnimateCamera (update, callback);
                }
            } else {
                map.MoveCamera (update);
            }
        }

        class MyCancelableCallback : Java.Lang.Object, GoogleMap.ICancelableCallback
        {
            public MyCancelableCallback (Action cancelHandler, Action finishHandler)
            {
                CancelHandler = cancelHandler;
                FinishHandler = finishHandler;
            }

            public Action CancelHandler { get; private set; }
            public Action FinishHandler { get; private set; }

            public void OnCancel ()
            {
                var h = CancelHandler;
                if (h != null)
                    h ();
            }

            public void OnFinish ()
            {
                var h = FinishHandler;
                if (h != null)
                    h ();
            }
        }
    }
}

