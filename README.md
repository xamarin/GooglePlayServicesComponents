# Google Play Services / Firebase for Xamarin.Android

Xamarin creates and maintains Xamarin.Android bindings for Google Play Services and Firebase.

## What is Google Play Services / Firebase

Google Play Services and Firebase (GPS/FB) are a set of libraries that allow Android apps to take advantage of Google APIs and services.

## Binding Policies

- This repository binds over 140 GPS/FB libraries that are published to [NuGet.org](https://nuget.org). The full package list can be found in [config.json](config.json).
- GPS/FB Java artifacts come from [Google's Maven Respository](https://maven.google.com/web/index.html#).
- Google's release notes for GPS/FB libraries are available [here](https://developers.google.com/android/guides/releases).
- The major/minor/patch version numbers are the GPS/FB library version prefixed with a `1`. For example, the NuGet `Xamarin.GooglePlayServices.Maps 117.0.1` binds version `17.0.1` of the GPS library `com.google.android.gms:play-services-maps`.
  - The revision version number is used when a new NuGet needs to be built but the GPS/FB library has not been updated.
- We endeavor to release updated NuGets within a month after new GPS/FB releases, however large changes occasionally require more time.
- In general, we do not bind pre-release libraries. As their API is not stable yet, it results in too much rework.

## License

The license for this repository is specified in 
[LICENSE.md](LICENSE.md)

The `externals` build task downloads some external dependencies from Google which are licensed under and subject to the terms of [Android Software Development Kit License Agreement](http://developer.android.com/sdk/terms.html)

## Building

Instructions for building this repository are specified in [BUILDING.md](BUILDING.md)

## Contribution Guidelines
The Contribution Guidelines for this repository are listed in [CONTRIBUTING.md](.github/CONTRIBUTING.md)

## .NET Foundation
This project is part of the [.NET Foundation](http://www.dotnetfoundation.org/projects)
