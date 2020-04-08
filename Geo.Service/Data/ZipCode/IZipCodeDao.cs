namespace Geo.Service.Data.ZipCode
{
	using System.Collections.Generic;

	/// <summary>
	/// DAO for ZipCodes
	/// </summary>
	public interface IZipCodeDao
	{
		/// <summary>
		/// Gets ZipCodes by Zip
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="zip">zip code</param>
		IList<ZipCode> GetByZip(string isoCountry, string zip);

		/// <summary>
		/// Gets ZipCodes by Country
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <returns></returns>
		IList<ZipCode> GetByCountry(string isoCountry);

		/// <summary>
		/// Gets ZipCodes by City
		/// </summary>
		/// <param name="idCity"></param>
		IList<ZipCode> GetByCity(string idCity);

		/// <summary>
		/// Gets ZipCodes by County full FIPS
		/// </summary>
		/// <param name="fipsCounty">county name</param>
		IList<ZipCode> GetByCounty(string fipsCounty);

		/// <summary>
		/// Gets ZipCodes by State ISO-3166
		/// </summary>
		/// <param name="isoState"></param>
		IList<ZipCode> GetByState(string isoState);

		/// <summary>
		/// Determines whether a ZipCode exists by Zip
		/// </summary>
		/// <param name="isoCountry"></param>
		/// <param name="zip"></param>
		/// <returns></returns>
		bool ExistsByZip(string isoCountry, string zip);
	}
}