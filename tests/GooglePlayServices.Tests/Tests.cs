using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Xunit;

namespace GooglePlayServices.Tests
{
	public class Tests
	{
		[Fact]
		public void Common_GooglePlayServicesUtil_Exists()
		{
			var versionCode = Android.Gms.Common.GooglePlayServicesUtil.GooglePlayServicesVersionCode;

			Console.WriteLine("Common.GooglePlayServicesUtil.GooglePlayServicesVersionCode: {0}", versionCode);

			Assert.True(versionCode > 0);
		}

		[Fact]
		public void Common_GoogleApiAvailability_Exists()
		{
			var versionCode = Android.Gms.Common.GoogleApiAvailability.GooglePlayServicesVersionCode;

			Console.WriteLine("Common.GoogleApiAvailability.GooglePlayServicesVersionCode:: {0}", versionCode);

			Assert.True(versionCode > 0);
		}

		[Fact]
		public void Auth_GoogleAuthUtil_Exists()
		{
			var googleAccountType = Android.Gms.Auth.GoogleAuthUtil.GoogleAccountType;

			Console.WriteLine("Auth.GoogleAuthUtil.GoogleAccountType: {0}", googleAccountType);

			Assert.True(googleAccountType != null);
		}

		[Fact]
		public void Location_Places_UI_PlacePicker_Exists()
		{
			var resErr = Android.Gms.Location.Places.UI.PlacePicker.ResultError;

			Console.WriteLine("Location.Places.UI.PlacePicker.ResultError: {0}", resErr);

			Assert.True(resErr == 2);
		}

		[Fact]
		public void Location_Places_UI_PlacePicker_IntentBuilder_Exists()
		{
			var ib = new Android.Gms.Location.Places.UI.PlacePicker.IntentBuilder ();

			Console.WriteLine("Location.Places.UI.PlacePicker.IntentBuilder: {0}", ib != null);

			Assert.True(ib != null);
		}

		[Fact]
		public void Location_Places_UI_PlaceAutocomplete_Exists()
		{
			var resErr = Android.Gms.Location.Places.UI.PlaceAutocomplete.ResultError;

			Console.WriteLine("Location.Places.UI.PlaceAutocomplete.ResultError: {0}", resErr);

			Assert.True(resErr == 2);
		}

        [Fact]
        public void Location_Places_UI_PlaceAutocomplete_IntentBuilder_Exists()
        {
            var resErr = new Android.Gms.Location.Places.UI.PlaceAutocomplete.IntentBuilder();

            Console.WriteLine("Location.Places.UI.PlaceAutocomplete.ResultError: {0}", resErr);

            Assert.True(resErr == 2);
        }

		[Fact]
		public void Cast_Framework_Media_Widget_ExpandedControllerActivity_Exists()
		{
			var a = Android.Gms.Cast.Framework.Media.Widget.ExpandedControllerActivity.ActivityService;

			Assert.True(a != null);
		}
	}
}
