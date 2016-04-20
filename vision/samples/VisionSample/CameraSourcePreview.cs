using System;
using Android.Views;
using Android.Gms.Vision;
using Android.Content;
using Android.Util;

namespace VisionSample
{
    public class CameraSourcePreview : ViewGroup
    {
        const string TAG = "CameraSourcePreview";

        Context mContext;
        SurfaceView mSurfaceView;
        bool mStartRequested;
        protected bool SurfaceAvailable { get; set; }
        CameraSource mCameraSource;

        GraphicOverlay mOverlay;

        public CameraSourcePreview(Context context, IAttributeSet attrs) : base (context, attrs)
        {
            mContext = context;
            mStartRequested = false;
            SurfaceAvailable = false;

            mSurfaceView = new SurfaceView (context);
            mSurfaceView.Holder.AddCallback (new SurfaceCallback (this));
            AddView (mSurfaceView);
        }

        public void Start (CameraSource cameraSource)
        {
            if (cameraSource == null)
                Stop();

            mCameraSource = cameraSource;

            if (mCameraSource != null) {
                mStartRequested = true;
                StartIfReady ();
            }
        }

        public void Start (CameraSource cameraSource, GraphicOverlay overlay)
        {
            mOverlay = overlay;
            Start (cameraSource);
        }

        public void Stop ()
        {
            if (mCameraSource != null)
                mCameraSource.Stop ();
        }

        public void Release ()
        {
            if (mCameraSource != null) {
                mCameraSource.Release ();
                mCameraSource = null;
            }
        }

        private void StartIfReady ()
        {
            if (mStartRequested && SurfaceAvailable) {
                mCameraSource.Start (mSurfaceView.Holder);
                if (mOverlay != null) {
                    var size = mCameraSource.PreviewSize;
                    var min = Math.Min(size.Width, size.Height);
                    var max = Math.Max(size.Width, size.Height);
                    if (IsPortraitMode ()) {
                        // Swap width and height sizes when in portrait, since it will be rotated by
                        // 90 degrees
                        mOverlay.SetCameraInfo (min, max, mCameraSource.CameraFacing);
                    } else {
                        mOverlay.SetCameraInfo (max, min, mCameraSource.CameraFacing);
                    }
                    mOverlay.Clear();
                }
                mStartRequested = false;
            }
        }

        private class SurfaceCallback : Java.Lang.Object, ISurfaceHolderCallback
        {
            public SurfaceCallback (CameraSourcePreview parent)
            {
                Parent = parent;
            }

            public CameraSourcePreview Parent { get; private set; }

            public void SurfaceCreated (ISurfaceHolder surface)
            {
                Parent.SurfaceAvailable = true;
                try {
                    Parent.StartIfReady ();
                } catch (Exception ex) {
                    Android.Util.Log.Error (TAG, "Could not start camera source.", ex);
                }
            }

            public void SurfaceDestroyed (ISurfaceHolder surface)
            {
                Parent.SurfaceAvailable = false;
            }

            public void SurfaceChanged (ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
            {
            }
        }
            
        protected override void OnLayout (bool changed, int left, int top, int right, int bottom)
        {
            int width = 320;
            int height = 240;
            if (mCameraSource != null) {
                var size = mCameraSource.PreviewSize;
                if (size != null) {
                    width = size.Width;
                    height = size.Height;
                }
            }

            // Swap width and height sizes when in portrait, since it will be rotated 90 degrees
            if (IsPortraitMode ()) {
                int tmp = width;
                width = height;
                height = tmp;
            }

            var layoutWidth = right - left;
            var layoutHeight = bottom - top;

            // Computes height and width for potentially doing fit width.
            int childWidth = layoutWidth;
            int childHeight = (int)(((float) layoutWidth / (float) width) * height);

            // If height is too tall using fit width, does fit height instead.
            if (childHeight > layoutHeight) {
                childHeight = layoutHeight;
                childWidth = (int)(((float) layoutHeight / (float) height) * width);
            }

            for (int i = 0; i < ChildCount; ++i)
                GetChildAt (i).Layout (0, 0, childWidth, childHeight);

            try {
                StartIfReady ();
            } catch (Exception ex) {
                Android.Util.Log.Error (TAG, "Could not start camera source.", ex);
            }
        }

        bool IsPortraitMode ()
        {
            var orientation = mContext.Resources.Configuration.Orientation;
            if (orientation == Android.Content.Res.Orientation.Landscape)
                return false;            
            if (orientation == Android.Content.Res.Orientation.Portrait)
                return true;

            Android.Util.Log.Debug (TAG, "isPortraitMode returning false by default");
            return false;
        }
    }
}

