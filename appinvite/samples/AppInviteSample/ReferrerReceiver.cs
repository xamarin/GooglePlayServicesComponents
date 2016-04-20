using System;

using Android.Gms.AppInvite;
using Android.Support.V4.Content;
using Android.Content;

namespace AppInviteSample
{    
    [BroadcastReceiver (Exported=true)]
    [Android.App.IntentFilter (new [] { "com.android.vending.INSTALL_REFERRER" })]
    public class ReferrerReceiver : BroadcastReceiver 
    {
        public ReferrerReceiver() : base ()
        {
        }
            
        public override void OnReceive (Context context, Intent intent) 
        {
            // Create deep link intent with correct action and add play store referral information
            var deepLinkIntent = AppInviteReferral.AddPlayStoreReferrerToIntent (intent,
                new Intent(context.GetString(Resource.String.action_deep_link)));            

            // Let any listeners know about the change
            LocalBroadcastManager.GetInstance (context).SendBroadcast (deepLinkIntent);
        }
    }

}

