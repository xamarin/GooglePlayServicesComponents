using System;
using System.Threading.Tasks;

namespace Android.Gms.InstantApps
{
	public partial class InstantAppsClient
	{
		public Task<Android.OS.ParcelFileDescriptor> GetInstantAppDataAsync ()
		{
			return InstantAppData.AsAsync<Android.OS.ParcelFileDescriptor>();
		}
	}
}
