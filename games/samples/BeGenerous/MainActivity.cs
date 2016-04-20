using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Graphics;
using Android.Gms.Games;
using Android.Gms.Plus;
using System.Threading.Tasks;
using Android.Gms.Games.Request;
using System.Collections.Generic;
using System.Text;
using Android.Gms.Common;


[assembly: MetaData ("com.google.android.gms.games.APP_ID", Value="@string/app_id")]

namespace BeGenerous
{
    [Activity (Label = "BeGenerous", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, IOnRequestReceivedListener
    {        
        const string TAG = "BeGenerous";

        const int SHOW_INBOX = 1;
        const int SEND_GIFT_CODE = 2;
        const int SEND_REQUEST_CODE = 3;

        /** Default lifetime of a request, 1 week. */
        const int DEFAULT_LIFETIME = 7;

        /** Icon to be used to send gifts/requests */
        Bitmap mGiftIcon;

        // Request code used to invoke sign in user interactions.
        const int RC_SIGN_IN = 9001;

        // Client used to interact with Google APIs.
        GoogleApiClient mGoogleApiClient;

        // Are we currently resolving a connection failure?
        bool mResolvingConnectionFailure = false;

        // Has the user clicked the sign-in button?
        bool mSignInClicked = false;

        // Set to true to automatically start the sign in flow when the Activity starts.
        // Set to false to require the user to click the button in order to sign in.
        bool mAutoStartSignInFlow = true;

        void Log (string msg) 
        {
            Android.Util.Log.Debug (TAG, msg);
        }

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.activity_main);

            Log ("onCreate()");

            // Create the Google Api Client with access to Plus and Games
            mGoogleApiClient = new GoogleApiClient.Builder (this)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this)
                .AddApi (PlusClass.API).AddScope (PlusClass.ScopePlusLogin)
                .AddApi (GamesClass.API).AddScope (GamesClass.ScopeGames)
                .Build ();

            // Set up click listeners
            FindViewById<ImageButton> (Resource.Id.button_open_inbox).Click += delegate {
                // show inbox!
                if (mGoogleApiClient != null && mGoogleApiClient.IsConnected)
                    StartActivityForResult (GamesClass.Requests.GetInboxIntent (mGoogleApiClient), SHOW_INBOX);
            };
            FindViewById<ImageButton> (Resource.Id.button_send_gift).Click += delegate {
                // send gift!
                showSendIntent(GameRequest.TypeGift);  
            };
            FindViewById<ImageButton> (Resource.Id.button_send_request).Click += delegate {
                // request gift!
                showSendIntent(GameRequest.TypeWish);
            };
            FindViewById<SignInButton> (Resource.Id.button_sign_in).Click += delegate {
                // Check to see the developer who's running this sample code read the instructions :-)
                // NOTE: this check is here only because this is a sample! Don't include this
                // check in your actual production app.
                //if (!BaseGameUtils.VerifySampleSetup (this, Resource.String.app_id))
                //    Log ("*** Warning: setup problems detected. Sign in may not work!");

                // start the sign-in flow
                Log ("Sign-in button clicked");
                mSignInClicked = true;
                mGoogleApiClient.Connect();
            };
            FindViewById<Button> (Resource.Id.button_sign_out).Click += delegate {
                // sign out.
                Log ("Sign-out button clicked");
                mSignInClicked = false;
                GamesClass.SignOut (mGoogleApiClient);
                mGoogleApiClient.Disconnect ();
                showSignInBar();
            };

            mGiftIcon = BitmapFactory.DecodeResource (Resources, Resource.Drawable.ic_send_gift);
        }

        // Shows the "sign in" bar (explanation and button).
        void showSignInBar() 
        {
            FindViewById (Resource.Id.sign_in_bar).Visibility = ViewStates.Visible;
            FindViewById (Resource.Id.sign_out_bar).Visibility = ViewStates.Gone;
            FindViewById<ImageView> (Resource.Id.avatar).SetImageBitmap (null);
            FindViewById<TextView> (Resource.Id.playerName).Text = string.Empty;
            FindViewById<TextView> (Resource.Id.playerEmail).Text = string.Empty;
        }

