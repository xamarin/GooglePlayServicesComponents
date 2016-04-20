using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace AndroidPayQuickstart.UITests
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
        public void Login ()
        {
            app.Screenshot ("Launch");

            app.Tap (q => q.Id ("login"));
            app.WaitForElement (q => q.Id ("sign_in_button"));
            app.Screenshot ("Sign In");

            app.Tap (q => q.Id ("sign_in_button"));
            System.Threading.Thread.Sleep (1000);
            app.Screenshot ("Sign In Dialog");
        }

        [Test]
        public void AddItemToCart ()
        {
            app.Screenshot ("Launch");
            app.WaitForElement (q => q.Id ("promo_image"));

            app.Tap (q => q.Text ("Simple Bike"));
            app.WaitForElement (q => q.Id ("text_details_item_name"));
            app.Screenshot ("Item Details");

            app.Tap (q => q.Id ("button_details_button_add"));
            app.WaitForElement (q => q.Id ("dynamic_wallet_button_fragment"));
            app.Screenshot ("Cart");

            app.Tap (q => q.Text ("Buy with "));
            System.Threading.Thread.Sleep (1000);
            app.Screenshot ("Checkout Dialog");
        }
    }
}

