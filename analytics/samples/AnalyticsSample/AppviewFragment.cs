
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
    public class AppviewFragment : Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            var v = inflater.Inflate (Resource.Layout.screenview, container, false);

            setupAppview (v, Resource.Id.homePageview, Resource.String.homePath);
            setupAppview (v, Resource.Id.helpPageview, Resource.String.helpPath);

            v.FindViewById<Button> (Resource.Id.pageviewDispatch).Click += (sender, e) => {
                // Manually start a dispatch (Unnecessary if the tracker has a dispatch interval)
                GoogleAnalytics.GetInstance(Activity.BaseContext).DispatchLocalHits ();
            };

            return v;
        }

        void setupAppview (View v, int buttonId, int pathId)
        {
            var pageviewButton = v.FindViewById<Button> (buttonId);

            var path = GetString (pathId);
            var sendText = GetString (Resource.String.sendPrefix) + path;

            pageviewButton.Text = sendText;

            pageviewButton.Click += (sender, e) =>  {
                Tracker t = AnalyticsSampleApp.GetTracker (AnalyticsSampleApp.TrackerName.AppTracker);

                t.SetScreenName (path);
                t.Send (new HitBuilders.AppViewBuilder ().Build ());
            };
        }
    }
}

