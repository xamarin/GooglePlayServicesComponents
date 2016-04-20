using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MapsSample
{
    [Activity (Label = "Maps Sample", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : ListActivity
    {
        /**
        * A custom array adapter that shows a {@link FeatureView} containing details about the demo.
        */
        class CustomArrayAdapter : ArrayAdapter<DemoDetails> 
        {                
            public CustomArrayAdapter (Context context, DemoDetails[] demos) 
                : base (context, Resource.Layout.feature, Resource.Id.title, demos)
            {                
            }


            public override View GetView (int position, View convertView, ViewGroup parent) 
            {
                FeatureView featureView;
                if (convertView is FeatureView) {
                    featureView = (FeatureView) convertView;
                } else {
                    featureView = new FeatureView(Context);
                }

                DemoDetails demo = GetItem (position);

                featureView.SetTitleId (demo.TitleId);
                featureView.SetDescriptionId (demo.DescriptionId);


                var title = Context.Resources.GetString (demo.TitleId);
                var description = Context.Resources.GetString (demo.DescriptionId);
                featureView.ContentDescription = title + ". " + description;

                return featureView;
            }
        }

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.main);

            var adapter = new CustomArrayAdapter(this, DemoDetailsList.Demos);

            ListAdapter = adapter;
        }

        public override bool OnCreateOptionsMenu (IMenu menu) 
        {
            // Inflate the menu; this adds items to the action bar if it is present.
            MenuInflater.Inflate (Resource.Menu.activity_main, menu);
            return true;
        }


        public override bool OnOptionsItemSelected (IMenuItem item) 
        {
            // Handle item selection
            if (item.ItemId == Resource.Id.menu_legal) {
                //StartActivity (typeof (LegalInfoActivity));
                return true;
            }
            return base.OnOptionsItemSelected (item);
        }
           
        protected override void OnListItemClick (ListView l, View v, int position, long id) 
        {
            var demo = (DemoDetails)ListAdapter.GetItem (position);
            StartActivity (demo.ActivityType);
        }
    }
}


