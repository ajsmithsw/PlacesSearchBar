using System.Collections.Generic;
using Newtonsoft.Json;

namespace DurianCode.PlacesSearchBar
{
	public class AutoCompleteResult
	{
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("predictions")]
		public List<AutoCompletePrediction> AutoCompletePlaces { get; set; }
	}

}
