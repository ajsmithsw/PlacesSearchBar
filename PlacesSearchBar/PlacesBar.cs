using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace DurianCode.PlacesSearchBar
{
	public delegate void PlacesRetrievedEventHandler(object sender, AutoCompleteResult result);

	public class PlacesBar : SearchBar
	{
		/// <summary>
		/// The place type.
		/// </summary>
		PlaceType placeType = PlaceType.All;

		/// <summary>
		/// The location bias.
		/// </summary>
		LocationBias locationBias;

		/// <summary>
		/// The API key.
		/// </summary>
		string apiKey;

		/// <summary>
		/// The minimum search text.
		/// </summary>
		int minimumSearchText;

		#region Property accessors
		/// <summary>
		/// Gets or sets the place type.
		/// </summary>
		/// <value>The type.</value>
		public PlaceType Type
		{
			get
			{
				return placeType;
			}
			set
			{
				placeType = value;
			}
		}

		/// <summary>
		/// Gets or sets the location bias.
		/// </summary>
		/// <value>The bias.</value>
		public LocationBias Bias
		{
			get
			{
				return locationBias;
			}
			set
			{
				locationBias = value;
			}
		}

		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		/// <value>The API key.</value>
		public string ApiKey
		{
			get
			{
				return apiKey;
			}
			set
			{
				apiKey = value;
			}
		}

		/// <summary>
		/// Gets or sets the minimum search text.
		/// </summary>
		/// <value>The minimum search text.</value>
		public int MinimumSearchText
		{
			get
			{
				return minimumSearchText;
			}
			set
			{
				minimumSearchText = value;
			}
		}
		#endregion

		/// <summary>
		/// The places retrieved handler.
		/// </summary>
		public PlacesRetrievedEventHandler PlacesRetrieved;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DurianCode.PlacesSearchBar.PlacesBar"/> class.
		/// </summary>
		public PlacesBar()
		{
			TextChanged += OnTextChanged;
		}

		/// <summary>
		/// Handles changes to search text.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length >= minimumSearchText)
			{
				var predictions = await GetPlaces(e.NewTextValue);
				if (PlacesRetrieved != null && predictions != null)
					PlacesRetrieved(this, predictions);
				else
					PlacesRetrieved(this, new AutoCompleteResult());
			}
			else
			{
				PlacesRetrieved(this, new AutoCompleteResult());
			}
		}

		/// <summary>
		/// Calls the Google Places API to retrieve autofill suggestions
		/// </summary>
		/// <returns>The places.</returns>
		/// <param name="newTextValue">New text value.</param>
		async Task<AutoCompleteResult> GetPlaces(string newTextValue)
		{
			if (string.IsNullOrEmpty(apiKey))
			{
				throw new Exception(
					string.Format("You have not assigned a Google API key to PlacesBar"));
			}

			try 
			{
				var requestURI = CreatePredictionsUri(newTextValue);
				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
				var response = await client.SendAsync(request);

				if (!response.IsSuccessStatusCode)
				{
					Debug.WriteLine("PlacesBar HTTP request denied.");
					return null;
				}

				var result = await response.Content.ReadAsStringAsync();

				if (result == "ERROR")
				{
					Debug.WriteLine("PlacesSearchBar Google Places API returned ERROR");
					return null;
				}

				return JsonConvert.DeserializeObject<AutoCompleteResult>(result);
			} 
			catch (Exception ex)
			{
				Debug.WriteLine("PlacesBar HTTP issue: {0} {1}", ex.Message, ex);
				return null;
			}
		}

		/// <summary>
		/// Creates the predictions URI.
		/// </summary>
		/// <returns>The predictions URI.</returns>
		/// <param name="newTextValue">New text value.</param>
		string CreatePredictionsUri(string newTextValue)
		{
			var url = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
			var input = Uri.EscapeUriString(newTextValue);
			var pType = PlaceTypeValue(placeType);
			var constructedUrl = $"{url}?input={input}&types={pType}&key={apiKey}";

			if (locationBias != null)
				constructedUrl = constructedUrl + locationBias;

			return constructedUrl;
		}

		/// <summary>
		/// Returns a string representation of the specified place type.
		/// </summary>
		/// <returns>The type value.</returns>
		/// <param name="type">Type.</param>
		string PlaceTypeValue(PlaceType type)
		{
			switch (type)
			{
				case PlaceType.All:
					return "";
				case PlaceType.Geocode:
					return "geocode";
				case PlaceType.Address:
					return "address";
				case PlaceType.Establishment:
					return "establishment";
				case PlaceType.Regions:
					return "(regions)";
				case PlaceType.Cities:
					return "(cities)";
				default:
					return "";
			}
		}

	}
}
