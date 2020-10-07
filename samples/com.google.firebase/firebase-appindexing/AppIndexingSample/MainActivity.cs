using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using AppIndexingAction = Android.Gms.AppIndexing.Action;

// Specify our noindex.xml in the manifest
using Android.Gms.Common.Apis;
using Android.Gms.AppIndexing;


[assembly: MetaData ("search-engine", Resource="@xml/noindex")]

namespace AppIndexingSample
{
    // These intent filters will matche these urls:
    //    http://recipe-app.com/recipes
    //    recipe-app://recipes/grilled-potato-salad
    //    recipe-app://recipes/desserts/peach-pie
    [IntentFilter (new [] { Intent.ActionView }, 
        Label = "View Recipe",
        Categories = new [] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataHost = "recipes",
        DataScheme = "recipe-app")]
    [IntentFilter (new [] { Intent.ActionView }, 
        Label = "View Recipes",
        Categories = new [] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataHost = "http",
        DataScheme = "recipe-app.com",
        DataPathPrefix = "/recipes")]
    
    [Activity (Label = "App Indexing Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly Android.Net.Uri APP_URI = Android.Net.Uri.Parse("android-app://com.xamarin.googleplayservices.appindexingsample/http/recipe-app.com/recipes");
        static readonly Android.Net.Uri WEB_URL = Android.Net.Uri.Parse("http://recipe-app.com/recipes/");

        GoogleApiClient googleApiClient;

        TextView textId;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            googleApiClient = new GoogleApiClient.Builder (this)
                .AddApi (AppIndex.API)
                .Build ();
            
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            textId = FindViewById<TextView> (Resource.Id.textId);

            if (Intent != null)
                OnNewIntent (Intent);
        }

        protected override void OnNewIntent (Intent intent)
        {
            base.OnNewIntent (intent);

            var action = intent.Action;
            var data = intent.DataString;

            if (Intent.ActionView == action && !string.IsNullOrEmpty (data)) {

                var recipeId = data.Substring (data.LastIndexOf('/') + 1);

                // TODO: Show recipe details UI
                //ShowRecipe (recipeId);

                textId.Text = "Recipe Id Specified: " + recipeId;
            }
        }

        protected override async void OnStart ()
        {
            base.OnStart ();

            googleApiClient.Connect ();

            // Define a title for your current page, shown in autocompletion UI
            var title = "View Recipes";

            // Construct the Action performed by the user
            var viewAction = AppIndexingAction.NewAction (Android.Gms.AppIndexing.Action.TypeView, title, WEB_URL, APP_URI);

            // Call the App Indexing API start method after the view has completely rendered
            await AppIndex.AppIndexApi.StartAsync (googleApiClient, viewAction);
        }

        protected override async void OnStop ()
        {
            base.OnStop ();

            // Call End() and disconnect the client
            var title = "View Recipes";
            var viewAction = AppIndexingAction.NewAction (Android.Gms.AppIndexing.Action.TypeView, title, WEB_URL, APP_URI);

            await AppIndex.AppIndexApi.EndAsync (googleApiClient, viewAction);

            googleApiClient.Disconnect ();
        }
    }
}


