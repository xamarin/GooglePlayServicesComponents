using System;
using Android.App;
using System.Collections.Generic;
using Android.Gms.Analytics;

namespace Analytics
{
    public class AnalyticsSampleApp
    {
        const string PROPERTY_ID = "UA-XXXXX-Y";

        public const int GENERAL_TRACKER = 0;

        public enum TrackerName {
            AppTracker,
            GlobalTracker,
            EcommerceTracker
        }

        static Dictionary<TrackerName, Tracker> trackers = new Dictionary<TrackerName, Tracker> ();

        public static Tracker GetTracker (TrackerName trackerId) 
        {
            if (!trackers.ContainsKey (trackerId)) {
                var analytics = GoogleAnalytics.GetInstance (Application.Context);
                analytics.Logger.LogLevel = LoggerLogLevel.Verbose;

                Tracker t;

                if (trackerId == TrackerName.AppTracker)
                    t = analytics.NewTracker (PROPERTY_ID);
                else if (trackerId == TrackerName.GlobalTracker)
                    t = analytics.NewTracker (Resource.Xml.global_tracker);
                else
                    t = analytics.NewTracker (Resource.Xml.ecommerce_tracker);

                t.EnableAdvertisingIdCollection (true);

                trackers.Add (trackerId, t);
            }

            return trackers [trackerId];
        }
    }
}

