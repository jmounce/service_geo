namespace Geo.Service.Model
{
	using System.Collections.Generic;

	/// <summary>
	/// City
	/// </summary>
	public class CityUs
	{
		public CityUs(string id, string name, IEnumerable<CountySimpleUs> counties, IEnumerable<ZipCodeSimpleUs> zipCodes, StateSimpleUs state)
		{
			Id = id;
			Name = name;
			Counties = counties;
			ZipCodes = zipCodes;
			State = state;
		}

		/// <summary>
		/// City ID (unique)
		/// </summary>
		public string Id { get; internal set; }
		/// <summary>
		/// City name
		/// </summary>
		public string Name { get; internal set; }
		/// <summary>
		/// US Counties this city spans
		/// </summary>
		public IEnumerable<CountySimpleUs> Counties { get; internal set; }
		/// <summary>
		/// Zip codes in the city
		/// </summary>
		public IEnumerable<ZipCodeSimpleUs> ZipCodes { get; internal set; }
		/// <summary>
		/// US State
		/// </summary>
		public StateSimpleUs State { get; internal set; }

		/// <summary>
		/// Converts to a flattened type
		/// </summary>
		/// <returns></returns>
		public CitySimpleUs ToSimple()
		{
			return new CitySimpleUs(Id, Name, State.Abbreviation);
		}
	}
}
