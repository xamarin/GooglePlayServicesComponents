using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace FirebaseInvitesQuickstart.UITests
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
            app.WaitForElement(q => q.Id("invite_button"));

            app.Screenshot("Launch");
        }

        [Test]
        public void InviteFriends()
        {
            app.WaitForElement(q => q.Id("invite_button"));

            app.Screenshot("Launch");

            app.Tap(q => q.Id("invite_button"));

            System.Threading.Thread.Sleep(3000);

            app.Screenshot("Invite Screen");
        }
    }
}
