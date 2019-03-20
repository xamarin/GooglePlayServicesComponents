using System;
using Android.App;
using Firebase.Iid;

namespace FirebaseMessagingQuickstart
{
    [Service]
    [IntentFilter (new [] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        /**
         * Called if InstanceID token is updated. This may occur if the security of
         * the previous token had been compromised. Note that this is called when the InstanceID token
         * is initially generated so this is where you would retrieve the token.
         */
        // [START refresh_token]
        public override void OnTokenRefresh ()
        {
            // Get updated InstanceID token.
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Android.Util.Log.Debug (TAG, "Refreshed token: " + refreshedToken);

            // TODO: Implement this method to send any registration to your app's servers.
            SendRegistrationToServer (refreshedToken);
        }
        // [END refresh_token]

        /**
         * Persist token to third-party servers.
         *
         * Modify this method to associate the user's FCM InstanceID token with any server-side account
         * maintained by your application.
         */
        void SendRegistrationToServer (string token)
        {
            // Add custom implementation, as needed.
        }
    }
}
