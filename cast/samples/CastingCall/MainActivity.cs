using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Media;
using Android.Gms.Cast;
using Android.Gms.Common.Apis;

namespace CastingCall
{
    [Activity (Label = "Casting Call", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Android.Support.V4.App.FragmentActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        const string APP_ID = "CC1AD845";

        GoogleApiClient googleApiClient;

        Android.Support.V7.App.MediaRouteButton mediaRouteButton;

        MediaRouter mediaRouter;
        MediaRouteSelector mediaRouteSelector;
        MyMediaRouterCallback myMediaRouterCallback;
        MyCastListener myCastListener;
        RemoteMediaPlayer mediaPlayer;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            mediaRouteButton = FindViewById<Android.Support.V7.App.MediaRouteButton> (Resource.Id.mediaRouteButton);

            Init ();
        }
           
        void Init ()
        {
            mediaRouter = MediaRouter.GetInstance (this);

            mediaRouteSelector =
                new MediaRouteSelector.Builder ().AddControlCategory (MediaControlIntent.CategoryRemotePlayback) //CastMediaControlIntent.CategoryForCast (APP_ID))
                    .Build();
            mediaRouteButton.RouteSelector = mediaRouteSelector;

            myMediaRouterCallback = new MyMediaRouterCallback {
                OnRouteSelectedHandler = (router, route) => {

                    Console.WriteLine ("Route Selected: " + route.Name);

                    var device = CastDevice.GetFromBundle(route.Extras);

                    myCastListener = new MyCastListener();

                    var apiOptionsBuilder = new CastClass.CastOptions.Builder (
                        device,
                        myCastListener).SetVerboseLoggingEnabled (true);

                    googleApiClient = new GoogleApiClient.Builder(this)
                        .AddApi (CastClass.API, apiOptionsBuilder.Build())
                        .AddConnectionCallbacks (this)
                        .AddOnConnectionFailedListener (this)
                        .Build ();

                    googleApiClient.Connect();

                },
                OnRouteUnselectedHandler = (router, route) => {
                    Console.WriteLine ("Route Unselected: " + route.Name);
                },
                RouteCountChangedHandler = newCount => {
                    mediaRouteButton.Visibility = newCount > 0 ? ViewStates.Visible : ViewStates.Gone;
                }
            };

            mediaRouter.AddCallback(mediaRouteSelector, myMediaRouterCallback, MediaRouter.CallbackFlagRequestDiscovery);
        }

        // Google API
        public void OnConnected (Bundle connectionHint)
        {
            Console.WriteLine ("Google API: Connected");

            CastClass.CastApi.LaunchApplication (googleApiClient, APP_ID)
                .SetResultCallback<CastClass.IApplicationConnectionResult> (result => {
            
                    Console.WriteLine ("Launch Application Result: " + result.ApplicationStatus);

                    mediaPlayer = new RemoteMediaPlayer ();
                    mediaPlayer.MetadataUpdated += (sender, e) => {
                        Console.WriteLine ("MediaPlayer: Metadata Updated");
                    };
                    mediaPlayer.StatusUpdated += (sender, e) => {
                        Console.WriteLine ("MediaPlayer: Status Updated");

                        var s = mediaPlayer.MediaStatus;
                        Console.WriteLine ("");
                    };
                    CastClass.CastApi.SetMessageReceivedCallbacks (
                        googleApiClient,
                        mediaPlayer.Namespace,
                        mediaPlayer);

                    SetSongUri ();
                });            
        }

        public void OnConnectionSuspended (int cause)
        {
            Console.WriteLine ("Google API: Connection Suspended");
        }

        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
        {
            Console.WriteLine ("Google API: Connection Failed");
        }

        public void OnMessageReceived (CastDevice castDevice, string ns, string message)
        {
            Console.WriteLine ("Message Received: {0} => {1}", ns, message);

            if (mediaPlayer != null && mediaPlayer.MediaStatus != null) {
                Console.WriteLine ("MediaStatus available");
            } else {
                Console.WriteLine ("MediaPlayer or MediaStatus is null");
            }
        }

        public void SetSongUri()
        {
            string songUri =
                "http://freemusicarchive.org/music/download/4cc908b1d8b19b9bdfeb87f9f9fd5086b66258b8";
            
            if (googleApiClient != null && mediaPlayer != null)
            {
                try
                {
                    //currentSongInfo = info;
                    var metadata = new MediaMetadata(MediaMetadata.MediaTypeMusicTrack);
                    metadata.PutString(MediaMetadata.KeyArtist, "Deadlines");
                    metadata.PutString(MediaMetadata.KeyAlbumTitle, "Magical Inertia");
                    metadata.PutString(MediaMetadata.KeyTitle, "The Wire");
                    var androidUri =
                        Android.Net.Uri.Parse("http://freemusicarchive.org/file/images/albums/Deadlines_-_Magical_Inertia_-_20150407163159222.jpg?width=290&height=290");
                    var webImage = new Android.Gms.Common.Images.WebImage(androidUri);
                    metadata.AddImage(webImage);

                    MediaInfo mediaInfo =
                        new MediaInfo.Builder(songUri).SetContentType("audio/mp3")
                            .SetMetadata(metadata)
                            .SetStreamType(MediaInfo.StreamTypeBuffered)
                            .Build();

                    mediaPlayer.Load(googleApiClient, mediaInfo, true, 0)
                        .SetResultCallback<RemoteMediaPlayer.IMediaChannelResult> (r => {
                            Console.WriteLine ("Loaded");
                        });
                }
                catch (Exception e)
                {
                    Console.WriteLine ("Exception while sending a song. Exception : " + e.Message);
                }
            }
        }
    }

