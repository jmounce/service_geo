namespace Geo.Service.Data.City
{
	using System.Collections.Generic;

	public interface ICityDao
	{
		IList<City> GetByCountry(string isoCountry);
		City GetById(string id);
		bool ExistsById(string id);
		IList<City> GetByName(string isoCountry, string name, bool exactNameMatch);
		bool ExistsByName(string isoCountry, string name, bool exactNameMatch);
		IList<City> GetByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch);
		bool ExistsByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch);
		IList<City> GetByCounty(string isoCountry, string fipsCounty);
		IList<City> GetByState(string isoCountry, string isoState);
		IList<City> GetByZip(string isoCountry, string zip);
	}
}