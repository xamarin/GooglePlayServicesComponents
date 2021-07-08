## Building

The build script for this project uses [Cake](http://cakebuild.net).  To run the build, you can use one of the bootstrapper files either for Mac or Windows (experimental support only):

The bootstrapper script will automatically download Cake.exe and all the required tools and files into the `./tools/` folder.

Required dotnet core tools:

* binderator

Optional dotnet core tools:

* cake

To update all tools: 

	dotnet tool uninstall 	-g Cake.Tool
	dotnet tool install 	-g Cake.Tool	
	dotnet tool uninstall 	-g Xamarin.AndroidBinderator.Tool
	dotnet tool install 	-g Xamarin.AndroidBinderator.Tool

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
