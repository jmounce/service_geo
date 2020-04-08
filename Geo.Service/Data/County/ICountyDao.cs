namespace Geo.Service.Data.County
{
	using System.Collections.Generic;

	/// <summary>
	/// Interface for accessing County-related data
	/// </summary>
	public interface ICountyDao
	{
		/// <summary>
		/// Returns whether a County exists by Full FIPS
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="fipsCounty"></param>
		/// <returns></returns>
		bool ExistsByFips(string isoCountry, string fipsCounty);

		/// <summary>
		/// Gets a County by Full FIPS
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="fipsCounty"></param>
		/// <returns></returns>
		County GetByFips(string isoCountry, string fipsCounty);

		/// <summary>
		/// Determines whether a County exists by Name
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		bool ExistsByName(string isoCountry, string name, bool exactNameMatch);

		/// <summary>
		/// Determines whether a County exists by Name
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="isoState"></param>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		bool ExistsByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch);

		/// <summary>
		/// Gets a County by Name
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		IList<County> GetByName(string isoCountry, string name, bool exactNameMatch);

		/// <summary>
		/// Gets a County by State ISO and Name
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="isoState"></param>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		County GetByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch);

		/// <summary>
		/// Gets all of the counties for a country.
		/// </summary>
		/// <param name="isoCountry">The country.</param>
		/// <returns></returns>
		IList<County> GetCounties(string isoCountry);

		/// <summary>
		/// Gets all of the counties for a state in a country.
		/// </summary>
		/// <param name="isoCountry">The country.</param>
		/// <param name="isoState"></param>
		/// <returns></returns>
		IList<County> GetByState(string isoCountry, string isoState);

		/// <summary>
		/// Gets all of the counties for a zip in a country.
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="zip"></param>
		/// <returns></returns>
		IList<County> GetByZip(string isoCountry, string zip);
	}
}