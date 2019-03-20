using System;
using Android.Runtime;
using Java.Interop;

namespace Android.Gms.Flags
{
	public partial class Flag
	{
		public partial class BooleanFlag
		{
			static Delegate cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
#pragma warning disable 0169
			static Delegate GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler()
			{
				if (cb_get_Lcom_google_android_gms_flags_IFlagProvider_ == null)
					cb_get_Lcom_google_android_gms_flags_IFlagProvider_ = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr, IntPtr>)n_Get_Lcom_google_android_gms_flags_IFlagProvider_);
				return cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
			}

			static IntPtr n_Get_Lcom_google_android_gms_flags_IFlagProvider_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::Android.Gms.Flags.Flag.BooleanFlag __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.Flag.BooleanFlag>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Gms.Flags.IFlagProvider p0 = (global::Android.Gms.Flags.IFlagProvider)global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.IFlagProvider>(native_p0, JniHandleOwnership.DoNotTransfer);
				IntPtr __ret = JNIEnv.ToLocalJniHandle(__this.Get(p0));
				return __ret;
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.flags']/class[@name='Flag.BooleanFlag']/method[@name='get' and count(parameter)=1 and parameter[1][@type='com.google.android.gms.flags.IFlagProvider']]"
			[Register("get", "(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Boolean;", "GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler")]
			protected override unsafe global::Java.Lang.Object Get(global::Android.Gms.Flags.IFlagProvider p0)
			{
				const string __id = "get.(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Boolean;";
				try
				{
					JniArgumentValue* __args = stackalloc JniArgumentValue[1];
					__args[0] = new JniArgumentValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
					return global::Java.Lang.Object.GetObject<global::Java.Lang.Object>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
				}
				finally
				{
				}
			}
		}

		public partial class IntegerFlag
		{
			static Delegate cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
#pragma warning disable 0169
			static Delegate GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler()
			{
				if (cb_get_Lcom_google_android_gms_flags_IFlagProvider_ == null)
					cb_get_Lcom_google_android_gms_flags_IFlagProvider_ = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr, IntPtr>)n_Get_Lcom_google_android_gms_flags_IFlagProvider_);
				return cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
			}

			static IntPtr n_Get_Lcom_google_android_gms_flags_IFlagProvider_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::Android.Gms.Flags.Flag.BooleanFlag __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.Flag.BooleanFlag>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Gms.Flags.IFlagProvider p0 = (global::Android.Gms.Flags.IFlagProvider)global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.IFlagProvider>(native_p0, JniHandleOwnership.DoNotTransfer);
				IntPtr __ret = JNIEnv.ToLocalJniHandle(__this.Get(p0));
				return __ret;
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.flags']/class[@name='Flag.BooleanFlag']/method[@name='get' and count(parameter)=1 and parameter[1][@type='com.google.android.gms.flags.IFlagProvider']]"
			[Register("get", "(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Boolean;", "GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler")]
			protected override unsafe global::Java.Lang.Object Get(global::Android.Gms.Flags.IFlagProvider p0)
			{
				const string __id = "get.(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Boolean;";
				try
				{
					JniArgumentValue* __args = stackalloc JniArgumentValue[1];
					__args[0] = new JniArgumentValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
					return global::Java.Lang.Object.GetObject<global::Java.Lang.Boolean>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
				}
				finally
				{
				}
			}
		}

		public partial class StringFlag
		{
			static Delegate cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
#pragma warning disable 0169
			static Delegate GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler()
			{
				if (cb_get_Lcom_google_android_gms_flags_IFlagProvider_ == null)
					cb_get_Lcom_google_android_gms_flags_IFlagProvider_ = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr, IntPtr>)n_Get_Lcom_google_android_gms_flags_IFlagProvider_);
				return cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
			}

			static IntPtr n_Get_Lcom_google_android_gms_flags_IFlagProvider_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::Android.Gms.Flags.Flag.LongFlag __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.Flag.LongFlag>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Gms.Flags.IFlagProvider p0 = (global::Android.Gms.Flags.IFlagProvider)global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.IFlagProvider>(native_p0, JniHandleOwnership.DoNotTransfer);
				IntPtr __ret = JNIEnv.ToLocalJniHandle(__this.Get(p0));
				return __ret;
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.flags']/class[@name='Flag.LongFlag']/method[@name='get' and count(parameter)=1 and parameter[1][@type='com.google.android.gms.flags.IFlagProvider']]"
			[Register("get", "(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Long;", "GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler")]
			protected override unsafe global::Java.Lang.Object Get(global::Android.Gms.Flags.IFlagProvider p0)
			{
				const string __id = "get.(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Long;";
				try
				{
					JniArgumentValue* __args = stackalloc JniArgumentValue[1];
					__args[0] = new JniArgumentValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
					return global::Java.Lang.Object.GetObject<global::Java.Lang.Long>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
				}
				finally
				{
				}
			}




		}

		public partial class LongFlag
		{
			static Delegate cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
#pragma warning disable 0169
			static Delegate GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler()
			{
				if (cb_get_Lcom_google_android_gms_flags_IFlagProvider_ == null)
					cb_get_Lcom_google_android_gms_flags_IFlagProvider_ = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr, IntPtr>)n_Get_Lcom_google_android_gms_flags_IFlagProvider_);
				return cb_get_Lcom_google_android_gms_flags_IFlagProvider_;
			}

			static IntPtr n_Get_Lcom_google_android_gms_flags_IFlagProvider_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::Android.Gms.Flags.Flag.LongFlag __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.Flag.LongFlag>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Gms.Flags.IFlagProvider p0 = (global::Android.Gms.Flags.IFlagProvider)global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.IFlagProvider>(native_p0, JniHandleOwnership.DoNotTransfer);
				IntPtr __ret = JNIEnv.ToLocalJniHandle(__this.Get(p0));
				return __ret;
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.flags']/class[@name='Flag.LongFlag']/method[@name='get' and count(parameter)=1 and parameter[1][@type='com.google.android.gms.flags.IFlagProvider']]"
			[Register("get", "(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Long;", "GetGet_Lcom_google_android_gms_flags_IFlagProvider_Handler")]
			protected override unsafe global::Java.Lang.Object Get(global::Android.Gms.Flags.IFlagProvider p0)
			{
				const string __id = "get.(Lcom/google/android/gms/flags/IFlagProvider;)Ljava/lang/Long;";
				try
				{
					JniArgumentValue* __args = stackalloc JniArgumentValue[1];
					__args[0] = new JniArgumentValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
					return global::Java.Lang.Object.GetObject<global::Java.Lang.Long>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
				}
				finally
				{
				}
			}
		}
	}
}

