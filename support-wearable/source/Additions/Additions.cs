using System;
using Android.Runtime;
using Java.Interop;

namespace Android.Support.Wearable.Views
{
    public partial class ActionPage
    {

        public override bool Enabled {
            get { return base.Enabled; }
            set { SetEnabled (value); }
        }

        static Delegate cb_setEnabled_Z;
        #pragma warning disable 0169
        static Delegate GetSetEnabled_ZHandler ()
        {
            if (cb_setEnabled_Z == null)
                cb_setEnabled_Z = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, bool>) n_SetEnabled_Z);
            return cb_setEnabled_Z;
        }

        static void n_SetEnabled_Z (IntPtr jnienv, IntPtr native__this, bool enabled)
        {
            global::Android.Support.Wearable.Views.ActionPage __this = global::Java.Lang.Object.GetObject<global::Android.Support.Wearable.Views.ActionPage> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetEnabled (enabled);
        }
        #pragma warning restore 0169

        static IntPtr id_setEnabled_Z;
        // Metadata.xml XPath method reference: path="/api/package[@name='android.support.wearable.view']/class[@name='ActionPage']/method[@name='setEnabled' and count(parameter)=1 and parameter[1][@type='boolean']]"
        [Register ("setEnabled", "(Z)V", "GetSetEnabled_ZHandler")]
        public unsafe void SetEnabled (bool enabled)
        {
            if (id_setEnabled_Z == IntPtr.Zero)
                id_setEnabled_Z = JNIEnv.GetMethodID (class_ref, "setEnabled", "(Z)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (enabled);

                if (GetType () == ThresholdType)
                    JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setEnabled_Z, __args);
                else
                    JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object) this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setEnabled", "(Z)V"), __args);
            } finally {
            }
        }



        public global::Android.Animation.StateListAnimator StateListAnimator {
            get { return base.StateListAnimator; }
            set { SetStateListAnimator (value); }
        }

        static Delegate cb_setStateListAnimator_Landroid_animation_StateListAnimator_;
#pragma warning disable 0169
        static Delegate GetSetStateListAnimator_Landroid_animation_StateListAnimator_Handler ()
        {
            if (cb_setStateListAnimator_Landroid_animation_StateListAnimator_ == null)
                cb_setStateListAnimator_Landroid_animation_StateListAnimator_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetStateListAnimator_Landroid_animation_StateListAnimator_);
            return cb_setStateListAnimator_Landroid_animation_StateListAnimator_;
        }

        static void n_SetStateListAnimator_Landroid_animation_StateListAnimator_ (IntPtr jnienv, IntPtr native__this, IntPtr native_stateListAnimator)
        {
            global::Android.Support.Wearable.Views.ActionPage __this = global::Java.Lang.Object.GetObject<global::Android.Support.Wearable.Views.ActionPage> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            global::Android.Animation.StateListAnimator stateListAnimator = global::Java.Lang.Object.GetObject<global::Android.Animation.StateListAnimator> (native_stateListAnimator, JniHandleOwnership.DoNotTransfer);
            __this.SetStateListAnimator (stateListAnimator);
        }
#pragma warning restore 0169

        static IntPtr id_setStateListAnimator_Landroid_animation_StateListAnimator_;
        // Metadata.xml XPath method reference: path="/api/package[@name='android.support.wearable.view']/class[@name='ActionPage']/method[@name='setStateListAnimator' and count(parameter)=1 and parameter[1][@type='android.animation.StateListAnimator']]"
        [Register ("setStateListAnimator", "(Landroid/animation/StateListAnimator;)V", "GetSetStateListAnimator_Landroid_animation_StateListAnimator_Handler")]
        public unsafe void SetStateListAnimator (global::Android.Animation.StateListAnimator stateListAnimator)
        {
            if (id_setStateListAnimator_Landroid_animation_StateListAnimator_ == IntPtr.Zero)
                id_setStateListAnimator_Landroid_animation_StateListAnimator_ = JNIEnv.GetMethodID (class_ref, "setStateListAnimator", "(Landroid/animation/StateListAnimator;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (stateListAnimator);

                if (GetType () == ThresholdType)
                    JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setStateListAnimator_Landroid_animation_StateListAnimator_, __args);
                else
                    JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object) this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setStateListAnimator", "(Landroid/animation/StateListAnimator;)V"), __args);
            } finally {
            }
        }
    }

    public partial class RecyclerViewMergeAdapter
    {
        public new bool HasStableIds {
            get { return base.HasStableIds; }
            set { SetHasStableIds (value); }
        }

        static Delegate cb_setHasStableIds_Z;
#pragma warning disable 0169
        static Delegate GetSetHasStableIds_ZHandler ()
        {
            if (cb_setHasStableIds_Z == null)
                cb_setHasStableIds_Z = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, bool>) n_SetHasStableIds_Z);
            return cb_setHasStableIds_Z;
        }

        static void n_SetHasStableIds_Z (IntPtr jnienv, IntPtr native__this, bool hasStableIds)
        {
            global::Android.Support.Wearable.Views.RecyclerViewMergeAdapter __this = global::Java.Lang.Object.GetObject<global::Android.Support.Wearable.Views.RecyclerViewMergeAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetHasStableIds (hasStableIds);
        }
#pragma warning restore 0169

        static IntPtr id_setHasStableIds_Z;
        // Metadata.xml XPath method reference: path="/api/package[@name='android.support.wearable.view']/class[@name='RecyclerViewMergeAdapter']/method[@name='setHasStableIds' and count(parameter)=1 and parameter[1][@type='boolean']]"
        [Register ("setHasStableIds", "(Z)V", "GetSetHasStableIds_ZHandler")]
        public unsafe void SetHasStableIds (bool hasStableIds)
        {
            if (id_setHasStableIds_Z == IntPtr.Zero)
                id_setHasStableIds_Z = JNIEnv.GetMethodID (class_ref, "setHasStableIds", "(Z)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (hasStableIds);

                if (GetType () == ThresholdType)
                    JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setHasStableIds_Z, __args);
                else
                    JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object) this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setHasStableIds", "(Z)V"), __args);
            } finally {
            }
        }
    }
}
