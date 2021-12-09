using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Xamarin.Google.Android.DataTransport.Runtime.Dagger.Internal 
{

	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.datatransport.runtime.dagger.internal']/class[@name='ProviderOfLazy']"
	// [global::Android.Runtime.Register ("com/google/android/datatransport/runtime/dagger/internal/ProviderOfLazy", DoNotGenerateAcw=true)]
	// [global::Java.Interop.JavaTypeParameters (new string [] {"T"})]
	public sealed partial class ProviderOfLazy //: global::Java.Lang.Object, global::JavaX.Inject.IProvider 
    {

 		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.datatransport.runtime.dagger.internal']/class[@name='ProviderOfLazy']/method[@name='get' and count(parameter)=0]"
		[Register ("get", "()Lcom/google/android/datatransport/runtime/dagger/Lazy;", "")]
		public unsafe global::Java.Lang.Object Get ()
		{
			const string __id = "get.()Lcom/google/android/datatransport/runtime/dagger/Lazy;";
			try {
				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod (__id, this, null);
				return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Xamarin.Google.Android.DataTransport.Runtime.Dagger.ILazy> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}
   }
}