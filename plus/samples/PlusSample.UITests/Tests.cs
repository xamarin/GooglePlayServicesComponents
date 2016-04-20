using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace PlusSample.UITests
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
        public void Launch ()
        {
            app.Screenshot ("Launch");
        }

        [Test]
        public void PlusSignIn ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Text ("Sign in"));

            app.WaitForElement (q => q.Class ("SignInButton"));

            app.Screenshot ("Sign In");
        }

        [Test]
        public void PlusPlusOne ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Text ("+1"));

            app.WaitForElement (q => q.Class ("PlusOneButton"));

            app.Screenshot ("+1 Button");
        }
    }
}

