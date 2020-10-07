using System;
using Android.Graphics;
using Android.Gms.Vision.Faces;

namespace VisionSample
{
    class FaceGraphic : GraphicOverlay.Graphic 
    {
        const float FACE_POSITION_RADIUS = 10.0f;
        const float ID_TEXT_SIZE = 40.0f;
        const float ID_Y_OFFSET = 50.0f;
        const float ID_X_OFFSET = -50.0f;
        const float BOX_STROKE_WIDTH = 5.0f;

        readonly Color[] COLOR_CHOICES = {
            Color.Blue,
            Color.Cyan,
            Color.Green,
            Color.Magenta,
            Color.Red,
            Color.White,
            Color.Yellow
        };

        static int mCurrentColorIndex = 0;

        Paint mFacePositionPaint;
        Paint mIdPaint;
        Paint mBoxPaint;

        Face mFace;
        public int Id { get;set; }

        public FaceGraphic (GraphicOverlay overlay) : base (overlay)
        {            
            mCurrentColorIndex = (mCurrentColorIndex + 1) % COLOR_CHOICES.Length;
            var selectedColor = COLOR_CHOICES[mCurrentColorIndex];

            mFacePositionPaint = new Paint();
            mFacePositionPaint.Color = selectedColor;

            mIdPaint = new Paint();
            mIdPaint.Color = selectedColor;
            mIdPaint.TextSize = ID_TEXT_SIZE;

            mBoxPaint = new Paint();
            mBoxPaint.Color = selectedColor;
            mBoxPaint.SetStyle (Paint.Style.Stroke);
            mBoxPaint.StrokeWidth = BOX_STROKE_WIDTH;
        }

        /**
     * Updates the face instance from the detection of the most recent frame.  Invalidates the
     * relevant portions of the overlay to trigger a redraw.
     */
        public void UpdateFace(Face face)
        {
            mFace = face;
            PostInvalidate ();
        }

        /**
     * Draws the face annotations for position on the supplied canvas.
     */
        public override void Draw (Canvas canvas) 
        {
            Face face = mFace;
            if (face == null)
                return;

            // Draws a circle at the position of the detected face, with the face's track id below.
            float x = TranslateX(face.Position.X + face.Width / 2);
            float y = TranslateY(face.Position.Y + face.Height / 2);
            canvas.DrawCircle (x, y, FACE_POSITION_RADIUS, mFacePositionPaint);
            canvas.DrawText ("id: " + Id, x + ID_X_OFFSET, y + ID_Y_OFFSET, mIdPaint);

            // Draws a bounding box around the face.
            float xOffset = ScaleX(face.Width / 2.0f);
            float yOffset = ScaleY(face.Height / 2.0f);
            float left = x - xOffset;
            float top = y - yOffset;
            float right = x + xOffset;
            float bottom = y + yOffset;

            canvas.DrawRect (left, top, right, bottom, mBoxPaint);
        }
    }
}

