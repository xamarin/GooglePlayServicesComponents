
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
using Fragment = Android.Support.V4.App.Fragment;
using Android.Gms.Analytics;

namespace Analytics
{
    public class ExceptionFragment : Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            var view = inflater.Inflate (Resource.Layout.exception, container, false);

            view.FindViewById<Button> (Resource.Id.btnDispatch).Click += (sender, e) => {
                GoogleAnalytics.GetInstance (Activity.ApplicationContext).DispatchLocalHits();
            };

            view.FindViewById<Button> (Resource.Id.trackBtn).Click += (sender, e) => {

                var fatal = View.FindViewById<CheckBox> (Resource.Id.isFatalChk).Checked;
                var location = View.FindViewById<EditText> (Resource.Id.exceptionLocationEdit).Text;
                var method = View.FindViewById<EditText> (Resource.Id.exceptionMethodEdit).Text;

                var t = AnalyticsSampleApp.GetTracker (AnalyticsSampleApp.TrackerName.AppTracker);

                t.Send(new HitBuilders.ExceptionBuilder()
                    .SetDescription (method + ": " + location)
                    .SetFatal (fatal)
                    .Build());
            };

            return view;
        }
    }
}

