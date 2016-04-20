using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.Gms.Common;
using Android.Gms.Drive.Query;
using System.Collections.Generic;

namespace DriveSample
{
    [Activity (Label = "Drive Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {        
        GoogleApiClient googleApiClient;
        Button buttonCreate;
        Button buttonSearch;
        EditText editTextFilename;
        ListView listViewResults;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            buttonCreate = FindViewById<Button> (Resource.Id.buttonCreate);
            buttonSearch = FindViewById<Button> (Resource.Id.buttonSearch);
            editTextFilename = FindViewById<EditText> (Resource.Id.editTextFilename);
            listViewResults = FindViewById<ListView> (Resource.Id.listViewResults);

            buttonCreate.Click += async delegate {

                // Create the new file info
                var metadataChangeSet = new MetadataChangeSet.Builder()
                    .SetMimeType ("text/plain")
                    .SetTitle (editTextFilename.Text)
                    .Build();
                
                // Get our root folder
                var rootFolder = DriveClass.DriveApi.GetRootFolder (googleApiClient);

                // Create a blank file
                await rootFolder.CreateFile (googleApiClient, metadataChangeSet, null).AsAsync<IDriveFolderDriveFileResult> ();

            };

            buttonSearch.Click += async delegate {

                buttonSearch.Enabled = false;

                // Build a query to search for files of
                // This will only return files that you created/opened through this app
                var query = new QueryClass.Builder ()
                    .AddFilter (Filters.Contains (SearchableField.Title, editTextFilename.Text))
                    .Build ();

                // Execute search asynchronously
                var results = await DriveClass.DriveApi.Query (googleApiClient, query).AsAsync<IDriveApiMetadataBufferResult> ();

                // Check for a successful result
                if (results.Status.IsSuccess) {

                    var files = new List<Metadata> ();

                    // Loop through the results
                    for (var i = 0; i < results.MetadataBuffer.Count; i++)
                        files.Add (results.MetadataBuffer.Get (i).JavaCast<Metadata> ());

                    listViewResults.Adapter = new ArrayAdapter<string> (
                        this, 
                        Android.Resource.Layout.SimpleListItem1, 
                        Android.Resource.Id.Text1,
                        (from f in files select f.Title).ToArray ());

                    if (files.Count <= 0)
                        Toast.MakeText (this, "No results found!", ToastLength.Short).Show ();
                }

                buttonSearch.Enabled = true;
            };
                
            buttonCreate.Enabled = false;
            buttonSearch.Enabled = false;

            googleApiClient = new GoogleApiClient.Builder (this)
                .AddApi (DriveClass.API)
                .AddScope (DriveClass.ScopeFile)
                .AddScope (DriveClass.ScopeAppfolder)
                .UseDefaultAccount ()
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this)
                .Build ();
        }

        protected override void OnStart ()
        {
            base.OnStart ();
            googleApiClient.Connect ();
        }

        public async void OnConnected (Bundle connectionHint)
        {
            buttonCreate.Enabled = true;
            buttonSearch.Enabled = true;
        }

        public void OnConnectionSuspended (int cause)
        {
            Console.WriteLine ("Connection Suspended: {0}", cause);
        }

        public void OnConnectionFailed (ConnectionResult result)
        {
            if (result.HasResolution) {
                try {
                    result.StartResolutionForResult (this, 101);
                } catch (IntentSender.SendIntentException ex) {
                    // Unable to resolve, message user appropriately
                }
            } else {
                GooglePlayServicesUtil.GetErrorDialog (result.ErrorCode, this, 0).Show ();
            }
        }

        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult (requestCode, resultCode, data);

            switch (requestCode) {                
                case 101: // Something may have been resolved, try connecting again
                if (resultCode == Result.Ok)
                    googleApiClient.Connect ();
                break;
            }
        }
    }
}


