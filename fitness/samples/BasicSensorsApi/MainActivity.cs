using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Fitness;
using Android.Support.V7.App;
using Android.Gms.Fitness.Request;
using Android.Gms.Fitness.Data;
using Android.Gms.Fitness.Result;
using System.Threading.Tasks;

namespace BasicSensorsApi
{
    [Activity (Label = "Basic Sensors Api", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : ActionBarActivity
    {
        const string TAG = "BasicSensorsApi";
        // [START auth_variable_references]
        const int REQUEST_OAUTH = 1;

        /**
     *  Track whether an authorization activity is stacking over the current activity, i.e. when
     *  a known auth error is being resolved, such as showing the account chooser or presenting a
     *  consent dialog. This avoids common duplications as might happen on screen rotations, etc.
     */
        const string AUTH_PENDING = "auth_state_pending";
        bool authInProgress = false;

        TextView logView;

        GoogleApiClient mClient = null;
        // [END auth_variable_references]

        // [START mListener_variable_reference]
        // Need to hold a reference to this listener, as it's passed into the "unregister"
        // method in order to stop all sensors from sending data to this listener.
        IOnDataPointListener mListener;
        // [END mListener_variable_reference]


        // [START auth_oncreate_setup_beginning]
        protected override void OnCreate (Bundle savedInstanceState) 
        {
            base.OnCreate (savedInstanceState);
            // Put application specific code here.
            // [END auth_oncreate_setup_beginning]
            SetContentView (Resource.Layout.activity_main);

            logView = FindViewById<TextView> (Resource.Id.sample_logview);
            logView.SetTextAppearance (this, Resource.Style.Log);
            logView.SetBackgroundColor (Android.Graphics.Color.White);
            logView.Append ("\r\n");

            // [START auth_oncreate_setup_ending]

            if (savedInstanceState != null)
                authInProgress = savedInstanceState.GetBoolean (AUTH_PENDING);

            buildFitnessClient();
        }
        // [END auth_oncreate_setup_ending]

        // [START auth_build_googleapiclient_beginning]
        /**
     *  Build a {@link GoogleApiClient} that will authenticate the user and allow the application
     *  to connect to Fitness APIs. The scopes included should match the scopes your app needs
     *  (see documentation for details). Authentication will occasionally fail intentionally,
     *  and in those cases, there will be a known resolution, which the OnConnectionFailedListener()
     *  can address. Examples of this include the user never having signed in before, or having
     *  multiple accounts on the device and needing to specify which account to use, etc.
     */
        void buildFitnessClient() 
        {
            // Create the Google API Client
            mClient = new GoogleApiClient.Builder (this)
                .AddApi(FitnessClass.SENSORS_API)
                .AddScope(new Scope (Scopes.FitnessLocationRead))
                .AddConnectionCallbacks(
                    bundle => {
                        Log (TAG, "Connected!!!");
                        // Now you can make calls to the Fitness APIs.
                        // Put application specific code here.
                        // [END auth_build_googleapiclient_beginning]
                        //  What to do? Find some data sources!
                        findFitnessDataSources();

                        // [START auth_build_googleapiclient_ending]
                    },
                    i => {
                        // If your connection to the sensor gets lost at some point,
                        // you'll be able to determine the reason and react to it here.
                        if (i == GoogleApiClient.ConnectionCallbacks.CauseNetworkLost)
                            Log (TAG, "Connection lost.  Cause: Network Lost.");
                        else if (i == GoogleApiClient.ConnectionCallbacks.CauseServiceDisconnected)
                            Log (TAG, "Connection lost.  Reason: Service Disconnected");
                    })            
                .AddOnConnectionFailedListener (
                    result => {
                        Log (TAG, "Connection failed. Cause: " + result);
                        if (!result.HasResolution) {
                            // Show the localized error dialog
                            GooglePlayServicesUtil.GetErrorDialog (result.ErrorCode, this, 0).Show ();
                            return;
                        }
                        // The failure has a resolution. Resolve it.
                        // Called typically when the app is not yet authorized, and an
                        // authorization dialog is displayed to the user.
                        if (!authInProgress) {
                            try {
                                Log (TAG, "Attempting to resolve failed connection");
                                authInProgress = true;
                                result.StartResolutionForResult (this, REQUEST_OAUTH);
                            } catch (IntentSender.SendIntentException e) {
                                Log (TAG, "Exception while starting resolution activity: " + e);
                            }
                        }
                    })
                .Build ();
        }
        // [END auth_build_googleapiclient_ending]

        // [START auth_connection_flow_in_activity_lifecycle_methods]
        protected override void OnStart () 
        {
            base.OnStart ();
            // Connect to the Fitness API
            Log (TAG, "Connecting...");
            mClient.Connect ();
        }
            
        protected override void OnStop () 
        {
            base.OnStop ();
            if (mClient.IsConnected)
                mClient.Disconnect();
        }
            
        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data) 
        {
            if (requestCode == REQUEST_OAUTH) {
                authInProgress = false;
                if (resultCode == Result.Ok) {
                    // Make sure the app is not already connected or attempting to connect
                    if (!mClient.IsConnecting && !mClient.IsConnected)
                        mClient.Connect ();
                }
            }
        }
            
        protected override void OnSaveInstanceState (Bundle outState) 
        {
            base.OnSaveInstanceState (outState);
            outState.PutBoolean (AUTH_PENDING, authInProgress);
        }
        // [END auth_connection_flow_in_activity_lifecycle_methods]

        /**
     * Find available data sources and attempt to register on a specific {@link DataType}.
     * If the application cares about a data type but doesn't care about the source of the data,
     * this can be skipped entirely, instead calling
     *     {@link com.google.android.gms.fitness.SensorsApi
     *     #register(GoogleApiClient, SensorRequest, DataSourceListener)},
     * where the {@link SensorRequest} contains the desired data type.
     */
        async Task findFitnessDataSources() {
            // [START find_data_sources]
            var dataSourcesResult = await FitnessClass.SensorsApi.FindDataSourcesAsync (mClient, new DataSourcesRequest.Builder ()
                // At least one datatype must be specified.
                .SetDataTypes (DataType.TypeLocationSample)
                // Can specify whether data type is raw or derived.
                .SetDataSourceTypes (DataSource.TypeRaw)
                .Build ());
            
            Log (TAG, "Result: " + dataSourcesResult.Status);
            foreach (var dataSource in dataSourcesResult.DataSources) {
                Log (TAG, "Data source found: " + dataSource);
                Log (TAG, "Data Source type: " + dataSource.DataType.Name);

                //Let's register a listener to receive Activity data!
                if (dataSource.DataType.Name.Equals (DataType.TypeLocationSample.Name) && mListener == null) {
                    Log (TAG, "Data source for LOCATION_SAMPLE found!  Registering.");
                    await registerFitnessDataListener (dataSource, DataType.TypeLocationSample);
                }
            }
        
            // [END find_data_sources]
        }

        /**
     * Register a listener with the Sensors API for the provided {@link DataSource} and
     * {@link DataType} combo.
     */
        async Task registerFitnessDataListener (DataSource dataSource, DataType dataType) {
            // [START register_data_listener]
            mListener = new DataPointListener (dataPoint => {
                foreach (var field in dataPoint.DataType.Fields) {
                    var val = dataPoint.GetValue (field);
                    Log (TAG, "Detected DataPoint field: " + field.Name);
                    Log (TAG, "Detected DataPoint value: " + val);
                }
            });

            var status = await FitnessClass.SensorsApi.AddAsync (
                 mClient,
                 new SensorRequest.Builder ()
                .SetDataSource (dataSource) // Optional but recommended for custom data sets.
                .SetDataType (dataType) // Can't be omitted.
                .SetSamplingRate (10, Java.Util.Concurrent.TimeUnit.Seconds)
                .Build (),
                 mListener);

                
            if (status.IsSuccess)
                Log (TAG, "Listener registered!");
            else
                Log (TAG, "Listener not registered.");                    
            // [END register_data_listener]
        }

        /**
     * Unregister the listener with the Sensors API.
     */
        async Task unregisterFitnessDataListener() 
        {
            if (mListener == null) {
                // This code only activates one listener at a time.  If there's no listener, there's
                // nothing to unregister.
                return;
            }

            // [START unregister_data_listener]
            // Waiting isn't actually necessary as the unregister call will complete regardless,
            // even if called from within onStop, but a callback can still be added in order to
            // inspect the results.
            var status = await FitnessClass.SensorsApi.RemoveAsync (
                             mClient,
                             mListener);
            
            if (status.IsSuccess)
                Log (TAG, "Listener was removed!");
            else
                Log (TAG, "Listener was not removed.");                    
   
            // [END unregister_data_listener]
        }
            
        public override bool OnCreateOptionsMenu (IMenu menu) 
        {           
            // Inflate the menu; this adds items to the action bar if it is present.
            MenuInflater.Inflate (Resource.Menu.main, menu);
            return true;
        }
            
        public override bool OnOptionsItemSelected (IMenuItem item) 
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_unregister_listener) {
                unregisterFitnessDataListener ();
                return true;
            }
            return base.OnOptionsItemSelected (item);
        }
            
        void Log (string tag, string message)
        {
            Android.Util.Log.Info (tag, message);

            RunOnUiThread (() => 
                logView.Append (string.Format ("{0}\r\n", message)));
        }
    }

    class DataPointListener : Java.Lang.Object, IOnDataPointListener
    {
        public DataPointListener (Action<DataPoint> dataPointHandler)
        {
            DataPointHandler = dataPointHandler;
        }

        public Action<DataPoint> DataPointHandler { get; private set; }

        public void OnDataPoint (DataPoint dataPoint)
        {
            var h = DataPointHandler;
            if (h != null)
                h (dataPoint);
        }
    }
}


