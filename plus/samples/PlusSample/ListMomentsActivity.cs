//using System;
//using Android.App;
//using Android.Widget;
//using System.Collections.Generic;
//using Android.OS;
//using Android.Views;
//using Android.Content;
//using Android.Gms.Plus;
//using Android.Gms.Common.Apis;
//using Android.Gms.Common;
//using Java.Interop;
//using Android.Gms.Plus.Model.People;
//using Android.Gms.Plus.Model.Moments;

//namespace PlusSample
//{
//    [Activity (Label = "Moments")]
//    public class ListMomentsActivity : Activity, 
//        GoogleApiClient.IConnectionCallbacks, 
//        GoogleApiClient.IOnConnectionFailedListener, 
//    IDialogInterfaceOnCancelListener
//    {
//        const string STATE_RESOLVING_ERROR = "resolving_error";

//        const int DIALOG_GET_GOOGLE_PLAY_SERVICES = 1;
//        const int REQUEST_CODE_SIGN_IN = 1;
//        const int REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES = 2;

//        ListView mMomentListView;
//        MomentListAdapter mMomentListAdapter;
//        List<IMoment> mListItems;
//        bool mResolvingError;

//        GoogleApiClient mGoogleApiClient;

//        protected override void OnCreate (Bundle savedInstanceState) 
//        {
//            base.OnCreate (savedInstanceState);
//            SetContentView (Resource.Layout.list_moments_activity);

//            var options = new PlusClass.PlusOptions.Builder()
//                .AddActivityTypes (MomentUtil.ACTIONS).Build ();
            
//            mGoogleApiClient = new GoogleApiClient.Builder(this)
//                .AddConnectionCallbacks (this)
//                .AddOnConnectionFailedListener (this)
//                .AddApi (PlusClass.API, options)
//                .AddScope (PlusClass.ScopePlusLogin)
//                .Build();

//            mListItems = new List<IMoment>();
//            mMomentListAdapter = new MomentListAdapter (this, Android.Resource.Layout.SimpleListItem1, mListItems);
//            mMomentListView = FindViewById<ListView> (Resource.Id.moment_list);
//            mMomentListView.ItemClick += (sender, e) => {
//                var moment = mMomentListAdapter.GetItem (e.Position);
//                if (moment != null) {
//                    if (mGoogleApiClient.IsConnected) {
//                        PlusClass.MomentsApi.Remove (mGoogleApiClient, moment.Id);
//                        Toast.MakeText (this, GetString(Resource.String.plus_remove_moment_status),
//                            ToastLength.Short).Show ();
//                    } else {
//                        Toast.MakeText (this, GetString(Resource.String.greeting_status_sign_in_required),
//                            ToastLength.Short).Show ();
//                    }
//                }
//            };

//            mResolvingError = savedInstanceState != null
//                && savedInstanceState.GetBoolean (STATE_RESOLVING_ERROR, false);

//            int available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
//            if (available != CommonStatusCodes.Success) {
//                ShowDialog (DIALOG_GET_GOOGLE_PLAY_SERVICES);
//            }

//            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb) {
//                this.ActionBar.SetDisplayHomeAsUpEnabled (true);
//            }
//        }
            
//        protected override Dialog OnCreateDialog (int id) 
//        {
//            if (id != DIALOG_GET_GOOGLE_PLAY_SERVICES) {
//                return base.OnCreateDialog(id);
//            }

//            int available = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
//            if (available == CommonStatusCodes.Success)
//                return null;

//            if (GooglePlayServicesUtil.IsUserRecoverableError (available)) {
//                return GooglePlayServicesUtil.GetErrorDialog (
//                    available, this, REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES, this);
//            }
//            return new AlertDialog.Builder(this)
//                .SetMessage(Resource.String.plus_generic_error)
//                .SetCancelable(true)
//                .SetOnCancelListener(this)
//                .Create ();
//        }
            
