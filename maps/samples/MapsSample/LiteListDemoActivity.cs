
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
    [Activity (Label = "Lite List Demo")]			
    public class LiteListDemoActivity : FragmentActivity
    {
        Android.Support.V4.App.ListFragment list;
        MapAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView (Resource.Layout.lite_list_demo);

            // Set a custom list adapter for a list of locations
            adapter = new MapAdapter (this, LIST_LOCATIONS);
            list = SupportFragmentManager.FindFragmentById (Resource.Id.list).JavaCast<Android.Support.V4.App.ListFragment> ();
            list.ListAdapter = adapter;

            // Set a RecyclerListener to clean up MapView from ListView
            list.ListView.Recycler += (sender, e) => {
                var holder = (ViewHolder) e.View.Tag;
                if (holder != null && holder.Map != null) {
                    // Clear the map and free up resources by changing the map type to none
                    holder.Map.Clear();
                    holder.Map.MapType = GoogleMap.MapTypeNone;
                }
            };

        }
            
        private class MapAdapter : ArrayAdapter<NamedLocation> 
        {
            HashSet<MapView> mMaps = new HashSet<MapView>();

            public MapAdapter(Context context, NamedLocation[] locations) 
                : base(context, Resource.Layout.lite_list_demo_row, Resource.Id.lite_listrow_text, locations)
            {
            }
                           
            public override View GetView (int position, View convertView, ViewGroup parent) 
            {
                var row = convertView;
                ViewHolder holder;

                // Check if a view can be reused, otherwise inflate a layout and set up the view holder
                if (row == null) {
                    // Inflate view from layout file
                    row = LayoutInflater.From (Context).Inflate (Resource.Layout.lite_list_demo_row, null);

                    // Set up holder and assign it to the View
                    holder = new ViewHolder();
                    holder.Context = Context;
                    holder.MapView = row.FindViewById<MapView> (Resource.Id.lite_listrow_map);
                    holder.Title = row.FindViewById<TextView> (Resource.Id.lite_listrow_text);
                    // Set holder as tag for row for more efficient access.
                    row.Tag = holder;

                    // Initialise the MapView
                    holder.InitializeMapView ();

                    // Keep track of MapView
                    mMaps.Add (holder.MapView);
                } else {
                    // View has already been initialised, get its holder
                    holder = (ViewHolder) row.Tag;
                }

                // Get the NamedLocation for this item and attach it to the MapView
                NamedLocation item = GetItem (position);
                holder.MapView.Tag = item;

                // Ensure the map has been initialised by the on map ready callback in ViewHolder.
                // If it is not ready yet, it will be initialised with the NamedLocation set as its tag
                // when the callback is received.
                if (holder.Map != null) {
                    // The map is already ready to be used
                    setMapLocation(holder.Map, item);
                }

                // Set the text label for this item
                holder.Title.Text = item.Name;

                return row;
            }
        }

        /**
     * Displays a {@link LiteListDemoActivity.NamedLocation} on a
     * {@link com.google.android.gms.maps.GoogleMap}.
     * Adds a marker and centers the camera on the NamedLocation with the normal map type.
     *
     * @param map
     * @param data
     */
        private static void setMapLocation(GoogleMap map, NamedLocation data) {
            // Add a marker for this item and set the camera
            map.MoveCamera (CameraUpdateFactory.NewLatLngZoom(data.Location, 13f));
            map.AddMarker (new MarkerOptions().SetPosition (data.Location));

            // Set the map type back to normal.
            map.MapType = GoogleMap.MapTypeNormal;
        }

        /**
     * Holder for Views used in the {@link LiteListDemoActivity.MapAdapter}.
     * Once the  the <code>map</code> field is set, otherwise it is null.
     * When the {@link #onMapReady(com.google.android.gms.maps.GoogleMap)} callback is received and
     * the {@link com.google.android.gms.maps.GoogleMap} is ready, it stored in the {@link #map}
     * field. The map is then initialised with the NamedLocation that is stored as the tag of the
     * MapView. This ensures that the map is initialised with the latest data that it should
     * display.
     */
        class ViewHolder : Java.Lang.Object, IOnMapReadyCallback 
        {            
            public Context Context { get; set; }
            public MapView MapView { get; set; }
            public TextView Title { get; set; }
            public GoogleMap Map { get; set; }

            public void OnMapReady(GoogleMap googleMap) 
            {
                MapsInitializer.Initialize (Context);
                Map = googleMap;
                var data = (NamedLocation) MapView.Tag;
                if (data != null)
                    setMapLocation(Map, data);
            }

            /**
         * Initialises the MapView by calling its lifecycle methods.
         */
            public void InitializeMapView () 
            {
                if (MapView != null) {
                    // Initialise the MapView
                    MapView.OnCreate(null);
                    // Set the map ready callback to receive the GoogleMap object
                    MapView.GetMapAsync (this);
                }
            }

        }


        /**
     * Location represented by a position ({@link com.google.android.gms.maps.model.LatLng} and a
     * name ({@link java.lang.String}).
     */
        class NamedLocation : Java.Lang.Object
        {
            public string Name { get; private set; }
            public LatLng Location { get; private set; }

            public NamedLocation(string name, LatLng location) 
            {
                Name = name;
                Location = location;
            }
        }

        /**
     * A list of locations to show in this ListView.
     */
        static NamedLocation[] LIST_LOCATIONS = new NamedLocation[] {
            new NamedLocation("Cape Town", new LatLng(-33.920455, 18.466941)),
            new NamedLocation("Beijing", new LatLng(39.937795, 116.387224)),
            new NamedLocation("Bern", new LatLng(46.948020, 7.448206)),
            new NamedLocation("Breda", new LatLng(51.589256, 4.774396)),
            new NamedLocation("Brussels", new LatLng(50.854509, 4.376678)),
            new NamedLocation("Copenhagen", new LatLng(55.679423, 12.577114)),
            new NamedLocation("Hannover", new LatLng(52.372026, 9.735672)),
            new NamedLocation("Helsinki", new LatLng(60.169653, 24.939480)),
            new NamedLocation("Hong Kong", new LatLng(22.325862, 114.165532)),
            new NamedLocation("Istanbul", new LatLng(41.034435, 28.977556)),
            new NamedLocation("Johannesburg", new LatLng(-26.202886, 28.039753)),
            new NamedLocation("Lisbon", new LatLng(38.707163, -9.135517)),
            new NamedLocation("London", new LatLng(51.500208, -0.126729)),
            new NamedLocation("Madrid", new LatLng(40.420006, -3.709924)),
            new NamedLocation("Mexico City", new LatLng(19.427050, -99.127571)),
            new NamedLocation("Moscow", new LatLng(55.750449, 37.621136)),
            new NamedLocation("New York", new LatLng(40.750580, -73.993584)),
            new NamedLocation("Oslo", new LatLng(59.910761, 10.749092)),
            new NamedLocation("Paris", new LatLng(48.859972, 2.340260)),
            new NamedLocation("Prague", new LatLng(50.087811, 14.420460)),
            new NamedLocation("Rio de Janeiro", new LatLng(-22.90187, -43.232437)),
            new NamedLocation("Rome", new LatLng(41.889998, 12.500162)),
            new NamedLocation("Sao Paolo", new LatLng(-22.863878, -43.244097)),
            new NamedLocation("Seoul", new LatLng(37.560908, 126.987705)),
            new NamedLocation("Stockholm", new LatLng(59.330650, 18.067360)),
            new NamedLocation("Sydney", new LatLng(-33.873651, 151.2068896)),
            new NamedLocation("Taipei", new LatLng(25.022112, 121.478019)),
            new NamedLocation("Tokyo", new LatLng(35.670267, 139.769955)),
            new NamedLocation("Tulsa Oklahoma", new LatLng(36.149777, -95.993398)),
            new NamedLocation("Vaduz", new LatLng(47.141076, 9.521482)),
            new NamedLocation("Vienna", new LatLng(48.209206, 16.372778)),
            new NamedLocation("Warsaw", new LatLng(52.235474, 21.004057)),
            new NamedLocation("Wellington", new LatLng(-41.286480, 174.776217)),
            new NamedLocation("Winnipeg", new LatLng(49.875832, -97.150726))
        };
    }
}

