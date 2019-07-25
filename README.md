# Xamarin Component for Google Play Services Client Library

Xamarin creates and maintains Xamarin.Android bindings for the Google Play Services Client Library, including:

 - Ads
 - Analytics
 - AppIndexing
 - AppInvite
 - AppState
 - Auth
 - Base
 - Basement
 - Cast
 - Drive
 - Fitness
 - Games
 - GCM
 - Identity
 - Location
 - Maps
 - Measurement
 - Nearby
 - Panorama
 - Plus
 - SafetyNet
 - Support Wearable
 - Vision
 - Wallet
 - Wearable



## Building

The build script for this project uses [Cake](http://cakebuild.net).  To run the build, you can use one of the bootstrapper files either for Mac or Windows (experimental support only):

The bootstrapper script will automatically download Cake.exe and all the required tools and files into the `./tools/` folder.

The following targets can be specified:

 - `ci` builds the kitchen sink - what we run in CI
 - `libs` builds the class library bindings (depends on `binderate`)
 - `binderate` downloads the external dependencies and generates folder structure
 - `samples` builds all of the samples (depends on `libs`)
 - `nuget` builds the nuget packages (depends on `libs`)
 - `clean` cleans up everything

***NOTE***: The `binderate` build task may take awhile to run as it downloads several large dependencies.

You may want to consider passing `--verbosity diagnostic` (or `-Verbosity diagnostic` on Windows) to the bootstrapper to enable more verbose output, including downloading progress.

**Mac**:

```
sh ./build.sh --target=binderate && sh ./build.sh --target=libs
```

Optionally run:

```
sh ./build.sh --target=clean
```

before the build.

**Windows:**

```
./build.ps1 --target=binderate ; ./build.ps1 --target=libs
```

Optionally run:

```
./build.ps1 --target=clean
```

before the build.

To build nuget packages, samples and API diff:

**Mac**:

```
sh ./build.sh --target=nuget && sh ./build.sh --target=samples && sh ./build.sh --target=diff
```

**Windows:**

```
./build.ps1 --target=nuget ; ./build.ps1 --target=samples ; ./build.ps1 --target=diff
```

### Working in Visual Studio / Xamarin Studio

Before the `.sln` files will compile in Visual Studio or Xamarin Studio, the external dependencies need to be downloaded.  This can be done by running the `build.sh` or `build.ps1` with the target `externals`.  After the externals are setup, the `.sln` files should compile in an IDE.


### Versioning

Historically, Component and later NuGet package versions were based on the revision number of the Google Play Services SDK from the SDK Manager, and later based on the Google Play Services Maven Repository (m2repository) version from the SDK Manager.  Because of this, the versions of the components and NuGet packages do not match the version strings of Google Play Services releases from Google.  Here is a table which shows a translation of versions mapped to Google's version strings:


| NuGet / Component Version |  Google m2repository Version | Google Play Services Version       |
|---------------------------|------------------------------|------------------------------------|
| 25.0                      | 19 (GPS SDK 25)              | 7.5.0                              |
| 26.0                      | 21 (GPS SDK 26)              | 7.8.0                              |
| 27.0                      | 22 (GPS SDK 27)              | 8.1.0                              |
| 29.0                      | 24 (GPS SDK 29)              | 8.4.0                              |
|                           | 25                           | 8.4.0 (no binary changes)          |
|                           | 26                           | 9.0.0                              |
| 30.0.1.alpha4             | 27 (GPS SDK 30)              | 9.0.1                              |
| 30.0.2-alpha1             | 28                           | 9.0.2                              |
|                           | 29                           | 9.2.0                              |
|                           | 30                           | (never published)                  |
|                           | 31                           | 9.2.1                              |
| 32.4.0-beta2              | 32                           | 9.4.0                              |
| 32.961.0                  | 32                           | 9.6.1                              |
| 42.1001.0                 | 42                           | 10.0.1                             |
| 42.1021.0                 | n/a                          | 10.2.1                             |
| 42.1021.1                 | n/a                          | 10.2.1                             |
| 60.1142.0                 | n/a                          | 11.4.2                             |
| 71                        | n/a                          | [71][71]                           |
| 71.20190725               | n/a                          | [71.20190725][71.20190725]         |


[71]: https://github.com/xamarin/GooglePlayServicesComponents/blob/46fb07d8724f6c2342ff2b36bd332cc70106bab3/config.json
[71.20190725]: https://github.com/xamarin/GooglePlayServicesComponents/blob/46fb07d8724f6c2342ff2b36bd332cc70106bab3/config.json


## License

The license for this repository is specified in 
[LICENSE.md](LICENSE.md)

The `externals` build task downloads some external dependencies from Google which are licensed under and subject to the terms of [Android Software Development Kit License Agreement](http://developer.android.com/sdk/terms.html)


## Contribution Guidelines
The Contribution Guidelines for this repository are listed in [CONTRIBUTING.md](.github/CONTRIBUTING.md)

## .NET Foundation
This project is part of the [.NET Foundation](http://www.dotnetfoundation.org/projects)

