using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Crash;
using Android.Util;

namespace FirebaseCrashReportingQuickstart
{
    [Activity (Label = "Firebase Crash Reporting Quickstart", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class MainActivity : AppCompatActivity
    {
        const string TAG = "MainActivity";

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.activity_main);

            if (GetString (Resource.String.google_app_id) == "YOUR-APP-ID")
                throw new Exception ("Invalid google-services.json file.  Make sure you've downloaded your own config file and added it to your app project with the 'GoogleServicesJson' build action.");

            // Checkbox to indicate when to catch the thrown exception.
            var catchCrashCheckBox = FindViewById<CheckBox> (Resource.Id.catchCrashCheckBox);

            // Button that causes the NullPointerException to be thrown.
            var crashButton = FindViewById<Button> (Resource.Id.crashButton);
            crashButton.Click += (sender, e) => {
                // Log that crash button was clicked. This version of Crash.log() will include the
                // message in the crash report as well as show the message in logcat.
                FirebaseCrash.Logcat (LogPriority.Info, TAG, "Crash button clicked");

                // If catchCrashCheckBox is checked catch the exception and report is using
                // Crash.report(). Otherwise throw the exception and let Firebase Crash automatically
                // report the crash.
                if (catchCrashCheckBox.Checked) {
                    try {
                        throw new Exception ();
                    } catch (Exception ex) {
                        // [START log_and_report]
                        FirebaseCrash.Logcat (LogPriority.Error, TAG, "NPE caught");
                        FirebaseCrash.Report (ex);
                        // [END log_and_report]
                        Java.Lang.Throwable.FromException (ex);
                    }
                } else {
                    throw new Exception ("Intentionally Unhandled Exception");
                }
            };

        // Log that the Activity was created. This version of Crash.log() will include the message
        // in the crash report but will not be shown in logcat.
        // [START log_event]
        FirebaseCrash.Log ("Activity created");
        // [END log_event]
        }
    }
}


