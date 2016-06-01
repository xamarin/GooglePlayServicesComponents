using System;
using Android.Runtime;

namespace Firebase.Storage
{
    public partial class FileDownloadTask
    {
        static IntPtr id_getResult;
        protected override unsafe global::Java.Lang.Object RawResult {
            [Register ("getResult", "()Ljava/lang/Object;", "GetGetRawResultHandler")]
            get {
                if (id_getResult == IntPtr.Zero)
                    id_getResult = JNIEnv.GetMethodID (class_ref, "getResult", "()Ljava/lang/Object;");
                try {
                    return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_getResult), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }

    public partial class StreamDownloadTask
    {
        static IntPtr id_getResult;
        protected override unsafe global::Java.Lang.Object RawResult {
            [Register ("getResult", "()Ljava/lang/Object;", "GetGetRawResultHandler")]
            get {
                if (id_getResult == IntPtr.Zero)
                    id_getResult = JNIEnv.GetMethodID (class_ref, "getResult", "()Ljava/lang/Object;");
                try {
                    return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_getResult), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }

    public partial class UploadTask
    {
        static IntPtr id_getResult;
        protected override unsafe global::Java.Lang.Object RawResult {
            [Register ("getResult", "()Ljava/lang/Object;", "GetGetRawResultHandler")]
            get {
                if (id_getResult == IntPtr.Zero)
                    id_getResult = JNIEnv.GetMethodID (class_ref, "getResult", "()Ljava/lang/Object;");
                try {
                    return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_getResult), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }
}

