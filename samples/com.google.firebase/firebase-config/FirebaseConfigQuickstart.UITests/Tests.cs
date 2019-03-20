using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace FirebaseConfigQuickstart.UITests
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
            app.WaitForElement(q => q.Id("priceView"));

            app.Screenshot("Launch");
        }

        [Test]
        public void FetchPrice()
        {
            app.WaitForElement(q => q.Id("fetchButton"));

            app.Screenshot("Launch");

            app.Tap(q => q.Id("fetchButton"));

            app.Screenshot("Fetching");

            app.WaitForElement(q => q.Text("Fetch Succeeded"));

            app.Screenshot("Fetched");
        }
    }
}
