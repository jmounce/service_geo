namespace Geo.Service.Client
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Model;

	/// <summary>
	/// Client for Geographical service
	/// </summary>
	public interface IGeoServiceClient
	{
		/// <summary>
		/// Gets all US States
		/// </summary>
		/// <returns></returns>
		Task<IList<StateUs>> GetStatesAll();
		/// <summary>
		/// Gets US States covered by the given Zip Code
		/// </summary>
		/// <param name="zip"></param>
		/// <returns></returns>
		Task<IList<StateUs>> GetStatesByZipCode(string zip);
		/// <summary>
		/// Gets US States by County FIPS
		/// </summary>
		/// <param name="fipsCounty"></param>
		/// <returns></returns>
		Task<IList<StateUs>> GetStatesByCounty(string fipsCounty);
		/// <summary>
		/// Gets US State by ISO-3166
		/// </summary>
		/// <param name="abbreviation"></param>
		/// <returns></returns>
		Task<StateUs> GetStateByAbbreviation(string abbreviation);
		/// <summary>
		/// Determines whether a US State exists by ISO-3166
		/// </summary>
		/// <param name="abbreviation"></param>
		/// <returns></returns>
		Task<bool> ExistsStateByAbbreviation(string abbreviation);
		/// <summary>
		/// Gets US State by name (using either an exact match or starting text)
		/// </summary>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<StateUs> GetStateByName(string name, bool exactNameMatch = false);
		/// <summary>
		/// Determines whether a US State exists by name (using either an exact match or starting text)
		/// </summary>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<bool> ExistsStateByName(string name, bool exactNameMatch = false);

		/// <summary>
		/// Gets all US Counties
		/// </summary>
		/// <returns></returns>
		Task<IList<CountyUs>> GetCountiesAll();
		/// <summary>
		/// Gets US Counties covered by the given Zip Code
		/// </summary>
		/// <param name="zip"></param>
		/// <returns></returns>
		Task<IList<CountyUs>> GetCountiesByZipCode(string zip);
		/// <summary>
		/// Gets all US Counties with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<IList<CountyUs>> GetCountiesByName(string name, bool exactNameMatch = false);
		/// <summary>
		/// Determines whether a US County exists with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<bool> ExistsCountyByName(string name, bool exactNameMatch = false);
		/// <summary>
		/// Gets the US County with the given name in a state
		/// </summary>
		/// <param name="name"></param>
		/// <param name="abbreviationState"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<CountyUs> GetCountyByStateAndName(string name, string abbreviationState, bool exactNameMatch = false);
		/// <summary>
		/// Determines whether a US County exists within the given state with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <param name="abbreviationState"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<bool> ExistsCountyByStateAndName(string name, string abbreviationState, bool exactNameMatch = false);
		/// <summary>
		/// Gets all US Counties within the given state
		/// </summary>
		/// <param name="abbreviationState"></param>
		/// <returns></returns>
		Task<IList<CountyUs>> GetCountiesByState(string abbreviationState);
		/// <summary>
		/// Gets a US County by full FIPS code
		/// </summary>
		/// <param name="fips"></param>
		/// <returns></returns>
		Task<CountyUs> GetCountyByFips(string fips);
		/// <summary>
		/// Determines whether a US County exists by full FIPS code
		/// </summary>
		/// <param name="fips"></param>
		/// <returns></returns>
		Task<bool> ExistsCountyByFips(string fips);

		/// <summary>
		/// Gets all US Zip Codes within the given city
		/// </summary>
		/// <param name="idCity"></param>
		/// <returns></returns>
		Task<IList<ZipCodeUs>> GetZipCodesByCity(string idCity);
		/// <summary>
		/// Gets all US Zip Codes in the given County
		/// </summary>
		/// <param name="fipsCounty"></param>
		/// <returns></returns>
		Task<IList<ZipCodeUs>> GetZipCodesByCounty(string fipsCounty);
		/// <summary>
		/// Gets all US Zip Codes in the given State
		/// </summary>
		/// <param name="abbreviationState"></param>
		/// <returns></returns>
		Task<IList<ZipCodeUs>> GetZipCodesByState(string abbreviationState);
		/// <summary>
		/// Gets all US Zip Codes in the given County and State
		/// </summary>
		/// <param name="fipsCounty"></param>
		/// <param name="abbreviationState"></param>
		/// <returns></returns>
		Task<IList<ZipCodeUs>> GetZipCodesByCountyAndState(string fipsCounty, string abbreviationState);

		/// <summary>
		/// Gets US Zip Code information by zip code
		/// </summary>
		/// <param name="zip"></param>
		/// <returns></returns>
		Task<ZipCodeUs> GetZipCodeByZip(string zip);
		/// <summary>
		/// Determines whether a US Zip Code exists
		/// </summary>
		/// <param name="zip"></param>
		/// <returns></returns>
		Task<bool> ExistsZipCodeByZip(string zip);

		/// <summary>
		/// Gets all US Cities
		/// </summary>
		/// <returns></returns>
		Task<IList<CityUs>> GetCitiesAll();
		/// <summary>
		/// Gets all US Cities with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<IList<CityUs>> GetCitiesByName(string name, bool exactNameMatch = false);
		/// <summary>
		/// Determines whether a US City exists with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<bool> ExistsCityByName(string name, bool exactNameMatch = false);
		/// <summary>
		/// Gets the US City with the given name in a state
		/// </summary>
		/// <param name="name"></param>
		/// <param name="abbreviationState"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<CityUs> GetCityByStateAndName(string name, string abbreviationState, bool exactNameMatch = false);

		/// <summary>
		/// Determines whether a US City exists with the given name within the given state
		/// </summary>
		/// <param name="name"></param>
		/// <param name="abbreviationState"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		Task<bool> ExistsCityByStateAndName(string name, string abbreviationState, bool exactNameMatch = false);
		/// <summary>
		/// Gets all US Cities within the given state
		/// </summary>
		/// <param name="abbreviationState"></param>
		/// <returns></returns>
		Task<IList<CityUs>> GetCitiesByState(string abbreviationState);
		/// <summary>
		/// Gets all US Cities within the given county
		/// </summary>
		/// <param name="fipsCounty"></param>
		/// <returns></returns>
		Task<IList<CityUs>> GetCitiesByCounty(string fipsCounty);
		/// <summary>
		/// Gets US Cities covered by the given Zip Code
		/// </summary>
		/// <param name="zip"></param>
		/// <returns></returns>
		Task<IList<CityUs>> GetCitiesByZipCode(string zip);
		/// <summary>
		/// Gets a US City by ID
		/// </summary>
		/// <param name="idCity"></param>
		/// <returns></returns>
		Task<CityUs> GetCityById(string idCity);
		/// <summary>
		/// Determines whether a US City exists by ID
		/// </summary>
		/// <param name="idCity"></param>
		/// <returns></returns>
		Task<bool> ExistsCityById(string idCity);
	}
}