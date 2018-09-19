using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace FirebaseAnalyticsQuickstart.UITests
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
        public void ChooseItem()
        {
            app.Screenshot("Launch");

            app.WaitForElement(q => q.Text("Hamburgers"));

            app.Screenshot("Choices");

            app.Tap(q => q.Text("Hamburgers"));

            app.Screenshot("Chose");
        }
    }
}
