using System;
using Android.Runtime;

namespace Android.Gms.Games 
{
    public partial class GamesClass
    {
        [Obsolete ("Use API instead")]
        public static Android.Gms.Common.Apis.Api Api {
            get { return GamesClass.API; }
        }
    }
}

namespace Android.Gms.Games.MultiPlayer
{
    public partial interface IMultiplayer : Android.Runtime.IJavaObject
    {

    }
}

namespace Android.Gms.Games.AppContent
{
    internal static class FreezeMethodImplementor
    {
        internal static Java.Lang.Object Freeze (ref IntPtr id_freeze, IntPtr class_ref, IntPtr Handle)
        {
            if (id_freeze == IntPtr.Zero)
                id_freeze = JNIEnv.GetMethodID (class_ref, "freeze", "()Ljava/lang/Object;");
            return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_freeze), JniHandleOwnership.TransferLocalRef);
        }
    }

    public partial class AppContentActionEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentActionRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentAnnotationEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentAnnotationRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentCardEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentCardRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentConditionEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentConditionRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentSectionEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentSectionRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }

        static IntPtr id_getActions;
        public unsafe global::System.Collections.Generic.IList<global::Android.Gms.Games.AppContent.IAppContentAction> Actions {
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.appcontent']/class[@name='AppContentCardRef']/method[@name='getActions' and count(parameter)=0]"
            [Register ("getActions", "()Ljava/util/List;", "GetGetActionsHandler")]
            get {
                if (id_getActions == IntPtr.Zero)
                    id_getActions = JNIEnv.GetMethodID (class_ref, "getActions", "()Ljava/util/List;");
                try {
                    return global::Android.Runtime.JavaList<global::Android.Gms.Games.AppContent.IAppContentAction>.FromJniHandle (JNIEnv.CallObjectMethod  (Handle, id_getActions), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }

    public partial class AppContentTupleEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class AppContentTupleRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }
}

namespace Android.Gms.Games.LeaderBoard
{
    internal static class FreezeMethodImplementor
    {
        internal static Java.Lang.Object Freeze (ref IntPtr id_freeze, IntPtr class_ref, IntPtr Handle)
        {
            if (id_freeze == IntPtr.Zero)
                id_freeze = JNIEnv.GetMethodID (class_ref, "freeze", "()Ljava/lang/Object;");
            return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_freeze), JniHandleOwnership.TransferLocalRef);
        }
    }

    public partial class LeaderboardScoreEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class LeaderboardScoreRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class LeaderboardEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class LeaderboardRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class LeaderboardVariantEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class LeaderboardVariantRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }
}

namespace Android.Gms.Games.Stats
{
    internal static class FreezeMethodImplementor
    {
        internal static Java.Lang.Object Freeze (ref IntPtr id_freeze, IntPtr class_ref, IntPtr Handle)
        {
            if (id_freeze == IntPtr.Zero)
                id_freeze = JNIEnv.GetMethodID (class_ref, "freeze", "()Ljava/lang/Object;");
            return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_freeze), JniHandleOwnership.TransferLocalRef);
        }
    }

    public partial class PlayerStatsEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class PlayerStatsRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }
}

namespace Android.Gms.Games.Request
{
    public sealed partial class GameRequestSummaryBuffer
    {
        IntPtr id_get;

        internal static IntPtr this_java_class_handle;
        internal static IntPtr this_class_ref {
            get {
                return JNIEnv.FindClass ("com/google/android/gms/games/request/GameRequestSummaryBuffer", ref this_java_class_handle);
            }
        }

        public override Java.Lang.Object Get (int position)
        {
            if (id_get == IntPtr.Zero)
                id_get = JNIEnv.GetMethodID (this_class_ref, "get", "(I)Ljava/lang/Object;");
            return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_get, new JValue (position)), JniHandleOwnership.TransferLocalRef);
        }
    }
}

namespace Android.Gms.Games.Stats
{
    public sealed partial class PlayerStatsBuffer
    {
        IntPtr id_get;

        internal static IntPtr this_java_class_handle;
        internal static IntPtr this_class_ref {
            get {
                return JNIEnv.FindClass ("com/google/android/gms/games/stats/PlayerStatsBuffer", ref this_java_class_handle);
            }
        }