namespace Android.Gms.Flags.Impl
{
	public partial class DataUtils
	{
		public partial class StringUtils
		{
			static Delegate cb_getFromJSONObject_Lorg_json_JSONObject_;
#pragma warning disable 0169
			static Delegate GetGetFromJSONObject_Lorg_json_JSONObject_Handler()
			{
				if (cb_getFromJSONObject_Lorg_json_JSONObject_ == null)
					cb_getFromJSONObject_Lorg_json_JSONObject_ = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr, IntPtr>)n_GetFromJSONObject_Lorg_json_JSONObject_);
				return cb_getFromJSONObject_Lorg_json_JSONObject_;
			}

			static IntPtr n_GetFromJSONObject_Lorg_json_JSONObject_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::Android.Gms.Flags.Impl.DataUtils.StringUtils __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.Impl.DataUtils.StringUtils>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Org.Json.JSONObject p0 = global::Java.Lang.Object.GetObject<global::Org.Json.JSONObject>(native_p0, JniHandleOwnership.DoNotTransfer);
				IntPtr __ret = JNIEnv.ToLocalJniHandle(__this.GetFromJSONObject(p0));
				return __ret;
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.flags.impl']/class[@name='DataUtils.StringUtils']/method[@name='getFromJSONObject' and count(parameter)=1 and parameter[1][@type='org.json.JSONObject']]"
			[Register("getFromJSONObject", "(Lorg/json/JSONObject;)Ljava/lang/String;", "GetGetFromJSONObject_Lorg_json_JSONObject_Handler")]
			public override unsafe global::Java.Lang.Object GetFromJSONObject(global::Org.Json.JSONObject p0)
			{
				const string __id = "getFromJSONObject.(Lorg/json/JSONObject;)Ljava/lang/String;";
				try
				{
					JniArgumentValue* __args = stackalloc JniArgumentValue[1];
					__args[0] = new JniArgumentValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
					return JNIEnv.GetString(__rm.Handle, JniHandleOwnership.TransferLocalRef);
				}
				finally
				{
				}
			}

