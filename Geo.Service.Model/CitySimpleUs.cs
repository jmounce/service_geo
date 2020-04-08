namespace Geo.Service.Model
{
	/// <summary>
	/// City with no references
	/// </summary>
	public class CitySimpleUs
	{
		public CitySimpleUs(string id, string name, string stateAbbreviation)
		{
			Id = id;
			Name = name;
			StateAbbreviation = stateAbbreviation;
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
		/// US State abbreviation
		/// </summary>
		public string StateAbbreviation { get; internal set; }
	}
}
