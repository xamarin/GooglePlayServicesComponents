using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Xamarin.Google.Android.Play.Core.AssetPacks;
using Xamarin.Google.Android.Play.Core.AssetPacks.Model;
using static Xamarin.Google.Android.Play.Core.AssetPacks.AssetPackStateUpdateListenerWrapper;

namespace BuildAllDotNet;

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
    }

    void Listener_StateUpdate(object? sender, AssetPackStateEventArgs e)
    {
        var status = e.State.Status();
        Android.Util.Log.Info ("Listener_StateUpdate", status.ToString ());
    }
}