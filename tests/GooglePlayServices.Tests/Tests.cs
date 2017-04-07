using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Xunit;
using Xunit.Extensions;

namespace GooglePlayServices.Tests
{
	public class Tests
	{
		[Fact]
		public void GooglePlayServicesUtil_Exists ()
		{
			var versionCode = Android.Gms.Common.GooglePlayServicesUtil.GooglePlayServicesVersionCode;

			Console.WriteLine("Google Play Services Version: {0}", versionCode);

			Assert.True(versionCode > 0);
		}

		[Fact]
		public void GoogleApiAvailability_Exists()
		{
			var versionCode = Android.Gms.Common.GoogleApiAvailability.GooglePlayServicesVersionCode;

			Console.WriteLine("Google Play Services Version: {0}", versionCode);

			Assert.True(versionCode > 0);
		}
	}
}
