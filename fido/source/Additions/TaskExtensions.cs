using System;
using System.Threading.Tasks;
using Android.Gms.Extensions;

namespace Android.Gms.Fido.U2F
{
	public partial class U2fApiClient
	{
		public Task<IU2fPendingIntent> GetRegisterIntentAsync(Android.Gms.Api.Common.RegisterRequestParams requestParams)
		{
			return GetRegisterIntent(requestParams).AsAsync<IU2fPendingIntent>();
		}

		public Task<IU2fPendingIntent> GetSignIntentAsync(Android.Gms.Api.Common.SignRequestParams requestParams)
		{
			return GetSignIntent(requestParams).AsAsync<IU2fPendingIntent>();
		}
	}
}
