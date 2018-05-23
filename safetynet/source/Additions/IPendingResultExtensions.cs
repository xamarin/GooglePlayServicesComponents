using System;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;
using System.Collections.Generic;

namespace Android.Gms.SafetyNet
{
    public static partial class ISafetyNetApiExtensions
    {
        //[Obsolete]
        //public static async Task<ISafetyNetApiAttestationResponse> AttestAsync(this ISafetyNetApi api, GoogleApiClient googleApiClient, byte[] nonce)
        //{
        //    return (await api.Attest(googleApiClient, nonce)).JavaCast<ISafetyNetApiAttestationResult>();
        //}
        //public static async Task<ISafetyNetApiSafeBrowsingResponse> LookupUriAsync(this ISafetyNetApi api, GoogleApiClient googleApiClient, IList<Java.Lang.Integer> threatTypes, string uri)
        //{
        //    return (await api.LookupUri(googleApiClient, threatTypes, uri)).JavaCast<ISafetyNetApiSafeBrowsingResponse>();
        //}

        //[Obsolete]
        //public static async Task<ISafetyNetApiSafeBrowsingResponse> LookupUriAsync(this ISafetyNetApi api, GoogleApiClient client, string uri, int[] threatTypes)
        //{
        //    return (await api.LookupUri(client, uri, threatTypes)).JavaCast<ISafetyNetApiSafeBrowsingResponse>();
        //}

        [Obsolete]
        public static async Task<IResult> VerifyWithRecaptchaAsync(this ISafetyNetApi api, GoogleApiClient client, string recaptcha)
        {
            return (await api.VerifyWithRecaptcha(client, recaptcha)).JavaCast<IResult>();
        }

		//[Obsolete]
  //      public static async Task<ISafetyNetApiSafeBrowsingResponse> LookupUriAsync(this ISafetyNetApi api, GoogleApiClient client, string uri, string apiKey, int[] threatTypes)
		//{
  //          return (await api.LookupUri(client, uri, apiKey, threatTypes)).JavaCast<ISafetyNetApiSafeBrowsingResponse>();
		//}

		//[Obsolete]
		//public static async Task<ISafetyNetApiVerifyAppsUserResponse> IsVerifyAppsEnabledAsync(this ISafetyNetApi api, GoogleApiClient client)
		//{
  //          return (await api.IsVerifyAppsEnabled(client)).JavaCast<ISafetyNetApiVerifyAppsUserResponse>();
		//}

		//[Obsolete]
  //      public static async Task<ISafetyNetApiVerifyAppsUserResponse> EnableVerifyAppsAsync(this ISafetyNetApi api, GoogleApiClient client)
		//{
  //          return (await api.EnableVerifyApps(client)).JavaCast<ISafetyNetApiVerifyAppsUserResponse>();
		//}

		//[Obsolete]
  //      public static async Task<ISafetyNetApiVerifyAppsUserResponse> ListHarmfulAppsAsync(this ISafetyNetApi api, GoogleApiClient client)
		//{
  //          return (await api.ListHarmfulApps(client)).JavaCast<ISafetyNetApiVerifyAppsUserResponse>();
		//}
    }
}
