using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
// [SNIPPET load_banner_ad]
// Load an ad into the AdView.
// [START load_banner_ad]
// [START_EXCLUDE] with InterstitialAd
using Android.Gms.Ads;
// [END_EXCLUDE]

namespace Admob
{
	[Activity(Label = "Admob", MainLauncher = true), IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.ActionMain, Intent.CategoryLauncher })]
	[MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
	public class MainActivity : FragmentActivity, IAppCompatCallback, Android.Support.V4.App.TaskStackBuilder.ISupportParentable, Android.Support.V7.App.ActionBarDrawerToggle.IDelegateProvider
	{
		private AdView mAdView;
		// [START_EXCLUDE]
		private InterstitialAd mInterstitialAd;
		private Button mLoadInterstitialButton;
		// [END_EXCLUDE]

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			mAdView = (AdView)FindViewById(Resource.Id.adView);
			AdRequest adRequest = new AdRequest.Builder().Build();
			mAdView.LoadAd(adRequest);
			// [END load_banner_ad]

			// AdMob ad unit IDs are not currently stored inside the google-services.json file.
			// Developers using AdMob can store them as custom values in a string resource file or
			// simply use constants. Note that the ad units used here are configured to return only test
			// ads, and should not be used outside this sample.

			// [START instantiate_interstitial_ad]
			// Create an InterstitialAd object. This same object can be re-used whenever you want to
			// show an interstitial.
			mInterstitialAd = new InterstitialAd(this);
			mInterstitialAd.AdUnitId = GetString(Resource.String.interstitial_ad_unit_id);
			// [END instantiate_interstitial_ad]

			// [START create_interstitial_ad_listener]
			var adListener = new CustomAdlistener();
			adListener.AdClosed += () =>
			{
				RequestNewInterstitial();
				BeginSecondActivity();
			};

			mInterstitialAd.AdListener = adListener;
			// [END create_interstitial_ad_listener]

			// [START display_interstitial_ad]
			mLoadInterstitialButton = (Button)FindViewById(Resource.Id.load_interstitial_button);
			mLoadInterstitialButton.Click += delegate
			{
				if (mInterstitialAd.IsLoaded)
				{
					mInterstitialAd.Show();
				}
				else {
					BeginSecondActivity();
				}
			};
			// [END display_interstitial_ad]
		}

		/**
     * Load a new interstitial ad asynchronously.
     */
		// [START request_new_interstitial]
		private void RequestNewInterstitial()
		{
			AdRequest adRequest = new AdRequest.Builder().Build();
			mInterstitialAd.LoadAd(adRequest);
		}
		// [END request_new_interstitial]

		private void BeginSecondActivity()
		{
			Intent intent = new Intent(this, typeof(SecondActivity));
			StartActivity(intent);
		}

		// [START add_lifecycle_methods]
		/** Called when leaving the activity */
		protected override void OnPause()
		{
			if (mAdView != null)
			{
				mAdView.Pause();
			}
			base.OnPause();
		}

		/** Called when returning to the activity */
		protected override void OnResume()
		{
			base.OnResume();
			if (mAdView != null)
			{
				mAdView.Resume();
			}
			if (!mInterstitialAd.IsLoaded)
			{
				RequestNewInterstitial();
			}
		}

		/** Called before the activity is destroyed */
		protected override void OnDestroy()
		{
			if (mAdView != null)
			{
				mAdView.Destroy();
			}
			base.OnDestroy();
		}
		// [END add_lifecycle_methods]


		AdView GetAdView()
		{
			return mAdView;
		}

		public Intent SupportParentActivityIntent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Android.Support.V7.App.ActionBarDrawerToggle.IDelegate DrawerToggleDelegate
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void OnSupportActionModeFinished(Android.Support.V7.View.ActionMode mode)
		{
			throw new NotImplementedException();
		}

		public void OnSupportActionModeStarted(Android.Support.V7.View.ActionMode mode)
		{
			throw new NotImplementedException();
		}

		public Android.Support.V7.View.ActionMode OnWindowStartingSupportActionMode(Android.Support.V7.View.ActionMode.ICallback callback)
		{
			throw new NotImplementedException();
		}

	}

	class CustomAdlistener : AdListener
	{
		// Declare the delegate (if using non-generic pattern).
		public delegate void AdLoadedEvent();
		public delegate void AdClosedEvent();
		public delegate void AdOpenedEvent();
		// Declare the event.
		public event AdLoadedEvent AdLoaded;
		public event AdClosedEvent AdClosed;
		public event AdOpenedEvent AdOpened;
		public override void OnAdLoaded()
		{
			if (AdLoaded != null) this.AdLoaded();
			base.OnAdLoaded();
		}
		public override void OnAdClosed()
		{

		}
		public override void OnAdOpened()
		{
			if (AdOpened != null) this.AdOpened();
			base.OnAdOpened();
		}
	}
}