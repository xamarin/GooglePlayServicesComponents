using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirebasePerf.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FirebasePerf
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();


			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			Firebase.Perf.FirebasePerformance.Instance.PerformanceCollectionEnabled = true;
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
