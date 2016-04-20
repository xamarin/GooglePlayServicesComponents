
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Vision.Faces;
using Android.Gms.Vision;


[assembly: MetaData ("com.google.android.gms.vision.DEPENDENCIES", Value="barcode,face")]

namespace VisionSample
{
    [Activity (Label = "Face Tracker")]			
    public class FaceTrackerActivity : Activity
    {
        const string TAG = "FaceTracker";

        CameraSource mCameraSource;
        CameraSourcePreview mPreview;
        GraphicOverlay mGraphicOverlay;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.FaceTracker);

            mPreview = FindViewById<CameraSourcePreview> (Resource.Id.preview);
            mGraphicOverlay = FindViewById<GraphicOverlay> (Resource.Id.faceOverlay);

            var detector = new FaceDetector.Builder (Application.Context).Build ();
            detector.SetProcessor (
                new MultiProcessor.Builder (new GraphicFaceTrackerFactory (mGraphicOverlay)).Build ());

            if (!detector.IsOperational) {
                // Note: The first time that an app using face API is installed on a device, GMS will
                // download a native library to the device in order to do detection.  Usually this
                // completes before the app is run for the first time.  But if that download has not yet
                // completed, then the above call will not detect any faces.
                //
                // isOperational() can be used to check if the required native library is currently
                // available.  The detector will automatically become operational once the library
                // download completes on device.
                Android.Util.Log.Warn (TAG, "Face detector dependencies are not yet available.");
            }

            mCameraSource = new CameraSource.Builder(Application.Context, detector)
                .SetRequestedPreviewSize (640, 480)
                .SetFacing (CameraFacing.Back)
                .SetRequestedFps (30.0f)
                .Build ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();

            StartCameraSource ();
        }

        protected override void OnPause ()
        {
            base.OnPause ();

            mPreview.Stop ();
        }

        protected override void OnDestroy ()
        {
            mCameraSource.Release ();

            base.OnDestroy ();
        }


        //==============================================================================================
        // Camera Source Preview
        //==============================================================================================

        /**
     * Starts or restarts the camera source, if it exists.  If the camera source doesn't exist yet
     * (e.g., because onResume was called before the camera source was created), this will be called
     * again when the camera source is created.
     */
        void StartCameraSource ()
        {
            try {
                mPreview.Start (mCameraSource, mGraphicOverlay);
            } catch (Exception e) {
                Android.Util.Log.Error (TAG, "Unable to start camera source.", e);
                mCameraSource.Release ();
                mCameraSource = null;
            }
        }

        //==============================================================================================
        // Graphic Face Tracker
        //==============================================================================================

        /**
     * Factory for creating a face tracker to be associated with a new face.  The multiprocessor
     * uses this factory to create face trackers as needed -- one for each individual.
     */
        class GraphicFaceTrackerFactory : Java.Lang.Object, MultiProcessor.IFactory
        {            
            public GraphicFaceTrackerFactory (GraphicOverlay overlay) : base ()
            {
                Overlay = overlay;
            }

            public GraphicOverlay Overlay { get; private set; }

            public Android.Gms.Vision.Tracker Create (Java.Lang.Object item)
            {
                return new GraphicFaceTracker (Overlay);
            }
        }

        /**
     * Face tracker for each detected individual. This maintains a face graphic within the app's
     * associated face overlay.
     */
        class GraphicFaceTracker : Tracker
        {
            GraphicOverlay mOverlay;
            FaceGraphic mFaceGraphic;

            public GraphicFaceTracker (GraphicOverlay overlay) 
            {
                mOverlay = overlay;
                mFaceGraphic = new FaceGraphic (overlay);
            }

            /**
            * Start tracking the detected face instance within the face overlay.
            */
            public override void OnNewItem (int idValue, Java.Lang.Object item)
            {
                mFaceGraphic.Id = idValue;
            }

            /**
            * Update the position/characteristics of the face within the overlay.
            */
            public override void OnUpdate (Detector.Detections detections, Java.Lang.Object item)
            {
                mOverlay.Add (mFaceGraphic);
                mFaceGraphic.UpdateFace (item.JavaCast<Face> ());
            }

            /**
            * Hide the graphic when the corresponding face was not detected.  This can happen for
            * intermediate frames temporarily (e.g., if the face was momentarily blocked from
            * view).
            */
            public override void OnMissing (Detector.Detections detections)
            {
                mOverlay.Remove (mFaceGraphic);
            }

            /**
            * Called when the face is assumed to be gone for good. Remove the graphic annotation from
            * the overlay.
            */
            public override void OnDone ()
            {
                mOverlay.Remove (mFaceGraphic);
            }
        }
    }
}

