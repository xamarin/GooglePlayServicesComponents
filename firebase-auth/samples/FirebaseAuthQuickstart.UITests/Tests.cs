using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace FirebaseAuthQuickstart.UITests
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
            app.WaitForElement(q => q.Text("AnonymousAuthActivity"));

            app.Screenshot("Launch");
        }

        [Test]
        public void AnonymousAuth()
        {
            app.WaitForElement(q => q.Text("AnonymousAuthActivity"));

            app.Screenshot("Launch");

            app.Tap(q => q.Text("AnonymousAuthActivity"));

            app.WaitForElement(q => q.Text("Anonymous Sign In"));

            app.Tap(q => q.Id("button_anonymous_sign_in"));

            System.Threading.Thread.Sleep(500);

            app.WaitForNoElement(q => q.Id("progress"));

            app.Screenshot("Signed In");
        }
    }
}
