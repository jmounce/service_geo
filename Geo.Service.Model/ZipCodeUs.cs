namespace Geo.Service.Model
{
	using System.Collections.Generic;

	/// <summary>
	///  US Zip Code
	/// </summary>
	public class ZipCodeUs
	{
		public ZipCodeUs(string zipCode, decimal? latitude, decimal? longitude, IEnumerable<CitySimpleUs> cities, IEnumerable<CountySimpleUs> counties, IEnumerable<StateSimpleUs> states, Country country)
		{
			ZipCode = zipCode;
			Latitude = latitude;
			Longitude = longitude;
			Cities = cities;
			Counties = counties;
			States = states;
			Country = country;
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
		/// <summary>
		/// Cities this zip code spans
		/// </summary>
		public IEnumerable<CitySimpleUs> Cities { get; internal set; }
		/// <summary>
		/// Counties this zip code spans
		/// </summary>
		public IEnumerable<CountySimpleUs> Counties { get; internal set; }
		/// <summary>
		/// States this zip code spans
		/// </summary>
		public IEnumerable<StateSimpleUs> States { get; internal set; }
		///// <summary>
		///// Country this zip code is in
		///// </summary>
		public Country Country { get; internal set; }

		/// <summary>
		/// Converts to a flattened type
		/// </summary>
		/// <returns></returns>
		public ZipCodeSimpleUs ToSimple()
		{
			return new ZipCodeSimpleUs(ZipCode, Latitude, Longitude);
		}
	}
}