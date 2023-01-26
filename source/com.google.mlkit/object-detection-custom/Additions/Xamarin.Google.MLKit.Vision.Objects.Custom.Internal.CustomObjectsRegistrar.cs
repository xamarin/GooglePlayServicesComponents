﻿using System;
using Android.Views;
using Android.Widget;
using Android.Graphics;


using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Xamarin.Google.MLKit.Vision.Objects.Custom.Internal 
{
	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.mlkit.vision.objects.custom.internal']/class[@name='CustomObjectsRegistrar']"
	//[global::Android.Runtime.Register ("com/google/mlkit/vision/objects/custom/internal/CustomObjectsRegistrar", DoNotGenerateAcw=true)]
	public partial class CustomObjectsRegistrar : global::Java.Lang.Object, global::Firebase.Components.IComponentRegistrar 
    {
		public unsafe global::System.Collections.Generic.IList <global:: Firebase.Components.Component > Components 
        {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.mlkit.vision.objects.custom.internal']/class[@name='CustomObjectsRegistrar']/method[@name='getComponents' and count(parameter)=0]"
			[Register ("getComponents", "()Ljava/util/List;", "")]
			get {
				const string __id = "getComponents.()Ljava/util/List;";
				try {
					var __rm = _members.InstanceMethods.InvokeNonvirtualObjectMethod (__id, this, null);
					return (System.Collections.Generic.IList<Firebase.Components.Component>) global::Android.Runtime.JavaList.FromJniHandle (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}
    }
}