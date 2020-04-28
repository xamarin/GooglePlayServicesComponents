
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
using Fragment = AndroidX.Fragment.App.Fragment;
using Android.Gms.Analytics;

namespace Analytics
{
    public class IssuesFragment : Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            var view = inflater.Inflate (Resource.Layout.issues, container, false);

            view.FindViewById<Button> (Resource.Id.buttonIssue213).Click +=
                (sender, e) =>
                {
                    try
                    {
                        ForceError();
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText (Activity, ex.Message, ToastLength.Short).Show ();
                    }

                };

            return view;
        }

        private static void ForceError()
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "k", "v" },
            };

            GoogleAnalytics analytics = GoogleAnalytics.GetInstance(Application.Context);
            Android.Gms.Analytics.Tracker t = analytics.NewTracker("aaa");
            t.Send(data);

            return;
        }
    }
}

