
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
using Android.Gms.Common;
using Android.Gms.Plus;

namespace PlusSample
{
    [Activity (Label = "Share")]			
    public class ShareActivity : Activity, IDialogInterfaceOnCancelListener
    {
        const string TAG = "ShareActivity";

        const string STATE_SHARING = "state_sharing";

        const int DIALOG_GET_GOOGLE_PLAY_SERVICES = 1;

        const int REQUEST_CODE_INTERACTIVE_POST = 1;
        const int REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES = 2;

        /** The button should say "View item" in English. */
        const string LABEL_VIEW_ITEM = "VIEW_ITEM";

        EditText mEditSendText;

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);

            SetContentView (Resource.Layout.share_activity);

            var sendButton = FindViewById<Button>(Resource.Id.send_interactive_button);
            sendButton.Click += (sender, e) => {
                StartActivityForResult (getInteractivePostIntent(), REQUEST_CODE_INTERACTIVE_POST);
            };

            mEditSendText = FindViewById<EditText> (Resource.Id.share_prefill_edit);
            var available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
            if (available != ConnectionResult.Success)
                ShowDialog (DIALOG_GET_GOOGLE_PLAY_SERVICES);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
                ActionBar.SetDisplayHomeAsUpEnabled (true);
        }

        protected override Dialog OnCreateDialog (int id) 
        {
            if (id != DIALOG_GET_GOOGLE_PLAY_SERVICES)
                return base.OnCreateDialog (id);

            int available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
            if (available == ConnectionResult.Success)
                return null;
            
            if (GooglePlayServicesUtil.IsUserRecoverableError (available)) {
                return GooglePlayServicesUtil.GetErrorDialog (
                    available, this, REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES, this);
            }

            return new AlertDialog.Builder(this)
                .SetMessage (Resource.String.plus_generic_error)
                .SetCancelable (true)
                .SetOnCancelListener (this)
                .Create();
        }


        protected override void OnActivityResult (int requestCode, Result resultCode, Intent intent) 
        {
            switch (requestCode) {
            case REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES:
                if (resultCode != Result.Ok) {
                    Console.WriteLine ("Unable to sign the user in.");
                    Finish();
                }
                break;

            case REQUEST_CODE_INTERACTIVE_POST:
                if (resultCode != Result.Ok) {
                    Console.WriteLine ("Failed to create interactive post");
                }
                break;
            }
        }

        private Intent getInteractivePostIntent() 
        {
            // Create an interactive post with the "VIEW_ITEM" label. This will
            // create an enhanced share dialog when the post is shared on Google+.
            // When the user clicks on the deep link, ParseDeepLinkActivity will
            // immediately parse the deep link, and route to the appropriate resource.
            var action = "/?view=true";
            var callToActionUrl = Android.Net.Uri.Parse (GetString (Resource.String.plus_example_deep_link_url) + action);
            var callToActionDeepLinkId = GetString (Resource.String.plus_example_deep_link_id) + action;

            // Create an interactive post builder.
            var builder = new PlusShare.Builder(this);

            // Set call-to-action metadata.
            builder.AddCallToAction (LABEL_VIEW_ITEM, callToActionUrl, callToActionDeepLinkId);

            // Set the target url (for desktop use).
            builder.SetContentUrl (Android.Net.Uri.Parse (GetString (Resource.String.plus_example_deep_link_url)));

            // Set the target deep-link ID (for mobile use).
            builder.SetContentDeepLinkId (GetString (Resource.String.plus_example_deep_link_id), null, null, null);

            // Set the pre-filled message.
            builder.SetText (mEditSendText.Text.ToString ());

            return builder.Intent;
        }            

        public void OnCancel (IDialogInterface dialog)
        {
            Console.WriteLine ("Unable to sign the user in.");
            Finish ();
        }
            
        public override bool OnOptionsItemSelected (IMenuItem item) 
        {
            switch (item.ItemId) {
            case Android.Resource.Id.Home:
                var intent = new Intent (this, typeof (PlusSampleActivity));
                intent.AddFlags (ActivityFlags.ClearTop);
                StartActivity (intent);
                Finish ();
                return true;

            default:
                return base.OnOptionsItemSelected (item);
            }
        }
    }
}