        // Shows the "sign out" bar (explanation and button).
        void showSignOutBar() 
        {
            FindViewById (Resource.Id.sign_in_bar).Visibility = ViewStates.Gone;
            FindViewById (Resource.Id.sign_out_bar).Visibility = ViewStates.Visible;

            var player = GamesClass.Players.GetCurrentPlayer (mGoogleApiClient);
            var url = player.IconImageUrl;
            FindViewById<TextView> (Resource.Id.playerName).Text = player.DisplayName;
            if (!string.IsNullOrEmpty (url)) {
                var vw = FindViewById<ImageView> (Resource.Id.avatar);

                Task.Factory.StartNew (() => {
                    // Download URL and set it to image
                    try {
                        var icon = BitmapFactory.DecodeStream (new Java.Net.URL (url).OpenStream ());

                        RunOnUiThread (() => {
                            vw.SetImageBitmap (icon);
                            vw.Visibility = ViewStates.Visible;
                        });
                    } catch (Exception ex) {
                        Log ("Download Image Failed: " + ex.Message);
                    }
                });
            }
            var email = PlusClass.AccountApi.GetAccountName (mGoogleApiClient);

            FindViewById<TextView> (Resource.Id.playerEmail).Text = email;
        }

        // Count GameRequests in a GameRequestBuffer that have not yet expired
        int countNotExpired (GameRequestBuffer buf) 
        {
            if (buf == null)
                return 0;

            var giftCount = 0;
            for (int i = 0; i < buf.Count; i++) {
                var gr = buf.Get (i).JavaCast<GameRequestRef> ();

                var currentTimeMillis = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                if (gr.ExpirationTimestamp > (long)currentTimeMillis.TotalMilliseconds)
                    giftCount++;
            }
            return giftCount;
        }


        void LoadRequestsCallback (IRequestsLoadRequestsResult result)
        {
            var giftCount = countNotExpired(result.GetRequests (GameRequest.TypeGift));
            var wishCount = countNotExpired(result.GetRequests(GameRequest.TypeWish));

            FindViewById<TextView> (Resource.Id.tv_gift_count).Text = 
                Java.Lang.String.Format (GetString (Resource.String.gift_count), giftCount);
            FindViewById<TextView> (Resource.Id.tv_request_count).Text = 
                Java.Lang.String.Format (GetString (Resource.String.request_count), wishCount);            
        }

        // Changes the numbers at the top of the layout
        async Task updateRequestCounts() 
        {
            var result = await GamesClass.Requests.LoadRequestsAsync (mGoogleApiClient, 
                             Requests.RequestDirectionInbound,
                             GameRequest.TypeAll,
                             Requests.SortOrderExpiringSoonFirst);
            
            LoadRequestsCallback (result);
        }

        public void OnRequestReceived (IGameRequest request)
        {
            int requestStringResource;
            switch (request.Type) {
            case GameRequest.TypeGift:
                requestStringResource = Resource.String.new_gift_received;
                break;
            case GameRequest.TypeWish:
                requestStringResource = Resource.String.new_request_received;
                break;
            default:
                return;
            }

            Toast.MakeText (this, requestStringResource, ToastLength.Long).Show ();
            updateRequestCounts();
        }

        public void OnRequestRemoved (string requestId)
        {
            throw new NotImplementedException ();
        }

        public void OnConnected (Bundle connectionHint)
        {
            Log ("onConnected() called. Sign in successful!");
            showSignOutBar();
            // This is *NOT* required; if you do not register a handler for
            // request events, you will get standard notifications instead.
            GamesClass.Requests.RegisterRequestListener (mGoogleApiClient, this);

            if (connectionHint != null) {

                //var requests = new List<GameRequest> ();
                // Do we have any requests pending? (getGameRequestsFromBundle never returns null
                var requests = GamesClass.Requests.GetGameRequestsFromBundle (connectionHint);

                if (requests.Count > 0) {
                    // We have requests in onConnected's connectionHint.
                    Log ("onConnected: connection hint has " + requests.Count + " request(s)");
                }
                Log ("===========\nRequests count " + requests.Count);
                // Use regular handler
                handleRequests(requests);
            }

            // Our sample displays the request counts.
            updateRequestCounts();
        }

