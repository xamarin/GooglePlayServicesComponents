using System;
using Android.App;
using Android.Widget;
using System.Text;

namespace AppInviteSample
{
    public class DeepLinkActivity : Activity
    {
        TextView textViewInfo;

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.DeepLink);

            textViewInfo = FindViewById<TextView> (Resource.Id.textViewInfo);


            var info = new StringBuilder ();
            info.AppendLine (string.Format ("ACTION: {0}", Intent.Action));
            info.AppendLine (string.Format ("DATA:   {0}", Intent.DataString));
            info.AppendLine ();

            foreach (var key in Intent.Extras.KeySet ()) {
                var value = Intent.Extras.Get (key);
                info.AppendLine (string.Format ("{0} ({1}) = {2}",
                    key, value, value.GetType ().Name));                
            }

            textViewInfo.Text = info.ToString ();
        }
    }
}

