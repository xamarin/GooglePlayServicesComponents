using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Extensions;

namespace Android.Gms.Auth.Api.SignIn
{

    public partial class GoogleSignIn
    {
        public static Task<GoogleSignInAccount> GetSignedInAccountFromIntentAsync (Intent intent)
        {
            return GetSignedInAccountFromIntent (intent).AsAsync<GoogleSignInAccount>();
        }
    }

    public partial class GoogleSignInClient
    {
        public Task RevokeAccessAsync ()
        {
            return RevokeAccess ().AsAsync ();
        }

        public Task SignOutAsync ()
        {
            return SignOut ().AsAsync ();
        }

        public Task<GoogleSignInAccount> SilentSignInAsync ()
        {
            return SilentSignIn ().AsAsync<GoogleSignInAccount> ();
        }
    }
}

namespace Android.Gms.Auth.Api.Credentials
{
    public partial class CredentialsClient
    {
        public Task DeleteAsync (Credential c)
        {
            return Delete (c).AsAsync ();
        }

        public Task DisableAutoSignInAsync ()
        {
            return DisableAutoSignIn ().AsAsync ();
        }

        public Task<CredentialRequestResponse> RequestAsync (CredentialRequest req)
        {
            return Request (req).AsAsync<CredentialRequestResponse> ();
        }

        public Task SaveAsync (Credential c)
        {
            return Save (c).AsAsync ();
        }
    }
}
