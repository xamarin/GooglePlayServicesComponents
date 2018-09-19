using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Preferences;
using Android.Views;
using Android.Content;
using Java.Util;
using Android.Support.V4.App;
using Firebase.Analytics;

namespace FirebaseAnalyticsQuickstart
{
    [Activity (Label = "Firebase Analytics Quickstart", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class MainActivity : AppCompatActivity
    {
        const string TAG = "MainActivity";
        const string KEY_FAVORITE_FOOD = "favorite_food";

        static readonly ImageInfo [] IMAGE_INFOS = new ImageInfo[] {
            new ImageInfo (Resource.Drawable.favorite, Resource.String.pattern1_title, Resource.String.pattern1_id),
            new ImageInfo (Resource.Drawable.flash, Resource.String.pattern2_title, Resource.String.pattern2_id),
            new ImageInfo (Resource.Drawable.face, Resource.String.pattern3_title, Resource.String.pattern3_id),
            new ImageInfo (Resource.Drawable.whitebalance, Resource.String.pattern4_title, Resource.String.pattern4_id),
        };

        /**
         * PagerAdapter that will provide fragments for each image.
         * This uses a FragmentPagerAdapter, which keeps every loaded fragment in memory.
         */
        ImagePagerAdapter imagePagerAdapter;

        /**
         * The ViewPager that will host the patterns.
         */
        ViewPager viewPager;

        /**
         * The FirebaseAnalytics used to record screen views.
         */
        // [START declare_analytics]
        FirebaseAnalytics firebaseAnalytics;
        // [END declare_analytics]

        /**
         * The user's favorite food, chosen from a dialog.
         */
        string favoriteFood;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            
            SetContentView (Resource.Layout.activity_main);

            if (GetString (Resource.String.google_app_id) == "YOUR-APP-ID")
                throw new System.Exception ("Invalid google-services.json file.  Make sure you've downloaded your own config file and added it to your app project with the 'GoogleServicesJson' build action.");

            // [START shared_app_measurement]
            // Obtain the FirebaseAnalytics instance.
            firebaseAnalytics = FirebaseAnalytics.GetInstance (this);
            // [END shared_app_measurement]

            // On first app open, ask the user his/her favorite food. Then set this as a user property
            // on all subsequent opens.
            var userFavoriteFood = GetUserFavoriteFood ();
            if (userFavoriteFood == null)
                AskFavoriteFood ();
            else
                SetUserFavoriteFood (userFavoriteFood);

            // Create the adapter that will return a fragment for each image.
            imagePagerAdapter = new ImagePagerAdapter (SupportFragmentManager, IMAGE_INFOS, this);

            // Set up the ViewPager with the pattern adapter.
            viewPager = FindViewById<ViewPager> (Resource.Id.pager);
            viewPager.Adapter = imagePagerAdapter;

            // When the visible image changes, send a screen view hit.
            viewPager.PageSelected += (sender, e) => {
                RecordImageView ();
            };

            // Send initial screen screen view hit.
            RecordImageView ();
        }

        /**
         * Display a dialog prompting the user to pick a favorite food from a list, then record
         * the answer.
         */
        void AskFavoriteFood ()
        {
            var choices = Resources.GetStringArray (Resource.Array.food_items);
            var ad = new Android.Support.V7.App.AlertDialog.Builder (this)
                .SetCancelable (false)
                .SetTitle (Resource.String.food_dialog_title)
                .SetItems (choices, (sender, e) => {
                    var food = choices[e.Which];
                    SetUserFavoriteFood (food);
            }).Create ();

            ad.Show();
        }

        /**
         * Get the user's favorite food from shared preferences.
         * returns favorite food, as a string.
         */
        string GetUserFavoriteFood ()
        {
            return PreferenceManager.GetDefaultSharedPreferences (this)
                    .GetString (KEY_FAVORITE_FOOD, null);
        }

        /**
         * Set the user's favorite food as an app measurement user property and in shared preferences.
         */
        void SetUserFavoriteFood (string food)
        {
            Android.Util.Log.Debug (TAG, "setFavoriteFood: " + food);
            favoriteFood = food;

            PreferenceManager.GetDefaultSharedPreferences (this).Edit ()
                    .PutString (KEY_FAVORITE_FOOD, food)
                    .Apply ();

            // [START user_property]
            firebaseAnalytics.SetUserProperty ("favorite_food", favoriteFood);
            // [END user_property]
        }

        public override bool OnCreateOptionsMenu (IMenu menu)
        {
            MenuInflater.Inflate (Resource.Menu.main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected (IMenuItem item)
        {
            switch (item.ItemId) {
            case Resource.Id.menu_share:
                var name = GetCurrentImageTitle ();
                var text = "I'd love you to hear about " + name;

                var sendIntent = new Intent ();
                sendIntent.SetAction (Intent.ActionSend);
                sendIntent.PutExtra (Intent.ExtraText, text);
                sendIntent.SetType ("text/plain");
                StartActivity (sendIntent);

                // [START custom_event]
                var p = new Bundle ();
                p.PutString ("image_name", name);
                p.PutString ("full_text", text);

                firebaseAnalytics.LogEvent ("share_image", p);
                // [END custom_event]
                break;
            }
            return false;
        }

        /**
         * Return the title of the currently displayed image.
         */
        string GetCurrentImageTitle ()
        {
            var position = viewPager.CurrentItem;
            var info = IMAGE_INFOS [position];
            return GetString (info.Title);
        }

        /**
         * Return the id of the currently displayed image.
         */
        string GetCurrentImageId ()
        {
            var position = viewPager.CurrentItem;
            var info = IMAGE_INFOS [position];
            return GetString (info.Id);
        }

        /**
         * Record a screen view for the visible ImageFragment displayed
         * inside FragmentPagerAdapter.
         */
        void RecordImageView ()
        {
            var id = GetCurrentImageId ();
            var name = GetCurrentImageTitle ();

            // [START image_view_event]
            var bundle = new Bundle ();
            bundle.PutString (FirebaseAnalytics.Param.ItemId, id);
            bundle.PutString (FirebaseAnalytics.Param.ItemName, name);
            bundle.PutString (FirebaseAnalytics.Param.ContentType, "image");
            firebaseAnalytics.LogEvent (FirebaseAnalytics.Event.SelectContent, bundle);
            // [END image_view_event]
        }

        /**
         * A FragmentPagerAdapter that returns a fragment corresponding to
         * one of the sections/tabs/pages.
         */
        public class ImagePagerAdapter : FragmentPagerAdapter
        {
            public Activity Parent { get; set; }

            ImageInfo [] infos;

            public ImagePagerAdapter (Android.Support.V4.App.FragmentManager fm, ImageInfo [] infos, Activity parent) : base (fm)
            {
                this.infos = infos;
                this.Parent = parent;
            }

            public override Android.Support.V4.App.Fragment GetItem (int position)
            {
                var info = infos [position];
                return ImageFragment.NewInstance (info.Image);
            }

            public override int Count {
                get { return infos.Length; }
            }

            public override Java.Lang.ICharSequence GetPageTitleFormatted (int position)
            {
                if (position < 0 || position >= infos.Length)
                    return null;

                var info = infos [position];
                return new Java.Lang.String (Parent.GetString (info.Title).ToUpperInvariant ());
            }
        }
    }
}
