using System;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;
using System.Collections.Generic;

namespace Android.Gms.SafetyNet
{
    public static partial class ISafetyNetApiExtensions
    {
        public static async Task<ISafetyNetApiAttestationResult> AttestAsync (this ISafetyNetApi api, GoogleApiClient googleApiClient, byte[] nonce)
        {
            return (await api.Attest (googleApiClient, nonce)).JavaCast<ISafetyNetApiAttestationResult> ();
        }
        public static async Task<ISafetyNetApiSafeBrowsingResult> LookupUriAsync (this ISafetyNetApi api, GoogleApiClient googleApiClient, IList<Java.Lang.Integer> threatTypes, string uri)
        {
            return (await api.LookupUri (googleApiClient, threatTypes, uri)).JavaCast<ISafetyNetApiSafeBrowsingResult> ();
        }
    }
}
