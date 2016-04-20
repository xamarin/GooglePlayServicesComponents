
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
    public class SocialFragment : Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            var view = inflater.Inflate (Resource.Layout.social, container, false);

            view.FindViewById<Button> (Resource.Id.socialSend).Click += (sender, e) => {
                try {
                    var t = AnalyticsSampleApp.GetTracker (AnalyticsSampleApp.TrackerName.AppTracker);

                    var socialNetwork = View.FindViewById<EditText> (Resource.Id.editSocialNetwork).Text;
                    if (string.IsNullOrWhiteSpace (socialNetwork))
                        throw new Exception (GetString (Resource.String.socialNetworkWarning));

                    var socialAction = View.FindViewById<EditText> (Resource.Id.editSocialAction).Text;
                    if (string.IsNullOrWhiteSpace (socialAction))
                        throw new Exception (GetString (Resource.String.socialActionWarning));

                    var socialTarget = View.FindViewById<EditText> (Resource.Id.editSocialTarget).Text;

                    t.Send (new HitBuilders.SocialBuilder ()
                        .SetNetwork (socialNetwork)
                        .SetAction (socialAction)
                        .SetTarget (socialTarget)
                        .Build ());

                } catch (Exception ex) {
                    Console.WriteLine (ex);
                }
            };

            view.FindViewById<Button> (Resource.Id.socialDispatch).Click += (sender, e) => {
                GoogleAnalytics.GetInstance (Activity.BaseContext).DispatchLocalHits ();
            };

            return view;
        }
    }
}