        public void OnConnectionSuspended (int cause)
        {
            Log ("onConnectionSuspended() called. Trying to reconnect.");
            mGoogleApiClient.Connect ();
        }

        public void OnConnectionFailed (ConnectionResult result)
        {
            Log ("onConnectionFailed() called, result: " + result);

            if (mResolvingConnectionFailure) {
                Log ("onConnectionFailed() ignoring connection failure; already resolving.");
                return;
            }

            if (mSignInClicked || mAutoStartSignInFlow) {
                mAutoStartSignInFlow = false;
                mSignInClicked = false;
                mResolvingConnectionFailure = resolveConnectionFailure(this, mGoogleApiClient,
                        result, RC_SIGN_IN, GetString(Resource.String.signin_other_error));
            }
            showSignInBar();
        }

        bool resolveConnectionFailure (Activity activity, GoogleApiClient client, ConnectionResult result, int requestCode, string fallbackErrorMessage) {

            if (result.HasResolution) {
                try {
                    result.StartResolutionForResult (activity, requestCode);
                    return true;
                } catch (IntentSender.SendIntentException e) {
                    // The intent was canceled before it was sent.  Return to the default
                    // state and attempt to connect to get an updated ConnectionResult.
                    client.Connect ();
                    return false;
                }
            } else {
                // not resolvable... so show an error message
                int errorCode = result.ErrorCode;
                var dialog = GooglePlayServicesUtil.GetErrorDialog (errorCode, activity, requestCode);
                if (dialog != null) {
                    dialog.Show ();
                } else {
                    // no built-in dialog: show the fallback error message
                    //ShowAlert (activity, fallbackErrorMessage);
                    (new AlertDialog.Builder (activity)).SetMessage (fallbackErrorMessage)
                        .SetNeutralButton (Android.Resource.String.Ok, delegate { }).Create().Show ();
                }
                return false;
            }
        }

        /**
        * Show a send gift or send wish request using startActivityForResult.
        *
        * @param type
        *            the type of GameRequest (gift or wish) to show
        */
        void showSendIntent(int type) 
        {
            // Make sure we have a valid API client.
            if (mGoogleApiClient != null && mGoogleApiClient.IsConnected) {

                string description;
                int intentCode;
                Bitmap icon;
                switch (type) {
                case GameRequest.TypeGift:
                    description = GetString (Resource.String.send_gift_description);
                    intentCode = SEND_GIFT_CODE;
                    icon = mGiftIcon;
                    break;
                case GameRequest.TypeWish:
                    description = GetString (Resource.String.send_request_description);
                    intentCode = SEND_REQUEST_CODE;
                    icon = mGiftIcon;
                    break;
                default:
                    return;
                }
                var intent = GamesClass.Requests.GetSendIntent (mGoogleApiClient, type,
                    Encoding.ASCII.GetBytes (""), DEFAULT_LIFETIME, icon, description);

                StartActivityForResult (intent, intentCode);
            }
        }

        string getRequestsString (IList<IGameRequest> requests) 
        {
            if (requests.Count == 0)
                return "You have no requests to accept.";

            if (requests.Count == 1)
                return "Do you want to accept this request from " + requests[0].Sender.DisplayName + "?";

            var retVal = new StringBuilder ();
            retVal.AppendLine ("Do you want to accept the following requests?");
            retVal.AppendLine ();

            foreach (var request in requests) {
                retVal.AppendLine ("  A "
                    + (request.Type == GameRequest.TypeGift ? "gift"
                        : "game request") + " from "
                    + request.Sender.DisplayName);
            }

            return retVal.ToString ();
        }

