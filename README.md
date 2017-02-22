# Places Search Bar

An extension of Xamarin.Forms SearchBar control for implementing Google Places Autocomplete API.

<center><img src="https://2.bp.blogspot.com/-rI4zz5S5RnA/WKy9bWl6K7I/AAAAAAAAE3k/Tbo8u25M720LJ-5ij0nDnAgNEXeZA8wOgCLcB/s1600/PlacesSearchBar.png" /> </center>


## Contributing
If you wish to contribute, you're welcome to submit pull requests or get in touch.


## Obtaining API key
Use the [Google Developers Console](https://console.developers.google.com/) to enable the 'Google Places API Web Service' and create a 'Server' API key credential. In both cases do not use the iOS options.

Note: if you are developing for multi-platform you will likely need a separate API key for each device, and set your key property based on OS:
```csharp
public static string ApiKey = Device.OS == TargetPlatform.iOS ? "add_ios_key_here" :
                                            TargetPlatform.Android ? "add_android_key_here" :
                                            TargetPlatform.WinPhone ? "add_winphone_key_here" :
                                            "...etc...";
```

## Usage
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