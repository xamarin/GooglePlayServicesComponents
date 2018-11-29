using System;
using Android.Gms.Extensions;

namespace Firebase.Iid 
{
    public partial class FirebaseInstanceId 
	{
        public System.Threading.Tasks.Task<IInstanceIdResult> GetInstanceIdAsync ()
        {
            return GetInstanceId ().AsAsync<IInstanceIdResult> ();
        }
    }
}
