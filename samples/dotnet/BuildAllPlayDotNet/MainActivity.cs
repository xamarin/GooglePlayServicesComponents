using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Xamarin.Google.Android.Play.Core.AssetPacks;
using Xamarin.Google.Android.Play.Core.AssetPacks.Model;
using static Xamarin.Google.Android.Play.Core.AssetPacks.AssetPackStateUpdateListenerWrapper;

namespace BuildAllPlayDotNet;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    IAssetPackManager? assetPackManager;
    AssetPackStateUpdateListenerWrapper? listener;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        assetPackManager = AssetPackManagerFactory.GetInstance (this);
        // Create our Wrapper and set up the event handler.
        listener = new AssetPackStateUpdateListenerWrapper();
        listener.StateUpdate += Listener_StateUpdate;

        var location = assetPackManager.GetPackLocation ("installtimepack");
        assetPackManager.Fetch (new string[] {"fastfollowpack"});
        assetPackManager.Fetch (new string[] {"ondemandpack"});
    }

    void Listener_StateUpdate(object? sender, AssetPackStateEventArgs e)
    {
        var status = e.State.Status();
        Android.Util.Log.Info ("Listener_StateUpdate", status.ToString ());
    }

    protected override void OnResume()
    {
        // regsiter our Listener Wrapper with the IAssetPackManager so we get feedback.
        assetPackManager?.RegisterListener(listener?.Listener);
        base.OnResume();
    }

    protected override void OnPause()
    {
        assetPackManager?.UnregisterListener(listener?.Listener);
        base.OnPause();
    }
}