			static Delegate cb_getFromSharedPreferences_Landroid_content_SharedPreferences_;
#pragma warning disable 0169
			static Delegate GetGetFromSharedPreferences_Landroid_content_SharedPreferences_Handler()
			{
				if (cb_getFromSharedPreferences_Landroid_content_SharedPreferences_ == null)
					cb_getFromSharedPreferences_Landroid_content_SharedPreferences_ = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr, IntPtr>)n_GetFromSharedPreferences_Landroid_content_SharedPreferences_);
				return cb_getFromSharedPreferences_Landroid_content_SharedPreferences_;
			}

			static IntPtr n_GetFromSharedPreferences_Landroid_content_SharedPreferences_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::Android.Gms.Flags.Impl.DataUtils.StringUtils __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.Impl.DataUtils.StringUtils>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Content.ISharedPreferences p0 = (global::Android.Content.ISharedPreferences)global::Java.Lang.Object.GetObject<global::Android.Content.ISharedPreferences>(native_p0, JniHandleOwnership.DoNotTransfer);
				IntPtr __ret = JNIEnv.ToLocalJniHandle(__this.GetFromSharedPreferences(p0));
				return __ret;
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.flags.impl']/class[@name='DataUtils.StringUtils']/method[@name='getFromSharedPreferences' and count(parameter)=1 and parameter[1][@type='android.content.SharedPreferences']]"
			[Register("getFromSharedPreferences", "(Landroid/content/SharedPreferences;)Ljava/lang/String;", "GetGetFromSharedPreferences_Landroid_content_SharedPreferences_Handler")]
			public override unsafe Java.Lang.Object GetFromSharedPreferences(global::Android.Content.ISharedPreferences p0)
			{
				const string __id = "getFromSharedPreferences.(Landroid/content/SharedPreferences;)Ljava/lang/String;";
				try
				{
					JniArgumentValue* __args = stackalloc JniArgumentValue[1];
					__args[0] = new JniArgumentValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
					return JNIEnv.GetString(__rm.Handle, JniHandleOwnership.TransferLocalRef);
				}
				finally
				{
				}
			}



			static Delegate cb_putInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_;
#pragma warning disable 0169
			static Delegate GetPutInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_Handler()
			{
				if (cb_putInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_ == null)
					cb_putInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, IntPtr, IntPtr>)n_PutInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_);
				return cb_putInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_;
			}

			static void n_PutInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
			{
				global::Android.Gms.Flags.Impl.DataUtils.StringUtils __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Flags.Impl.DataUtils.StringUtils>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Content.ISharedPreferencesEditor p0 = (global::Android.Content.ISharedPreferencesEditor)global::Java.Lang.Object.GetObject<global::Android.Content.ISharedPreferencesEditor>(native_p0, JniHandleOwnership.DoNotTransfer);

				var p1 = Java.Lang.Object.GetObject<Java.Lang.Object>(native_p1, JniHandleOwnership.DoNotTransfer);

				//string p1 = JNIEnv.ToLocalJniHandle(native_p1, JniHandleOwnership.DoNotTransfer);
				__this.PutInSharedPreferences(p0, p1);
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.flags.impl']/class[@name='DataUtils.StringUtils']/method[@name='putInSharedPreferences' and count(parameter)=2 and parameter[1][@type='android.content.SharedPreferences.Editor'] and parameter[2][@type='java.lang.String']]"
			[Register("putInSharedPreferences", "(Landroid/content/SharedPreferences$Editor;Ljava/lang/String;)V", "GetPutInSharedPreferences_Landroid_content_SharedPreferences_Editor_Ljava_lang_String_Handler")]
			public override unsafe void PutInSharedPreferences(global::Android.Content.ISharedPreferencesEditor p0, global::Java.Lang.Object p1)
			{
				const string __id = "putInSharedPreferences.(Landroid/content/SharedPreferences$Editor;Ljava/lang/String;)V";
				IntPtr native_p1 = JNIEnv.ToLocalJniHandle(p1);
				try
				{
					JniArgumentValue* __args = stackalloc JniArgumentValue[2];
					__args[0] = new JniArgumentValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
					__args[1] = new JniArgumentValue(native_p1);
					_members.InstanceMethods.InvokeVirtualVoidMethod(__id, this, __args);
				}
				finally
				{
					JNIEnv.DeleteLocalRef(native_p1);
				}
			}
		}
	}
}