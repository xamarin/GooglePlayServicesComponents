using System;

namespace AndroidPayQuickstart
{
    [Android.App.Activity (Label = "Login")]
    public class LoginActivity : BikestoreFragmentActivity
    {
        public const string EXTRA_ACTION = "EXTRA_ACTION";

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        
            SetContentView (Resource.Layout.activity_login);

            var loginAction = Intent.GetIntExtra (EXTRA_ACTION, Action.LOGIN);
            var fragment = ResultTargetFragment;
            if (fragment == null) {
                fragment = LoginFragment.NewInstance (loginAction);
                SupportFragmentManager.BeginTransaction ()
                    .Add (Resource.Id.login_fragment, fragment)
                    .Commit ();
            }
        }

        public override bool OnCreateOptionsMenu (Android.Views.IMenu menu)
        {
            return false;
        }

        protected override Android.Support.V4.App.Fragment ResultTargetFragment {
            get {
                return SupportFragmentManager.FindFragmentById (Resource.Id.login_fragment);
            }
        }

        public static class Action {
            public const int LOGIN = 2000;
            public const int LOGOUT = 2001;
        }
    }
}

