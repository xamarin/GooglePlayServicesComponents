using System;
using System.Threading.Tasks;
using Java.Interop;
using Android.Gms.Common.Apis;

namespace Android.Gms.InstantApp
{
    public static class IInstantAppsApiExtensions
    {
		public static async Task<IInstantAppsApiGetInstantAppDataResult> GetInstantAppDataAsync(this IInstantAppsApi api, Android.Gms.Common.Apis.GoogleApiClient apiClient)
		{
			return (await api.GetInstantAppData(apiClient)).JavaCast<IInstantAppsApiGetInstantAppDataResult>();
		}
    }
}
