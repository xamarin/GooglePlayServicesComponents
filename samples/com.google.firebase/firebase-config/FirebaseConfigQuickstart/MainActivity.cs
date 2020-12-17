using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using AndroidX.AppCompat.App;
using Firebase.RemoteConfig;

namespace Config
{
	[Activity(Label = "Config", MainLauncher = true), IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.ActionMain, Intent.CategoryLauncher })]
	public class MainActivity : AppCompatActivity
	{
		private static string TAG = "MainActivity";

		// Remote Config keys
		private static string PRICE_CONFIG_KEY = "price";
		private static string LOADING_PHRASE_CONFIG_KEY = "loading_phrase";
		private static string PRICE_PREFIX_CONFIG_KEY = "price_prefix";
		private static string DISCOUNT_CONFIG_KEY = "discount";
		private static string IS_PROMOTION_CONFIG_KEY = "is_promotion_on";

		private FirebaseRemoteConfig mFirebaseRemoteConfig;
		private TextView mPriceTextView;

		protected override async void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			mPriceTextView = (TextView)FindViewById(Resource.Id.priceView);

			Button fetchButton = (Button)FindViewById(Resource.Id.fetchButton);
			fetchButton.Click += async delegate
			{
				await FetchDiscount();
			};


			// Get Remote Config instance.
			// [START get_remote_config_instance]
			mFirebaseRemoteConfig = FirebaseRemoteConfig.Instance;
			// [END get_remote_config_instance]

			// Create Remote Config Setting to enable developer mode.
			// Fetching configs from the server is normally limited to 5 requests per hour.
			// Enabling developer mode allows many more requests to be made per hour, so developers
			// can test different config values during development.
			// [START enable_dev_mode]
			FirebaseRemoteConfigSettings configSettings = new FirebaseRemoteConfigSettings.Builder().Build();
			mFirebaseRemoteConfig.SetConfigSettingsAsync(configSettings).Wait();
			// [END enable_dev_mode]

			// Set default Remote Config values. In general you should have in app defaults for all
			// values that you may configure using Remote Config later on. The idea is that you
			// use the in app defaults and when you need to adjust those defaults, you set an updated
			// value in the App Manager console. Then the next time you application fetches from the
			// server, the updated value will be used. You can set defaults via an xml file like done
			// here or you can set defaults inline by using one of the other setDefaults methods.S
			// [START set_default_values]
			mFirebaseRemoteConfig.SetDefaultsAsync(Resource.Xml.remote_config_defaults).Wait();
			// [END set_default_values]

			// Fetch discount config.
			await FetchDiscount();
		}

		/**
     * Fetch discount from server.
     */
		private async System.Threading.Tasks.Task FetchDiscount()
		{
			mPriceTextView.SetText(LOADING_PHRASE_CONFIG_KEY, null);

			long cacheExpiration = 3600; // 1 hour in seconds.

			try
			{
				await mFirebaseRemoteConfig.FetchAsync(cacheExpiration);

				Toast.MakeText(this, "Fetch Succeeded", ToastLength.Long).Show();

				// Once the config is successfully fetched it must be activated before newly fetched
				// values are returned.
				mFirebaseRemoteConfig.Activate().Wait();
			}
			catch
			{
				Toast.MakeText(this, "Fetch Failed", ToastLength.Long).Show();
			}

			DisplayPrice();
		}

		/**
	* Display price with discount applied if promotion is on. Otherwise display original price.
	*/
		// [START display_price]
		private void DisplayPrice()
		{
			long initialPrice = mFirebaseRemoteConfig.GetLong(PRICE_CONFIG_KEY);
			long finalPrice = initialPrice;
			if (mFirebaseRemoteConfig.GetBoolean(IS_PROMOTION_CONFIG_KEY))
			{
				// [START get_config_values]
				finalPrice = initialPrice - mFirebaseRemoteConfig.GetLong(DISCOUNT_CONFIG_KEY);
				// [END get_config_values]
			}
			mPriceTextView.SetText(PRICE_PREFIX_CONFIG_KEY + finalPrice, null);
		}
		// [END display_price]
	}
}

