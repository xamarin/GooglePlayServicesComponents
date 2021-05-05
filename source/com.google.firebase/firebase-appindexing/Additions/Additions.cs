using System;
using Java.Interop;
using Android.Runtime;

namespace Android.Gms.AppIndexing
{
    public partial class AppIndex
    {
        [Obsolete ("Use APP_INDEX_API instead")]
        public static Android.Gms.Common.Apis.Api AppIndexApiField {
            get { return AppIndex.APP_INDEX_API; }
        }

        [Obsolete ("Use API instead")]
        public static Android.Gms.Common.Apis.Api Api {
            get { return AppIndex.API; }
        }
    }
}
