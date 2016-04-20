using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace VisionSample
{
    [Activity (Label = "VisionSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {        
        Button buttonFaceTracker;
        Button buttonPhotoFace;
        Button buttonBarcode;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            buttonFaceTracker = FindViewById<Button> (Resource.Id.buttonFaceTracker);
            buttonPhotoFace = FindViewById<Button> (Resource.Id.buttonPhotoFace);
            buttonBarcode = FindViewById<Button> (Resource.Id.buttonBarcode);

            buttonFaceTracker.Click += delegate {
                StartActivity (typeof(FaceTrackerActivity));    
            };

            buttonPhotoFace.Click += delegate {
                
            };

            buttonBarcode.Click += delegate {
                StartActivity (typeof(BarcodeScannerActivity));
            };
        }
    }
}


