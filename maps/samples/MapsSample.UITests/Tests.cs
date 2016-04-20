using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace MapsSample.UITests
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
        public void BasicMap ()
        {
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Basic Map"));
            app.WaitForElement (q => q.Id ("map"));
            app.Screenshot ("Map");
            app.PinchToZoomIn (q => q.Id ("map"));
            app.Screenshot ("Pinch Zoom");
        }

        [Test]
        public void CameraControls ()
        {
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Camera"));
            app.WaitForElement (q => q.Id ("map"));
            app.Screenshot ("Map");

            //app.Tap (q => q.Id ("animate"));
            //app.Screenshot ("Disable Animation");

            app.Tap (q => q.Id ("scroll_left"));
            pause ();
            app.Screenshot ("Scroll Left");

            app.Tap (q => q.Id ("scroll_up"));
            pause ();
            app.Screenshot ("Scroll Up");

            app.Tap (q => q.Id ("scroll_down"));
            pause ();
            app.Screenshot ("Scroll Down");

            app.Tap (q => q.Id ("scroll_right"));
            pause ();
            app.Screenshot ("Scroll Right");

            app.Tap (q => q.Id ("tilt_more"));
            pause ();
            app.Screenshot ("Tilt More");

            app.Tap (q => q.Id ("tilt_less"));
            pause ();
            app.Screenshot ("Tilt Less");

            app.Tap (q => q.Id ("sydney"));
            pause ();
            app.Screenshot ("Go to Sydney");

            app.Tap (q => q.Id ("bondi"));
            pause ();
            app.Screenshot ("Go to Bondi");
        }

        [Test]
        public void IndoorDemo ()
        {
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Indoor"));
            app.WaitForElement (q => q.Id ("map"));
            app.Screenshot ("Map");

            app.Tap (q => q.Id ("focused_bulding_info_button"));
            app.WaitForElement (q => q.Text ("3 2 1 "));
            app.Screenshot ("Focused Building");

            app.Tap (q => q.Id ("focused_level_info_button"));
            app.WaitForElement (q => q.Text ("2"));
            app.Screenshot ("Focused Level");

            // Tap level 1
            app.Tap (q => q.Text ("1"));
            app.Tap (q => q.Id ("focused_level_info_button"));
            app.WaitForElement (q => q.Text ("1"));
            app.Screenshot ("Level 1");

            app.Tap (q => q.Id ("toggle_level_picker_button"));
            app.WaitForNoElement (q => q.Text ("3"));
            app.Screenshot ("Toggle Level Picker");
            app.Tap (q => q.Id ("toggle_level_picker_button"));
            app.WaitForElement (q => q.Text ("3"));
            app.Screenshot ("Toggle Level Picker");

            app.Tap (q => q.Id ("higher_level_button"));
            app.WaitForElement (q => q.Text ("Activating level 2"));
            app.Screenshot ("Activate Higher Level");
        }

        [Test]
        public void LayersDemo ()
        {            
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Layers"));
            app.WaitForElement (q => q.Id ("map"));
            app.Screenshot ("Map");

            //app.Device.SetLocation (37.7842, -122.4016);

            app.Tap (q => q.Id ("my_location"));           
            app.WaitForElement (q => q.Class ("ImageView").Marked ("My Location"));
            app.Screenshot ("Enable My Location");
            app.Tap (q => q.Class ("ImageView").Marked ("My Location"));
            pause (5000);
            app.Screenshot ("Show My Location");

            app.DoubleTap (q => q.Id ("map"));
            pause ();
            app.DoubleTap (q => q.Id ("map"));
            pause ();
            app.Screenshot ("Zoomed In");

            app.Tap (q => q.Id ("traffic"));
            app.Screenshot ("Show Traffic");

            app.Tap (q => q.Id ("buildings"));
            app.Screenshot ("Hide Buildings");

            app.Tap (q => q.Id ("indoor"));
            app.Screenshot ("Hide Indoor");

            app.Tap (q => q.Id ("layers_spinner"));
            app.WaitForElement (q => q.Text ("Hybrid"));
            app.Tap (q => q.Text ("Hybrid"));
            pause ();
            app.Screenshot ("Hybrid");
        }

        [Test]
        public void LiteMode ()
        {
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Lite Mode"));
            app.WaitForElement (q => q.Id ("map"));
            app.Screenshot ("Map");

            app.Tap (q => q.Id ("go_to_darwin"));
            pause ();
            app.Screenshot ("Darwin");

            app.Tap (q => q.Id ("go_to_adelaide"));
            pause ();
            app.Screenshot ("Adelaide");

            app.Tap (q => q.Id ("go_to_australia"));
            pause ();
            app.Screenshot ("Australia");

        }

        [Test]
        public void LiteModeListView ()
        {
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Lite Mode ListView"));
            app.WaitForElement (q => q.Marked ("Google Map"));
            app.Screenshot ("Maps");

            app.ScrollDown ();
            pause ();
            app.Screenshot ("Scroll Down 1");
            app.ScrollDown ();
            pause ();
            app.Screenshot ("Scroll Down 2");
            app.ScrollDown ();
            pause ();
            app.Screenshot ("Scroll Down 3");
        }

        [Test]
        public void ProgrammaticallyAddMap ()
        {            
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Programmatically add map"));
            app.WaitForElement (q => q.Marked ("Google Map"));
            app.Screenshot ("Map");
        }

        [Test]
        public void RawMapView ()
        {            
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Raw MapView"));
            app.WaitForElement (q => q.Id ("map"));
            app.Screenshot ("Map");
        }

        [Test]
        public void StreeViewPanoramaAndMap ()
        {            
            app.Screenshot ("Launch");
            app.ScrollDownAndTap (q => q.Text ("Street View Panorama and Map"));
            app.WaitForElement (q => q.Id ("map"));
            app.WaitForElement (q => q.Id ("streetviewpanorama"));
            app.Screenshot ("Map");
        }

        void pause (int ms = 1000)
        {
            System.Threading.Thread.Sleep (ms);
        }                      
    }

    static class AppExtensions
    {
        public static void ScrollDownAndTap(this IApp app, Func<AppQuery, AppQuery> query, int timesToScroll = 100)
        {
            for (int i = 0; i < timesToScroll; i++) {
                var results = app.Query (query);
                if (results.Any ()) {
                    app.Tap (query);
                    return;
                }

                app.ScrollDown ();
                // sleep half a sec before next scroll
                System.Threading.Thread.Sleep (200);
            }

            throw new Exception ("Item was not found after scrolling " + timesToScroll + " times.");
        }
    }
}

