
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
    public class TimingFragment : Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            var view = inflater.Inflate (Resource.Layout.timing, container, false);

            view.FindViewById<Button> (Resource.Id.timingSend).Click += (sender, e) => {
                var t = AnalyticsSampleApp.GetTracker (AnalyticsSampleApp.TrackerName.AppTracker);

                var timingCategory = View.FindViewById<EditText> (Resource.Id.editTimingCategory).Text;
                var timingName = View.FindViewById<EditText> (Resource.Id.editTimingName).Text;
                var timingInterval = long.Parse (View.FindViewById<EditText> (Resource.Id.editTimingInterval).Text);
                var timingLabel = View.FindViewById<EditText> (Resource.Id.editTimingLabel).Text;

                t.Send (new HitBuilders.TimingBuilder ()
                    .SetCategory (timingCategory)
                    .SetValue (timingInterval)
                    .SetVariable (timingName)
                    .SetLabel (timingLabel)
                    .Build ());
            };
        
            view.FindViewById<Button> (Resource.Id.timingDispatch).Click += (sender, e) => {
                GoogleAnalytics.GetInstance (Activity.BaseContext).DispatchLocalHits ();
            };

            return view;
        }
    }
}

