using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace Xamarin.GooglePlayServices.Tasks
{
    public class ProcessGoogleServicesJson : Task
    {
        const string RESFILE_VALUES = "goog_svcs_json.xml";
        const string RESFILE_XML = "global_tracker.xml";

        public string IntermediateOutputPrefix { get; set; }

        [Output]
        public ITaskItem[] GoogleServicesGeneratedResources { get; set; }

        [Required]
        public string AndroidPackageName { get; set; }

        [Required]
        public ITaskItem[] GoogleServicesJsons { get; set; }

        [Required]
        public string IntermediateOutputPath { get; set; }

        [Required]
        public string MonoAndroidResDirIntermediate { get;set; }

        public override bool Execute ()
        {
            Log.LogMessage ("Started ProcessGoogleServicesJson...");

            Log.LogMessage ("Android Package Name: {0}", AndroidPackageName);

            // Paths to write resource files to
            var xmlPath = Path.Combine (MonoAndroidResDirIntermediate, "xml", RESFILE_XML);
            var valuesPath = Path.Combine (MonoAndroidResDirIntermediate, "values", RESFILE_VALUES);

            var wroteXmlPath = false;
            var wroteValuesPath = false;

            if (GoogleServicesJsons == null || !GoogleServicesJsons.Any ()) {
                Log.LogMessage ("No GoogleServicesJson Build Action items specified, skipping task.");
                DeleteFiles (valuesPath, xmlPath);
                return true;
            }

            if (GoogleServicesJsons.Count () > 1)
                Log.LogMessage ("More than one GoogleServicesJson file specified for build configuration, using first one found.");

            var gsItem = GoogleServicesJsons.First ();

            var gsPath = CleanPath (gsItem.ItemSpec);

            GoogleServices googleServices;

            try {
                using (var sr = File.OpenRead (gsPath)) {
                    googleServices = GoogleServicesJsonProcessor.ProcessJson (AndroidPackageName, sr);
                }
                if (googleServices == null)
                    throw new NullReferenceException ();
            } catch (Exception ex) {
                Log.LogError ("Failed to Read or Deserialize GoogleServicesJson file: {0}{1}{2}", gsPath, Environment.NewLine, ex);
                DeleteFiles (valuesPath, xmlPath);
                return false;
            }

            if (string.IsNullOrEmpty (AndroidPackageName)) {
                Log.LogError ("Android Package Name not specified for project");
                return false;
            }

            var resolvedClientInfo = googleServices.GetClient (AndroidPackageName);
            if (resolvedClientInfo == null) {
                Log.LogWarning ("Failed to find client_info in google-services.json matching package name: {0}", AndroidPackageName);
            }

            var valuesItems = new Dictionary <string, string> {
                { "ga_trackingId", googleServices.GetGATrackingId (AndroidPackageName) },
                { "gcm_defaultSenderId", googleServices.GetDefaultGcmSenderId () },
                { "google_app_id", googleServices.GetGoogleAppId (AndroidPackageName) },
                { "test_banner_ad_unit_id", googleServices.GetTestBannerAdUnitId (AndroidPackageName) },
                { "test_interstitial_ad_unit_id", googleServices.GetTestInterstitialAdUnitId (AndroidPackageName) },
                { "default_web_client_id", googleServices.GetDefaultWebClientId (AndroidPackageName) },
                { "firebase_database_url", googleServices.GetFirebaseDatabaseUrl () },
                { "google_api_key", googleServices.GetGoogleApiKey (AndroidPackageName) },
                { "google_crash_reporting_api_key", googleServices.GetCrashReportingApiKey (AndroidPackageName) },
            };

            // We only want to create the file if not all of these values are missing
            if (valuesItems.Any (kvp => !string.IsNullOrEmpty (kvp.Value))) {
                Log.LogMessage ("Writing Resource File: {0}", valuesPath);
                WriteResourceDoc (valuesPath, valuesItems);
                wroteValuesPath = true;
                Log.LogMessage ("Wrote Resource File: {0}", valuesPath);
            } else {
                if (File.Exists (valuesPath)) {
                    Log.LogMessage ("Deleting Resource File: {0}", valuesPath);
                    try {
                        File.Delete (valuesPath);
                    } catch (Exception ex) {
                        Log.LogWarning ("Failed to delete Resource File: {0}{1}{2}", valuesPath, Environment.NewLine, ex);
                    }
                }
            }

            var xmlItems = new Dictionary <string, string> {
                { "ga_trackingId", googleServices.GetGATrackingId (AndroidPackageName) }
            };

            // We only want to create the file if not all of these values are missing
            if (xmlItems.Any (kvp => !string.IsNullOrEmpty (kvp.Value))) {
                Log.LogMessage ("Writing Resource File: {0}", xmlPath);
                WriteResourceDoc (xmlPath, xmlItems);
                wroteXmlPath = true;
                Log.LogMessage ("Wrote Resource File: {0}", xmlPath);
            } else {
                // If no 
                if (File.Exists (xmlPath)) {
                    Log.LogMessage ("Deleting Resource File: {0}", xmlPath);
                    try {
                        File.Delete (xmlPath);
                    } catch (Exception ex) {
                        Log.LogWarning ("Failed to delete Resource File: {0}{1}{2}", xmlPath, Environment.NewLine, ex);
                    }
                }
            }

            var outputFiles = new List<ITaskItem> ();
            if (wroteXmlPath)
                outputFiles.Add (new TaskItem (xmlPath));
            if (wroteValuesPath)
                outputFiles.Add (new TaskItem (valuesPath));

            if (outputFiles.Any ())
                GoogleServicesGeneratedResources = outputFiles.ToArray ();
            
            Log.LogMessage ("Finished ProcessGoogleServicesJson...");
            return true;
        }

        void WriteResourceDoc (string path, Dictionary<string, string> resourceValues)
        {
            var pathInfo = new FileInfo (path);

            if (!pathInfo.Directory.Exists)
                pathInfo.Directory.Create ();

            var xws = new XmlWriterSettings {
                Indent = true
            };
            using (var sw = File.Create (path)) 
            using (var xw = XmlTextWriter.Create (sw, xws)) {
                xw.WriteStartDocument ();
                xw.WriteStartElement ("resources");

                foreach (var kvp in resourceValues) {
                    if (!string.IsNullOrEmpty (kvp.Value)) {
                        xw.WriteStartElement ("string");
                        xw.WriteAttributeString ("name", kvp.Key);
                        xw.WriteAttributeString ("translatable", "false");
                        xw.WriteString (kvp.Value);
                        xw.WriteEndElement ();
                    }
                }

                xw.WriteEndElement ();
                xw.WriteEndDocument ();
                xw.Flush ();
                xw.Close ();
            }
        }

        void DeleteFiles (params string[] paths)
        {
            if (paths == null || !paths.Any ())
                return;

            foreach (var p in paths) {
                if (!File.Exists (p))
                    continue;
                try { 
                    File.Delete (p); 
                } catch (Exception ex) {
                    Log.LogWarning ("Failed to delete file: {0}{1}{2}", p, Environment.NewLine, ex);
                }
            }
        }

        static string CleanPath (params string[] paths)
        {
            var combined = Path.Combine (paths);
            return combined.Replace ('/', Path.DirectorySeparatorChar).Replace ('\\', Path.DirectorySeparatorChar);
        }
    }
}
