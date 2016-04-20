using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Gcm.Iid;
using Android.Gms.Gcm;

// @PACKAGE_NAME@ will inject your own package name into the value
[assembly: UsesPermission (Android.Manifest.Permission.GetAccounts)]

namespace GCMSample
{
    [Activity (Label = "GCMSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        const string TAG = "GCMSAMPLE";

        TextView textToken;
        EditText textMessage;
        Button buttonToken;
        Button buttonSend;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            textToken = FindViewById<TextView> (Resource.Id.textToken);
            textMessage = FindViewById<EditText> (Resource.Id.editTextSendMsg);
            buttonToken = FindViewById<Button> (Resource.Id.buttonToken);
            buttonSend = FindViewById<Button> (Resource.Id.buttonSend);

            buttonToken.Click += async delegate {
                MyRegistrationService.Register (this);
            };

            if (MyRegistrationService.GCM_SENDER_ID == "YOUR-SENDER-ID")
                throw new Exception ("You must change MyGcmListenerService.GCM_SENDER_ID const to the value of your own Google Developer Console Project # which has Cloud Messaging For Android API enabled, and an OAUTH and Android Key setup in the Developer Console");            
        }

        protected override void OnStart ()
        {
            base.OnStart ();

            MyRegistrationService.TokenRefreshed += TokenUpdated;
        }

        protected override void OnStop ()
        {
            MyRegistrationService.TokenRefreshed -= TokenUpdated;

            base.OnStop ();
        }

        void TokenUpdated (string token)
        {
            Android.Util.Log.Debug (TAG, "Got Token: {0}", token);

            RunOnUiThread (() => textToken.Text = token);
        }

        static int msgId = 0;

        bool SendUpstreamMessage (string msg)
        {
            var gcm = GoogleCloudMessaging.GetInstance (this);

            try {
                var data = new Bundle();
                data.PutString ("my_message", msg);

                var id = msgId++;
                                    
                gcm.Send (MyRegistrationService.GCM_SENDER_ID + "@gcm.googleapis.com", id.ToString (), data);

                return true;

            } catch (Exception ex) {
                Android.Util.Log.Error (TAG, "SendUpstreamMessage Failed: {0}", ex);
            }

            return false;
        }
    }

    [Service]
    public class MyRegistrationService : IntentService
    {
        public const string GCM_SENDER_ID = "YOUR-SENDER-ID";

        public static event Action<string> TokenRefreshed;

        public static void Register (Context context)
        {
            context.StartService (new Intent (context, typeof(MyRegistrationService)));
        }

        protected override void OnHandleIntent (Intent intent)
        {
            // Get the new token and send to the server
            var instanceID = InstanceID.GetInstance (Application.Context);
            var token = instanceID.GetToken (GCM_SENDER_ID, GoogleCloudMessaging.InstanceIdScope);

            // Fire the event for any UI subscribed to it
            TokenRefreshed?.Invoke (token);

            Console.WriteLine ("OnTokenRefresh: {0}", token);
        }
    }

    [Service (Exported=false)]
    [IntentFilter (new [] { InstanceID.IntentFilterAction })]
    public class MyInstanceIDListenerService : InstanceIDListenerService
    {
        public override async void OnTokenRefresh ()
        {
            MyRegistrationService.Register (this);
        }
    }

    [Service (Exported=false)]
    [IntentFilter (new [] { GoogleCloudMessaging.IntentFilterActionReceive })]
    public class MyGcmListenerService : GcmListenerService
    {
        const string TAG = "GCMSAMPLE";

        public override void OnDeletedMessages ()
        {
            base.OnDeletedMessages ();

            Android.Util.Log.Debug (TAG, "Messages Deleted");
        }

        public override void OnMessageReceived (string from, Bundle data)
        {
            var message = data.GetString ("message");
            Android.Util.Log.Debug (TAG, "From: " + from);
            Android.Util.Log.Debug (TAG, "Message: " + message);

            /**
             * Production applications would usually process the message here.
             * Eg: - Syncing with server.
             *     - Store message in local database.
             *     - Update UI.
             */

            /**
            * In some cases it may be useful to show a notification indicating to the user
            * that a message was received.
            */
        }

        public override void OnMessageSent (string msgId)
        {
            base.OnMessageSent (msgId);

            Android.Util.Log.Debug (TAG, "Message Sent: {0}", msgId);
        }

        public override void OnSendError (string msgId, string error)
        {
            base.OnSendError (msgId, error);

            Android.Util.Log.Debug (TAG, "Message Failed: {0} - {1}", msgId, error);
        }
    }

}
