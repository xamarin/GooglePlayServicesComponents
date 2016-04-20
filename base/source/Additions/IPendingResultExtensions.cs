//using System;
//using System.Threading.Tasks;
//using Android.Gms.Common.Apis;
//using Android.Gms.Common;
//using Android.Runtime;
//
//namespace Android.Gms.Auth.Api.Credentials 
//{
//    public static partial class ICredentialsApiExtensions 
//    {
//        public static async Task<Statuses> DeleteAsync (this ICredentialsApi api, IGoogleApiClient client, Credential credential) 
//        {
//            return (await api.Delete (client, credential)).JavaCast<Statuses> ();
//        }
//        public static async Task<Statuses> DisableAutoSignInAsync (this ICredentialsApi api, IGoogleApiClient client) 
//        {
//            return (await api.DisableAutoSignIn (client)).JavaCast<Android.Gms.Common.Apis.Statuses> ();
//        }
//        public static async Task<ICredentialRequestResult> RequestAsync (this ICredentialsApi api, IGoogleApiClient client, CredentialRequest request) 
//        {
//            return (await api.Request (client, request)).JavaCast<ICredentialRequestResult> ();
//        }
//        public static async Task<Statuses> SaveAsync (this ICredentialsApi api, IGoogleApiClient client, Credential credential) 
//        {
//            return (await api.Save (client, credential)).JavaCast<Statuses> ();
//        }
//    }
//}
//
//namespace Android.Gms.Auth.Api.Proxy
//{
//    public static partial class IProxyApiExtensions 
//    {
//        public static async Task<IProxyApiProxyResult> PerformProxyRequestAsync (this IProxyApi api, IGoogleApiClient client, Android.Gms.Auth.Api.Proxy.ProxyRequest request) 
//        {
//            return (await api.PerformProxyRequest (client, request)).JavaCast<IProxyApiProxyResult> ();
//        }
//    }
//}
