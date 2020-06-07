# tz-lookup-net

The port of the fast and lightweight JS library https://github.com/darkskyapp/tz-lookup-oss

Provides an IANA time zone identifier from latitude and longitude coordinates.

[![PLATFORM](https://img.shields.io/badge/platform-.NET%20Standard%202.0-lightgrey)]() [![NuGet](https://img.shields.io/nuget/v/TzLookup)](https://www.nuget.org/packages/TzLookup/)

## Installing

Add [NuGet package](https://www.nuget.org/packages/TzLookup) to your [.NET Standard 2.0 - compatible](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md#platform-support) project

```
PM> Install-Package TzLookup
```

## Usage

```csharp
var timeZoneName = TimeZoneLookup.Iana(42.7235, -73.6931);

// timeZoneName: "America/New_York"
```

See the README of the original library for more details.

## Special thanks to

[DarkSky](https://github.com/darkskyapp) and all [contributors of the original JS library](https://github.com/darkskyapp/tz-lookup-oss/graphs/contributors).

## License

Like the original library, this project is licensed under the Creative Commons Zero v1.0.

This work and all rights to it are dedicated to the public domain.