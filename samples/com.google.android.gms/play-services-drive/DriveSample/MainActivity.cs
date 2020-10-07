using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Gms.Common.Apis;
using Android.Gms.Common;

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
                var metadataChangeSet = new Android.Gms.Drive.MetadataChangeSet.Builder()
                    .SetMimeType ("text/plain")
                    .SetTitle (editTextFilename.Text)
                    .Build();
                
                // Get our root folder
                var rootFolder = Android.Gms.Drive.DriveClass.DriveApi.GetRootFolder (googleApiClient);

                // Create a blank file
                await rootFolder.CreateFile (googleApiClient, metadataChangeSet, null).AsAsync<Android.Gms.Drive.IDriveFolderDriveFileResult> ();

            };

            buttonSearch.Click += async delegate {

                buttonSearch.Enabled = false;

                // Build a query to search for files of
                // This will only return files that you created/opened through this app
                var query = new Android.Gms.Drive.Query.QueryClass.Builder ()
                    .AddFilter (Android.Gms.Drive.Query.Filters.Contains (Android.Gms.Drive.Query.SearchableField.Title, editTextFilename.Text))
                    .Build ();

                // Execute search asynchronously
                var results = await Android.Gms.Drive.DriveClass.DriveApi.Query (googleApiClient, query).AsAsync<Android.Gms.Drive.IDriveApiMetadataBufferResult> ();

                // Check for a successful result
                if (results.Status.IsSuccess) {

                    var files = new List<Android.Gms.Drive.Metadata> ();

                    // Loop through the results
                    for (var i = 0; i < results.MetadataBuffer.Count; i++)
                        files.Add (results.MetadataBuffer.Get (i).JavaCast<Android.Gms.Drive.Metadata> ());

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
                .AddApi (Android.Gms.Drive.DriveClass.API)
                .AddScope (Android.Gms.Drive.DriveClass.ScopeFile)
                .AddScope (Android.Gms.Drive.DriveClass.ScopeAppfolder)
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


