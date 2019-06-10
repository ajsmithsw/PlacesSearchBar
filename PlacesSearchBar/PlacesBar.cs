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
using Newtonsoft.Json.Linq;
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
		
        /// <summary>
        /// Backing store for the MinimumSearchText property.
        /// </summary>
        public static readonly BindableProperty LanguageProperty = BindableProperty.Create(nameof(Language), typeof(GoogleAPILanguage), typeof(PlacesBar), GoogleAPILanguage.Unset, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

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

		/// <summary>
		/// Gets or sets the language for the API call.
		/// The language code, indicating in which language the results should be returned, if possible.
		/// Searches are also biased to the selected language; results in the selected language may be given a higher ranking
		/// </summary>
		/// <value>The languagey.</value>
		public GoogleAPILanguage Language
		{
			get
			{
				return (GoogleAPILanguage)this.GetValue(PlacesBar.LanguageProperty);

			}
			set
			{
				this.SetValue(PlacesBar.LanguageProperty, (object)value);
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
				var predictions = await Places.GetPlaces(e.NewTextValue, ApiKey, Bias, Components, Type, Language);
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
	}
}
