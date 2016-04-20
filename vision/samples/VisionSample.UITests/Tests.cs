using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace VisionSample.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest ()
        {
            app = ConfigureApp
                .Android
                .ApkFile ("app.apk")
                .PreferIdeSettings ()
                .StartApp ();
        }

        //[Test]
        public void Repl ()
        {
            app.Repl ();
        }

        [Test]
        public void AppLaunches ()
        {
            app.Screenshot ("Launch");
        }

        [Test]
        public void FaceTracker ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Id ("buttonFaceTracker"));

            app.WaitForElement (q => q.Class ("CameraSourcePreview"));

            app.Screenshot ("Face Tracker");
        }

        [Test]
        public void BarcodeTracker ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Id ("buttonBarcode"));

            app.WaitForElement (q => q.Class ("CameraSourcePreview"));

            app.Screenshot ("Barcode Tracker");
        }
    }
}

