namespace Geo.Service.Model
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	/// <summary>
	/// US County
	/// </summary>
	public class CountyUs
	{
		public CountyUs(string countyName, string countyFips, string fullFips, StateSimpleUs state, IEnumerable<CitySimpleUs> cities, IEnumerable<ZipCodeSimpleUs> zipCodes)
		{
			CountyName = countyName;
			CountyFips = countyFips;
			FullFips = fullFips;
			State = state;
			Cities = cities;
			ZipCodes = zipCodes;
		}

		/// <summary>
		/// County name
		/// </summary>
		public string CountyName { get; internal set; }
		/// <summary>
		/// 3-digit local FIPS code
		/// </summary>
		public string CountyFips { get; internal set; }
		/// <summary>
		/// 5-digit full FIPS code (unique)
		/// </summary>
		public string FullFips { get; internal set; }
		/// <summary>
		/// US State
		/// </summary>
		public StateSimpleUs State { get; internal set; }
		/// <summary>
		/// Cities this county includes
		/// </summary>
		public IEnumerable<CitySimpleUs> Cities { get; internal set; }
		/// <summary>
		/// Zip codes this county includes
		/// </summary>
		public IEnumerable<ZipCodeSimpleUs> ZipCodes { get; internal set; }

		/// <summary>
		/// determines if passed value is a valid 5-digit "county" FIPS code
		/// </summary>
		/// <param name="fips"></param>
		/// <returns></returns>
		public static bool IsValidFullFIPS(string fips)
		{
			return !string.IsNullOrEmpty(fips) && Regex.IsMatch(fips, @"^\d{5}$");
		}

		/// <summary>
		/// Converts to a flattened type
		/// </summary>
		/// <returns></returns>
		public CountySimpleUs ToSimple()
		{
			return new CountySimpleUs(CountyName, CountyFips, FullFips, State.Abbreviation);
		}
	}
}