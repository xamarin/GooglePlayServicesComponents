
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.Gms.Plus.Model.Moments;
//using Android.Gms.Plus;
//using Android.Gms.Common;
//using Android.Gms.Common.Apis;

//namespace PlusSample
//{
//    [Activity (Label = "Moment")]			
//    public class MomentActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, IDialogInterfaceOnCancelListener
//    {
//        const string TAG = "MomentActivity";

//        const string STATE_RESOLVING_ERROR = "resolvingError";

//        const int DIALOG_GET_GOOGLE_PLAY_SERVICES = 1;

//        const int REQUEST_CODE_SIGN_IN = 1;
//        const int REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES = 2;

//        GoogleApiClient mGoogleApiClient;
//        ArrayAdapter<string> mListAdapter;
//        ListView mMomentListView;
//        bool mResolvingError;

//        protected override void OnCreate (Bundle savedInstanceState) 
//        {
//            base.OnCreate (savedInstanceState);

//            SetContentView (Resource.Layout.multi_moment_activity);

//            var options = new PlusClass.PlusOptions.Builder().AddActivityTypes (MomentUtil.ACTIONS).Build ();
//            mGoogleApiClient = new GoogleApiClient.Builder (this)
//                .AddConnectionCallbacks(this)
//                .AddOnConnectionFailedListener(this)
//                .AddApi (PlusClass.API, options)
//                .AddScope (PlusClass.ScopePlusLogin)
//                .Build ();

//            mListAdapter = new ArrayAdapter<string>(
//                this, Android.Resource.Layout.SimpleListItem1, MomentUtil.MOMENT_LIST);
//            mMomentListView = FindViewById<ListView>(Resource.Id.moment_list);
//            mMomentListView.ItemClick += (sender, e) => {
//                if (mGoogleApiClient.IsConnected) {
//                    var textView = e.View as TextView;
//                    var momentType = textView.Text;
//                    var targetUrl = MomentUtil.MOMENT_TYPES[momentType];

//                    var target = new ItemScopeBuilder ().SetUrl(targetUrl).Build ();

//                    var momentBuilder = new MomentBuilder ();
//                    momentBuilder.SetType ("http://schemas.google.com/" + momentType);
//                    momentBuilder.SetTarget (target);

//                    var result = MomentUtil.GetResultFor (momentType);
//                    if (result != null)
//                        momentBuilder.SetResult (result);

//                    PlusClass.MomentsApi.Write (mGoogleApiClient, momentBuilder.Build ()).SetResultCallback<Statuses> (status => 
//                    {
//                        switch (status.StatusCode) {
//                        case CommonStatusCodes.Success:
//                            Toast.MakeText (this, GetString (Resource.String.plus_write_moment_status_success), ToastLength.Short).Show ();
//                            break;

//                        case CommonStatusCodes.SuccessCache:
//                            Toast.MakeText(this, GetString (Resource.String.plus_write_moment_status_cached), ToastLength.Short).Show ();
//                            break;

//                        case CommonStatusCodes.SignInRequired:
//                            Toast.MakeText (this, GetString (Resource.String.plus_write_moment_status_auth_error), ToastLength.Short).Show();
//                            mGoogleApiClient.Disconnect();
//                            mGoogleApiClient.Connect();
//                            break;

//                        default:
//                            Toast.MakeText (this, GetString (Resource.String.plus_write_moment_status_error),
//                                ToastLength.Short).Show();
//                            Console.WriteLine ("Error when writing moments: " + status);
//                            break;
//                        }
//                    });
//                }
//            };

//            mResolvingError = savedInstanceState != null
//                && savedInstanceState.GetBoolean (STATE_RESOLVING_ERROR, false);

//            var available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
//            if (available != CommonStatusCodes.Success) {
//                ShowDialog (DIALOG_GET_GOOGLE_PLAY_SERVICES);
//            }

//            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb) {
//                ActionBar.SetDisplayHomeAsUpEnabled (true);
//            }
//        }
            
//        protected override Dialog OnCreateDialog (int id)
//        {
//            if (id != DIALOG_GET_GOOGLE_PLAY_SERVICES) {
//                return base.OnCreateDialog (id);
//            }

//            int available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
//            if (available == CommonStatusCodes.Success) {
//                return null;
//            }
//            if (GooglePlayServicesUtil.IsUserRecoverableError (available)) {
//                return GooglePlayServicesUtil.GetErrorDialog(
//                    available, this, REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES, this);
//            }
//            return new AlertDialog.Builder(this)
//                .SetMessage (Resource.String.plus_generic_error)
//                .SetCancelable (true)
//                .SetOnCancelListener(this)
//                .Create();
//        }
            
//        protected override void OnStart() 
//        {
//            base.OnStart ();
//            mGoogleApiClient.Connect ();
//        }
            
//        protected override void OnStop () 
//        {
//            mGoogleApiClient.Disconnect();
//            base.OnStop();
//        }
            
            
//        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
//        {           
//            switch (requestCode) {
//            case REQUEST_CODE_SIGN_IN:
//                mResolvingError = false;
//                handleResult(resultCode);
//                break;
//            case REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES:
//                handleResult(resultCode);
//                break;
//            }
//        }

//        void handleResult(Result resultCode) 
//        {
//            if (resultCode == Result.Ok) {
//                // onActivityResult is called after onStart (but onStart is not
//                // guaranteed to be called while signing in), so we should make
//                // sure we're not already connecting before we call connect again.
//                if (!mGoogleApiClient.IsConnecting && !mGoogleApiClient.IsConnected) {
//                    mGoogleApiClient.Connect ();
//                }
//            } else {
//                Console.WriteLine ("Unable to sign the user in.");
//                Finish();
//            }
//        }
            
//        protected override void OnSaveInstanceState (Bundle outState) 
//        {
//            base.OnSaveInstanceState(outState);
//            outState.PutBoolean (STATE_RESOLVING_ERROR, mResolvingError);
//        }
            
//        public void OnConnected(Bundle connectionHint) 
//        {
//            mMomentListView.Adapter = mListAdapter;
//        }
            
//        public void OnConnectionSuspended (int cause) 
//        {
//            mMomentListView.Adapter = null;
//        }
            
//        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result) 
//        {
//            mMomentListView.Adapter = null;
//            if (mResolvingError) {
//                return;
//            }
//            try {
//                result.StartResolutionForResult (this, REQUEST_CODE_SIGN_IN);
//                mResolvingError = true;
//            } catch (IntentSender.SendIntentException e) {
//                // Reconnect to get another intent to start.
//                mGoogleApiClient.Connect ();
//            }
//        }            

//        public void OnCancel (IDialogInterface dialog)
//        {
//            Console.WriteLine ("Unable to sign the user in.");
//            Finish();
//        }
            
//        public override bool OnOptionsItemSelected (IMenuItem item) 
//        {
//            switch (item.ItemId) {
//            case Android.Resource.Id.Home:
//                var intent = new Intent(this, typeof (PlusSampleActivity));
//                intent.AddFlags (ActivityFlags.ClearTop);
//                StartActivity(intent);
//                Finish();
//                return true;

//            default:
//                return base.OnOptionsItemSelected(item);
//            }
//        }
//    }
//}

