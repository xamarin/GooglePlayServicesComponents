using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace FirebaseStorageQuickstart.UITests
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
            app.WaitForElement(q => q.Id("button_sign_in"));

            app.Screenshot("Launch");
        }

        [Test]
        public void SignIn()
        {
            app.WaitForElement(q => q.Id("button_sign_in"));

            app.Screenshot("Launch");

            app.Tap(q => q.Id("button_sign_in"));

            app.WaitForElement(q => q.Id("button_camera"));

            app.Screenshot("Signed In");
        }

        [Test]
        public void Upload()
        {
            app.WaitForElement(q => q.Id("button_sign_in"));

            app.Screenshot("Launch");

            app.Tap(q => q.Id("button_sign_in"));

            app.WaitForElement(q => q.Id("button_camera"));

            app.Screenshot("Signed In");

            app.Tap(q => q.Id("button_camera"));

            System.Threading.Thread.Sleep(2000);

            app.Screenshot("Camera");
        }
    }
}
