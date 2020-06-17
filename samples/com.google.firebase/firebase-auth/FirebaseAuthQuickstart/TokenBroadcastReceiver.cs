using System;
using Android.Content;

namespace FirebaseAuthQuickstart
{
    public class TokenBroadcastReceiver : BroadcastReceiver
    {
        public Action<string> NewTokenHandler { get; set; }

        const string TAG = "TokenBroadcastReceiver";
        const string ACTION_TOKEN = "com.google.example.ACTION_TOKEN";
        const string EXTRA_KEY_TOKEN = "key_token";

        public override void OnReceive (Context context, Intent intent)
        {
            Android.Util.Log.Debug (TAG, "onReceive:" + intent);

            if (ACTION_TOKEN == intent.Action) {
                var token = intent.Extras.GetString (EXTRA_KEY_TOKEN);
                NewTokenHandler?.Invoke (token);
            }
        }

        public static IntentFilter Filter
        {
            get {
                return new IntentFilter (ACTION_TOKEN);
            }
        }
    }
}

