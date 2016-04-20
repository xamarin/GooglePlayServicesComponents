
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
    [Activity (Label = "Indoor Demo")]			
    public class IndoorDemoActivity : FragmentActivity, IOnMapReadyCallback
    {
        GoogleMap map;
        bool showLevelPicker = true;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.indoor_demo);

            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById (Resource.Id.map);
            mapFragment.GetMapAsync (this);
        }

        public void OnMapReady (GoogleMap googleMap)
        {
            map = googleMap;
            map.MoveCamera (CameraUpdateFactory.NewLatLngZoom (new LatLng (37.614631, -122.385153), 18));
        }

        [Export ("onToggleLevelPicker")]
        public void OnToggleLevelPicker (View view)
        {
            showLevelPicker = !showLevelPicker;
            map.UiSettings.IndoorLevelPickerEnabled = showLevelPicker;
        }

        [Export ("onFocusedBuildingInfo")]
        public void OnFocusedBuildingInfo (View view)
        {
            var building = map.FocusedBuilding;

            if (building != null) {
                var s = string.Empty;

                foreach (var level in building.Levels) {
                    s+= level.Name + " ";
                }
                if (building.IsUnderground)
                    s += "is underground";
                setText (s);
            } else {
                setText ("No visible building");
            }
        }

        [Export ("onVisibleLevelInfo")]
        public void OnVisibleLevelInfo (View view)
        {
            var building = map.FocusedBuilding;

            if (building != null) {

                if (building.Levels.Any ()) {
                    var level = building.Levels [building.ActiveLevelIndex];

                    setText (level.Name);
                } else {
                    setText ("No visible level");
                }           
            } else {
                setText ("No visible building");
            }
        }

        [Export ("onHigherLevel")]
        public void OnHigherLevel (View view)
        {
            var building = map.FocusedBuilding;

            if (building != null) {

                if (building.Levels.Any ()) {
                    var currentLevel = building.ActiveLevelIndex;

                    var newLevel = currentLevel - 1;
                    if (newLevel < 0)
                        newLevel = building.Levels.Count - 1;

                    var level = building.Levels [newLevel];
                    setText ("Activating level " + level.Name);
                    level.Activate ();
                } else {
                    setText ("No levels in building");
                }
            } else {
                setText ("No visible building");
            }
        }

        void setText (string msg)
        {
            FindViewById<TextView> (Resource.Id.top_text).Text = msg;
        }
    }
}

