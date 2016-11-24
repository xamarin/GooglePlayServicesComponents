﻿using System;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Runtime;

namespace Android.Gms.Auth.Api.Credentials 
{
    public static partial class ICredentialsApiExtensions 
    {
        public static async Task<Statuses> DeleteAsync (this ICredentialsApi api, GoogleApiClient client, Credential credential) 
        {
            return (await api.Delete (client, credential)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> DisableAutoSignInAsync (this ICredentialsApi api, GoogleApiClient client) 
        {
            return (await api.DisableAutoSignIn (client)).JavaCast<Android.Gms.Common.Apis.Statuses> ();
        }
        public static async Task<ICredentialRequestResult> RequestAsync (this ICredentialsApi api, GoogleApiClient client, CredentialRequest request) 
        {
            return (await api.Request (client, request)).JavaCast<ICredentialRequestResult> ();
        }
        public static async Task<Statuses> SaveAsync (this ICredentialsApi api, GoogleApiClient client, Credential credential) 
        {
            return (await api.Save (client, credential)).JavaCast<Statuses> ();
        }
    }
}
