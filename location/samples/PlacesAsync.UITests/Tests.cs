using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace PlacesAsync.UITests
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
        public void Autocomplete ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Id("textSearch"));;
            app.EnterText("the");;

            app.Screenshot ("Enter Text");

            app.WaitForElement (q => q.Id ("text1"));

            app.Screenshot ("Autocomplete");
        }

        [Test]
        public void PlaceDetails ()
        {
            Autocomplete ();

            app.Tap (q => q.Id ("text1"));

            app.WaitForElement (q => q.Id ("textTitle"));

            app.Screenshot ("Details");
        }
    }
}

