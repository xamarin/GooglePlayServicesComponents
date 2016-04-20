using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace NearbySample.UITests
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
        public void NearbyAdvertise ()
        {
            app.Screenshot ("Launch");

            var beforeLength = GetDebugTextLength ();

            app.Tap (q => q.Id ("button_advertise"));

            System.Threading.Thread.Sleep (3000);

            app.Screenshot ("Advertise");

            Assert.Greater (GetDebugTextLength (), beforeLength);
        }

        [Test]
        public void NearbyDiscover ()
        {
            app.Screenshot ("Launch");

            var beforeLength = GetDebugTextLength ();

            app.Tap (q => q.Id ("button_discover"));

            System.Threading.Thread.Sleep (3000);

            app.Screenshot ("Discover");

            Assert.Greater (GetDebugTextLength (), beforeLength);
        }

        int GetDebugTextLength ()
        {
            return app.Query (q => q.Id ("debug_text")).FirstOrDefault ().Text.Length;
        }
    }
}

