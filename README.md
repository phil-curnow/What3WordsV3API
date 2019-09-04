# What3WordsV3API

Unofficial .NET wrapper for Version 3 of the what3words API

All API methods are provided in a single class called W3WClient. You will need a what3words API key to be able to use the library, which you can obtain from the what3words after signing up for an account.

The following methods are available in the library:

- ConvertTo3WA - Converts a provided latitude and longitude to a 3 word address.
- ConvertToCoordinates - Converts a provided 3 word address to its corresponding latitude and longitude
- AvailableLanguages - Returns a list of all available what3words languages
- GridSection - Returns a section of a 3m x 3m what3words grid for the provided bounding box.
- AutoSuggest - Takes a slightly incorrect 3 word address and suggests a list of valid 3 word addresses

There are also Asynchronous versions of all these methods, which are:

- ConvertToW3AAsync
- ConvertToCoordinatesAsync
- AvailableLanguagesAsync
- GridSectionAsync
- AutoSuggestAsync

## API Namespaces

You should include the following usings in your code to use the API wrapper:

```csharp
using Curnow.Biz.What3WordsV3Net;
using Curnow.Biz.What3WordsV3Net.Enums;
using Curnow.Bix.What3WordsV3Net.Models;
```

## Creating an instance of W3WClient

Creating an instance of the API wrapper is very easy, you simply need to supply your API key as follows:

```csharp
W3WClient w3w = new W3WClient("<<your api key>>");
```

## ConvertTo3WA Example

```csharp
AddressResponse response = w3w.ConvertTo3WA(51.520847, -0.19552);
Console.WriteLine($"Country: {response.country}\nWords: {response.words}\n{response.language}");
```

There is an optional language parameter that can be used on this method. If language isn't supplied, the default used is en (English):

```csharp
AddressResponse response = w3w.ConvertTo3WA(51.520847, -0.19552, "fr");
Console.WriteLine($"Country: {response.country}\nWords: {response.words}\n{response.language}");
```

## ConvertToCoordinates

```csharp
AddressResponse response = w3w.ConvertToCoordinates("pinch","veal","sector");
Console.WriteLine($"Country: {response.country}\nURL@ {response.map}\nCoords: {response.coordinates.lat},{response.coordinates.lng}");

AddressResponse response = w3w.ConvertToCoordinates("pinch","veal","sector","fr");
Console.WriteLine($"Country: {response.country}\nURL@ {response.map}\nCoords: {response.coordinates.lat},{response.coordinates.lng}");
```

## AvailableLanguages

```csharp
LanguagesResponse response = w3w.AvailableLanguages();
response.languages.ForEach(l => { Console.WriteLine($"Native: {l.nativeName} - Code: {l.code} - Name: {l.name}"); });
```

## GridSection

```csharp
GridSectionResponse response = w3w.GridSection(new BoundingBox
{
    South = 52.207988,
    West = 0.116126,
    North = 52.208867,
    East = 0.117540
});
response.lines.ForEach(l => { Console.WriteLine($"Start: {l.start.lng}, {l.start.lat} End: {l.end.lng}, {l.end.lat}"); });
```

## AutoSuggest

There are many optional parameters that can be supplied with this method call. Please refer to the [what3words API documentation](https://docs.what3words.com/api/v3/#overview) for this.

```csharp
AutoSuggestResponse response = w3w.AutoSuggest("plan.clips.a");
foreach (var r in result.suggestions)
    Console.WriteLine($"Country: {r.country}\nNearest: {r.nearestPlace}\nWords: {r.words}\ndistancetoFocus: {r.distanceToFocusKm}\nRank: {r.rank}\nLanguage: {r.language}\n\n");
```

## Handling Errors

The library handles an exceptions by throwing an exception of the type W3WException. There are specific errors returned by the what3words API based on HTTP Response code. Please refer to the [what3words API documentation](https://docs.what3words.com/api/v3/#overview) for this information.

```csharp
try
{
  ...
}
catch(W3WException e)
{
    Console.WriteLine($"Error Code: {e.W3WCode} - Message: {e.W3WMessage}");
}
```

