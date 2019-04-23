using System;
using Android.Runtime;

namespace Firebase.ML.Vision.Text
{

    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.firebase.ml.vision.text']/class[@name='FirebaseVisionText']"
    // [global::Android.Runtime.Register ("com/google/firebase/ml/vision/text/FirebaseVisionText", DoNotGenerateAcw=true)]
    public partial class FirebaseVisionText // : global::Java.Lang.Object
    {

        // Metadata.xml XPath class reference: path="/api/package[@name='com.google.firebase.ml.vision.text']/class[@name='FirebaseVisionText.Element']"
        // [global::Android.Runtime.Register("com/google/firebase/ml/vision/text/FirebaseVisionText$Element", DoNotGenerateAcw = true)]
        public partial class TextBlock // : global::Java.Lang.Object
        {
			static Delegate cb_FirebaseVisionText_TextBlock_getRecognizedLanguages;
#pragma warning disable 0169
			static Delegate GetGetRecognizedLanguagesHandler ()
			{
				if (cb_FirebaseVisionText_TextBlock_getRecognizedLanguages == null)
					cb_FirebaseVisionText_TextBlock_getRecognizedLanguages = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetRecognizedLanguages);
				return cb_FirebaseVisionText_TextBlock_getRecognizedLanguages;
			}

			static IntPtr n_GetRecognizedLanguages (IntPtr jnienv, IntPtr native__this)
			{
				global::Firebase.ML.Vision.Text.FirebaseVisionText.TextBlock __this = global::Java.Lang.Object.GetObject<global::Firebase.ML.Vision.Text.FirebaseVisionText.TextBlock> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				return global::Android.Runtime.JavaList.ToLocalJniHandle (__this.RecognizedLanguages);
			}
#pragma warning restore 0169

			public virtual unsafe global::System.Collections.IList RecognizedLanguages {
				// Metadata.xml XPath method reference: path="/api/package[@name='com.google.firebase.ml.vision.text']/class[@name='FirebaseVisionText.TextBlock']/method[@name='getRecognizedLanguages' and count(parameter)=0]"
				[Register ("getRecognizedLanguages", "()Ljava/util/List;", "GetGetRecognizedLanguagesHandler")]
				get {
					const string __id = "getRecognizedLanguages.()Ljava/util/List;";
					try {
						var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
						return global::Android.Runtime.JavaList.FromJniHandle (__rm.Handle, JniHandleOwnership.TransferLocalRef);
					} finally {
					}
				}
			}
        }
    }
}