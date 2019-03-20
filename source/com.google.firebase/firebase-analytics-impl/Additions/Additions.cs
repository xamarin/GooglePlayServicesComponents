using System;
namespace Firebase.Analytics
{
    public partial class FirebaseAnalytics
    {
        [Obsolete]
        public Android.Gms.Tasks.Task AppInstanceId { get { return GetAppInstanceId (); } }
    }
}
