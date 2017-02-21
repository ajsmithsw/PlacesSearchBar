using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DurianCode.PlacesSearchBar
{
	public static class Places
	{
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

		static string CreateDetailsRequestUri(string place_id, string apiKey)
		{
			var url = "https://maps.googleapis.com/maps/api/place/details/json";
			return $"{url}?placeid={Uri.EscapeUriString(place_id)}&key={apiKey}";
		}
	}
}