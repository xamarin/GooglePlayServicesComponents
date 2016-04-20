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

**Mac**:
```
sh build.sh --target libs
```

**Windows (experimental support only):**

***NOTE:*** Windows build support is still experimental. You may need to first build the externals target, then open the `GooglePlayServices.sln` in Visual Studio, rebuild it, build the clean target, and then continue on normally building whichever targets you like. This will ensure the appropriate files are downloaded and cached in your user's AppData folder.

```
powershell .\build.ps1 -Target libs
```

The bootstrapper script will automatically download Cake.exe and all the required tools and files into the `./tools/` folder.

The following targets can be specified:

 - `libs` builds the class library bindings (depends on `externals`)
 - `externals` downloads the external dependencies
 - `samples` builds all of the samples (depends on `libs`)
 - `nuget` builds the nuget packages (depends on `libs`)
 - `component` builds the xamarin components (depends on `samples` and `nuget`)
 - `clean` cleans up everything

***NOTE***: The `externals` build task may take awhile to run as it downloads several large dependencies.

You may want to consider passing `--verbosity diagnostic` (or `-Verbosity diagnostic` on Windows) to the bootstrapper to enable more verbose output, including downloading progress.


### Working in Visual Studio / Xamarin Studio

Before the `.sln` files will compile in Visual Studio or Xamarin Studio, the external dependencies need to be downloaded.  This can be done by running the `build.sh` or `build.ps1` with the target `externals`.  After the externals are setup, the `.sln` files should compile in an IDE.


## License

The license for this repository is specified in 
[LICENSE.md](LICENSE.md)

The `externals` build task downloads some external dependencies from Google which are licensed under and subject to the terms of [Android Software Development Kit License Agreement](http://developer.android.com/sdk/terms.html)