using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace FirebaseCrashReportingQuickstart.UITests
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
            app.WaitForElement(q => q.Id("crashButton"));

            app.Screenshot("Launch");
        }

        [Test]
        public void CatchCrash()
        {
            app.WaitForElement(q => q.Id("crashButton"));

            app.Screenshot("Launch");

            app.Tap(q => q.Id("catchCrashCheckBox"));

            app.Screenshot("Catch Crash");

            app.Tap(q => q.Id("crashButton"));

            app.WaitForElement(q => q.Id("crashButton"));

            app.Screenshot("Caught Crash");
        }
    }
}