        public override Java.Lang.Object Get (int position)
        {
            if (id_get == IntPtr.Zero)
                id_get = JNIEnv.GetMethodID (this_class_ref, "get", "(I)Ljava/lang/Object;");
            return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_get, new JValue (position)), JniHandleOwnership.TransferLocalRef);
        }
    }
}
//path="/api/package[@name='com.google.android.gms.games.social']/class[@name='SocialInviteBuffer']"
namespace Android.Gms.Games.Social
{
    internal static class FreezeMethodImplementor
    {
        internal static Java.Lang.Object Freeze (ref IntPtr id_freeze, IntPtr class_ref, IntPtr Handle)
        {
            if (id_freeze == IntPtr.Zero)
                id_freeze = JNIEnv.GetMethodID (class_ref, "freeze", "()Ljava/lang/Object;");
            return (Java.Lang.Object)global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod (Handle, id_freeze), JniHandleOwnership.TransferLocalRef);
        }
    }

    public sealed partial class SocialInviteBuffer
    {
        IntPtr id_get;

        internal static IntPtr this_java_class_handle;
        internal static IntPtr this_class_ref {
            get {
                return JNIEnv.FindClass ("com/google/android/gms/games/social/SocialInviteBuffer", ref this_java_class_handle);
            }
        }

        public override Java.Lang.Object Get (int position)
        {
            if (id_get == IntPtr.Zero)
                id_get = JNIEnv.GetMethodID (this_class_ref, "get", "(I)Ljava/lang/Object;");
            return (Java.Lang.Object)global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod (Handle, id_get, new JValue (position)), JniHandleOwnership.TransferLocalRef);
        }
    }

    public partial class SocialInviteEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class SocialInviteRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }
}

namespace Android.Gms.Games.Video
{
    internal static class FreezeMethodImplementor
    {
        internal static Java.Lang.Object Freeze (ref IntPtr id_freeze, IntPtr class_ref, IntPtr Handle)
        {
            if (id_freeze == IntPtr.Zero)
                id_freeze = JNIEnv.GetMethodID (class_ref, "freeze", "()Ljava/lang/Object;");
            return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_freeze), JniHandleOwnership.TransferLocalRef);
        }
    }

    public partial class VideoRef
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public partial class VideoEntity
    {
        IntPtr id_freeze;
        public Java.Lang.Object Freeze ()
        {
            return FreezeMethodImplementor.Freeze (ref id_freeze, class_ref, Handle);
        }
    }

    public sealed partial class VideoBuffer
    {
        IntPtr id_get;

        internal static IntPtr this_java_class_handle;
        internal static IntPtr this_class_ref {
            get {
                return JNIEnv.FindClass ("com/google/android/gms/games/video/VideoBuffer", ref this_java_class_handle);
            }
        }

        public override Java.Lang.Object Get (int position)
        {
            if (id_get == IntPtr.Zero)
                id_get = JNIEnv.GetMethodID (this_class_ref, "get", "(I)Ljava/lang/Object;");
            return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_get, new JValue (position)), JniHandleOwnership.TransferLocalRef);
        }
    }
}

namespace Android.Gms.Games.Internal
{
	public partial class GamesAbstractSafeParcelable
	{
		static IntPtr id_describeContents;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='GameEntity']/method[@name='describeContents' and count(parameter)=0]"
		[Register ("describeContents", "()I", "")]
		public int DescribeContents ()
		{
			if (id_describeContents == IntPtr.Zero)
				id_describeContents = JNIEnv.GetMethodID (class_ref, "describeContents", "()I");
			return JNIEnv.CallIntMethod  (Handle, id_describeContents);
		}

		static IntPtr id_writeToParcel_Landroid_os_Parcel_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='GameEntity']/method[@name='writeToParcel' and count(parameter)=2 and parameter[1][@type='android.os.Parcel'] and parameter[2][@type='int']]"
		[Register ("writeToParcel", "(Landroid/os/Parcel;I)V", "")]
		public void WriteToParcel (global::Android.OS.Parcel p0, [global::Android.Runtime.GeneratedEnum] global::Android.OS.ParcelableWriteFlags p1)
		{
			if (id_writeToParcel_Landroid_os_Parcel_I == IntPtr.Zero)
				id_writeToParcel_Landroid_os_Parcel_I = JNIEnv.GetMethodID (class_ref, "writeToParcel", "(Landroid/os/Parcel;I)V");
			JNIEnv.CallVoidMethod  (Handle, id_writeToParcel_Landroid_os_Parcel_I, new JValue (p0), new JValue ((int) p1));
		}
	}
}


namespace Android.Gms.Games.Video
{
	public partial class VideoEntity
	{
		static IntPtr id_describeContents;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='GameEntity']/method[@name='describeContents' and count(parameter)=0]"
		[Register("describeContents", "()I", "")]
		public int DescribeContents()
		{
			if (id_describeContents == IntPtr.Zero)
				id_describeContents = JNIEnv.GetMethodID(class_ref, "describeContents", "()I");
			return JNIEnv.CallIntMethod(Handle, id_describeContents);
		}

