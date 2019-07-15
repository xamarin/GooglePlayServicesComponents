using System;
using System.Windows.Input;
using Square.OkHttp3;
using Xamarin.Forms;

namespace FirebasePerf.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "About";

			OpenWebCommand = new Command(MakeHttpCall);
		}

		public ICommand OpenWebCommand { get; }

		private async void MakeHttpCall()
		{
#if __ANDROID__
			var client = new OkHttpClient();
			var requestBody = RequestBody.Create(
				MediaType.Parse("application/json"),
				"{\"title\":\"foo\",\"body\": \"bar\", \"userId\":\"1\", \"id\":\"1\"}");
			var request = new Request.Builder()
				.Url("https://jsonplaceholder.typicode.com/posts")
				.Post(requestBody)
				.Build();

			var response = await client.NewCall(request).ExecuteAsync();
#endif
		}
	}
}