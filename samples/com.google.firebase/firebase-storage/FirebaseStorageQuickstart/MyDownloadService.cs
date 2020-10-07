using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Firebase.Storage;

namespace FirebaseStorageQuickstart
{
	[Service (Exported=false)]
	public class MyDownloadService : Service
	{
		private static string TAG = "Storage#DownloadService";

		/** Actions **/
		public static string ACTION_DOWNLOAD = "action_download";
		public static string ACTION_COMPLETED = "action_completed";
		public static string ACTION_ERROR = "action_error";

		/** Extras **/
		public static string EXTRA_DOWNLOAD_PATH = "extra_download_path";
		public static string EXTRA_BYTES_DOWNLOADED = "extra_bytes_downloaded";

		private StorageReference mStorage;
		private int mNumTasks = 0;

		public override void OnCreate()
		{
			base.OnCreate();
			// Initialize Storage
			mStorage = FirebaseStorage.Instance.Reference;
		}

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			Log.Debug(TAG, "onStartCommand:" + intent + ":" + startId);

			if (ACTION_DOWNLOAD.Equals(intent.Action))
			{
				// Get the path to download from the intent
				string downloadPath = intent.GetStringExtra(EXTRA_DOWNLOAD_PATH);

				// Mark task started
				Log.Debug(TAG, ACTION_DOWNLOAD + ":" + downloadPath);
				TaskStarted();

				var streamProcessor = new StreamProcessor();

				// Download and get total bytes
				mStorage.Child(downloadPath).GetStream(streamProcessor);
			}

			return StartCommandResult.RedeliverIntent;
		}

		private void TaskStarted()
		{
			changeNumberOfTasks(1);
		}

		private void TaskCompleted()
		{
			changeNumberOfTasks(-1);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		private void changeNumberOfTasks(int delta)
		{
			Log.Debug(TAG, "changeNumberOfTasks:" + mNumTasks + ":" + delta);
			mNumTasks += delta;

			// If there are no tasks left, stop the service
			if (mNumTasks <= 0)
			{
				Log.Debug(TAG, "stopping");
				StopSelf();
			}
		}

		public static IntentFilter GetIntentFilter()
		{
			IntentFilter filter = new IntentFilter();
			filter.AddAction(ACTION_COMPLETED);
			filter.AddAction(ACTION_ERROR);

			return filter;
		}
	}

	public class StreamProcessor : Java.Lang.Object, StreamDownloadTask.IStreamProcessor
	{
		public void DoInBackground(StreamDownloadTask.TaskSnapshot state, Stream stream)
		{
			stream.Close();
		}
	}
}
