using System;

namespace Android.Gms.Wearable
{

    public partial class WearableClass 
    {
        [Obsolete ("Use API instead")]
        public static Android.Gms.Common.Apis.Api Api {
            get { return WearableClass.API; }
        }
    }

    //public partial class Zzf
    //{
    //    public Java.Lang.Object CreateFromParcel (Android.OS.Parcel source)
    //    {
    //        throw new Exception ("Android.Gms.WearableSdk.Zzf.CreateFromParcel not implemented!  Please contact Support!");
    //    }


    //    public Java.Lang.Object[] NewArray (int size)
    //    {
    //        throw new Exception ("Android.Gms.WearableSdk.Zzf.NewArray not implemented!  Please contact Support!");
    //    }
    //}
}

