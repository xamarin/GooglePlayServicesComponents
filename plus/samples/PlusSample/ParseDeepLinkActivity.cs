
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Plus;

namespace PlusSample
{    
    [Activity (Label = "Parse Deep Link")]			
    [IntentFilter (new string [] { "com.google.android.apps.plus.VIEW_DEEP_LINK" },
        DataScheme="vnd.google.deeplink",
        Categories=new string[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" })]
    public class ParseDeepLinkActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            var deepLinkId = PlusShare.GetDeepLinkId (this.Intent);
            var target = processDeepLinkId(deepLinkId);
            if (target != null)
                StartActivity (target);

            Finish ();
        }

        /**
     * Get the intent for an activity corresponding to the deep link ID.
     *
     * @param deepLinkId The deep link ID to parse.
     * @return The intent corresponding to the deep link ID.
     */
        Intent processDeepLinkId(String deepLinkId) 
        {
            Intent route;

            var uri = Android.Net.Uri.Parse (deepLinkId);
            if (uri.Path.StartsWith (GetString (Resource.String.plus_example_deep_link_id))) {
                route = new Intent (this, typeof(SignInActivity));

                // Check for the call-to-action query parameter, and perform an action.
                var viewAction = uri.GetQueryParameter ("view");
                if (!string.IsNullOrEmpty (viewAction) && "true".Equals (viewAction)) {
                    Toast.MakeText (this, "Performed a view", ToastLength.Long).Show ();
                }

            } else {
                route = new Intent (this, typeof(PlusSampleActivity));
            }

            return route;
        }
    }
}

