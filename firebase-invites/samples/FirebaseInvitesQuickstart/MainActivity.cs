using System;
using Android.App;
using Android.Content;
using Android.Gms.AppInvite;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Uri = Android.Net.Uri;


namespace FirebaseInviteQuickstart
{
	[Activity(Label = "FirebaseInvitesQuickstart", MainLauncher = true), IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.ActionMain, Intent.CategoryLauncher })]
	[MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
	public class MainActivity : AppCompatActivity, GoogleApiClient.IOnConnectionFailedListener, View.IOnClickListener
	{
		private static string TAG = typeof(MainActivity).Name;
		private static int REQUEST_INVITE = 0;
		public static int RESULT_OK = -1;

		// [START define_variables]
		private GoogleApiClient mGoogleApiClient;
		// [END define_variables]

		// [START on_create]
		protected override void OnCreate(Bundle savedInstanceState)
		{
			// [START_EXCLUDE]
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.main_activity);

			// Invite button click listener
			FindViewById(Resource.Id.invite_button).SetOnClickListener(this);
			// [END_EXCLUDE]

			// Create an auto-managed GoogleApiClient with access to App Invites.
			mGoogleApiClient = new GoogleApiClient.Builder(this)
					.AddApi(AppInviteClass.API)
					.EnableAutoManage(this, this)
					.Build();

			// Check for App Invite invitations and launch deep-link activity if possible.
			// Requires that an Activity is registered in AndroidManifest.xml to handle
			// deep-link URLs.
			bool autoLaunchDeepLink = true;
			AppInviteClass.AppInviteApi.GetInvitation(mGoogleApiClient, this, autoLaunchDeepLink)
					.SetResultCallback(
							  new ResultCallback<IAppInviteInvitationResult>((IAppInviteInvitationResult result) =>
			{
				Console.WriteLine(TAG, string.Format("getInvitation:onResult:{0}", result.Status));
				if (result.Status.IsSuccess)
				{
					// Extract information from the intent
					Intent intent = result.InvitationIntent;
					String deepLink = AppInviteReferral.GetDeepLink(intent);
					String invitationId = AppInviteReferral.GetInvitationId(intent);

					// Because autoLaunchDeepLink = true we don't have to do anything
					// here, but we could set that to false and manually choose
					// an Activity to launch to handle the deep link here.
					// ...
				}
			}));
		}

		public void OnConnectionFailed(ConnectionResult result)
		{
			Console.WriteLine(TAG, "onConnectionFailed:" + result);
			ShowMessage(GetString(Resource.String.google_play_services_error));
		}

		/**
     * User has clicked the 'Invite' button, launch the invitation UI with the proper
     * title, message, and deep link
     */
		// [START on_invite_clicked]
		private void OnInviteClicked()
		{
			Intent intent = new AppInviteInvitation.IntentBuilder(GetString(Resource.String.invitation_title))
					.SetMessage(GetString(Resource.String.invitation_message))
					.SetDeepLink(Uri.Parse(GetString(Resource.String.invitation_deep_link)))
					.SetCustomImage(Uri.Parse(GetString(Resource.String.invitation_custom_image)))
					.SetCallToActionText(GetString(Resource.String.invitation_cta))
					.Build();
			GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			StartActivityForResult(intent, REQUEST_INVITE);
		}
		// [END on_invite_clicked]

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			Console.WriteLine(TAG, "onActivityResult: requestCode=" + requestCode + ", resultCode=" + resultCode);

			if (requestCode == REQUEST_INVITE)
			{
				if (resultCode == Result.Ok)
				{
					// Get the invitation IDs of all sent messages
					string[] ids = AppInviteInvitation.GetInvitationIds((int)resultCode, data);
					Console.WriteLine(TAG, "onActivityResult: sent invitation " + ids);
				}
				else {
					// Sending failed or it was canceled, show failure message to the user
					// [START_EXCLUDE]
					ShowMessage(GetString(Resource.String.send_failed));
					// [END_EXCLUDE]
				}
			}
		}

		public void OnClick(View v)
		{
			int i = v.Id;
			if (i == Resource.Id.invite_button)
			{
				OnInviteClicked();
			}
		}

		private void ShowMessage(String msg)
		{
			ViewGroup container = (ViewGroup)FindViewById(Resource.Id.snackbar_layout);
			Snackbar.Make(container, msg, Snackbar.LengthShort).Show();
		}
	}
}