
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
using Android.Gms.Plus;

namespace PlusSample
{
    [Activity (Label = "Plus One")]			
    public class PlusOneActivity : Activity
    {
        const string URL = "https://developers.google.com/+";

        // The request code must be 0 or higher.
        const int PLUS_ONE_REQUEST_CODE = 0;

        PlusOneButton mPlusOneSmallButton;
        PlusOneButton mPlusOneMediumButton;
        PlusOneButton mPlusOneTallButton;
        PlusOneButton mPlusOneStandardButton;
        PlusOneButton mPlusOneStandardButtonWithAnnotation;

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.plus_one_activity);

            /*
         * The {@link PlusOneButton} can be configured in code, but in this example we
         * have set the parameters in the layout.
         *
         * Example:
         * mPlusOneSmallButton.setAnnotation(PlusOneButton.ANNOTATION_INLINE);
         * mPlusOneSmallButton.setSize(PlusOneButton.SIZE_MEDIUM);
         */
            mPlusOneSmallButton = FindViewById<PlusOneButton> (Resource.Id.plus_one_small_button);
            mPlusOneMediumButton = FindViewById<PlusOneButton> (Resource.Id.plus_one_medium_button);
            mPlusOneTallButton = FindViewById<PlusOneButton> (Resource.Id.plus_one_tall_button);
            mPlusOneStandardButton = FindViewById<PlusOneButton> (Resource.Id.plus_one_standard_button);
            mPlusOneStandardButtonWithAnnotation = FindViewById<PlusOneButton> (Resource.Id.plus_one_standard_ann_button);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb) {
                ActionBar.SetDisplayHomeAsUpEnabled (true);
            }
        }
            
        protected override void OnResume () 
        {
            base.OnResume();
            // Refresh the state of the +1 button each time we receive focus.
            mPlusOneSmallButton.Initialize (URL, PLUS_ONE_REQUEST_CODE);
            mPlusOneMediumButton.Initialize (URL, PLUS_ONE_REQUEST_CODE);
            mPlusOneTallButton.Initialize (URL, PLUS_ONE_REQUEST_CODE);
            mPlusOneStandardButton.Initialize (URL, PLUS_ONE_REQUEST_CODE);
            mPlusOneStandardButtonWithAnnotation.Initialize (URL, PLUS_ONE_REQUEST_CODE);
        }
            
        public override bool OnOptionsItemSelected (IMenuItem item) 
        {
            switch (item.ItemId) {
            case Android.Resource.Id.Home:
                var intent = new Intent (this, typeof(PlusSampleActivity));
                intent.AddFlags (ActivityFlags.ClearTop);
                StartActivity (intent);
                Finish ();
                return true;

            default:
                return base.OnOptionsItemSelected (item);
            }
        }
    }
}

