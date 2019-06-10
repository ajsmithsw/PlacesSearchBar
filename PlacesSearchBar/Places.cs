//
// Places.cs
//
// Author:
//       Alex Smith <alex@duriancode.com>
//
// Copyright (c) 2017 (c) Alexander Smith
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DurianCode.PlacesSearchBar
{
	/// <summary>
	/// Places.
	/// </summary>
	public static class Places
	{
		/// <summary>
		/// Gets the place.
		/// </summary>
		/// <returns>The place.</returns>
		/// <param name="placeID">Place identifier.</param>
		/// <param name="apiKey">API key.</param>
		public static async Task<Place> GetPlace(string placeID, string apiKey)
		{
			try
			{
				var requestURI = CreateDetailsRequestUri(placeID, apiKey);
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

				return new Place(JObject.Parse(result));
			}
			catch (Exception ex)
			{
				Debug.WriteLine("PlacesBar HTTP issue: {0} {1}", ex.Message, ex);
				return null;
			}
		}

		/// <summary>
		/// Creates the details request URI.
		/// </summary>
		/// <returns>The details request URI.</returns>
		/// <param name="place_id">Place identifier.</param>
		/// <param name="apiKey">API key.</param>
		private static string CreateDetailsRequestUri(string place_id, string apiKey)
		{
			var url = "https://maps.googleapis.com/maps/api/place/details/json";
			return $"{url}?placeid={Uri.EscapeUriString(place_id)}&key={apiKey}";
		}
		
		/// <summary>
		/// Calls the Google Places API to retrieve autofill suggestions
		/// </summary>
		/// <returns>The places.</returns>
		/// <param name="newTextValue">New text value.</param>
		/// <param name="apiKey">The API key</param>
		/// <param name="bias">The location bias (can be NULL)</param>
		/// <param name="components">The components (can be NULL)</param>
		/// <param name="type">Filter for the returning types </param>
		/// <param name="language">The language of the results</param>
		public static async Task<AutoCompleteResult> GetPlaces(string newTextValue, string apiKey, LocationBias bias, Components components, PlaceType type, GoogleAPILanguage language)
		{
			if (string.IsNullOrEmpty(apiKey))
			{
				throw new Exception("You have not assigned a Google API key to PlacesBar");
			}

			try 
			{
				var requestURI = CreatePredictionsUri(newTextValue, apiKey, bias, components, type, language);
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

				return AutoCompleteResult.FromJson(JObject.Parse(result));
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
		/// <param name="apiKey">The API key</param>
		/// <param name="bias">The location bias (can be NULL)</param>
		/// <param name="components">The components (can be NULL)</param>
		/// <param name="type">Filter for the returning types </param>
		/// <param name="language">The language of the results</param>
		private static string CreatePredictionsUri(string newTextValue, string apiKey, LocationBias bias, Components components, PlaceType type, GoogleAPILanguage language)
		{
			var url = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
			var input = Uri.EscapeUriString(newTextValue);
			var pType = PlaceTypeValue(type);

			var constructedUrl = $"{url}?input={input}&types={pType}&key={apiKey}";

			if (bias != null)
				constructedUrl = constructedUrl + bias;

			if (components != null)
				constructedUrl += components;

			if (language != GoogleAPILanguage.Unset) 
				constructedUrl += "&Language=" + GoogleAPILanguageHelper.ToAPIString(language);

			return constructedUrl;
		}

		/// <summary>
		/// Returns a string representation of the specified place type.
		/// </summary>
		/// <returns>The type value.</returns>
		/// <param name="type">Type.</param>
		private static string PlaceTypeValue(PlaceType type)
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