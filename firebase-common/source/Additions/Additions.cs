using System;
using Android.Gms.Tasks;
using Android.Gms.Extensions;

namespace Firebase.Auth
{
    public partial class FirebaseUser
    {
        public System.Threading.Tasks.Task<IAuthResult> LinkWithCredentialAsync (AuthCredential credential)
        {
            return LinkWithCredential (credential).AsAsync<IAuthResult> ();
        }
    }
}
