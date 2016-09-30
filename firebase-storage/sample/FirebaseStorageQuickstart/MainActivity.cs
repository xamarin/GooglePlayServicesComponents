using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using Android.Support.V7.App;
using Android.Views;
using System;
using Android.Content;
using Android.Support.V4.Content;
using Android.Util;
using Java.Lang;
using Android;
using Java.IO;
using Android.Provider;
using Java.Util;
using Uri = Android.Net.Uri;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace FirebaseStorageQuickstart
{
	[Activity(Label = "FirebaseStorageQuickstart", MainLauncher = true), IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.ActionMain, Intent.CategoryLauncher })]
	[MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
	public class MainActivity : AppCompatActivity, View.IOnClickListener
	{
		private static string TAG = "Storage#MainActivity";

		private static int RC_TAKE_PICTURE = 101;
		private static int RC_STORAGE_PERMS = 102;

		private static string KEY_FILE_URI = "key_file_uri";
		private static string KEY_DOWNLOAD_URL = "key_download_url";

		private BroadcastReceiver mDownloadReceiver;
		private ProgressDialog mProgressDialog;
		private FirebaseAuth mAuth;

		private Uri mDownloadUrl = null;
		private Uri mFileUri = null;

		// [START declare_ref]
		private StorageReference mStorageRef;
		// [END declare_ref]



		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			// Initialize Firebase Auth
			mAuth = FirebaseAuth.Instance;

			// Initialize Firebase Storage Ref
			// [START get_storage_ref]
			mStorageRef = FirebaseStorage.Instance.Reference;
			// [END get_storage_ref]

			// Click listeners
			FindViewById(Resource.Id.button_camera).SetOnClickListener(this);
			FindViewById(Resource.Id.button_sign_in).SetOnClickListener(this);
			FindViewById(Resource.Id.button_download).SetOnClickListener(this);

			// Restore instance state
			if (savedInstanceState != null)
			{
				mFileUri = (Uri)savedInstanceState.GetParcelable(KEY_FILE_URI);
				mDownloadUrl = (Uri)savedInstanceState.GetParcelable(KEY_DOWNLOAD_URL);
			}

			// Download receiver
			mDownloadReceiver.OnReceive(this.BaseContext, this.Intent);
		}

		protected override void OnStart()
		{
			base.OnStart();

			UpdateUI(mAuth.CurrentUser);

			// Register download receiver
			LocalBroadcastManager.GetInstance(this)
					.RegisterReceiver(mDownloadReceiver, MyDownloadService.GetIntentFilter());
		}

		protected override void OnStop()
		{
			base.OnStop();

			// Unregister download receiver
			LocalBroadcastManager.GetInstance(this).UnregisterReceiver(mDownloadReceiver);
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);

			outState.PutParcelable(KEY_FILE_URI, (Android.OS.IParcelable)mFileUri);
			outState.PutParcelable(KEY_DOWNLOAD_URL, (Android.OS.IParcelable)mDownloadUrl);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			Log.Debug(TAG, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);
			if (requestCode == RC_TAKE_PICTURE)
			{
				if (resultCode == Result.Ok)
				{
					if (mFileUri != null)
					{
						UploadFromUri(mFileUri as Android.Net.Uri);
					}
					else {
						Log.Warn(TAG, "File URI is null");
					}
				}
				else {
					Toast.MakeText(this, "Taking picture failed.", ToastLength.Short).Show();
				}
			}
		}

		// [START upload_from_uri]
		private void UploadFromUri(Android.Net.Uri fileUri)
		{
			Log.Debug(TAG, "uploadFromUri:src:" + fileUri.ToString());

			// [START get_child_ref]
			// Get a reference to store file at photos/<FILENAME>.jpg
			StorageReference photoRef = mStorageRef.Child("photos")
					.Child(fileUri.LastPathSegment);
			// [END get_child_ref]

			// Upload file to Firebase Storage
			// [START_EXCLUDE]

			ShowProgressDialog();

			// [END_EXCLUDE]
			Log.Debug(TAG, "uploadFromUri:dst:" + photoRef.Path);

			photoRef.PutFile(fileUri).AddOnSuccessListener(this, new SuccessListener());
			photoRef.PutFile(fileUri).AddOnFailureListener(this, new FailureListener());

		}

		private void LaunchCamera()
		{
			Log.Debug(TAG, "launchCamera");

			// Check that we have permission to read images from external storage.
			string perm = Manifest.Permission.WriteExternalStorage;

			// Choose file storage location, must be listed in res/xml/file_paths.xml
			File dir = new File(Android.OS.Environment.ExternalStorageDirectory + "/photos");
			File file = new File(dir, UUID.RandomUUID().ToString() + ".jpg");
			try
			{
				// Create directory if it does not exist.
				if (!dir.Exists())
				{
					dir.Mkdir();
				}
				bool created = file.CreateNewFile();
				Log.Debug(TAG, "file.createNewFile:" + file.AbsolutePath + ":" + created);
			}
			catch (IOException e)
			{
				Log.Error(TAG, "file.createNewFile" + file.AbsolutePath + ":FAILED", e);
			}

			// Create content:// URI for file, required since Android N
			// See: https://developer.android.com/reference/android/support/v4/content/FileProvider.html
			mFileUri = FileProvider.GetUriForFile(this,
					"com.google.firebase.quickstart.firebasestorage.fileprovider", file);

			// Create and launch the intent
			Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
			takePictureIntent.PutExtra(MediaStore.ExtraOutput, mFileUri);

			StartActivityForResult(takePictureIntent, RC_TAKE_PICTURE);
		}

		private void signInAnonymously()
		{
			// Sign in anonymously. Authentication is required to read or write from Firebase Storage.
			ShowProgressDialog();
			mAuth.SignInAnonymously().AddOnSuccessListener(this, new AnonymousSuccessListener());
		}

		private void BeginDownload()
		{
			// Get path
			string path = "photos/" + mFileUri.LastPathSegment;

			// Kick off download service
			Intent intent = new Intent(this, typeof(MyDownloadService));
			intent.SetAction(MyDownloadService.ACTION_DOWNLOAD);
			intent.PutExtra(MyDownloadService.EXTRA_DOWNLOAD_PATH, path);

			StartService(intent);

			// Show loading spinner
			ShowProgressDialog();
		}

		private void UpdateUI(FirebaseUser user)
		{
			// Signed in or Signed out
			if (user != null)
			{
				FindViewById(Resource.Id.layout_signin).Visibility = ViewStates.Gone;
				FindViewById(Resource.Id.layout_storage).Visibility = ViewStates.Visible;
			}
			else {
				FindViewById(Resource.Id.layout_signin).Visibility = ViewStates.Visible;
				FindViewById(Resource.Id.layout_storage).Visibility = ViewStates.Gone;
			}

			// Download URL and Download button
			if (mDownloadUrl != null)
			{
				((TextView)FindViewById(Resource.Id.picture_download_uri))
					.Text = mDownloadUrl.ToString();
				FindViewById(Resource.Id.layout_download).Visibility = ViewStates.Visible;
			}
			else {
				((TextView)FindViewById(Resource.Id.picture_download_uri))
					.Text = string.Empty;
				FindViewById(Resource.Id.layout_download).Visibility = ViewStates.Gone;
			}
		}

		private void ShowMessageDialog(string title, string message)
		{
			AlertDialog ad = new AlertDialog.Builder(this)
					.SetTitle(title)
					.SetMessage(message)
					.Create();
			ad.Show();
		}

		private void ShowProgressDialog()
		{
			if (mProgressDialog == null)
			{
				mProgressDialog = new ProgressDialog(this);
				mProgressDialog.SetMessage("Loading...");
				mProgressDialog.Indeterminate = true;
			}

			mProgressDialog.Show();
		}

		private void HideProgressDialog()
		{
			if (mProgressDialog != null && mProgressDialog.IsShowing)
			{
				mProgressDialog.Dismiss();
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu_main, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			int i = item.ItemId;
			if (i == Resource.Id.action_logout)
			{
				FirebaseAuth.Instance.SignOut();
				UpdateUI(null);
				return true;
			}
			else {
				return base.OnOptionsItemSelected(item);
			}
		}

		public void OnClick(View v)
		{
			int i = v.Id;
			if (i == Resource.Id.button_camera)
			{
				LaunchCamera();
			}
			else if (i == Resource.Id.button_sign_in)
			{
				signInAnonymously();
			}
			else if (i == Resource.Id.button_download)
			{
				BeginDownload();
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		public class SuccessListener : MainActivity, IOnSuccessListener
		{
			public void OnSuccess(Java.Lang.Object result)
			{
				// Upload succeeded
				Log.Debug(TAG, "uploadFromUri:onSuccess");

				UploadTask.TaskSnapshot taskSnapshot = (UploadTask.TaskSnapshot)result;

				// Get the public download URL
				mDownloadUrl = taskSnapshot.Metadata.DownloadUrl;

				// [START_EXCLUDE]
				HideProgressDialog();
				UpdateUI(mAuth.CurrentUser);
				// [END_EXCLUDE]
			}
		}

		public class FailureListener : MainActivity, IOnFailureListener
		{
			public void OnFailure(Java.Lang.Exception e)
			{
				// Upload failed
				Log.Warn(TAG, "uploadFromUri:onFailure", e);

				mDownloadUrl = null;

				// [START_EXCLUDE]
				HideProgressDialog();
				Toast.MakeText(this.BaseContext, "Error: upload failed",
							   ToastLength.Short).Show();
				UpdateUI(mAuth.CurrentUser);
				// [END_EXCLUDE]
			}
		}

		public class AnonymousSuccessListener : MainActivity, IOnSuccessListener
		{
			public void OnSuccess(Java.Lang.Object result)
			{
				Log.Debug(TAG, "signInAnonymously:SUCCESS");
				IAuthResult authResult = (Firebase.Auth.IAuthResult)result;
				HideProgressDialog();
				UpdateUI(authResult.User);
			}
		}

		public class AnonymousFailureListener : MainActivity, IOnFailureListener
		{
			public void OnFailure(Java.Lang.Exception e)
			{
				Log.Error(TAG, "signInAnonymously:FAILURE", e);
				HideProgressDialog();
				UpdateUI(null);
			}
		}
	}
}