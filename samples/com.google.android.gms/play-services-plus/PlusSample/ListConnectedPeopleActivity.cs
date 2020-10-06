using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;
using Java.Interop;

using Android.Gms.Common.Apis;
using Android.Gms.Plus.Model.People;

namespace PlusSample
{
    [Activity (Label = "Connected People")]
    public class ListConnectedPeopleActivity : Activity,
        Android.Gms.Common.Apis.GoogleApiClient.IConnectionCallbacks,
        Android.Gms.Common.Apis.GoogleApiClient.IOnConnectionFailedListener, 
        IDialogInterfaceOnCancelListener
    {
        const string TAG = "ListConnectedPeople";
        const string STATE_RESOLVING_ERROR = "resolving_error";

        const int DIALOG_GET_GOOGLE_PLAY_SERVICES = 1;

        const int REQUEST_CODE_SIGN_IN = 1;
        const int REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES = 2;

        ArrayAdapter mListAdapter;
        ListView mPersonListView;
        List<String> mListItems;
        Android.Gms.Common.Apis.GoogleApiClient mGoogleApiClient;
        bool mResolvingError;

        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.person_list_activity);

            //var options = new PlusClass.PlusOptions.Builder ()
            //    .AddActivityTypes (MomentUtil.ACTIONS).Build();
            
            mGoogleApiClient = new Android.Gms.Common.Apis.GoogleApiClient.Builder (this)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this)
                .AddApi (Android.Gms.Plus.PlusClass.API)//, options)
                .AddScope (Android.Gms.Plus.PlusClass.ScopePlusLogin)
                .Build();

            mListItems = new List<String>();
            mListAdapter = new ArrayAdapter<String>(this,
                Android.Resource.Layout.SimpleListItem1, mListItems);
            mPersonListView = FindViewById<ListView> (Resource.Id.person_list);
            mResolvingError = savedInstanceState != null
                && savedInstanceState.GetBoolean (STATE_RESOLVING_ERROR, false);

            int available = Android.Gms.Common.GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
            if (available != Android.Gms.Common.Apis.CommonStatusCodes.Success)
                ShowDialog (DIALOG_GET_GOOGLE_PLAY_SERVICES);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
                this.ActionBar.SetDisplayHomeAsUpEnabled(true);
        }
            
        protected override Dialog OnCreateDialog (int id) 
        {
            if (id != DIALOG_GET_GOOGLE_PLAY_SERVICES) {
                return base.OnCreateDialog (id);
            }

            int available = Android.Gms.Common.GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
            if (available == Android.Gms.Common.Apis.CommonStatusCodes.Success)
                return null;
            
            if (Android.Gms.Common.GooglePlayServicesUtil.IsUserRecoverableError (available)) {
                return Android.Gms.Common.GooglePlayServicesUtil.GetErrorDialog (
                    available, this, REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES, this);
            }
            return new AlertDialog.Builder(this)
                .SetMessage (Resource.String.plus_generic_error)
                .SetCancelable (true)
                .SetOnCancelListener (this)
                .Create ();
        }
            
        protected override void OnStart() 
        {
            base.OnStart ();
            mGoogleApiClient.Connect ();
        }
            
        protected override void OnSaveInstanceState (Bundle outState) 
        {
            base.OnSaveInstanceState(outState);
            outState.PutBoolean (STATE_RESOLVING_ERROR, mResolvingError);
        }
            
        protected override void OnStop() 
        {
            base.OnStop();
            mGoogleApiClient.Disconnect ();
        }
            
        protected void OnActivityResult(int requestCode, int resultCode, Intent data) 
        {
            switch (requestCode) {
            case REQUEST_CODE_SIGN_IN:
                mResolvingError = false;
                handleResult(resultCode);
                break;
            case REQUEST_CODE_GET_GOOGLE_PLAY_SERVICES:
                handleResult(resultCode);
                break;
            }
        }

        private void handleResult (int resultCode) {
            if (resultCode == (int)Result.Ok) {
                // onActivityResult is called after onStart (but onStart is not
                // guaranteed to be called while signing in), so we should make
                // sure we're not already connecting before we call connect again.
                if (!mGoogleApiClient.IsConnecting && !mGoogleApiClient.IsConnected) {
                    mGoogleApiClient.Connect ();
                }
            } else {
                Console.WriteLine ("Unable to sign the user in.");
                Finish ();
            }
        }
            
        public void OnConnected (Bundle connectionHint) 
        {
            mPersonListView.Adapter = mListAdapter;
            Android.Gms.Plus.PlusClass.PeopleApi.LoadConnected (mGoogleApiClient)
                .SetResultCallback<Android.Gms.Plus.IPeopleLoadPeopleResult> (result => {

                    switch (result.Status.StatusCode) {
                    case CommonStatusCodes.Success:
                        mListItems.Clear ();
                        var personBuffer = result.PersonBuffer;
                        try {
                            int count = personBuffer.Count;
                            for (int i = 0; i < count; i++) {
                                var person = personBuffer.Get (i).JavaCast<IPerson> ();
                                mListItems.Add (person.DisplayName);                    
                            }
                        } finally {
                            personBuffer.Close();
                        }

                        mListAdapter.NotifyDataSetChanged ();
                        break;

                    case CommonStatusCodes.SignInRequired:
                        mGoogleApiClient.Disconnect ();
                        mGoogleApiClient.Connect ();
                        break;

                    default:
                        Console.WriteLine ("Error when listing people: " + result.Status);
                        break;
                    }
                });
        }
            
        public void OnConnectionSuspended (int cause) 
        {
            mPersonListView.Adapter = null;
            mGoogleApiClient.Connect ();
        }
            
        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result) 
        {
            if (mResolvingError)
                return;

            mPersonListView.Adapter = null;

            try {
                result.StartResolutionForResult (this, REQUEST_CODE_SIGN_IN);
                mResolvingError = true;
            } catch (IntentSender.SendIntentException ex) {
                // Get another pending intent to run.
                mGoogleApiClient.Connect();
            }
        }
            
        public void OnCancel (IDialogInterface dialog)
        {
            Console.WriteLine ("Unable to sign the user in.");
            Finish();
        }
            
        public override bool OnOptionsItemSelected (IMenuItem item) 
        {
            switch (item.ItemId) {
            case Android.Resource.Id.Home:
                var intent = new Intent (this, typeof(PlusSampleActivity));
                intent.AddFlags (ActivityFlags.ClearTop);
                StartActivity(intent);
                Finish();
                return true;

            default:
                return base.OnOptionsItemSelected (item);
            }
        }
    }
}

