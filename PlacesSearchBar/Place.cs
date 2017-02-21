using System;
using Newtonsoft.Json.Linq;

namespace DurianCode.PlacesSearchBar
{
	public class Place
	{
		public string Name { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public string Raw { get; set; }

		public Place(JObject jsonObject)
		{
			this.Name = (string) jsonObject["result"]["name"];
			this.Latitude = (double)jsonObject["result"]["geometry"]["location"]["lat"];
			this.Longitude = (double)jsonObject["result"]["geometry"]["location"]["lng"];
			this.Raw = jsonObject.ToString();
		}
	}
}
