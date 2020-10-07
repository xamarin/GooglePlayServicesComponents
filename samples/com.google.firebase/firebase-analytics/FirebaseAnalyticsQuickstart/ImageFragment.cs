using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;

namespace FirebaseAnalyticsQuickstart
{
    /**
 * This fragment displays a featured, specified image.
 */
    public class ImageFragment : Fragment
    {
        const string ARG_PATTERN = "pattern";
        int resId;

        /**
         * Create a ImageFragment displaying the given image.
         */
        public static ImageFragment NewInstance (int resId)
        {
            ImageFragment fragment = new ImageFragment ();
            var args = new Bundle ();
            args.PutInt (ARG_PATTERN, resId);
            fragment.Arguments = args;
            return fragment;
        }


        public override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            if (Arguments != null)
                resId = Arguments.GetInt (ARG_PATTERN);
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate (Resource.Layout.fragment_main, null);
            var imageView = view.FindViewById<ImageView> (Resource.Id.imageView);
            imageView.SetImageResource (resId);

            return view;
        }
    }
}

