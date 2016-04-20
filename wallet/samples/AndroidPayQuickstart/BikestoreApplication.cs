using System;
using Android.Content;

namespace AndroidPayQuickstart
{
    [Android.App.Application]
    public class BikestoreApplication : Android.App.Application
    {
        public BikestoreApplication (IntPtr javaRef, Android.Runtime.JniHandleOwnership transfer) : base (javaRef, transfer)
        {
        }

        const string USER_PREFS = "com.google.android.gms.samples.wallet.USER_PREFS";
        const string KEY_USERNAME = "com.google.android.gms.samples.wallet.KEY_USERNAME";

        string userName;

        // Not being saved in shared preferences to let users try new addresses
        // between app invocations
        bool addressValidForPromo;

        ISharedPreferences prefs;

        public override void OnCreate ()
        {
            base.OnCreate ();

            prefs = GetSharedPreferences (USER_PREFS, FileCreationMode.Private);
            userName = prefs.GetString (KEY_USERNAME, null);
        }

        public Boolean IsLoggedIn {
            get { return userName != null; }
        }

        public void Login (string userName)
        {
            this.userName = userName;
            prefs.Edit ().PutString (KEY_USERNAME, userName).Commit ();
        }

        public void Logout () 
        {
            userName = null;
            prefs.Edit ().Remove (KEY_USERNAME).Commit ();
        }

        public String AccountName {
            get { return prefs.GetString (KEY_USERNAME, null); }
        }

        public bool IsAddressValidForPromo {
            get { return addressValidForPromo; }
            set { addressValidForPromo = value; }
        }
    }
}

