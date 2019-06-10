//
// AutoCompletePrediction.cs
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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DurianCode.PlacesSearchBar
{
	/// <summary>
	/// Auto complete prediction.
	/// </summary>
	public class AutoCompletePrediction
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the place identifier.
		/// </summary>
		/// <value>The place identifier.</value>
		public string Place_ID { get; set; }

		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		/// <value>The reference.</value>
		public string Reference { get; set; }

		/// <summary>
		/// Gets the main text for UI display
		/// </summary>
		/// <value>The main text.</value>
		public string MainText { get; set; }

		/// <summary>
		/// Gets the secondary text for UI display
		/// </summary>
		/// <value>The secondary text.</value>
		public string SecondaryText { get; set; }

		public static AutoCompletePrediction FromJson(JObject json)
		{
			var r = new AutoCompletePrediction();

			r.Description   = json["description"].Value<string>();
			r.ID            = json["id"].Value<string>();
			r.Place_ID      = json["place_id"].Value<string>();
			r.Reference     = json["reference"].Value<string>();
			r.MainText      = json["structured_formatting"]["main_text"].Value<string>();
			r.SecondaryText = json["structured_formatting"]["secondary_text"].Value<string>();

			return r;
		}
	}

}