		//static IntPtr id_writeToParcel_Landroid_os_Parcel_I;
		//// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='GameEntity']/method[@name='writeToParcel' and count(parameter)=2 and parameter[1][@type='android.os.Parcel'] and parameter[2][@type='int']]"
		//[Register("writeToParcel", "(Landroid/os/Parcel;I)V", "")]
		//public void WriteToParcel(global::Android.OS.Parcel p0, [global::Android.Runtime.GeneratedEnum] global::Android.OS.ParcelableWriteFlags p1)
		//{
		//	if (id_writeToParcel_Landroid_os_Parcel_I == IntPtr.Zero)
		//		id_writeToParcel_Landroid_os_Parcel_I = JNIEnv.GetMethodID(class_ref, "writeToParcel", "(Landroid/os/Parcel;I)V");
		//	JNIEnv.CallVoidMethod(Handle, id_writeToParcel_Landroid_os_Parcel_I, new JValue(p0), new JValue((int)p1));
		//}
	}
}


//namespace Android.Gms.Games
//{
//    public partial class GameEntity
//    {
//        static IntPtr id_describeContents;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='GameEntity']/method[@name='describeContents' and count(parameter)=0]"
//        [Register ("describeContents", "()I", "")]
//        public int DescribeContents ()
//        {
//            if (id_describeContents == IntPtr.Zero)
//                id_describeContents = JNIEnv.GetMethodID (class_ref, "describeContents", "()I");
//            return JNIEnv.CallIntMethod  (Handle, id_describeContents);
//        }
//
//        static IntPtr id_writeToParcel_Landroid_os_Parcel_I;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='GameEntity']/method[@name='writeToParcel' and count(parameter)=2 and parameter[1][@type='android.os.Parcel'] and parameter[2][@type='int']]"
//        [Register ("writeToParcel", "(Landroid/os/Parcel;I)V", "")]
//        public void WriteToParcel (global::Android.OS.Parcel p0, [global::Android.Runtime.GeneratedEnum] global::Android.OS.ParcelableWriteFlags p1)
//        {
//            if (id_writeToParcel_Landroid_os_Parcel_I == IntPtr.Zero)
//                id_writeToParcel_Landroid_os_Parcel_I = JNIEnv.GetMethodID (class_ref, "writeToParcel", "(Landroid/os/Parcel;I)V");
//            JNIEnv.CallVoidMethod  (Handle, id_writeToParcel_Landroid_os_Parcel_I, new JValue (p0), new JValue ((int) p1));
//        }
//    }
//
//    public partial class PlayerEntity
//    {
//        static IntPtr id_describeContents;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='PlayerEntity']/method[@name='describeContents' and count(parameter)=0]"
//        [Register ("describeContents", "()I", "")]
//        public int DescribeContents ()
//        {
//            if (id_describeContents == IntPtr.Zero)
//                id_describeContents = JNIEnv.GetMethodID (class_ref, "describeContents", "()I");
//            return JNIEnv.CallIntMethod  (Handle, id_describeContents);
//        }
//
//        static IntPtr id_writeToParcel_Landroid_os_Parcel_I;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games']/class[@name='PlayerEntity']/method[@name='writeToParcel' and count(parameter)=2 and parameter[1][@type='android.os.Parcel'] and parameter[2][@type='int']]"
//        [Register ("writeToParcel", "(Landroid/os/Parcel;I)V", "")]
//        public void WriteToParcel (global::Android.OS.Parcel p0, [global::Android.Runtime.GeneratedEnum] global::Android.OS.ParcelableWriteFlags p1)
//        {
//            if (id_writeToParcel_Landroid_os_Parcel_I == IntPtr.Zero)
//                id_writeToParcel_Landroid_os_Parcel_I = JNIEnv.GetMethodID (class_ref, "writeToParcel", "(Landroid/os/Parcel;I)V");
//            JNIEnv.CallVoidMethod  (Handle, id_writeToParcel_Landroid_os_Parcel_I, new JValue (p0), new JValue ((int) p1));
//        }
//    }
//}
//
namespace Android.Gms.Games.MultiPlayer
{
    //public sealed partial class ParticipantBuffer
    //{
    //    IntPtr id_get;

    //    internal static IntPtr this_java_class_handle;
    //    internal static IntPtr this_class_ref
    //    {
    //        get
    //        {
    //            return JNIEnv.FindClass("com/google/android/gms/games/multiplayer/ParticipantBuffer", ref this_java_class_handle);
    //        }
    //    }

