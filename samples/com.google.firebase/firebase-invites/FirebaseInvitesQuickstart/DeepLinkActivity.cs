using System;
using Android.App;
using Android.Content;
using Android.Gms.AppInvite;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace FirebaseInviteQuickstart
{
	[Activity(Label = "FirebaseInvitesQuickstart", MainLauncher = true), IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.ActionView, Intent.CategoryDefault, Intent.CategoryBrowsable })]
	public class DeepLinkActivity : AppCompatActivity, View.IOnClickListener
	{
		private static string TAG = typeof(DeepLinkActivity).Name;

		protected override void OnCreate(Android.OS.Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.deep_link_activity);

			// Button click listener
			FindViewById(Resource.Id.button_ok).SetOnClickListener(this);
		}

		// [START deep_link_on_start]
		protected override void OnStart()
		{
			base.OnStart();

			// Check if the intent contains an AppInvite and then process the referral information.
			Intent intent = Intent;
			if (AppInviteReferral.HasReferral(intent))
			{
				ProcessReferralIntent(intent);
			}
		}
		// [END deep_link_on_start]

		// [START process_referral_intent]
		private void ProcessReferralIntent(Intent intent)
		{
			// Extract referral information from the intent
			string invitationId = AppInviteReferral.GetInvitationId(intent);
			string deepLink = AppInviteReferral.GetDeepLink(intent);

			// Display referral information
			// [START_EXCLUDE]
			Console.WriteLine(TAG, "Found Referral: " + invitationId + ":" + deepLink);
			((TextView)FindViewById(Resource.Id.deep_link_text))
				.SetText(Resource.String.deep_link_fmt);
			((TextView)FindViewById(Resource.Id.invitation_id_text))
				.SetText(Resource.String.invitation_id_fmt);
			// [END_EXCLUDE]
		}
		// [END process_referral_intent]

		public void OnClick(View v)
		{
			int i = v.Id;
			if (i == Resource.Id.button_ok)
			{
				Finish();
			}
		}
	}
}

