using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

delegate IntPtr _JniMarshal_PPIL_L (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1);
delegate IntPtr _JniMarshal_PPZ_L (IntPtr jnienv, IntPtr klass, bool p0);

namespace Android.Gms.Location {

	[Register ("com/google/android/gms/location/FusedLocationProviderClient", DoNotGenerateAcw=true)]
	public partial class FusedLocationProviderClient : Java.Lang.Object {
		internal FusedLocationProviderClient ()
		{
		}

		// Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/field[@name='KEY_MOCK_LOCATION']"
		[Register ("KEY_MOCK_LOCATION")]
		[global::System.Obsolete (@"deprecated")]
		public const string KeyMockLocation = (string) "mockLocation";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/field[@name='KEY_VERTICAL_ACCURACY']"
		[Register ("KEY_VERTICAL_ACCURACY")]
		[global::System.Obsolete (@"deprecated")]
		public const string KeyVerticalAccuracy = (string) "verticalAccuracy";

		// The following are fields from: com.google.android.gms.common.api.HasApiKey

		// The following are fields from: Android.Runtime.IJavaObject

		// The following are fields from: System.IDisposable

		// The following are fields from: Java.Interop.IJavaPeerable

	}

	[Register ("com/google/android/gms/location/FusedLocationProviderClient", DoNotGenerateAcw=true)]
	[global::System.Obsolete (@"Use the 'FusedLocationProviderClient' type. This type will be removed in a future release.", error: true)]
	public abstract class FusedLocationProviderClientConsts : FusedLocationProviderClient {
		private FusedLocationProviderClientConsts ()
		{
		}

	}

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']"
	[Register ("com/google/android/gms/location/FusedLocationProviderClient", "", "Android.Gms.Location.IFusedLocationProviderClientInvoker")]
	public partial interface IFusedLocationProviderClient : global::Android.Gms.Common.Apis.IHasApiKey {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='flushLocations' and count(parameter)=0]"
		[Register ("flushLocations", "()Lcom/google/android/gms/tasks/Task;", "GetFlushLocationsHandler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task FlushLocations ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='getCurrentLocation' and count(parameter)=2 and parameter[1][@type='com.google.android.gms.location.CurrentLocationRequest'] and parameter[2][@type='com.google.android.gms.tasks.CancellationToken']]"
		[Register ("getCurrentLocation", "(Lcom/google/android/gms/location/CurrentLocationRequest;Lcom/google/android/gms/tasks/CancellationToken;)Lcom/google/android/gms/tasks/Task;", "GetGetCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task GetCurrentLocation (global::Android.Gms.Location.CurrentLocationRequest p0, global::Android.Gms.Tasks.CancellationToken p1);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='getCurrentLocation' and count(parameter)=2 and parameter[1][@type='int'] and parameter[2][@type='com.google.android.gms.tasks.CancellationToken']]"
		[Register ("getCurrentLocation", "(ILcom/google/android/gms/tasks/CancellationToken;)Lcom/google/android/gms/tasks/Task;", "GetGetCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task GetCurrentLocation (int p0, global::Android.Gms.Tasks.CancellationToken p1);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='getLastLocation' and count(parameter)=0]"
		[Register ("getLastLocation", "()Lcom/google/android/gms/tasks/Task;", "GetGetLastLocationHandler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task GetLastLocation ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='getLastLocation' and count(parameter)=1 and parameter[1][@type='com.google.android.gms.location.LastLocationRequest']]"
		[Register ("getLastLocation", "(Lcom/google/android/gms/location/LastLocationRequest;)Lcom/google/android/gms/tasks/Task;", "GetGetLastLocation_Lcom_google_android_gms_location_LastLocationRequest_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task GetLastLocation (global::Android.Gms.Location.LastLocationRequest p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='getLocationAvailability' and count(parameter)=0]"
		[Register ("getLocationAvailability", "()Lcom/google/android/gms/tasks/Task;", "GetGetLocationAvailabilityHandler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task GetLocationAvailability ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='removeLocationUpdates' and count(parameter)=1 and parameter[1][@type='android.app.PendingIntent']]"
		[Register ("removeLocationUpdates", "(Landroid/app/PendingIntent;)Lcom/google/android/gms/tasks/Task;", "GetRemoveLocationUpdates_Landroid_app_PendingIntent_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RemoveLocationUpdates (global::Android.App.PendingIntent callbackIntent);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='removeLocationUpdates' and count(parameter)=1 and parameter[1][@type='com.google.android.gms.location.LocationCallback']]"
		[Register ("removeLocationUpdates", "(Lcom/google/android/gms/location/LocationCallback;)Lcom/google/android/gms/tasks/Task;", "GetRemoveLocationUpdates_Lcom_google_android_gms_location_LocationCallback_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RemoveLocationUpdates (global::Android.Gms.Location.LocationCallbackBase @callback);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='removeLocationUpdates' and count(parameter)=1 and parameter[1][@type='com.google.android.gms.location.LocationListener']]"
		[Register ("removeLocationUpdates", "(Lcom/google/android/gms/location/LocationListener;)Lcom/google/android/gms/tasks/Task;", "GetRemoveLocationUpdates_Lcom_google_android_gms_location_LocationListener_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RemoveLocationUpdates (global::Android.Gms.Location.ILocationListener p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='requestLocationUpdates' and count(parameter)=2 and parameter[1][@type='com.google.android.gms.location.LocationRequest'] and parameter[2][@type='android.app.PendingIntent']]"
		[Register ("requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Landroid/app/PendingIntent;)Lcom/google/android/gms/tasks/Task;", "GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest request, global::Android.App.PendingIntent callbackIntent);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='requestLocationUpdates' and count(parameter)=3 and parameter[1][@type='com.google.android.gms.location.LocationRequest'] and parameter[2][@type='com.google.android.gms.location.LocationCallback'] and parameter[3][@type='android.os.Looper']]"
		[Register ("requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Lcom/google/android/gms/location/LocationCallback;Landroid/os/Looper;)Lcom/google/android/gms/tasks/Task;", "GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest request, global::Android.Gms.Location.LocationCallbackBase @callback, global::Android.OS.Looper looper);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='requestLocationUpdates' and count(parameter)=3 and parameter[1][@type='com.google.android.gms.location.LocationRequest'] and parameter[2][@type='com.google.android.gms.location.LocationListener'] and parameter[3][@type='android.os.Looper']]"
		[Register ("requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Lcom/google/android/gms/location/LocationListener;Landroid/os/Looper;)Lcom/google/android/gms/tasks/Task;", "GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest p0, global::Android.Gms.Location.ILocationListener p1, global::Android.OS.Looper p2);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='requestLocationUpdates' and count(parameter)=3 and parameter[1][@type='com.google.android.gms.location.LocationRequest'] and parameter[2][@type='java.util.concurrent.Executor'] and parameter[3][@type='com.google.android.gms.location.LocationCallback']]"
		[Register ("requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Ljava/util/concurrent/Executor;Lcom/google/android/gms/location/LocationCallback;)Lcom/google/android/gms/tasks/Task;", "GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest p0, global::Java.Util.Concurrent.IExecutor p1, global::Android.Gms.Location.LocationCallbackBase p2);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='requestLocationUpdates' and count(parameter)=3 and parameter[1][@type='com.google.android.gms.location.LocationRequest'] and parameter[2][@type='java.util.concurrent.Executor'] and parameter[3][@type='com.google.android.gms.location.LocationListener']]"
		[Register ("requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Ljava/util/concurrent/Executor;Lcom/google/android/gms/location/LocationListener;)Lcom/google/android/gms/tasks/Task;", "GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest p0, global::Java.Util.Concurrent.IExecutor p1, global::Android.Gms.Location.ILocationListener p2);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='setMockLocation' and count(parameter)=1 and parameter[1][@type='android.location.Location']]"
		[Register ("setMockLocation", "(Landroid/location/Location;)Lcom/google/android/gms/tasks/Task;", "GetSetMockLocation_Landroid_location_Location_Handler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task SetMockLocation (global::Android.Locations.Location mockLocation);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.location']/interface[@name='FusedLocationProviderClient']/method[@name='setMockMode' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("setMockMode", "(Z)Lcom/google/android/gms/tasks/Task;", "GetSetMockMode_ZHandler:Android.Gms.Location.IFusedLocationProviderClientInvoker, Xamarin.GooglePlayServices.Location")]
		global::Android.Gms.Tasks.Task SetMockMode (bool isMockMode);

	}

	[global::Android.Runtime.Register ("com/google/android/gms/location/FusedLocationProviderClient", DoNotGenerateAcw=true)]
	internal partial class IFusedLocationProviderClientInvoker : global::Java.Lang.Object, IFusedLocationProviderClient {
		static readonly JniPeerMembers _members = new XAPeerMembers ("com/google/android/gms/location/FusedLocationProviderClient", typeof (IFusedLocationProviderClientInvoker));

		static IntPtr java_class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		IntPtr class_ref;

		public static IFusedLocationProviderClient GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IFusedLocationProviderClient> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException ($"Unable to convert instance of type '{JNIEnv.GetClassNameFromInstance (handle)}' to type 'com.google.android.gms.location.FusedLocationProviderClient'.");
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IFusedLocationProviderClientInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_flushLocations;
#pragma warning disable 0169
		static Delegate GetFlushLocationsHandler ()
		{
			if (cb_flushLocations == null)
				cb_flushLocations = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_L) n_FlushLocations);
			return cb_flushLocations;
		}

		static IntPtr n_FlushLocations (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.FlushLocations ());
		}
#pragma warning restore 0169

		IntPtr id_flushLocations;
		public unsafe global::Android.Gms.Tasks.Task FlushLocations ()
		{
			if (id_flushLocations == IntPtr.Zero)
				id_flushLocations = JNIEnv.GetMethodID (class_ref, "flushLocations", "()Lcom/google/android/gms/tasks/Task;");
			return global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_flushLocations), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_;
#pragma warning disable 0169
		static Delegate GetGetCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_Handler ()
		{
			if (cb_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_ == null)
				cb_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPLL_L) n_GetCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_);
			return cb_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_;
		}

		static IntPtr n_GetCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.CurrentLocationRequest> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.CancellationToken> (native_p1, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetCurrentLocation (p0, p1));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_;
		public unsafe global::Android.Gms.Tasks.Task GetCurrentLocation (global::Android.Gms.Location.CurrentLocationRequest p0, global::Android.Gms.Tasks.CancellationToken p1)
		{
			if (id_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_ == IntPtr.Zero)
				id_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_ = JNIEnv.GetMethodID (class_ref, "getCurrentLocation", "(Lcom/google/android/gms/location/CurrentLocationRequest;Lcom/google/android/gms/tasks/CancellationToken;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getCurrentLocation_Lcom_google_android_gms_location_CurrentLocationRequest_Lcom_google_android_gms_tasks_CancellationToken_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_;
#pragma warning disable 0169
		static Delegate GetGetCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_Handler ()
		{
			if (cb_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_ == null)
				cb_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPIL_L) n_GetCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_);
			return cb_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_;
		}

		static IntPtr n_GetCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_ (IntPtr jnienv, IntPtr native__this, int p0, IntPtr native_p1)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.CancellationToken> (native_p1, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetCurrentLocation (p0, p1));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_;
		public unsafe global::Android.Gms.Tasks.Task GetCurrentLocation (int p0, global::Android.Gms.Tasks.CancellationToken p1)
		{
			if (id_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_ == IntPtr.Zero)
				id_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_ = JNIEnv.GetMethodID (class_ref, "getCurrentLocation", "(ILcom/google/android/gms/tasks/CancellationToken;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue (p0);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getCurrentLocation_ILcom_google_android_gms_tasks_CancellationToken_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_getLastLocation;
#pragma warning disable 0169
		static Delegate GetGetLastLocationHandler ()
		{
			if (cb_getLastLocation == null)
				cb_getLastLocation = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_L) n_GetLastLocation);
			return cb_getLastLocation;
		}

		static IntPtr n_GetLastLocation (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GetLastLocation ());
		}
#pragma warning restore 0169

		IntPtr id_getLastLocation;
		public unsafe global::Android.Gms.Tasks.Task GetLastLocation ()
		{
			if (id_getLastLocation == IntPtr.Zero)
				id_getLastLocation = JNIEnv.GetMethodID (class_ref, "getLastLocation", "()Lcom/google/android/gms/tasks/Task;");
			return global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getLastLocation), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_;
#pragma warning disable 0169
		static Delegate GetGetLastLocation_Lcom_google_android_gms_location_LastLocationRequest_Handler ()
		{
			if (cb_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_ == null)
				cb_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_L) n_GetLastLocation_Lcom_google_android_gms_location_LastLocationRequest_);
			return cb_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_;
		}

		static IntPtr n_GetLastLocation_Lcom_google_android_gms_location_LastLocationRequest_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LastLocationRequest> (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetLastLocation (p0));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_;
		public unsafe global::Android.Gms.Tasks.Task GetLastLocation (global::Android.Gms.Location.LastLocationRequest p0)
		{
			if (id_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_ == IntPtr.Zero)
				id_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_ = JNIEnv.GetMethodID (class_ref, "getLastLocation", "(Lcom/google/android/gms/location/LastLocationRequest;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getLastLocation_Lcom_google_android_gms_location_LastLocationRequest_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_getLocationAvailability;
#pragma warning disable 0169
		static Delegate GetGetLocationAvailabilityHandler ()
		{
			if (cb_getLocationAvailability == null)
				cb_getLocationAvailability = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_L) n_GetLocationAvailability);
			return cb_getLocationAvailability;
		}

		static IntPtr n_GetLocationAvailability (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GetLocationAvailability ());
		}
#pragma warning restore 0169

		IntPtr id_getLocationAvailability;
		public unsafe global::Android.Gms.Tasks.Task GetLocationAvailability ()
		{
			if (id_getLocationAvailability == IntPtr.Zero)
				id_getLocationAvailability = JNIEnv.GetMethodID (class_ref, "getLocationAvailability", "()Lcom/google/android/gms/tasks/Task;");
			return global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getLocationAvailability), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_removeLocationUpdates_Landroid_app_PendingIntent_;
#pragma warning disable 0169
		static Delegate GetRemoveLocationUpdates_Landroid_app_PendingIntent_Handler ()
		{
			if (cb_removeLocationUpdates_Landroid_app_PendingIntent_ == null)
				cb_removeLocationUpdates_Landroid_app_PendingIntent_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_L) n_RemoveLocationUpdates_Landroid_app_PendingIntent_);
			return cb_removeLocationUpdates_Landroid_app_PendingIntent_;
		}

		static IntPtr n_RemoveLocationUpdates_Landroid_app_PendingIntent_ (IntPtr jnienv, IntPtr native__this, IntPtr native_callbackIntent)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var callbackIntent = global::Java.Lang.Object.GetObject<global::Android.App.PendingIntent> (native_callbackIntent, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RemoveLocationUpdates (callbackIntent));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_removeLocationUpdates_Landroid_app_PendingIntent_;
		public unsafe global::Android.Gms.Tasks.Task RemoveLocationUpdates (global::Android.App.PendingIntent callbackIntent)
		{
			if (id_removeLocationUpdates_Landroid_app_PendingIntent_ == IntPtr.Zero)
				id_removeLocationUpdates_Landroid_app_PendingIntent_ = JNIEnv.GetMethodID (class_ref, "removeLocationUpdates", "(Landroid/app/PendingIntent;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((callbackIntent == null) ? IntPtr.Zero : ((global::Java.Lang.Object) callbackIntent).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_removeLocationUpdates_Landroid_app_PendingIntent_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_;
#pragma warning disable 0169
		static Delegate GetRemoveLocationUpdates_Lcom_google_android_gms_location_LocationCallback_Handler ()
		{
			if (cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_ == null)
				cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_L) n_RemoveLocationUpdates_Lcom_google_android_gms_location_LocationCallback_);
			return cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_;
		}

		static IntPtr n_RemoveLocationUpdates_Lcom_google_android_gms_location_LocationCallback_ (IntPtr jnienv, IntPtr native__this, IntPtr native__callback)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var @callback = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationCallbackBase> (native__callback, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RemoveLocationUpdates (@callback));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_;
		public unsafe global::Android.Gms.Tasks.Task RemoveLocationUpdates (global::Android.Gms.Location.LocationCallbackBase @callback)
		{
			if (id_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_ == IntPtr.Zero)
				id_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_ = JNIEnv.GetMethodID (class_ref, "removeLocationUpdates", "(Lcom/google/android/gms/location/LocationCallback;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((@callback == null) ? IntPtr.Zero : ((global::Java.Lang.Object) @callback).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_removeLocationUpdates_Lcom_google_android_gms_location_LocationCallback_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_;
#pragma warning disable 0169
		static Delegate GetRemoveLocationUpdates_Lcom_google_android_gms_location_LocationListener_Handler ()
		{
			if (cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_ == null)
				cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_L) n_RemoveLocationUpdates_Lcom_google_android_gms_location_LocationListener_);
			return cb_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_;
		}

		static IntPtr n_RemoveLocationUpdates_Lcom_google_android_gms_location_LocationListener_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = (global::Android.Gms.Location.ILocationListener)global::Java.Lang.Object.GetObject<global::Android.Gms.Location.ILocationListener> (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RemoveLocationUpdates (p0));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_;
		public unsafe global::Android.Gms.Tasks.Task RemoveLocationUpdates (global::Android.Gms.Location.ILocationListener p0)
		{
			if (id_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_ == IntPtr.Zero)
				id_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_ = JNIEnv.GetMethodID (class_ref, "removeLocationUpdates", "(Lcom/google/android/gms/location/LocationListener;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_removeLocationUpdates_Lcom_google_android_gms_location_LocationListener_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_;
#pragma warning disable 0169
		static Delegate GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_Handler ()
		{
			if (cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_ == null)
				cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPLL_L) n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_);
			return cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_;
		}

		static IntPtr n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_ (IntPtr jnienv, IntPtr native__this, IntPtr native_request, IntPtr native_callbackIntent)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var request = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationRequest> (native_request, JniHandleOwnership.DoNotTransfer);
			var callbackIntent = global::Java.Lang.Object.GetObject<global::Android.App.PendingIntent> (native_callbackIntent, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RequestLocationUpdates (request, callbackIntent));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_;
		public unsafe global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest request, global::Android.App.PendingIntent callbackIntent)
		{
			if (id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_ == IntPtr.Zero)
				id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_ = JNIEnv.GetMethodID (class_ref, "requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Landroid/app/PendingIntent;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue ((request == null) ? IntPtr.Zero : ((global::Java.Lang.Object) request).Handle);
			__args [1] = new JValue ((callbackIntent == null) ? IntPtr.Zero : ((global::Java.Lang.Object) callbackIntent).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Landroid_app_PendingIntent_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_;
#pragma warning disable 0169
		static Delegate GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_Handler ()
		{
			if (cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_ == null)
				cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPLLL_L) n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_);
			return cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_;
		}

		static IntPtr n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_ (IntPtr jnienv, IntPtr native__this, IntPtr native_request, IntPtr native__callback, IntPtr native_looper)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var request = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationRequest> (native_request, JniHandleOwnership.DoNotTransfer);
			var @callback = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationCallbackBase> (native__callback, JniHandleOwnership.DoNotTransfer);
			var looper = global::Java.Lang.Object.GetObject<global::Android.OS.Looper> (native_looper, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RequestLocationUpdates (request, @callback, looper));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_;
		public unsafe global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest request, global::Android.Gms.Location.LocationCallbackBase @callback, global::Android.OS.Looper looper)
		{
			if (id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_ == IntPtr.Zero)
				id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_ = JNIEnv.GetMethodID (class_ref, "requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Lcom/google/android/gms/location/LocationCallback;Landroid/os/Looper;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue ((request == null) ? IntPtr.Zero : ((global::Java.Lang.Object) request).Handle);
			__args [1] = new JValue ((@callback == null) ? IntPtr.Zero : ((global::Java.Lang.Object) @callback).Handle);
			__args [2] = new JValue ((looper == null) ? IntPtr.Zero : ((global::Java.Lang.Object) looper).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationCallback_Landroid_os_Looper_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_;
#pragma warning disable 0169
		static Delegate GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_Handler ()
		{
			if (cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_ == null)
				cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPLLL_L) n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_);
			return cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_;
		}

		static IntPtr n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationRequest> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = (global::Android.Gms.Location.ILocationListener)global::Java.Lang.Object.GetObject<global::Android.Gms.Location.ILocationListener> (native_p1, JniHandleOwnership.DoNotTransfer);
			var p2 = global::Java.Lang.Object.GetObject<global::Android.OS.Looper> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RequestLocationUpdates (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_;
		public unsafe global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest p0, global::Android.Gms.Location.ILocationListener p1, global::Android.OS.Looper p2)
		{
			if (id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_ == IntPtr.Zero)
				id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_ = JNIEnv.GetMethodID (class_ref, "requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Lcom/google/android/gms/location/LocationListener;Landroid/os/Looper;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			__args [2] = new JValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Lcom_google_android_gms_location_LocationListener_Landroid_os_Looper_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_;
#pragma warning disable 0169
		static Delegate GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_Handler ()
		{
			if (cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_ == null)
				cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPLLL_L) n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_);
			return cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_;
		}

		static IntPtr n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationRequest> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = (global::Java.Util.Concurrent.IExecutor)global::Java.Lang.Object.GetObject<global::Java.Util.Concurrent.IExecutor> (native_p1, JniHandleOwnership.DoNotTransfer);
			var p2 = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationCallbackBase> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RequestLocationUpdates (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_;
		public unsafe global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest p0, global::Java.Util.Concurrent.IExecutor p1, global::Android.Gms.Location.LocationCallbackBase p2)
		{
			if (id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_ == IntPtr.Zero)
				id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_ = JNIEnv.GetMethodID (class_ref, "requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Ljava/util/concurrent/Executor;Lcom/google/android/gms/location/LocationCallback;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			__args [2] = new JValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationCallback_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_;
#pragma warning disable 0169
		static Delegate GetRequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_Handler ()
		{
			if (cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_ == null)
				cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPLLL_L) n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_);
			return cb_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_;
		}

		static IntPtr n_RequestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.LocationRequest> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = (global::Java.Util.Concurrent.IExecutor)global::Java.Lang.Object.GetObject<global::Java.Util.Concurrent.IExecutor> (native_p1, JniHandleOwnership.DoNotTransfer);
			var p2 = (global::Android.Gms.Location.ILocationListener)global::Java.Lang.Object.GetObject<global::Android.Gms.Location.ILocationListener> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.RequestLocationUpdates (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_;
		public unsafe global::Android.Gms.Tasks.Task RequestLocationUpdates (global::Android.Gms.Location.LocationRequest p0, global::Java.Util.Concurrent.IExecutor p1, global::Android.Gms.Location.ILocationListener p2)
		{
			if (id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_ == IntPtr.Zero)
				id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_ = JNIEnv.GetMethodID (class_ref, "requestLocationUpdates", "(Lcom/google/android/gms/location/LocationRequest;Ljava/util/concurrent/Executor;Lcom/google/android/gms/location/LocationListener;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			__args [2] = new JValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_requestLocationUpdates_Lcom_google_android_gms_location_LocationRequest_Ljava_util_concurrent_Executor_Lcom_google_android_gms_location_LocationListener_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_setMockLocation_Landroid_location_Location_;
#pragma warning disable 0169
		static Delegate GetSetMockLocation_Landroid_location_Location_Handler ()
		{
			if (cb_setMockLocation_Landroid_location_Location_ == null)
				cb_setMockLocation_Landroid_location_Location_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_L) n_SetMockLocation_Landroid_location_Location_);
			return cb_setMockLocation_Landroid_location_Location_;
		}

		static IntPtr n_SetMockLocation_Landroid_location_Location_ (IntPtr jnienv, IntPtr native__this, IntPtr native_mockLocation)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var mockLocation = global::Java.Lang.Object.GetObject<global::Android.Locations.Location> (native_mockLocation, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.SetMockLocation (mockLocation));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_setMockLocation_Landroid_location_Location_;
		public unsafe global::Android.Gms.Tasks.Task SetMockLocation (global::Android.Locations.Location mockLocation)
		{
			if (id_setMockLocation_Landroid_location_Location_ == IntPtr.Zero)
				id_setMockLocation_Landroid_location_Location_ = JNIEnv.GetMethodID (class_ref, "setMockLocation", "(Landroid/location/Location;)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((mockLocation == null) ? IntPtr.Zero : ((global::Java.Lang.Object) mockLocation).Handle);
			var __ret = global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_setMockLocation_Landroid_location_Location_, __args), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_setMockMode_Z;
#pragma warning disable 0169
		static Delegate GetSetMockMode_ZHandler ()
		{
			if (cb_setMockMode_Z == null)
				cb_setMockMode_Z = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPZ_L) n_SetMockMode_Z);
			return cb_setMockMode_Z;
		}

		static IntPtr n_SetMockMode_Z (IntPtr jnienv, IntPtr native__this, bool isMockMode)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.SetMockMode (isMockMode));
		}
#pragma warning restore 0169

		IntPtr id_setMockMode_Z;
		public unsafe global::Android.Gms.Tasks.Task SetMockMode (bool isMockMode)
		{
			if (id_setMockMode_Z == IntPtr.Zero)
				id_setMockMode_Z = JNIEnv.GetMethodID (class_ref, "setMockMode", "(Z)Lcom/google/android/gms/tasks/Task;");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (isMockMode);
			return global::Java.Lang.Object.GetObject<global::Android.Gms.Tasks.Task> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_setMockMode_Z, __args), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_getApiKey;
#pragma warning disable 0169
		static Delegate GetGetApiKeyHandler ()
		{
			if (cb_getApiKey == null)
				cb_getApiKey = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_L) n_GetApiKey);
			return cb_getApiKey;
		}

		static IntPtr n_GetApiKey (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Android.Gms.Location.IFusedLocationProviderClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.ApiKey);
		}
#pragma warning restore 0169

		IntPtr id_getApiKey;
		public unsafe global::Android.Gms.Common.Api.Internal.ApiKey ApiKey {
			get {
				if (id_getApiKey == IntPtr.Zero)
					id_getApiKey = JNIEnv.GetMethodID (class_ref, "getApiKey", "()Lcom/google/android/gms/common/api/internal/ApiKey;");
				return global::Java.Lang.Object.GetObject<global::Android.Gms.Common.Api.Internal.ApiKey> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getApiKey), JniHandleOwnership.TransferLocalRef);
			}
		}

	}
}
