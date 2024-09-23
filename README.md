# Deprecated Repository

Note that the GPS/FB/MLKit packages that were previously built and released from this repository 
have been moved to the https://github.com/xamarin/AndroidX repository (which will eventually be 
moved to the `dotnet` organization with a more generic name).

This was done to simplify maintenance and to reduce circular dependencies between the two 
repositories which should lead to fewer NuGet version conflict issues.

All packages have been moved, no packages have been obsoleted or dropped as part of this effort.

# Google Play Services / Firebase / ML Kit for .NET for Android

Microsoft creates and maintains .NET for Android bindings for many of Google's [Google Play Services](https://developers.google.com/android),
[Firebase](https://firebase.google.com/), and [ML Kit](https://developers.google.com/ml-kit) libraries.

Support for Xamarin.Android ended on [May 1st, 2024](https://dotnet.microsoft.com/en-us/platform/support/policy/xamarin). New versions of these packages will not support Xamarin.Android.

## What is Google Play Services / Firebase / ML Kit

[Google Play Services](https://developers.google.com/android), [Firebase](https://firebase.google.com/), and [ML Kit](https://developers.google.com/ml-kit)
 (GPS/FB/MLKit) are a set of libraries that allow Android apps to take advantage of Google APIs and services.

## Binding Policies

- This repository binds over 200 GPS/FB/MLKit libraries that are published to [NuGet.org](https://nuget.org). The full package list can be found in [config.json](config.json).
- GPS/FB/MLKit Java artifacts and some dependencies come from [Google's Maven Respository](https://maven.google.com/web/index.html#).
- Additional dependencies come from [Maven/Sonatype Central](https://repo1.maven.org/maven2/).
- Google's release notes for GPS/FB/MLKit libraries are available [here](https://developers.google.com/android/guides/releases).
- The major/minor/patch version numbers are the GPS/FB/MLKit library version prefixed with a `1`. For example, the NuGet `Xamarin.GooglePlayServices.Maps 117.0.1` binds version `17.0.1` of the GPS library `com.google.android.gms:play-services-maps`.
  - The revision version number is used when a new NuGet needs to be built but the GPS/FB/MLKit library has not been updated.
- We endeavor to release updated NuGets within a month after new GPS/FB/MLKit releases, however large changes occasionally require 
  more time.
- In general, we do not bind pre-release libraries. As their API is not stable yet, it results in too much rework.

### Details

Full list of maven artifact to NuGet mappings:

[./docs/artifact-list.md](./docs/artifact-list.md)

Full list of maven artifact with versions to NuGet mappings with versions:

[./docs/artifact-list-with-versions.md](./docs/artifact-list-with-versions.md)

## License

The license for this repository is specified in [LICENSE.md](LICENSE.md).

Each package published from this repository generally contains third-party code (ie: `.jar`/`.aar`) that is governed by its own license.  Per-package license information is available in [cgmanifest.json](cgmanifest.json).

The `externals` build task downloads some external dependencies from Google which are licensed under and subject to the terms of 
[Android Software Development Kit License Agreement](http://developer.android.com/sdk/terms.html)

## Building

Instructions for building this repository are specified in [BUILDING.md](BUILDING.md).

## Contribution Guidelines

The Contribution Guidelines for this repository are listed in [CONTRIBUTING.md](.github/CONTRIBUTING.md).

## .NET Foundation

This project is part of the [.NET Foundation](http://www.dotnetfoundation.org/projects).
