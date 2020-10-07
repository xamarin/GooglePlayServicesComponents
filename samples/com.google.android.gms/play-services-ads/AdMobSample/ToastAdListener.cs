using System;
using Android.Gms.Ads;
using Android.Content;
using Android.Widget;

namespace AdMob
{
    public class ToastAdListener : AdListener
    {
        public ToastAdListener (Context context)
        {
            this.context = context;
        }

        Context context;

        public Action AdLoadedAction { get; set; }
        public Action AdFailedToLoadAction { get; set; }

        public override void OnAdLoaded ()
        {
            if (AdLoadedAction != null) {
                AdLoadedAction ();
                return;
            }

            Toast.MakeText (context, "OnAdLoaded ()", ToastLength.Short).Show ();
        }

        public override void OnAdFailedToLoad (int errorCode)
        {
            if (AdFailedToLoadAction != null) {
                AdFailedToLoadAction ();
                return;
            }

            var errorReason = string.Empty;

            switch(errorCode) {
            case AdRequest.ErrorCodeInternalError:
                errorReason = "Internal error";
                break;
            case AdRequest.ErrorCodeInvalidRequest:
                errorReason = "Invalid request";
                break;
            case AdRequest.ErrorCodeNetworkError:
                errorReason = "Network Error";
                break;
            case AdRequest.ErrorCodeNoFill:
                errorReason = "No fill";
                break;
            }

            Toast.MakeText(context, String.Format("OnAdFailedToLoad ({0})", errorReason),
                ToastLength.Short).Show ();
        }

        public override void OnAdOpened ()
        {
            Toast.MakeText (context, "OnAdOpened ()", ToastLength.Short).Show ();
        }

        public override void OnAdClosed ()
        {
            Toast.MakeText (context, "OnAdClosed ()", ToastLength.Short).Show ();
        }

        public override void OnAdLeftApplication ()
        {
            Toast.MakeText (context, "OnAdLeftApplication ()", ToastLength.Short).Show ();
        }          
    }
}

