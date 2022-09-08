# Google Play Services / Firebase / ML Kit for Xamarin.Android

Xamarin creates and maintains Xamarin.Android bindings for Google Play Services, Firebase, and ML Kit.

## What is Google Play Services / Firebase / ML Kit

Google Play Services, Firebase, and ML Kit (GPS/FB/MLKit) are a set of libraries that allow Android apps to take advantage of Google 
APIs and services.

## Binding Policies

- This repository binds over 200 GPS/FB/MLKit (2022-09)libraries that are published to [NuGet.org](https://nuget.org). The full package list can be 
  found in [config.json](config.json).
- GPS/FB/MLKit Java artifacts come from [Google's Maven Respository](https://maven.google.com/web/index.html#).
- dependency Maven artifacts come from different maven repositories mostly:
  - [Google's Maven Respository](https://maven.google.com/web/index.html#) and
  - [Maven/Sonatype Central](https://repo1.maven.org/maven2/)
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

The license for this repository is specified in 
[LICENSE.md](LICENSE.md)

Detailed metada with licenses can be found in: [Component Governance cgmanifes.json](./cgmanifest.json)

The `externals` build task downloads some external dependencies from Google which are licensed under and subject to the terms of 
[Android Software Development Kit License Agreement](http://developer.android.com/sdk/terms.html)

## Building

Instructions for building this repository are specified in [BUILDING.md](BUILDING.md)

In depth building instructions: [./docs/build.md](./docs/build.md).


## Contribution Guidelines

The Contribution Guidelines for this repository are listed in [CONTRIBUTING.md](.github/CONTRIBUTING.md)

## .NET Foundation

This project is part of the [.NET Foundation](http://www.dotnetfoundation.org/projects)
