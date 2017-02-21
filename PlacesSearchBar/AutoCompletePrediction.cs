using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DurianCode.PlacesSearchBar
{
	public class AutoCompletePrediction
	{
		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("id")]
		public string ID { get; set; }

		[JsonProperty("place_id")]
		public string Place_ID { get; set; }

		[JsonProperty("reference")]
		public string Reference { get; set; }
	}

}
