
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

namespace PlusSample
{
    [Activity (Label = "Plus Sample", MainLauncher=true)]			
    public class PlusSampleActivity : ListActivity
    {
        static Dictionary<string, Type> SAMPLES_MAP;
        PlusSamplesAdapter adapter;

        static PlusSampleActivity ()
        {
            SAMPLES_MAP = new Dictionary<string, Type> ();
            SAMPLES_MAP.Add ("Sign in", typeof (SignInActivity));
            SAMPLES_MAP.Add ("+1", typeof (PlusOneActivity));
            SAMPLES_MAP.Add ("Send interactive post", typeof (ShareActivity));
            SAMPLES_MAP.Add ("Write moments", typeof (MomentActivity));
            SAMPLES_MAP.Add ("List & remove moments", typeof (ListMomentsActivity));
            SAMPLES_MAP.Add ("List visible people (circled by you)", typeof (ListVisiblePeopleActivity));
            SAMPLES_MAP.Add ("List connected people", typeof (ListConnectedPeopleActivity));
            SAMPLES_MAP.Add ("License info", typeof (LicenseActivity));
        }
            
        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            adapter = new PlusSamplesAdapter {
                Items = SAMPLES_MAP,
                Parent = this
            };
            ListAdapter = adapter;

            ListView.ItemClick += (sender, e) => {

                var item = adapter.Items.ElementAt (e.Position);

                var sampleIntent = new Intent (Intent.ActionMain);
                sampleIntent.SetClass (ApplicationContext, item.Value);

                StartActivity (sampleIntent);
            };
        }
            
        public override bool OnCreateOptionsMenu (IMenu menu) 
        {
            MenuInflater.Inflate (Resource.Menu.main_activity_menu, menu);
            return true;
        }
            
        public override bool OnOptionsItemSelected (IMenuItem item) 
        {
            int itemId = item.ItemId;
            if (itemId == Resource.Id.change_locale) {
                var intent = new Intent (Intent.ActionMain);
                intent.SetAction (Android.Provider.Settings.ActionLocaleSettings);
                intent.AddCategory (Intent.CategoryDefault);
                StartActivity (intent);
                return true;
            }
            return base.OnOptionsItemSelected (item);
        }

        class PlusSamplesAdapter : BaseAdapter<KeyValuePair<string, Type>>
        {
            public Activity Parent { get; set; }
            public Dictionary<string, Type> Items { get; set; }

            public override long GetItemId (int position)
            {
                return position;
            }

            public override View GetView (int position, View convertView, ViewGroup parent)
            {
                var item = Items.ElementAt (position);

                var view = convertView ??
                    Parent.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, parent, false);

                view.FindViewById<TextView> (Android.Resource.Id.Text1)
                    .Text = item.Key;

                return view;
            }

            public override int Count {
                get {
                    return Items.Count;
                }
            }

            public override KeyValuePair<string, Type> this [int index] {
                get {
                    return Items.ElementAt (index);
                }
            }
        }
    }
}

