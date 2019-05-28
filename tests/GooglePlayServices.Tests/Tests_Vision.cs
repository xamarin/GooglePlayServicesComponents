using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Xunit;

namespace GooglePlayServices.Tests
{
	public class Tests_Vision
	{
		[Fact]
		public void BarcodeDetector_Builder()
		{
			var detector = new BarcodeDetector.Builder (Application.Context).Build ();


			Assert.True(detector != null);
		}

		[Fact]
		public void FaceDetector_Builder()
		{
			var stillFaceDetector = new FaceDetector.Builder(Android.App.Application.Context)
			.SetTrackingEnabled(true)
			.SetMode(FaceDetectionMode.Accurate)
			.SetLandmarkType(LandmarkDetectionType.None)
			.Build();

			Assert.True(stillFaceDetector != null);
		}
	}
}
