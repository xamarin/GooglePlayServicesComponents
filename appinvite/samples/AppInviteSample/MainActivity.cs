using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.AppInvite;
using Android.Util;
using Android.Support.V4.Content;
using Android.Gms.Common.Apis;

namespace AppInviteSample
{
    [Activity (Label = "AppInvite Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        const string TAG = "APPINVITES";
        const int REQUEST_INVITE = 101;
        const int RESOLUTION_CODE = 102;

        LocalBroadcastReceiver deepLinkReceiver;
        GoogleApiClient googleApiClient;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var button = FindViewById<Button> (Resource.Id.buttonInvite);
            button.Click += delegate {
                SendAppInvite ();
            };

            googleApiClient = new GoogleApiClient.Builder (this)
                .AddApi (AppInviteClass.API)
                .AddConnectionCallbacks (async info => {

                    if (AppInviteReferral.HasReferral (Intent))
                        await UpdateInvitationStatus (Intent);
                    
                }, cause => Console.WriteLine ("Connection Suspended: {0}", cause))
                .AddOnConnectionFailedListener (result => {
                    
                    if (!result.IsSuccess) {
                        if (result.HasResolution)
                            result.StartResolutionForResult (this, RESOLUTION_CODE);
                        else
                            Toast.MakeText (this, "Failed to Connect: " + result.ErrorCode, ToastLength.Long).Show ();
                    }
                })
                .Build ();

            // If the app was already installed and the appinvite was opened
            // we may have information passed in about it here
            if (bundle == null) {
                // No savedInstanceState, so it is the first launch of this activity

                if (AppInviteReferral.HasReferral (Intent)) {
                    // In this case the referral data is in the intent launching the MainActivity,
                    // which means this user already had the app installed. We do not have to
                    // register the Broadcast Receiver to listen for Play Store Install information
                    LaunchDeepLinkActivity (Intent);
                }
            }
        }

        protected override void OnStart ()
        {
            base.OnStart ();

            googleApiClient.Connect ();

            RegisterDeepLinkReceiver ();
        }

        protected override void OnStop ()
        {
            googleApiClient.Disconnect ();

            UnregisterDeepLinkReceiver ();

            base.OnStop ();
        }
           
        void SendAppInvite ()
        {
            var intent = new AppInviteInvitation.IntentBuilder ("Invite Your Friends!")
                .SetMessage ("Check out this awesome app!")
                .SetDeepLink(Android.Net.Uri.Parse (""))
                .Build();
            
            StartActivityForResult (intent, REQUEST_INVITE);
        }

        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult (requestCode, resultCode, data);
         
            Log.Debug(TAG, "onActivityResult: requestCode=" + requestCode + ", resultCode=" + resultCode);

            if (requestCode == REQUEST_INVITE) {
                if (resultCode == Result.Ok) {
                    // Check how many invitations were sent and log a message
                    // The ids array contains the unique invitation ids for each invitation sent
                    // (one for each contact select by the user). You can use these for analytics
                    // as the ID will be consistent on the sending and receiving devices.
                    var ids = AppInviteInvitation.GetInvitationIds ((int)resultCode, data);

                    Log.Debug (TAG, string.Format ("You Sent {0} App Invitations!", ids.Length));
                } else {
                    // Sending failed or it was canceled, show failure message to the user
                    Toast.MakeText (this, "Failed to send App Invitations :(", ToastLength.Short).Show ();
                }
            } else if (requestCode == RESOLUTION_CODE) {
                if (resultCode == Result.Ok)
                    googleApiClient.Connect ();
                else
                    Toast.MakeText (this, "Failed to Connect to Google Play Services", ToastLength.Long).Show ();
            }
        }

        void LaunchDeepLinkActivity(Intent intent) 
        {
            Log.Debug (TAG, "launchDeepLinkActivity:" + intent);
            var newIntent = new Intent(intent).SetClass (this, typeof (DeepLinkActivity));
            StartActivity (newIntent);
        }

        void RegisterDeepLinkReceiver()
        {
            // Create local Broadcast receiver that starts DeepLinkActivity when a deep link
            // is found
            deepLinkReceiver = new LocalBroadcastReceiver {
                OnReceiveHandler = (context, intent) => {
                    if (AppInviteReferral.HasReferral (intent))
                        LaunchDeepLinkActivity (intent);
                }
            };

            var intentFilter = new IntentFilter (GetString (Resource.String.action_deep_link));

            LocalBroadcastManager.GetInstance (this).RegisterReceiver (deepLinkReceiver, intentFilter);
        }

        void UnregisterDeepLinkReceiver ()
        {
            if (deepLinkReceiver != null)
                LocalBroadcastManager.GetInstance (this).UnregisterReceiver (deepLinkReceiver);
        }

        async Task UpdateInvitationStatus (Intent intent) 
        {
            var invitationId = AppInviteReferral.GetInvitationId(intent);

            // Note: these  calls return PendingResult(s), so one could also wait to see
            // if this succeeds instead of using fire-and-forget, as is shown here
            if (AppInviteReferral.IsOpenedFromPlayStore (intent))
                await AppInviteClass.AppInviteApi.UpdateInvitationOnInstallAsync (googleApiClient, invitationId);            

            // If your invitation contains deep link information such as a coupon code, you may
            // want to wait to call `convertInvitation` until the time when the user actually
            // uses the deep link data, rather than immediately upon receipt
            await AppInviteClass.AppInviteApi.ConvertInvitationAsync (googleApiClient, invitationId);
        }
    }


    class LocalBroadcastReceiver : BroadcastReceiver
    {
        public Action<Context, Intent> OnReceiveHandler { get; set; }

        public override void OnReceive (Context context, Intent intent)
        {
            var h = OnReceiveHandler;
            if (h != null)
                h (context, intent);
        }
    }
}


