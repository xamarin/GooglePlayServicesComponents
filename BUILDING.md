# Building

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

```pwsh
./build.ps1 --target=nuget ; ./build.ps1 --target=samples ; ./build.ps1 --target=diff
```

## Working in Visual Studio

Before the `.sln` files will compile in Visual Studio or Xamarin Studio, the external dependencies need to be downloaded.  This can be done by running the `build.sh` or `build.ps1` with the target `externals`.  After the externals are setup, the `.sln` files should compile in an IDE.

## Advanced scenarios

Simple build (`ci` build done on CI Azure DevOps servers):

```
dotnet cake -t=ci
```

Clean `ci` build followed by nuget API diff and then several utility tqrgets:

On MacOSX (and Linux):

```bash
dotnet cake -t=clean && dotnet cake -t=ci && dotnet cake nuget-diff.cake && dotnet cake utilities.cake
```

### Build (`build.cake`)

*   `javadocs`

    Prepares javadocs for parameter names.

*   `tools-update`

    Updates some of the tools.

*   `binderate`

    Runs `binderator` on `config.json` data.

*   `binderate-prepare-dependencies-samples-packagereferences`

*   `binderate-prepare-dependencies-samples-packages-config`

*   `binderate-config-verify`

    Verifies versions in `config.json`

*   `binderate-diff`

    Runs `diff` to see details of the update.

*   `binderate-fix`

*   `mergetargets`

*   `libs-native`

    Builds native maven projects with gradle.

*   `libs`

    Builds projects (assemblies - libraries - libs)

*   `samples-directory-build-targets`

    Generates list of locally built packages which are tested during samples builds.

*   `samples`

    Builds samples

*   `allbindingprojectrefs`

*   `nuget`

    Generates (packaging) NuGet packages for projects.

*   `merge`

    Merges all assemblies/projects into single assembly for NuGet API diff.

*   `ci-setup`

    Sets up CI environment.

*   `nuget-dependecies`

*   `genapi`

*   `docs-api-diff`

    Generates MarkDown docs from API diff XML files.

*   `clean`

    Cleans folders and files.

*   `ci`

    Builds projects on CI (`libs`, `nuget`, `samples`).

### Nuget API diff (`nuget-diff.cake`)

Generates API diff (XML and MarkDown) files for differences between local packages and latest packages from `nuget.org`.

### Utilities (`utilities.cake`)

*   `generate-component-governance`

    Generates Components Governance `cgmanifest.json`

*   `mappings-artifact-nuget`

    Generates mapping maven artifacts to nuget packages.

*   `list-artifacts`

    Generate list of the maven artifacts and its nuget package.

*   `spell-check`

    Spell checks namespaces and nuget packages.

*   `namespace-check`

    Verifies namespaces (names and casing)

*   `binderate-diff`

    Runs `diff` to see details of the update.

*   `target-sdk-version-check`

    Verification of TFMs.

*   `api-diff-markdown-info-pr`

    Generates Markdown about update info for PRs (weekly stable updates)

*   `api-diff-analysis`

    Generates Markdown about update info for PRs (weekly stable updates)

*   `nuget-structure-analysis`

    Unpacks nuget packages, so the structure can be verified.

*   `read-analysis-files`

    Opens analysis files (API diff, spell checking, etc) VS Code required.

*   `dependencies`

    WIP: dependency trees generation (Maven and Nuget)

*   `generate-markdown-publish-log`

    Generates MarkDown based on CI NuGet publish log.

*   `Default`

    Default target - `ci`
    