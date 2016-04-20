using System;
using Android.Views;
using Android.Gms.Vision;
using System.Collections.Generic;
using Android.Graphics;
using Android.Content;
using Android.Util;

namespace VisionSample
{
    public class GraphicOverlay : View
    {
        object mLock = new object ();
        int mPreviewWidth;
        float mWidthScaleFactor = 1.0f;
        int mPreviewHeight;
        float mHeightScaleFactor = 1.0f;
        CameraFacing mFacing = CameraFacing.Back;
        List<Graphic> mGraphics = new List<Graphic>();

        /**
     * Base class for a custom graphics object to be rendered within the graphic overlay.  Subclass
     * this and implement the {@link Graphic#draw(Canvas)} method to define the
     * graphics element.  Add instances to the overlay using {@link GraphicOverlay#add(Graphic)}.
     */
        public abstract class Graphic 
        {
            private GraphicOverlay mOverlay;

            public Graphic (GraphicOverlay overlay) 
            {
                mOverlay = overlay;
            }

            /**
         * Draw the graphic on the supplied canvas.  Drawing should use the following methods to
         * convert to view coordinates for the graphics that are drawn:
         * <ol>
         * <li>{@link Graphic#scaleX(float)} and {@link Graphic#scaleY(float)} adjust the size of
         * the supplied value from the preview scale to the view scale.</li>
         * <li>{@link Graphic#translateX(float)} and {@link Graphic#translateY(float)} adjust the
         * coordinate from the preview's coordinate system to the view coordinate system.</li>
         * </ol>
         *
         * @param canvas drawing canvas
         */
            public abstract void Draw (Canvas canvas);

            /**
         * Adjusts a horizontal value of the supplied value from the preview scale to the view
         * scale.
         */
            public float ScaleX (float horizontal) 
            {
                return horizontal * mOverlay.mWidthScaleFactor;
            }

            /**
         * Adjusts a vertical value of the supplied value from the preview scale to the view scale.
         */
            public float ScaleY (float vertical) 
            {
                return vertical * mOverlay.mHeightScaleFactor;
            }

            /**
         * Adjusts the x coordinate from the preview's coordinate system to the view coordinate
         * system.
         */
            public float TranslateX (float x) 
            {
                if (mOverlay.mFacing == CameraFacing.Front) {
                    return mOverlay.Width - ScaleX (x);
                } else {
                    return ScaleX (x);
                }
            }

            /**
         * Adjusts the y coordinate from the preview's coordinate system to the view coordinate
         * system.
         */
            public float TranslateY (float y)
            {
                return ScaleY (y);
            }

            public void PostInvalidate ()
            {
                mOverlay.PostInvalidate ();
            }
        }

        public GraphicOverlay (Context context, IAttributeSet attrs) : base (context, attrs)
        {            
        }

        /**
     * Removes all graphics from the overlay.
     */
        public void Clear ()
        {
            lock (mLock) {
                mGraphics.Clear ();
            }
            PostInvalidate();
        }

        /**
     * Adds a graphic to the overlay.
     */
        public void Add (Graphic graphic)
        {
            lock (mLock) {
                mGraphics.Add (graphic);
            }
            PostInvalidate();
        }

        /**
     * Removes a graphic from the overlay.
     */
        public void Remove (Graphic graphic)
        {
            lock (mLock) {
                mGraphics.Remove (graphic);
            }
            PostInvalidate();
        }

        /**
     * Sets the camera attributes for size and facing direction, which informs how to transform
     * image coordinates later.
     */
        public void SetCameraInfo (int previewWidth, int previewHeight, CameraFacing facing)
        {
            lock (mLock) {
                mPreviewWidth = previewWidth;
                mPreviewHeight = previewHeight;
                mFacing = facing;
            }
            PostInvalidate ();
        }

        /**
     * Draws the overlay with its associated graphic objects.
     */

        protected override void OnDraw (Canvas canvas)
        {
            base.OnDraw (canvas);

            lock (mLock) {
                if ((mPreviewWidth != 0) && (mPreviewHeight != 0)) {
                    mWidthScaleFactor = (float) canvas.Width / (float) mPreviewWidth;
                    mHeightScaleFactor = (float) canvas.Height / (float) mPreviewHeight;
                }

                foreach (var graphic in mGraphics) {
                    graphic.Draw (canvas);
                }
            }
        }
    }
}