    class MyMediaRouterCallback : MediaRouter.Callback
    {
        public Action<int> RouteCountChangedHandler { get; set; }

        int routeCount = 0;

        public override void OnRouteAdded (MediaRouter router, MediaRouter.RouteInfo route)
        {
            Console.WriteLine ("Route Added: " + route.Name);

            routeCount++;

            if (RouteCountChangedHandler != null)
                RouteCountChangedHandler (routeCount);
        }
        public override void OnRouteRemoved (MediaRouter router, MediaRouter.RouteInfo route)
        {
            Console.WriteLine ("Route Removed: " + route.Name);

            routeCount--;

            if (RouteCountChangedHandler != null)
                RouteCountChangedHandler (routeCount);
        }

        public override void OnRouteChanged (MediaRouter router, MediaRouter.RouteInfo route)
        {
            Console.WriteLine ("Route Changed: " + route.Name);
        }

        public Action<MediaRouter, MediaRouter.RouteInfo> OnRouteSelectedHandler { get;set; }
        public override void OnRouteSelected (MediaRouter router, MediaRouter.RouteInfo route)
        {
            if (OnRouteSelectedHandler != null)
                OnRouteSelectedHandler (router, route);    
        }

        public Action<MediaRouter, MediaRouter.RouteInfo> OnRouteUnselectedHandler { get;set; }
        public override void OnRouteUnselected (MediaRouter router, MediaRouter.RouteInfo route)
        {
            if (OnRouteUnselectedHandler != null)
                OnRouteUnselectedHandler (router, route);
        }
            
    }

    class MyCastListener : CastClass.Listener
    {
        public override void OnApplicationDisconnected (int statusCode)
        {
            Console.WriteLine ("App Disconnected");
        }

        public override void OnApplicationStatusChanged ()
        {
            Console.WriteLine ("App Status Changed");
        }

        public override void OnApplicationMetadataChanged (ApplicationMetadata applicationMetadata)
        {
            Console.WriteLine ("Metadata Changed");
        }

        public override void OnVolumeChanged ()
        {
            Console.WriteLine ("Volume Changed");
        }
    }       
}


