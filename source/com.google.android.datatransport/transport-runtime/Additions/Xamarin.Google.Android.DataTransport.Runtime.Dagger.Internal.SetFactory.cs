using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Xamarin.Google.Android.DataTransport.Runtime.Dagger.Internal 
{
	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.datatransport.runtime.dagger.internal']/class[@name='SetFactory']"
	// [global::Android.Runtime.Register ("com/google/android/datatransport/runtime/dagger/internal/SetFactory", DoNotGenerateAcw=true)]
	// [global::Java.Interop.JavaTypeParameters (new string [] {"T"})]
	public sealed partial class SetFactory //: global::Java.Lang.Object, global::Xamarin.Google.Android.DataTransport.Runtime.Dagger.Internal.IFactory 
    {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.datatransport.runtime.dagger.internal']/class[@name='SetFactory']/method[@name='get' and count(parameter)=0]"
		[Register ("get", "()Ljava/util/Set;", "")]
		public unsafe global::Java.Lang.Object Get ()
		{
			const string __id = "get.()Ljava/util/Set;";
			try {
				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod (__id, this, null);
				return (Java.Lang.Object) global::Android.Runtime.JavaSet.FromJniHandle (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}
    }
}