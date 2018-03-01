//
// Place.cs
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

using Newtonsoft.Json.Linq;

namespace DurianCode.PlacesSearchBar
{
	/// <summary>
	/// Place.
	/// </summary>
	public class Place
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		public double Latitude { get; set; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public double Longitude { get; set; }

		/// <summary>
		/// Gets or sets the raw.
		/// </summary>
		/// <value>The raw.</value>
		public string Raw { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DurianCode.PlacesSearchBar.Place"/> class.
		/// </summary>
		/// <param name="jsonObject">Json object.</param>
		public Place(JObject jsonObject)
		{
			Name = (string) jsonObject["result"]["name"];
			Latitude = (double)jsonObject["result"]["geometry"]["location"]["lat"];
			Longitude = (double)jsonObject["result"]["geometry"]["location"]["lng"];
			Raw = jsonObject.ToString();
		}
	}
}
