
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Gms.Analytics;
using Fragment = Android.Support.V4.App.Fragment;

namespace Analytics
{
    public class EventFragment : Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            var view = inflater.Inflate (Resource.Layout.eventview, container, false);

            setupEvent (view, Resource.Id.video1Play, Resource.String.videoCategory, Resource.String.videoPlay,
                Resource.String.video1);
            setupEvent (view, Resource.Id.video1Pause, Resource.String.videoCategory, Resource.String.videoPause,
                Resource.String.video1);
            setupEvent (view, Resource.Id.video2Play, Resource.String.videoCategory, Resource.String.videoPlay,
                Resource.String.video2);
            setupEvent (view, Resource.Id.video2Pause, Resource.String.videoCategory, Resource.String.videoPause,
                Resource.String.video2);

            setupEvent (view, Resource.Id.book1View, Resource.String.bookCategory, Resource.String.bookView, Resource.String.book1);
            setupEvent (view, Resource.Id.book1Share, Resource.String.bookCategory, Resource.String.bookShare, Resource.String.book1);

            view.FindViewById<Button> (Resource.Id.eventDispatch).Click += (sender, e) => {
                GoogleAnalytics.GetInstance (Activity.ApplicationContext).DispatchLocalHits ();
            };

            return view;
        }

        void setupEvent (View v, int buttonId, int categoryId, int actionId, int labelId)
        {
            v.FindViewById<Button> (buttonId).Click += (sender, e) => {
                var t = AnalyticsSampleApp.GetTracker (AnalyticsSampleApp.TrackerName.AppTracker);

                t.Send (new HitBuilders.EventBuilder ()
                    .SetCategory (GetString (categoryId))
                    .SetAction (GetString (actionId))
                    .SetLabel (GetString (labelId))
                    .Build ());
            };
        }
    }
}

