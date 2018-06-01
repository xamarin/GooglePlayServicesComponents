using System;
using System.Linq;
using System.Collections.Generic;
using Android.Runtime;

namespace Android.Gms.Common.Apis
{
    //UnsupportedApiCallException
    public partial class UnsupportedApiCallException 
    {
        static IntPtr id_getMessage;
        public override unsafe string Message
        {
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.common.api']/class[@name='UnsupportedApiCallException']/method[@name='getMessage' and count(parameter)=0]"
            [Register("getMessage", "()Ljava/lang/String;", "GetGetMessageHandler")]
            get
            {
                if (id_getMessage == IntPtr.Zero)
                    id_getMessage = JNIEnv.GetMethodID(class_ref, "getMessage", "()Ljava/lang/String;");
                try
                {
                    return JNIEnv.GetString(JNIEnv.CallObjectMethod(((global::Java.Lang.Throwable)this).Handle, id_getMessage), JniHandleOwnership.TransferLocalRef);
                }
                finally
                {
                }
            }
        }
    }
}

