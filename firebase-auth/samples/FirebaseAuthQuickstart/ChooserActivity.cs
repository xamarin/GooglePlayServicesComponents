
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace FirebaseAuthQuickstart
{
    [Activity (Label = "Firebase Auth Quickstart", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class ChooserActivity : AppCompatActivity
    {
        static readonly Type[] CLASSES = {
            typeof (GoogleSignInActivity),
            //typeof (FacebookLoginActivity),
            //typeof (TwitterLoginActivity),
            typeof (EmailPasswordActivity),
            typeof (AnonymousAuthActivity),
            typeof (CustomAuthActivity)
        };

        static readonly int [] DESCRIPTION_IDS = {
            Resource.String.desc_google_sign_in,
            Resource.String.desc_facebook_login,
            Resource.String.desc_twitter_login,
            Resource.String.desc_emailpassword,
            Resource.String.desc_anonymous_auth,
            Resource.String.desc_custom_auth,
        };

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.activity_chooser);

            if (GetString (Resource.String.google_app_id) == "YOUR-APP-ID")
                throw new Exception ("Invalid google-services.json file.  Make sure you've downloaded your own config file and added it to your app project with the 'GoogleServicesJson' build action.");

            // Set up ListView and Adapter
            var listView = FindViewById<ListView> (Resource.Id.list_view);

            var adapter = new MyArrayAdapter (this, Android.Resource.Layout.SimpleListItem2, CLASSES);
            adapter.DescriptionIds = DESCRIPTION_IDS;

            listView.Adapter = adapter;
            listView.ItemClick += (sender, e) => {
                var clicked = CLASSES [e.Position];
                StartActivity (clicked);
            };
        }

        public class MyArrayAdapter : BaseAdapter<Type>
        {
            public int [] DescriptionIds { get; set; }

            Context context;
            Type [] classes;

            public MyArrayAdapter (Context context, int resource, Type [] objects) : base ()
            {
                this.context = context;
                classes = objects;
            }

            public override int Count { get {
                    return classes.Length;
                }
            }

            public override Type this [int position] {
                get {
                    return classes [position];
                }
            }

            public override long GetItemId (int position)
            {
                return position;
            }

            public override View GetView (int position, View convertView, ViewGroup parent)
            {
                View view = convertView;

                if (convertView == null) {
                    var inflater = LayoutInflater.FromContext (context);
                    view = inflater.Inflate (Android.Resource.Layout.SimpleListItem2, null);
                }

                view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = classes [position].Name;
                view.FindViewById<TextView> (Android.Resource.Id.Text2).SetText (DescriptionIds [position]);

                return view;
            }
        }
    }
}

