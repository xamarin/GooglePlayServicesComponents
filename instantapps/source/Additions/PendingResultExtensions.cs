using System;
using System.Threading.Tasks;
using Java.Interop;
using Android.Gms.Common.Apis;

namespace Android.Gms.InstantApp
{
    public static class IInstantAppsApiExtensions
    {
        public static async Task<IInstantAppsApiLaunchDataResult> GetInstantAppLaunchDataAsync (this IInstantAppsApi api, GoogleApiClient client, string url)
        {
            return (await api.GetInstantAppLaunchData (client, url)).JavaCast<IInstantAppsApiLaunchDataResult> ();
        }
    }
}
