# Places Search Bar

#### **PLEASE NOTE** This repository and unlisted nuget package are no longer supported. Issues will be closed. Thank you all for supporting this project. Please feel free to make use of the code as per the [License](https://github.com/ajsmithsw/PlacesSearchBar/blob/master/LICENSE.txt). If you do use the code, you may optionally wish to credit the original author, which would be appreciated but is not required.

An extension of Xamarin.Forms SearchBar control for implementing Google Places Autocomplete API.

<center><img src="https://2.bp.blogspot.com/-rI4zz5S5RnA/WKy9bWl6K7I/AAAAAAAAE3k/Tbo8u25M720LJ-5ij0nDnAgNEXeZA8wOgCLcB/s1600/PlacesSearchBar.png" /> </center>


## Contributing
If you have problems, please make an attempt at fixing and improving the code, and raise a pull request. If you still can't solve your problem then raise an issue, but please ensure that you include as much information as possible (stack traces, steps to reproduce). Please ensure that your issue is related to this library, not the general usage/debugging of the standard Xamarin Forms search bar.


## Obtaining API key
Use the [Google Developers Console](https://console.developers.google.com/) to enable the 'Google Places API Web Service' and create a new API key.

Note: if you are developing for multi-platform you will likely need a separate API key for each device, and set your key property based on OS:
```csharp
public static string ApiKey = Device.OS == TargetPlatform.iOS ? "add_ios_key_here" :
                                            TargetPlatform.Android ? "add_android_key_here" :
                                            TargetPlatform.WinPhone ? "add_winphone_key_here" :
                                            "...etc...";
```

## Usage
Add the search bar namespace to your Xaml file:
```xml
<?xml version="1.0" encoding="utf-8"?>
<ContentPage ...
             xmlns:places="clr-namespace:DurianCode.PlacesSearchBar;assembly=PlacesSearchBar">
```
...and then add the search bar to your layout:
```xml
<StackLayout>
        ...
        <places:PlacesBar x:Name="search_bar" />
```

### The PlacesBar has the following properties and methods:

#### Type
An optional ```PlaceType``` value that sets the results based on a particular location Type

#### Bias
An optional ```LocationBias``` value that sets a custom location bias (see API docs for details)

#### ApiKey
A required string value

#### MinimumSearchText
An optional integer value for setting the minimum number of search characters the user should type before API calls are made (useful for stemming API call volume)

#### PlacesRetrieved
Subscribe to this event handler to receive search results as ```AutoCompleteResults``` objects


### Retrieving Place details
The package also contains classes you can use to retrieve place details (including Latitude & Longitude)

#### Places class
A static class which exposes a ```GetPlace(placeID, apiKey)``` function which returns a ```Place``` object

#### Place class
A Place object contains ```Name```, ```Latitude```, ```Longitude``` and ```Raw``` (JSON response string) properties


### Implementing PlacesBar
See the PlacesBarExamples solution included to see a Xamarin Forms project implementing the ```PlacesBar``` and ```GetPlace()``` features.

## Notes
There is a known bug in Xamarin.Forms with Android N 7.0 where if a SearchBar's height is not set, the search bar will not display. Please explicitly set HeightRequest value to avoid [this](https://bugzilla.xamarin.com/show_bug.cgi?id=43975) issue.
