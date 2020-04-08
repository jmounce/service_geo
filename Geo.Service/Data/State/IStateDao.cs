namespace Geo.Service.Data.State
{
	using System.Collections.Generic;

	/// <summary>
	/// Interface for accessing State-related data
	/// </summary>
	public interface IStateDao
	{
		/// <summary>
		/// Returns whether a State exists by ISO-3166
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="isoState"></param>
		/// <returns></returns>
		bool ExistsByIso(string isoCountry, string isoState);

		/// <summary>
		/// Gets a State by ISO-3166
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="isoState"></param>
		/// <returns></returns>
		State GetByIso(string isoCountry, string isoState);

		/// <summary>
		/// Gets all of the states for a zip code.
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="zip"></param>
		/// <returns></returns>
		IList<State> GetByZip(string isoCountry, string zip);

		/// <summary>
		/// Gets all of the states for a country.
		/// </summary>
		/// <param name="isoCountry">The country.</param>
		/// <returns></returns>
		IList<State> GetStates(string isoCountry);

		/// <summary>
		/// Gets all of the territory for a country.
		/// </summary>
		/// <param name="isoCountry">The country.</param>
		/// <returns></returns>
		IList<State> GetTerritories(string isoCountry);

		/// <summary>
		/// Gets a US State by name
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		IList<State> GetByName(string isoCountry, string name, bool exactNameMatch);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="name"></param>
		/// <param name="exactNameMatch"></param>
		/// <returns></returns>
		bool ExistsByName(string isoCountry, string name, bool exactNameMatch);
	}
}