//        protected override void OnStart () 
//        {
//            base.OnStart();
//            mGoogleApiClient.Connect ();
//        }
            
//        protected override void OnSaveInstanceState (Bundle outState) 
//        {
//            base.OnSaveInstanceState(outState);
//            outState.PutBoolean (STATE_RESOLVING_ERROR, mResolvingError);
//        }
            
//        protected override void OnStop () 
//        {
//            mGoogleApiClient.Disconnect ();
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
//                if (!mGoogleApiClient.IsConnecting  && !mGoogleApiClient.IsConnected) {
//                    mGoogleApiClient.Connect ();
//                }
//            } else {
//                Console.WriteLine ("Unable to sign the user in.");
//                Finish ();
//            }
//        }

//        public void OnConnected (Bundle connectionHint) 
//        {
//            PlusClass.MomentsApi.Load (mGoogleApiClient).SetResultCallback<IMomentsLoadMomentsResult> (result => {
//                switch (result.Status.StatusCode) {
//                case CommonStatusCodes.Success:
//                    var momentBuffer = result.MomentBuffer;

//                    mListItems.Clear ();
//                    try {
//                        int count = momentBuffer.Count;
//                        for (int i = 0; i < count; i++) {
//                            var moment = momentBuffer.Get (i).JavaCast<IMoment> ();
//                            mListItems.Add (moment);
//                        }
//                    } finally {
//                        momentBuffer.Close ();
//                    }

//                    mMomentListAdapter.NotifyDataSetChanged ();
//                    break;

//                case CommonStatusCodes.SignInRequired:
//                    mGoogleApiClient.Disconnect();
//                    mGoogleApiClient.Connect();
//                    break;

//                default:
//                    Console.WriteLine ("Error when listing moments: " + result.Status);
//                    break;
//                }
//            });
//            mMomentListView.Adapter = mMomentListAdapter;
//        }
            
//        public void OnConnectionSuspended(int cause) 
//        {
//            mMomentListView.Adapter = null;
//            mGoogleApiClient.Connect();
//        }
            
//        public void OnConnectionFailed (ConnectionResult result) 
//        {
//            mMomentListView.Adapter = null;
//            if (mResolvingError) {
//                return;
//            }

//            try {
//                result.StartResolutionForResult (this, REQUEST_CODE_SIGN_IN);
//                mResolvingError = true;
//            } catch (IntentSender.SendIntentException e) {
//                // Try connecting again.
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
//                Intent intent = new Intent(this, typeof (PlusSampleActivity));
//                intent.AddFlags (ActivityFlags.ClearTop);
//                StartActivity(intent);
//                Finish ();
//                return true;

//            default:
//                return base.OnOptionsItemSelected (item);
//            }
//        }

//        /**
//     * Array adapter that maintains a Moment list.
//     */
//        private class MomentListAdapter : ArrayAdapter<IMoment> 
//        {
//            LayoutInflater mLayoutInflater;
//            List<IMoment> mItems;

//            public MomentListAdapter(Context context, int textViewResourceId, List<IMoment> objects)
//                : base (context, textViewResourceId, objects)
//            {
//                mLayoutInflater = LayoutInflater.From (context);
//                mItems = objects;
//            }
                
//            public override View GetView (int position, View convertView, ViewGroup parent) 
//            {
//                var resultView = convertView;
//                if (resultView == null) {
//                    resultView = mLayoutInflater.Inflate (Resource.Layout.moment_row, null);
//                }

//                var moment = mItems[position];
//                if (moment != null) {
//                    var typeView = resultView.FindViewById<TextView> (Resource.Id.moment_type);
//                    var titleView = resultView.FindViewById<TextView> (Resource.Id.moment_title);

//                    var type = Android.Net.Uri.Parse (moment.Type).Path.Substring (1);
//                    typeView.Text = type;

//                    if (moment.Target != null) {
//                        titleView.Text = moment.Target.Name;
//                    }
//                }

//                return resultView;
//            }
//        }
//    }
//}

