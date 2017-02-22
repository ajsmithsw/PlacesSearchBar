using DurianCode.PlacesSearchBar;
using Xamarin.Forms;

namespace PlacesBarExamples
{
	public partial class PlacesBarExamplesPage : ContentPage
	{
		string GooglePlacesApiKey = Keys.ApiKey;  // Replace this with your api key

		public PlacesBarExamplesPage()
		{
			InitializeComponent();

			search_bar.ApiKey = GooglePlacesApiKey;
			search_bar.Type = PlaceType.Cities;
			search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
			search_bar.TextChanged += Search_Bar_TextChanged;
			search_bar.MinimumSearchText = 2;
			results_list.ItemSelected += Results_List_ItemSelected;
		}

		void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
		{
			results_list.ItemsSource = result.AutoCompletePlaces;
			spinner.IsRunning = false;
			spinner.IsVisible = false;

			if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
				results_list.IsVisible = true;
		}

		void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.NewTextValue))
			{
				results_list.IsVisible = false;
				spinner.IsVisible = true;
				spinner.IsRunning = true;
			}
			else
			{
				results_list.IsVisible = true;
				spinner.IsRunning = false;
				spinner.IsVisible = false;
			}
		}

		async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var prediction = (AutoCompletePrediction)e.SelectedItem;
			results_list.SelectedItem = null;

			var place = await Places.GetPlace(prediction.Place_ID, GooglePlacesApiKey);

			if (place != null)
				await DisplayAlert(
					place.Name, string.Format("Lat: {0}\nLon: {1}", place.Latitude, place.Longitude), "OK");
		}
	}
}