    //    public override Java.Lang.Object Get(int position)
    //    {
    //        if (id_get == IntPtr.Zero)
    //            id_get = JNIEnv.GetMethodID(this_class_ref, "get", "(I)Ljava/lang/Object;");
    //        return (Java.Lang.Object)global::Java.Lang.Object.GetObject<global::Java.Lang.Object>(JNIEnv.CallObjectMethod(Handle, id_get, new JValue(position)), JniHandleOwnership.TransferLocalRef);
    //    }
    //}
//    public partial class InvitationEntity
//    {
//        static IntPtr id_describeContents;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.multiplayer']/class[@name='InvitationEntity']/method[@name='describeContents' and count(parameter)=0]"
//        [Register ("describeContents", "()I", "")]
//        public int DescribeContents ()
//        {
//            if (id_describeContents == IntPtr.Zero)
//                id_describeContents = JNIEnv.GetMethodID (class_ref, "describeContents", "()I");
//            return JNIEnv.CallIntMethod  (Handle, id_describeContents);
//        }
//
//        static IntPtr id_writeToParcel_Landroid_os_Parcel_I;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.multiplayer']/class[@name='InvitationEntity']/method[@name='writeToParcel' and count(parameter)=2 and parameter[1][@type='android.os.Parcel'] and parameter[2][@type='int']]"
//        [Register ("writeToParcel", "(Landroid/os/Parcel;I)V", "")]
//        public void WriteToParcel (global::Android.OS.Parcel p0, [global::Android.Runtime.GeneratedEnum] global::Android.OS.ParcelableWriteFlags p1)
//        {
//            if (id_writeToParcel_Landroid_os_Parcel_I == IntPtr.Zero)
//                id_writeToParcel_Landroid_os_Parcel_I = JNIEnv.GetMethodID (class_ref, "writeToParcel", "(Landroid/os/Parcel;I)V");
//            JNIEnv.CallVoidMethod  (Handle, id_writeToParcel_Landroid_os_Parcel_I, new JValue (p0), new JValue ((int) p1));
//        }
//    }
//
//    public partial class ParticipantEntity
//    {
//        static IntPtr id_describeContents;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.multiplayer']/class[@name='ParticipantEntity']/method[@name='describeContents' and count(parameter)=0]"
//        [Register ("describeContents", "()I", "")]
//        public int DescribeContents ()
//        {
//            if (id_describeContents == IntPtr.Zero)
//                id_describeContents = JNIEnv.GetMethodID (class_ref, "describeContents", "()I");
//            return JNIEnv.CallIntMethod  (Handle, id_describeContents);
//        }
//
//        static IntPtr id_writeToParcel_Landroid_os_Parcel_I;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.multiplayer']/class[@name='ParticipantEntity']/method[@name='writeToParcel' and count(parameter)=2 and parameter[1][@type='android.os.Parcel'] and parameter[2][@type='int']]"
//        [Register ("writeToParcel", "(Landroid/os/Parcel;I)V", "")]
//        public void WriteToParcel (global::Android.OS.Parcel p0, [global::Android.Runtime.GeneratedEnum] global::Android.OS.ParcelableWriteFlags p1)
//        {
//            if (id_writeToParcel_Landroid_os_Parcel_I == IntPtr.Zero)
//                id_writeToParcel_Landroid_os_Parcel_I = JNIEnv.GetMethodID (class_ref, "writeToParcel", "(Landroid/os/Parcel;I)V");
//            JNIEnv.CallVoidMethod  (Handle, id_writeToParcel_Landroid_os_Parcel_I, new JValue (p0), new JValue ((int) p1));
//        }
//    }
}
//
//namespace Android.Gms.Games.MultiPlayer.RealTime
//{
//    public partial class RoomEntity
//    {
//        static IntPtr id_describeContents;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.multiplayer.realtime']/class[@name='RoomEntity']/method[@name='describeContents' and count(parameter)=0]"
//        [Register ("describeContents", "()I", "")]
//        public int DescribeContents ()
//        {
//            if (id_describeContents == IntPtr.Zero)
//                id_describeContents = JNIEnv.GetMethodID (class_ref, "describeContents", "()I");
//            return JNIEnv.CallIntMethod  (Handle, id_describeContents);
//        }
//
//        static IntPtr id_writeToParcel_Landroid_os_Parcel_I;
//        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.multiplayer.realtime']/class[@name='RoomEntity']/method[@name='writeToParcel' and count(parameter)=2 and parameter[1][@type='android.os.Parcel'] and parameter[2][@type='int']]"
//        [Register ("writeToParcel", "(Landroid/os/Parcel;I)V", "")]
//        public void WriteToParcel (global::Android.OS.Parcel p0, [global::Android.Runtime.GeneratedEnum] global::Android.OS.ParcelableWriteFlags p1)
//        {
//            if (id_writeToParcel_Landroid_os_Parcel_I == IntPtr.Zero)
//                id_writeToParcel_Landroid_os_Parcel_I = JNIEnv.GetMethodID (class_ref, "writeToParcel", "(Landroid/os/Parcel;I)V");
//            JNIEnv.CallVoidMethod  (Handle, id_writeToParcel_Landroid_os_Parcel_I, new JValue (p0), new JValue ((int) p1));
//        }
//    }
//}
