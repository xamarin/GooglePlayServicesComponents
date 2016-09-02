using System;
using Android.Gms.Tasks;
using Android.Gms.Extensions;

namespace Firebase
{
    public partial class FirebaseApp
    {
        public System.Threading.Tasks.Task<Firebase.Auth.GetTokenResult> GetTokenAsync (bool flag)
        {
            return GetToken (flag).AsAsync<Firebase.Auth.GetTokenResult> ();
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
