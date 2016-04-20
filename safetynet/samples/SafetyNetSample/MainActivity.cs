using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Support.V4.App;
using Android.Gms.SafetyNet;
using System.Json;

namespace SafetyNetSample
{
    [Activity (Label = "SafetyNet Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        GoogleApiClient googleApiClient;
        Button buttonCheck;
        Button buttonVerify;
        ISafetyNetApiAttestationResult attestationResult;

        const string DEVELOPERS_CONSOLE_API_KEY = "YOUR-API-KEY";

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            buttonCheck = FindViewById<Button> (Resource.Id.buttonCheck);
            buttonVerify = FindViewById<Button> (Resource.Id.buttonVerify);
            buttonVerify.Enabled = false;

            googleApiClient = new GoogleApiClient.Builder (this)
                .EnableAutoManage (this, 1, result => {
                    Toast.MakeText (this, "Failed to connect to Google Play Services", ToastLength.Long).Show ();
                })
                .AddApi (SafetyNetClass.API)
                .Build ();

            buttonCheck.Click += async delegate {
                
                var nonce = Nonce.Generate (); // Should be at least 16 bytes in length.
                var r = await SafetyNetClass.SafetyNetApi.AttestAsync (googleApiClient, nonce);

                if (r.Status.IsSuccess) {

                    // Store for future verification with google servers
                    attestationResult = r;

                    // Get the JSON from the JWS result by decoding
                    var decodedResult = r.DecodeJwsResult (nonce);

                    string error = null;

                    // Try and verify the body of the response
                    if (VerifyAttestResponse (decodedResult, nonce, out error)) {
                        Toast.MakeText (this, "Compatibility Passed!", ToastLength.Long).Show ();
                        buttonVerify.Enabled = true;
                    } else {
                        Toast.MakeText (this, "Compatibility Failed: " + error, ToastLength.Long).Show ();
                    }
                } else {
                    // Error
                    Toast.MakeText (this, "Failed to Check Compatibility", ToastLength.Long).Show ();
                }
            };


            buttonVerify.Click += async delegate {
               
                // Validate the JWS response with Google to ensure it came from them
                var valid = await attestationResult.ValidateWithGoogle (DEVELOPERS_CONSOLE_API_KEY);

                if (valid)
                    Toast.MakeText (this, "Successfully validated response with Google!", ToastLength.Short).Show ();
                else
                    Toast.MakeText (this, "Failed response validation with Google!", ToastLength.Short).Show ();
            };
        }

        bool VerifyAttestResponse (string data, byte[] sentNonce, out string errorMessage)
        {            
            errorMessage = null;

            var json = JsonObject.Parse (data);

            var error = GetValue (json, "error");
            var nonce = GetValue (json, "nonce");
            var timestampMs = GetValue (json, "timestampMs");
            var apkPackageName = GetValue (json, "apkPackageName");
            var apkCertDigestSha256 = GetValue (json, "apkCertificateDigestSha256");
            var apkDigestSha256 = GetValue (json, "apkDigestSha256");
            var ctsProfileMatch = GetValue (json, "ctsProfileMatch") == "true";

            // See if there was any error in the response itself
            if (!string.IsNullOrEmpty (error)) {  
                errorMessage = "Response Contained an Error: " + error;
                return false;
            }

            // Nonce comes back base64 encoded
            var sentNonceStr = Convert.ToBase64String (sentNonce);
            if (!nonce.Equals (sentNonceStr)) {
                errorMessage = "Nonce's do no match";
                return false;
            }

            // Package names should match
            if (PackageName != apkPackageName) {
                errorMessage = "Package Names do not match";
                return false;
            }

            // CTS Profile Match should be true - This will be false on rooted devices!
            if (!ctsProfileMatch) {
                errorMessage = "CTS Profile was false";
                return false;
            }

            return true;
        }

        string GetValue (JsonValue json, string field)
        {
            if (!json.ContainsKey (field))
                return string.Empty;
            
            return json [field].ToString ().Trim ('"');
        }
    }
}


