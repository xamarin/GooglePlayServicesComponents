using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace AdsLiteSample.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android.StartApp();
        }

        [Test]
        public void AppDidLaunch()
        {
            app.WaitForElement(c => c.Id("content"));
            app.Screenshot("App launched");
        }
    }
}
