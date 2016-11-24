using System;
using Android.Gms.Extensions;

namespace Firebase.Auth
{
    public partial class FirebaseAuth
    {
        public System.Threading.Tasks.Task<IAuthResult> SignInAnonymouslyAsync ()
        {
            return SignInAnonymously ().AsAsync<IAuthResult> ();
        }

        public System.Threading.Tasks.Task<IAuthResult> SignInWithCustomTokenAsync (string customToken)
        {
            return SignInWithCustomToken (customToken).AsAsync<IAuthResult> ();
        }

        public System.Threading.Tasks.Task<IAuthResult> CreateUserWithEmailAndPasswordAsync (string email, string password)
        {
            return CreateUserWithEmailAndPassword (email, password).AsAsync<IAuthResult> ();
        }

        public System.Threading.Tasks.Task<IAuthResult> SignInWithCredentialAsync (AuthCredential authCredential)
        {
            return SignInWithCredential (authCredential).AsAsync<IAuthResult> ();
        }

        public System.Threading.Tasks.Task<IAuthResult> SignInWithEmailAndPasswordAsync (string email, string password)
        {
            return SignInWithEmailAndPassword (email, password).AsAsync<IAuthResult> ();
        }

        public System.Threading.Tasks.Task<IProviderQueryResult> FetchProvidersForEmailAsync (string email)
        {
            return FetchProvidersForEmail (email).AsAsync<IProviderQueryResult> ();
        }

        public System.Threading.Tasks.Task SendPasswordResetEmailAsync (string email)
        {
            return SendPasswordResetEmail (email).AsAsync ();
        }
    }
}

namespace Firebase.Auth
{
    public partial class FirebaseUser
    {
        public System.Threading.Tasks.Task<IAuthResult> LinkWithCredentialAsync (AuthCredential credential)
        {
            return LinkWithCredential (credential).AsAsync<IAuthResult> ();
        }

        public System.Threading.Tasks.Task DeleteAsync ()
        {
            return Delete ().AsAsync ();
        }

        public System.Threading.Tasks.Task<GetTokenResult> GetTokenAsync (bool flag)
        {
            return GetToken (flag).AsAsync<GetTokenResult> ();
        }

        public System.Threading.Tasks.Task ReauthenticateAsync (AuthCredential credential)
        {
            return Reauthenticate (credential).AsAsync ();
        }

        public System.Threading.Tasks.Task ReloadAsync ()
        {
            return Reload ().AsAsync ();
        }

        public System.Threading.Tasks.Task<IAuthResult> UnlinkAsync (string value)
        {
            return Unlink (value).AsAsync<IAuthResult> ();
        }

        public System.Threading.Tasks.Task UpdateEmailAsync (string email)
        {
            return UpdateEmail (email).AsAsync ();
        }

        public System.Threading.Tasks.Task UpdatePasswordAsync (string password)
        {
            return UpdatePassword (password).AsAsync ();
        }

        public System.Threading.Tasks.Task UpdateProfileAsync (UserProfileChangeRequest userProfileChangeRequest)
        {
            return UpdateProfile (userProfileChangeRequest).AsAsync ();
        }
    }
}

