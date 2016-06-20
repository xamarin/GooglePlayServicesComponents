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
    }
}

