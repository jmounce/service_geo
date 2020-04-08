namespace Geo.Service.Model
{
	/// <summary>
	/// US State with no references
	/// </summary>
	public class StateSimpleUs
	{
		public StateSimpleUs(string name, string abbreviation, bool isTerritory, string countryCode)
		{
			Name = name;
			Abbreviation = abbreviation;
			IsTerritory = isTerritory;
			CountryCode = countryCode;
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
		/// Country ISO code
		/// </summary>
		public string CountryCode { get; internal set; }
	}
}