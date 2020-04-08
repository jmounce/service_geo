namespace Geo.Service.Model
{
	public class Country
	{
		public const string CountryUs = "US";

		public Country(string countryIso, string countryName)
		{
			CountryIso = countryIso;
			CountryName = countryName;
		}

		public string CountryIso { get; internal set; }
		public string CountryName { get; internal set; }
	}
}