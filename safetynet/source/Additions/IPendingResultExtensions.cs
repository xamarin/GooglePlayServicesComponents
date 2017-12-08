using System;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;
using System.Collections.Generic;

namespace Android.Gms.SafetyNet
{
    public static partial class ISafetyNetApiExtensions
    {
        [Obsolete]
        public static async Task<ISafetyNetApiAttestationResult> AttestAsync(this ISafetyNetApi api, GoogleApiClient googleApiClient, byte[] nonce)
        {
            return (await api.Attest(googleApiClient, nonce)).JavaCast<ISafetyNetApiAttestationResult>();
        }
        public static async Task<ISafetyNetApiSafeBrowsingResult> LookupUriAsync(this ISafetyNetApi api, GoogleApiClient googleApiClient, IList<Java.Lang.Integer> threatTypes, string uri)
        {
            return (await api.LookupUri(googleApiClient, threatTypes, uri)).JavaCast<ISafetyNetApiSafeBrowsingResult>();
        }

        [Obsolete]
        public static async Task<ISafetyNetApiSafeBrowsingResult> LookupUriAsync(this ISafetyNetApi api, GoogleApiClient client, string uri, int[] threatTypes)
        {
            return (await api.LookupUri(client, uri, threatTypes)).JavaCast<ISafetyNetApiSafeBrowsingResult>();
        }

        [Obsolete]
        public static async Task<IResult> VerifyWithRecaptchaAsync(this ISafetyNetApi api, GoogleApiClient client, string recaptcha)
        {
            return (await api.VerifyWithRecaptcha(client, recaptcha)).JavaCast<IResult>();
        }

		[Obsolete]
		public static async Task<ISafetyNetApiSafeBrowsingResult> LookupUriAsync(this ISafetyNetApi api, GoogleApiClient client, string uri, string apiKey, int[] threatTypes)
		{
			return (await api.LookupUri(client, uri, apiKey, threatTypes)).JavaCast<ISafetyNetApiSafeBrowsingResult>();
		}

		[Obsolete]
		public static async Task<ISafetyNetApiVerifyAppsUserResult> IsVerifyAppsEnabledAsync(this ISafetyNetApi api, GoogleApiClient client)
		{
			return (await api.IsVerifyAppsEnabled(client)).JavaCast<ISafetyNetApiVerifyAppsUserResult>();
		}

		[Obsolete]
		public static async Task<ISafetyNetApiVerifyAppsUserResult> EnableVerifyAppsAsync(this ISafetyNetApi api, GoogleApiClient client)
		{
			return (await api.EnableVerifyApps(client)).JavaCast<ISafetyNetApiVerifyAppsUserResult>();
		}

		[Obsolete]
		public static async Task<ISafetyNetApiHarmfulAppsResult> ListHarmfulAppsAsync(this ISafetyNetApi api, GoogleApiClient client)
		{
			return (await api.ListHarmfulApps(client)).JavaCast<ISafetyNetApiHarmfulAppsResult>();
		}
    }
}
