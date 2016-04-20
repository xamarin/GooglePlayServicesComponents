using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Nearby;
using Android.Gms.Nearby.Connection;
using System.Collections.Generic;

[assembly: MetaData ("com.google.android.gms.nearby.connection.SERVICE_ID", Value="@string/service_id")]

namespace NearbySample
{
    [Activity (Label = "Nearby Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity,
        GoogleApiClient.IConnectionCallbacks,
        GoogleApiClient.IOnConnectionFailedListener,
        IConnectionsMessageListener,
        IConnectionsConnectionRequestListener,
        IConnectionsEndpointDiscoveryListener
    {        

        class ConnectionResponseCallback : Java.Lang.Object, IConnectionsConnectionResponseCallback
        {
            public Action<string, Statuses, byte[]> OnConnectionResponseHandler { get; set; }

            public void OnConnectionResponse (string endpointId, Statuses status, byte[] payload)
            {
                var h = OnConnectionResponseHandler;
                if (h != null)
                    h (endpointId, status, payload);
            }
        }
        const string TAG = "NEARBY";

        /**
     * Timeouts (in millis) for startAdvertising and startDiscovery.  At the end of these time
     * intervals the app will silently stop advertising or discovering.
     *
     * To set advertising or discovery to run indefinitely, use 0L where timeouts are required.
     */
        const long TIMEOUT_ADVERTISE = 0L; // 1000L * 30L;
        const long TIMEOUT_DISCOVER = 0L; //1000L * 30L;

        /** GoogleApiClient for connecting to the Nearby Connections API **/
        GoogleApiClient mGoogleApiClient;

        /** Views and Dialogs **/
        TextView mDebugInfo;
        EditText mMessageText;
        AlertDialog mConnectionRequestDialog;
        MyListDialog mMyListDialog;

        /** The current state of the application **/
        NearbyConnectionState mState = NearbyConnectionState.Idle;

        /** The endpoint ID of the connected peer, used for messaging **/
        string mOtherEndpointId;

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            // Button listeners
            FindViewById<Button> (Resource.Id.button_advertise).Click += delegate {
                StartAdvertising ();
            };
            FindViewById<Button> (Resource.Id.button_discover).Click += delegate {
                StartDiscovery ();
            };
            FindViewById<Button> (Resource.Id.button_send).Click += delegate {
                SendMessage ();
            };

            // EditText
            mMessageText = FindViewById<EditText> (Resource.Id.edittext_message);

            // Debug text view
            mDebugInfo = FindViewById<TextView> (Resource.Id.debug_text);
            mDebugInfo.MovementMethod = new Android.Text.Method.ScrollingMovementMethod ();

            // Initialize Google API Client for Nearby Connections. Note: if you are using Google+
            // sign-in with your project or any other API that requires Authentication you may want
            // to use a separate Google API Client for Nearby Connections.  This API does not
            // require the user to authenticate so it can be used even when the user does not want to
            // sign in or sign-in has failed.
            mGoogleApiClient = new GoogleApiClient.Builder (this)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(NearbyClass.CONNECTIONS_API)
                .Build();
        }

        void Log (string msg)
        {
            Android.Util.Log.Debug (TAG, msg);
            mDebugInfo.Append ("\n" + msg);
        }
            
        protected override void OnStart ()
        {            
            base.OnStart ();
            Log ("onStart");
            mGoogleApiClient.Connect();
        }

        protected override void OnStop ()
        {
            base.OnStop ();
            Log ("onStop");

            // Disconnect the Google API client and stop any ongoing discovery or advertising. When the
            // GoogleAPIClient is disconnected, any connected peers will get an onDisconnected callback.
            if (mGoogleApiClient != null) {
                mGoogleApiClient.Disconnect();
            }
        }

        /**
     * Check if the device is connected (or connecting) to a WiFi network.
     * @return true if connected or connecting, false otherwise.
     */
        bool IsConnectedToNetwork () 
        {
            var connManager = Android.Net.ConnectivityManager.FromContext (this);
            var info = connManager.GetNetworkInfo (Android.Net.ConnectivityType.Wifi);

            return (info != null && info.IsConnectedOrConnecting);
        }

        /**
     * Begin advertising for Nearby Connections, if possible.
     */
        async void StartAdvertising() 
        {
            // Discover nearby apps that are advertising with the required service ID.
            var serviceId = GetString (Resource.String.service_id);

            Log ("startAdvertising " + serviceId);

            if (!IsConnectedToNetwork ()) {
                Log ("startAdvertising: not connected to WiFi network.");
                return;
            }

            // Advertising with an AppIdentifer lets other devices on the network discover
            // this application and prompt the user to install the application.
            var appIdentifierList = new List<AppIdentifier> ();
            appIdentifierList.Add (new AppIdentifier (this.PackageName));
            var appMetadata = new AppMetadata(appIdentifierList);

            // Advertise for Nearby Connections. This will broadcast the service id defined in
            // AndroidManifest.xml. By passing 'null' for the name, the Nearby Connections API
            // will construct a default name based on device model such as 'LGE Nexus 5'.
            string name = null;
            var result = await NearbyClass.Connections.StartAdvertisingAsync (
                mGoogleApiClient, name, appMetadata, TIMEOUT_ADVERTISE, this);
            
            Log ("startAdvertising:onResult:" + result);

            if (result.Status.IsSuccess) {
                Log("startAdvertising:onResult: SUCCESS");

                UpdateViewVisibility (NearbyConnectionState.Advertising);
            } else {
                Log("startAdvertising:onResult: FAILURE ");

                // If the user hits 'Advertise' multiple times in the timeout window,
                // the error will be STATUS_ALREADY_ADVERTISING
                if (result.Status.StatusCode == ConnectionsStatusCodes.StatusAlreadyAdvertising) {
                    Log("STATUS_ALREADY_ADVERTISING");
                } else {
                    UpdateViewVisibility (NearbyConnectionState.Ready);
                }
            }
        }

        /**
     * Begin discovering devices advertising Nearby Connections, if possible.
     */
        async void StartDiscovery () 
        {
            // Discover nearby apps that are advertising with the required service ID.
            var serviceId = GetString (Resource.String.service_id);

            Log("startDiscovery " + serviceId);

            if (!IsConnectedToNetwork ()) {
                Log("startDiscovery: not connected to WiFi network.");
                return;
            }

            var status = await NearbyClass.Connections.StartDiscoveryAsync (mGoogleApiClient, serviceId, TIMEOUT_DISCOVER, this);

            if (status.IsSuccess) {
                Log("startDiscovery:onResult: SUCCESS");

                UpdateViewVisibility (NearbyConnectionState.Discovering);
            } else {
                Log("startDiscovery:onResult: FAILURE");

                // If the user hits 'Discover' multiple times in the timeout window,
                // the error will be STATUS_ALREADY_DISCOVERING
                if (status.StatusCode == ConnectionsStatusCodes.StatusAlreadyDiscovering)
                    Log("STATUS_ALREADY_DISCOVERING");
                else
                    UpdateViewVisibility(NearbyConnectionState.Ready);
            }
        }

        /**
     * Send a reliable message to the connected peer. Takes the contents of the EditText and
     * sends the message as a byte[].
     */
        void SendMessage() 
        {
            // Sends a reliable message, which is guaranteed to be delivered eventually and to respect
            // message ordering from sender to receiver. Nearby.Connections.sendUnreliableMessage
            // should be used for high-frequency messages where guaranteed delivery is not required, such
            // as showing one player's cursor location to another. Unreliable messages are often
            // delivered faster than reliable messages.
            var msgBytes = System.Text.Encoding.UTF8.GetBytes (mMessageText.Text);

            NearbyClass.Connections.SendReliableMessage (mGoogleApiClient, mOtherEndpointId, msgBytes);

            mMessageText.Text = string.Empty;
        }

        /**
     * Send a connection request to a given endpoint.
     * @param endpointId the endpointId to which you want to connect.
     * @param endpointName the name of the endpoint to which you want to connect. Not required to
     *                     make the connection, but used to display after success or failure.
     */
        async void ConnectTo (String endpointId, string endpointName) 
        {
            Log("connectTo:" + endpointId + ":" + endpointName);

            // Send a connection request to a remote endpoint. By passing 'null' for the name,
            // the Nearby Connections API will construct a default name based on device model
            // such as 'LGE Nexus 5'.
            string myName = null;
            byte[] myPayload = null;

            var connectionResponseCallback = new ConnectionResponseCallback {
                OnConnectionResponseHandler = (epId, s2, bytes) => {
                    Log ("onConnectionResponse:" + epId + ":" + s2);
                    if (s2.IsSuccess) {
                        Log("onConnectionResponse: " + endpointName + " SUCCESS");
                        Toast.MakeText (this, "Connected to " + endpointName, ToastLength.Short).Show ();

                        mOtherEndpointId = endpointId;
                        UpdateViewVisibility (NearbyConnectionState.Connected);
                    } else {
                        Log("onConnectionResponse: " + endpointName + " FAILURE");
                    }
                }
            };

            var status = await NearbyClass.Connections.SendConnectionRequestAsync (mGoogleApiClient, myName, endpointId, myPayload, connectionResponseCallback, this);

            Log ("connectTo: " + status.IsSuccess);
        }

        public void OnConnectionRequest (string endpointId, string deviceId, string endpointName, byte[] payload) 
        {
            Log("onConnectionRequest:" + endpointId + ":" + endpointName);

            // This device is advertising and has received a connection request. Show a dialog asking
            // the user if they would like to connect and accept or reject the request accordingly.
            mConnectionRequestDialog = new AlertDialog.Builder(this)
                .SetTitle("Connection Request")
                .SetMessage("Do you want to connect to " + endpointName + "?")
                .SetCancelable(false)
                .SetPositiveButton ("Connect", async delegate {

                    //byte[] payload = null;
                    var status = await NearbyClass.Connections.AcceptConnectionRequestAsync (mGoogleApiClient, endpointId, payload, this);
                            
                    if (status.IsSuccess) {
                        Log("acceptConnectionRequest: SUCCESS");

                        mOtherEndpointId = endpointId;
                        UpdateViewVisibility (NearbyConnectionState.Connected);
                    } else {
                        Log("acceptConnectionRequest: FAILURE");
                    }                
                })
                .SetNegativeButton("No", async delegate {
                    await NearbyClass.Connections.RejectConnectionRequestAsync (mGoogleApiClient, endpointId);
                }).Create ();

            mConnectionRequestDialog.Show ();
        }
            
        public void OnMessageReceived (string endpointId, byte[] payload, bool isReliable) 
        {
            var msg = System.Text.Encoding.UTF8.GetString (payload);

            // A message has been received from a remote endpoint.
            Log("onMessageReceived:" + endpointId + ":" + msg);
        }
            
        public void OnDisconnected (string endpointId) 
        {
            Log("onDisconnected:" + endpointId);

            UpdateViewVisibility (NearbyConnectionState.Ready);
        }
            
        public void OnEndpointFound (string endpointId, string deviceId, string serviceId, string endpointName) 
        {
            Log ("onEndpointFound:" + endpointId + ":" + endpointName);

            // This device is discovering endpoints and has located an advertiser. Display a dialog to
            // the user asking if they want to connect, and send a connection request if they do.
            if (mMyListDialog == null) {
                // Configure the AlertDialog that the MyListDialog wraps
                var builder = new AlertDialog.Builder(this)
                    .SetTitle("Endpoint(s) Found")
                    .SetCancelable(true)
                    .SetNegativeButton("Cancel", delegate {
                        mMyListDialog.Dismiss ();
                    });

                // Create the MyListDialog with a listener
                mMyListDialog = new MyListDialog (this, builder, (sender, e) => {
                    var selectedEndpointName = mMyListDialog.GetItemKey (e.Which);
                    var selectedEndpointId = mMyListDialog.GetItemValue (e.Which);

                    ConnectTo (selectedEndpointId, selectedEndpointName);
                    mMyListDialog.Dismiss ();
                });
            }

            mMyListDialog.AddItem (endpointName, endpointId);
            mMyListDialog.Show ();
        }
            
        public void OnEndpointLost (string endpointId) 
        {
            Log("onEndpointLost:" + endpointId);

            // An endpoint that was previously available for connection is no longer. It may have
            // stopped advertising, gone out of range, or lost connectivity. Dismiss any dialog that
            // was offering a connection.
            if (mMyListDialog != null) {
                mMyListDialog.RemoveItemByValue (endpointId);
            }
        }
            
        public void OnConnected (Bundle bundle) 
        {
            Log("onConnected");

            UpdateViewVisibility(NearbyConnectionState.Ready);
        }
            
        public void OnConnectionSuspended (int i) 
        {
            Log("onConnectionSuspended: " + i);

            UpdateViewVisibility (NearbyConnectionState.Idle);

            // Try to re-connect
            mGoogleApiClient.Reconnect();
        }
            
        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult connectionResult) 
        {
            Log("onConnectionFailed: " + connectionResult);

            UpdateViewVisibility (NearbyConnectionState.Idle);
        }

        /**
     * Change the application state and update the visibility on on-screen views '
     * based on the new state of the application.
     * @param newState the state to move to (should be NearbyConnectionState)
     */
        void UpdateViewVisibility (NearbyConnectionState newState) 
        {
            mState = newState;
            switch (mState) {
            case NearbyConnectionState.Idle:
                // The GoogleAPIClient is not connected, we can't yet start advertising or
                // discovery so hide all buttons
                FindViewById(Resource.Id.layout_nearby_buttons).Visibility = ViewStates.Gone;
                FindViewById(Resource.Id.layout_message).Visibility = ViewStates.Gone;
                break;
            case NearbyConnectionState.Ready:
                // The GoogleAPIClient is connected, we can begin advertising or discovery.
                FindViewById(Resource.Id.layout_nearby_buttons).Visibility = ViewStates.Visible;
                FindViewById(Resource.Id.layout_message).Visibility = ViewStates.Gone;
                break;
            case NearbyConnectionState.Advertising:
                break;
            case NearbyConnectionState.Discovering:
                break;
            case NearbyConnectionState.Connected:
                // We are connected to another device via the Connections API, so we can
                // show the message UI.
                FindViewById (Resource.Id.layout_nearby_buttons).Visibility = ViewStates.Visible;
                FindViewById (Resource.Id.layout_message).Visibility = ViewStates.Visible;
                break;
            }
        }
    }

    public enum NearbyConnectionState {
        Idle = 1023,
        Ready = 1024,
        Advertising = 1025,
        Discovering = 1026,
        Connected = 1027
    }
}


