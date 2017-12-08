using System;
using System.Threading.Tasks;
using Android.Gms.Extensions;

namespace Android.Gms.InstantApp
{
	public partial class InstantAppsClient
	{
		public Task<Android.OS.ParcelFileDescriptor> GetInstantAppDataAsync ()
		{
			return InstantAppData.AsAsync<Android.OS.ParcelFileDescriptor>();
		}
	}
}
