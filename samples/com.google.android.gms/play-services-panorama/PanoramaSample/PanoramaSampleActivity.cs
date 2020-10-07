using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PanoramaSample
{
    [Activity (Label = "PanoramaSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class PanoramaSampleActivity : Activity, Android.Gms.Common.Apis.GoogleApiClient.IConnectionCallbacks, Android.Gms.Common.Apis.GoogleApiClient.IOnConnectionFailedListener
    {
        Android.Gms.Common.Apis.GoogleApiClient client;

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);

            client = new Android.Gms.Common.Apis.GoogleApiClient.Builder (this, this, this)
                .AddApi (Android.Gms.Panorama.PanoramaClass.API)
                .Build ();
        }
            
        protected override void OnStart () 
        {
            base.OnStart ();

            client.Connect ();
        }

        public async void OnConnected (Bundle connectionHint) 
        {
            var uri = Android.Net.Uri.Parse ("android.resource://" + PackageName + "/" + Resource.Raw.pano1);

            var result =
                // TODO: asyncify
                //await
                    Android.Gms.Panorama.PanoramaClass.PanoramaApi
                        //.LoadPanoramaInfoAsync (client, uri);
                        .LoadPanoramaInfo (client, uri);

            /*
            if (result.Status.IsSuccess) {
                var viewerIntent = result.ViewerIntent;

                if (viewerIntent != null) {
                    Console.WriteLine ("found viewerIntent: " + viewerIntent);
                    StartActivity (viewerIntent);
                }
            } else {
                Console.WriteLine ("error: " + result);
            }
            */
        }

        public void OnConnectionSuspended (int cause) 
        {
            Console.WriteLine ("connection suspended: " + cause);
        }            

        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
        {
            Console.WriteLine ("connection failed: " + result);
        }
            
        protected override void OnStop () 
        {
            base.OnStop ();

            client.Disconnect ();
        }
    }
}


