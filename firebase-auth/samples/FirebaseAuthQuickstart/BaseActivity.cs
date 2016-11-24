
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace FirebaseAuthQuickstart
{
    [Activity (Label = "Base Activity")]
    public class BaseActivity : AppCompatActivity
    {
        ProgressDialog mProgressDialog;

        public void ShowProgressDialog ()
        {
            if (mProgressDialog == null) {
                mProgressDialog = new ProgressDialog (this);
                mProgressDialog.SetMessage (GetString (Resource.String.loading));
                mProgressDialog.Indeterminate = true;
            }

            mProgressDialog.Show ();
        }

        public void HideProgressDialog ()
        {
            if (mProgressDialog != null && mProgressDialog.IsShowing) {
                mProgressDialog.Hide ();
            }
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy ();

            HideProgressDialog ();
        }

    }
}

