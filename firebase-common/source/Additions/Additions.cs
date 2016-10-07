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
