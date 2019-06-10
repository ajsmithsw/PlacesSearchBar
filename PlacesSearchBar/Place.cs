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

using System;
using System.Collections.Generic;
using System.Linq;
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
		/// Gets or sets the individual address components.
		/// </summary>
		/// <value>The address components.</value>
		public List<AddressComponent> AddressComponents { get; set; }

		/// <summary>
		/// Gets or sets the raw json value.
		/// </summary>
		/// <value>json string.</value>
		public string Raw { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DurianCode.PlacesSearchBar.Place"/> class.
		/// </summary>
		/// <param name="jsonObject">Json object.</param>
		public Place(JObject jsonObject)
		{
			Name              = jsonObject["result"]["name"].Value<string>();
			Latitude          = jsonObject["result"]["geometry"]["location"]["lat"].Value<double>();
			Longitude         = jsonObject["result"]["geometry"]["location"]["lng"].Value<double>();
			AddressComponents = jsonObject["result"]["address_components"].Value<JArray>().Select(p => AddressComponent.FromJSON(p.Value<JObject>())).ToList();
			Raw       = jsonObject.ToString();
		}

		public AddressComponent GetAddressComponentOrNull(string type)
		{
			foreach (var component in AddressComponents)
			{
				if (component.Types.Contains(type)) return component;
			}
			return null;
		}

		public AddressComponent AdminArea       => GetAddressComponentOrNull("administrative_area_level_1");
		public AddressComponent SubAdminArea    => GetAddressComponentOrNull("administrative_area_level_2");
		public AddressComponent SubSubAdminArea => GetAddressComponentOrNull("administrative_area_level_3");
		public AddressComponent Locality        => GetAddressComponentOrNull("locality");
		public AddressComponent SubLocality     => GetAddressComponentOrNull("sublocality_level_1") ?? GetAddressComponentOrNull("sublocality");
		public AddressComponent Thoroughfare    => GetAddressComponentOrNull("route");
		public AddressComponent SubThoroughfare => GetAddressComponentOrNull("street_number");
		public AddressComponent PostalCode      => GetAddressComponentOrNull("postal_code");
		public AddressComponent Country         => GetAddressComponentOrNull("country");
		public AddressComponent StreetName      => GetAddressComponentOrNull("route");
		public AddressComponent StreetNumber    => GetAddressComponentOrNull("street_number");
	}
}
