using Android.App;
using AndroidX.AppCompat.App;

namespace Admob
{
	[Activity]
	public class SecondActivity : AppCompatActivity
	{
		protected override void OnCreate(Android.OS.Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.second_activity);
		}
	}
}

