namespace DurianCode.PlacesSearchBar
{
	/// <summary>
	/// LocationBias object enables location biasing for PlacesBar Google Places API requests
	/// </summary>
	public class LocationBias
	{
		/// <summary>
		/// The latitude.
		/// </summary>
		public readonly double latitude;

		/// <summary>
		/// The longitude.
		/// </summary>
		public readonly double longitude;

		/// <summary>
		/// The radius.
		/// </summary>
		public readonly int radius;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DurianCode.PlacesSearchBar.LocationBias"/> class.
		/// </summary>
		/// <param name="latitude">Latitude.</param>
		/// <param name="longitude">Longitude.</param>
		/// <param name="radius">Radius.</param>
		public LocationBias(double latitude, double longitude, int radius)
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.radius = radius;
		}

		/// <summary>
		/// Returns a formatted <see cref="T:System.String"/> 
		/// that represents the current <see cref="T:DurianCode.PlacesSearchBar.LocationBias"/> for PlacesBar API calls.
		/// </summary>
		/// <returns>A formatted <see cref="T:System.String"/> 
		/// that represents the current <see cref="T:DurianCode.PlacesSearchBar.LocationBias"/> for PlacesBar API calls..</returns>
		public override string ToString()
		{
			return $"&location={latitude},{longitude}&radius={radius}";
		}
	}

}
