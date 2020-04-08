namespace Geo.Service.Data.Country
{
	using System.Collections.Generic;

	public class Country
	{
		public static IDictionary<string, Country> CountryMap = new Dictionary<string, Country>
		{
			{ Model.Country.CountryUs, new Country { CountryIso = Model.Country.CountryUs, CountryName = "United States" } }
		};
		public string CountryIso { get; set; }
		public string CountryName { get; set; }

		public static Model.Country ToModel(Country country)
		{
			if (country == null) return null;

			return new Model.Country(country.CountryIso, country.CountryName);
		}
	}
}
