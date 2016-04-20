//using System;
//using Android.Runtime;
//
//namespace Android.Gms.Location.Places 
//{
//
////    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.gms.location.places']/class[@name='PlaceBuffer']"
////    public partial class PlaceBuffer 
////    {
////        IntPtr id_get;
////
////        public override global::Java.Lang.Object Get (int index)
////        {            
////            if (id_get == IntPtr.Zero)
////                id_get = JNIEnv.GetMethodID (class_ref, "get", "(I)Lcom/google/android/gms/location/places/PlaceLikelihoodBuffer;");
////
////            if (GetType () == ThresholdType)
////                return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_get, new JValue (index)), JniHandleOwnership.TransferLocalRef);
////            else
////                return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "get", "(I)Lcom/google/android/gms/location/places/PlaceLikelihoodBuffer;"), new JValue (index)), JniHandleOwnership.TransferLocalRef);
////        }
////    }
////
////    public partial class PlaceLikelihoodBuffer
////    {
////        IntPtr id_get;
////
////        public override global::Java.Lang.Object Get (int index)
////        {            
////            if (id_get == IntPtr.Zero)
////                id_get = JNIEnv.GetMethodID (class_ref, "get", "(I)Lcom/google/android/gms/location/places/PlaceLikelihoodBuffer;");
////
////            if (GetType () == ThresholdType)
////                return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_get, new JValue (index)), JniHandleOwnership.TransferLocalRef);
////            else
////                return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "get", "(I)Lcom/google/android/gms/location/places/PlaceLikelihoodBuffer;"), new JValue (index)), JniHandleOwnership.TransferLocalRef);
////        }
////    }
//
//}