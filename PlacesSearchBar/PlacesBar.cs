//
// PlacesBar.cs
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
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DurianCode.PlacesSearchBar
{
    /// <summary>
    /// Places retrieved event handler.
    /// </summary>
    public delegate void PlacesRetrievedEventHandler(object sender, AutoCompleteResult result);

	/// <summary>
	/// Places bar.
	/// </summary>
	public class PlacesBar : SearchBar
	{
		/// <summary>
		/// Backing store for the Type property.
		/// </summary>
		public static readonly BindableProperty PlaceTypeProperty = BindableProperty.Create(nameof(Type), typeof(PlaceType), typeof(PlacesBar), PlaceType.All, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null); 

		/// <summary>
		/// Backing store for the Bias property.
		/// </summary>
        public static readonly BindableProperty LocationBiasProperty = BindableProperty.Create(nameof(Bias), typeof(LocationBias), typeof(PlacesBar), (object)null, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        /// <summary>
        /// Backing store for the Components property.
        /// </summary>
        public static readonly BindableProperty ComponentsProperty = BindableProperty.Create(nameof(Components), typeof(Components), typeof(PlacesBar), (object)null, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        /// <summary>
        /// Backing store for the ApiKey property.
        /// </summary>
        public static readonly BindableProperty ApiKeyProperty = BindableProperty.Create(nameof(ApiKey), typeof(string), typeof(PlacesBar), string.Empty, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        /// <summary>
        /// Backing store for the MinimumSearchText property.
        /// </summary>
        public static readonly BindableProperty MinimumSearchTextProperty = BindableProperty.Create(nameof(MinimumSearchText), typeof(int), typeof(PlacesBar), 2, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        #region Property accessors
        /// <summary>
        /// Gets or sets the place type.
        /// </summary>
        /// <value>The type.</value>
        public PlaceType Type
		{
			get
			{
				return (PlaceType) this.GetValue(PlacesBar.PlaceTypeProperty);
			}
			set
			{
                this.SetValue(PlacesBar.PlaceTypeProperty, (object)value);
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
                return (LocationBias)this.GetValue(PlacesBar.LocationBiasProperty);
            }
            set
			{
                this.SetValue(PlacesBar.LocationBiasProperty, (object)value);
            }
        }

        /// <summary>
        /// Gets or sets the components
        /// </summary>
        public Components Components
        {
            get
            {
                return (Components)this.GetValue(PlacesBar.ComponentsProperty);
            }
            set
            {
                this.SetValue(PlacesBar.ComponentsProperty, (object)value);
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
                return (string)this.GetValue(PlacesBar.ApiKeyProperty);

            }
            set
			{
                this.SetValue(PlacesBar.ApiKeyProperty, (object)value);
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
                return (int)this.GetValue(PlacesBar.MinimumSearchTextProperty);

            }
            set
			{
                this.SetValue(PlacesBar.MinimumSearchTextProperty, (object)value);
            }
        }
		#endregion

		/// <summary>
		/// The places retrieved handler.
		/// </summary>
		public event PlacesRetrievedEventHandler PlacesRetrieved;

        protected virtual void OnPlacesRetrieved(AutoCompleteResult e)
        {
            PlacesRetrievedEventHandler handler = PlacesRetrieved;
            handler?.Invoke(this, e);
        }

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
			if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length >= MinimumSearchText)
			{
				var predictions = await GetPlaces(e.NewTextValue);
				if (PlacesRetrieved != null && predictions != null)
					OnPlacesRetrieved(predictions);
				else
                    OnPlacesRetrieved(new AutoCompleteResult());
            }
            else
			{
                OnPlacesRetrieved(new AutoCompleteResult());
            }
        }

		/// <summary>
		/// Calls the Google Places API to retrieve autofill suggestions
		/// </summary>
		/// <returns>The places.</returns>
		/// <param name="newTextValue">New text value.</param>
		async Task<AutoCompleteResult> GetPlaces(string newTextValue)
		{
			if (string.IsNullOrEmpty(ApiKey))
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
			var pType = PlaceTypeValue(Type);
			var constructedUrl = $"{url}?input={input}&types={pType}&key={ApiKey}";

			if (Bias != null)
				constructedUrl = constructedUrl + Bias;
            if (Components != null)
                constructedUrl += Components;

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
