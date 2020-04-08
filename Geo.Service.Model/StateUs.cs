namespace Geo.Service.Model
{
	/// <summary>
	/// US State
	/// </summary>
	public class StateUs
	{
		public StateUs(string name, string abbreviation, bool isTerritory, Country country)
		{
			Name = name;
			Abbreviation = abbreviation;
			IsTerritory = isTerritory;
			Country = country;
		}

		/// <summary>
		/// State Name
		/// </summary>
		public string Name { get; internal set; }
		/// <summary>
		/// State ISO-3166 code
		/// </summary>
		public string Abbreviation { get; internal set; }
		/// <summary>
		/// Is a territory or not?
		/// </summary>
		public bool IsTerritory { get; internal set; }
		/// <summary>
		/// Country
		/// </summary>
		public Country Country { get; internal set; }

		/// <summary>
		/// Converts to a flattened type
		/// </summary>
		/// <returns></returns>
		public StateSimpleUs ToSimple()
		{
			return new StateSimpleUs(Name, Abbreviation, IsTerritory, Country.CountryIso);
		}
	}
}