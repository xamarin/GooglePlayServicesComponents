using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Messaging;
using Firebase.Iid;
using System.Threading.Tasks;

namespace FirebaseMessagingQuickstart
{
    [Activity (Label = "Firebase Messaging Quickstart", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class MainActivity : AppCompatActivity
    {
        const string TAG = "MainActivity";

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.activity_main);

            if (GetString (Resource.String.google_app_id) == "YOUR-APP-ID")
                throw new System.Exception ("Invalid google-services.json file.  Make sure you've downloaded your own config file and added it to your app project with the 'GoogleServicesJson' build action.");

            // If a notification message is tapped, any data accompanying the notification
            // message is available in the intent extras. In this sample the launcher
            // intent is fired when the notification is tapped, so any accompanying data would
            // be handled here. If you want a different intent fired, set the click_action
            // field of the notification message to the desired intent. The launcher intent
            // is used when no click_action is specified.
            //
            // Handle possible data accompanying notification message.
            // [START handle_data_extras]
            if (Intent.Extras != null) {
                foreach (var key in Intent.Extras.KeySet ()) {
                    var value = Intent.Extras.GetString (key);
                    Android.Util.Log.Debug (TAG, "Key: {0} Value: {1}", key, value);
                }
            }
            // [END handle_data_extras]

            var subscribeButton = FindViewById<Button> (Resource.Id.subscribeButton);
            subscribeButton.Click += delegate {
                // [START subscribe_topics]
                FirebaseMessaging.Instance.SubscribeToTopic ("news");
                Android.Util.Log.Debug (TAG, "Subscribed to news topic");
                // [END subscribe_topics]
            };

            var logTokenButton = FindViewById<Button> (Resource.Id.logTokenButton);
            logTokenButton.Click += delegate {
                Android.Util.Log.Debug (TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            };

            //Task.Run (() => {
            //    var instanceID = FirebaseInstanceId.Instance;
            //    instanceID.DeleteInstanceId ();
            //    var iid1 = instanceID.Token;
            //    var iid2 = instanceID.GetToken (GetString (Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope);
            //    Android.Util.Log.Debug (TAG, "Iid1: {0}, iid2: {1}", iid1, iid2);
            //});

            // [END get_token]
        }
    }
}
