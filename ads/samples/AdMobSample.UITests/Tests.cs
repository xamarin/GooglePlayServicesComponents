using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace AdMobSample.UITests
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
        public void XmlBanner ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Text ("Banner in XML"));

            app.Screenshot ("Tap Xml");

            app.WaitForElement (q => q.Class ("AdView"), timeout: TimeSpan.FromSeconds (10));

            app.Screenshot ("Ad");
        }

        [Test]
        public void CodeBanner ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Text ("Banner in Code"));

            app.Screenshot ("Tap Xml");

            System.Threading.Thread.Sleep (3000);

            app.Screenshot ("Ad");
        }

        [Test]
        public void Interstitial ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Text ("Interstitial"));

            app.WaitForElement (q => q.Id ("loadButton"));

            app.Screenshot ("Tap Interstitial");

            app.Tap (q => q.Id ("loadButton"));

            System.Threading.Thread.Sleep (3000);

            app.Screenshot ("Ad");
        }
    }
}

