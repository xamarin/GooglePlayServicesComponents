
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
//using Fragment = Android.Support.V4.App.Fragment;

namespace Analytics
{
    [Activity (MainLauncher = true, Label = "@string/app_name", Theme = "@style/Theme.AppCompat")]			
    public class MobilePlayground : AppCompatActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            setupDisplay ();
        }

        void setupDisplay ()
        {
            var bar = SupportActionBar;

            bar.NavigationMode = (int)ActionBarNavigationMode.Tabs;
            bar.SetDisplayOptions (0, (int)ActionBarDisplayOptions.ShowTitle);

            bar.AddTab (bar.NewTab ().SetText (Resource.String.screenTabName).SetTabListener (
                new TabListener<AppviewFragment> (this, "appview")));

            bar.AddTab (bar.NewTab ().SetText (Resource.String.eventTabName).SetTabListener (
                new TabListener<EventFragment> (this, "event")));

            bar.AddTab (bar.NewTab ().SetText (Resource.String.exceptionTabName).SetTabListener (
                new TabListener<ExceptionFragment> (this, "exception")));

            bar.AddTab (bar.NewTab ().SetText (Resource.String.socialTabName).SetTabListener (
                new TabListener<SocialFragment> (this, "social")));

            bar.AddTab (bar.NewTab ().SetText (Resource.String.timingTabName).SetTabListener (
                new TabListener<TimingFragment> (this, "timing")));

            bar.AddTab (bar.NewTab ().SetText (Resource.String.ecommerceTabName).SetTabListener (
                new TabListener<EcommerceFragment> (this, "ecommerce")));

            bar.AddTab (bar.NewTab ().SetText (Resource.String.ecommerceTabName).SetTabListener (
                new TabListener<IssuesFragment> (this, "issues")));
        }

        class TabListener<TFragment> : Java.Lang.Object, AndroidX.AppCompat.App.ActionBar.ITabListener 
            where TFragment : AndroidX.Fragment.App.Fragment
        {
            AppCompatActivity parent;
            string tag;
            Type fragmentType;
            Bundle args;
            AndroidX.Fragment.App.Fragment fragment;

            public TabListener (AppCompatActivity activity, string tag, Bundle args = null)
            {
                this.parent = activity;
                this.tag = tag;
                this.fragmentType = typeof(TFragment);
                this.args = args;

                this.fragment = parent.SupportFragmentManager.FindFragmentByTag (tag);

                if (fragment != null && !fragment.IsDetached) {
                    activity.SupportFragmentManager.BeginTransaction ()
                        .Detach (fragment)
                        .Commit ();
                }

            }

            public void OnTabReselected(AndroidX.AppCompat.App.ActionBar.Tab tab, AndroidX.Fragment.App.FragmentTransaction ft)
            {
                throw new NotImplementedException();
            }

            public void OnTabSelected (AndroidX.AppCompat.App.ActionBar.Tab tab, AndroidX.Fragment.App.FragmentTransaction ft)
            {
                var className = Java.Lang.Class.FromType (fragmentType).Name;

                if (fragment == null) {
                    fragment = AndroidX.Fragment.App.Fragment.Instantiate (parent, className, args);
                    ft.Add (Android.Resource.Id.Content, fragment, tag);
                } else {
                    ft.Attach (fragment);
                }

                // TODO: Add AutoTracking here once the autoTracking fixes make it into the SDK.
            }

            public void OnTabUnselected (AndroidX.AppCompat.App.ActionBar.Tab tab, AndroidX.Fragment.App.FragmentTransaction ft)
            {
                if (fragment != null)
                    ft.Detach (fragment);
            }
        }
    }
}

    