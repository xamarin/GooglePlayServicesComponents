using System;
using Android.Graphics;
using Android.Gms.Vision.Barcodes;

namespace VisionSample
{
    class BarcodeGraphic : GraphicOverlay.Graphic 
    {
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

        Paint mPositionPaint;
        Paint mIdPaint;
        Paint mBoxPaint;

        Barcode mBarcode;
        public int Id { get;set; }

        public BarcodeGraphic (GraphicOverlay overlay) : base (overlay)
        {            
            mCurrentColorIndex = (mCurrentColorIndex + 1) % COLOR_CHOICES.Length;
            var selectedColor = COLOR_CHOICES[mCurrentColorIndex];

            mPositionPaint = new Paint();
            mPositionPaint.Color = selectedColor;

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
        public void UpdateBarcode (Barcode barcode)
        {
            Console.WriteLine ("Barcode Format: {0}", barcode.Format);
            Console.WriteLine ("Value Format: {0}", barcode.ValueFormat);

            mBarcode = barcode;
            PostInvalidate ();
        }

        /**
     * Draws the face annotations for position on the supplied canvas.
     */
        public override void Draw (Canvas canvas) 
        {
            Barcode barcode = mBarcode;
            if (barcode == null)
                return;

            var v = barcode.DisplayValue;

            //canvas.DrawText ("id: " + Id, x + ID_X_OFFSET, y + ID_Y_OFFSET, mIdPaint);

            canvas.DrawRect (barcode.BoundingBox.Left, barcode.BoundingBox.Top, barcode.BoundingBox.Right, barcode.BoundingBox.Bottom, mBoxPaint);
        }
    }
}

