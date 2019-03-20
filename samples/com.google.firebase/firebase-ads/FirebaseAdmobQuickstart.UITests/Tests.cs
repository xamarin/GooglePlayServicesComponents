using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace FirebaseAdmobQuickstart.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp
                .Android
                .ApkFile("app.apk")
                .PreferIdeSettings()
                .StartApp();
        }

        //[Test]
        public void Repl()
        {
            app.Repl();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("Launch");
        }

        [Test]
        public void ShowBannerAd()
        {
            app.Screenshot("Launch");

            app.WaitForElement(t => t.Class("AdView"));

            app.Screenshot("Banner Ad");
        }

        [Test]
        public void InterstitialAd()
        {
            app.Screenshot("Launch");

            app.Tap(q => q.Id("load_interstitial_button"));

            app.WaitForElement(q => q.Marked("Interstitial close button"));

            app.Screenshot("Interstitial Ad");
        }
    }
}
