using System;
using Android.Runtime;

namespace Android.Gms.Vision.Barcodes
{
    public partial class Barcode
    {
        public BarcodeFormat Format {
            get {
                return (BarcodeFormat)_Format;
            }
        }

        public BarcodeValueFormat ValueFormat {
            get {
                return (BarcodeValueFormat)_ValueFormat;
            }
        }
    }
}

