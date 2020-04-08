namespace Geo.Service.Model
{
	/// <summary>
	///  US Zip Code with no references
	/// </summary>
	public class ZipCodeSimpleUs
	{
		public ZipCodeSimpleUs(string zipCode, decimal? latitude, decimal? longitude)
		{
			ZipCode = zipCode;
			Latitude = latitude;
			Longitude = longitude;
		}

		/// <summary>
		/// 5-digit zip code
		/// </summary>
		public string ZipCode { get; internal set; }
		/// <summary>
		/// Latitude
		/// </summary>
		public decimal? Latitude { get; internal set; }
		/// <summary>
		/// Longitude
		/// </summary>
		public decimal? Longitude { get; internal set; }
	}
}