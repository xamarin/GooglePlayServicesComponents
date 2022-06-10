#! "net5.0"

#r "nuget: Newtonsoft.Json, 13.0.1"

#r "nuget: HolisticWare.Xamarin.Tools.ComponentGovernance, 0.0.1"
#r "nuget: HolisticWare.Core.Net.HTTP, 0.0.1"
#r "nuget: HolisticWare.Core.IO, 0.0.1"

// Usage:
//   dotnet tool install -g dotnet-script
//   dotnet script update-config.csx -- ../../config.json <update|bump>
// This script compares the versions of Java packages we are currently binding to the
// stable versions available in Google's Maven repository.  The specified configuration
// file can be automatically updated by including the "update" argument.  A revision bump
// can be applied to all packages with the "bump" argument, which is mutually exclusive
// with "update".

using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using HolisticWare.Xamarin.Tools.ComponentGovernance;

List<string> spell_errors = null;
JArray binderator_json_array = null;
List<(string, string, string, string)> mappings_artifact_nuget = new List<(string, string, string, string)>();

using (StreamReader reader = System.IO.File.OpenText(@"./config.json"))
{
	JsonTextReader jtr = new JsonTextReader(reader);
	binderator_json_array = (JArray)JToken.ReadFrom(jtr);
}

foreach(JObject jo in binderator_json_array[0]["artifacts"])
{
	bool? dependency_only = (bool?) jo["dependencyOnly"];
	if ( dependency_only == true)
	{
		continue;
	}

	string group_id  	= (string) jo["groupId"];
	string artifact_id  = (string) jo["artifactId"];
	string artifact_v   = (string) jo["version"];
	string nuget_id  	= (string) jo["nugetId"];
	string nuget_v  	= (string) jo["nugetVersion"];

	mappings_artifact_nuget.Add
	(
		(
			$"{group_id}:{artifact_id}",
			$"{artifact_v}",
			$"{nuget_id}",
			$"{nuget_v}"
		)
	);
}

Manifest manifest = new Manifest();

manifest.MappingMavenArtifact2NuGetPackage = mappings_artifact_nuget;

Console.WriteLine($"Saving ComponetGovernanceManifest cgmanifest.json...");
manifest.Save("./cgmanifest.json");