        // Actually accepts the requests
        async Task acceptRequests(IList<IGameRequest> requests) 
        {
            // Attempt to accept these requests.
            var requestIds = new List<string> ();

            /**
            * Map of cached game request ID to its corresponding game request
            * object.
            */
            var gameRequestMap = new Dictionary<string, IGameRequest> ();

            // Cache the requests.
            foreach (var request in requests) {
                requestIds.Add (request.RequestId);
                gameRequestMap.Add (request.RequestId, request);

                Log ("Processing request " + request.RequestId);
            }

            // Accept the requests.
            var result = await GamesClass.Requests.AcceptRequestsAsync (mGoogleApiClient, requestIds);

            var numGifts = 0;
            var numRequests = 0;

            // Scan each result outcome.
            foreach (var requestId in result.RequestIds) {
                // We must have a local cached copy of the request
                // and the request needs to be a
                // success in order to continue.
                if (!gameRequestMap.ContainsKey (requestId)
                    || result.GetRequestOutcome (requestId) != Requests.RequestUpdateOutcomeSuccess) {
                    continue;
                }

                // Update succeeded here. Find the type of request
                // and act accordingly. For wishes, a
                // responding gift will be automatically sent.
                switch (gameRequestMap [requestId].Type) {
                case GameRequest.TypeGift:
                    // Toast the player!
                    ++numGifts;
                    break;
                case GameRequest.TypeWish:
                    ++numRequests;
                    break;
                }
            }

            if (numGifts != 0) {
                // Toast our gifts.
                Toast.MakeText (this, Java.Lang.String.Format (GetString (Resource.String.gift_toast), numGifts), ToastLength.Long).Show ();
            }

            if (numGifts != 0 || numRequests != 0) {
                // if the user accepted any gifts or requests,
                // update
                // the displayed counts
                await updateRequestCounts();
            }        
        }

        // Deal with any requests that are incoming, either from a bundle from the
        // app starting via notification, or from the inbox. Players should give
        // explicit approval to accept any gift or request, so we pop up a dialog.
        private void handleRequests(IList<IGameRequest> requests) 
        {
            if (requests == null || requests.Count <= 0)
                return;
            
            new AlertDialog.Builder (this)
                .SetMessage (getRequestsString (requests))
                .SetPositiveButton ("Absolutely!", delegate {
                    acceptRequests (requests);
                })
                .SetNegativeButton ("No thanks", delegate {
                        
                })
                .Create ()
                .Show ();
        }

        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
        {
            switch (requestCode) {
            case SEND_REQUEST_CODE:
                if ((int)resultCode == GamesActivityResultCodes.ResultSendRequestFailed)
                    Toast.MakeText(this, "FAILED TO SEND REQUEST!", ToastLength.Long).Show();
                break;
            case SEND_GIFT_CODE:
                if ((int)resultCode == GamesActivityResultCodes.ResultSendRequestFailed)
                    Toast.MakeText (this, "FAILED TO SEND GIFT!", ToastLength.Long).Show ();
                break;
            case SHOW_INBOX:
                if (resultCode == Result.Ok && data != null) {
                    handleRequests(GamesClass.Requests.GetGameRequestsFromInboxResponse(data));
                } else {
                    Log ("Failed to process inbox result: resultCode = "
                        + resultCode + ", data = "
                        + (data == null ? "null" : "valid"));
                }
                break;
            case RC_SIGN_IN:
                Log ("onActivityResult with requestCode == RC_SIGN_IN, responseCode=" + resultCode + ", intent=" + data);
                mSignInClicked = false;
                mResolvingConnectionFailure = false;
                if (resultCode == Result.Ok) {
                    mGoogleApiClient.Connect ();
                } else {
                    Toast.MakeText (this, GetString (Resource.String.signin_other_error), ToastLength.Long).Show ();
                    //BaseGameUtils.ShowActivityResultError (this, requestCode, resultCode, Resource.String.signin_other_error);
                }
                break;
            }

            base.OnActivityResult (requestCode, resultCode, data);
        }
    }
}



