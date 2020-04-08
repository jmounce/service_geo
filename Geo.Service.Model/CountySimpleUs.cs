namespace Geo.Service.Model
{
	/// <summary>
	/// US County with no references
	/// </summary>
	public class CountySimpleUs
	{
		public CountySimpleUs(string countyName, string countyFips, string fullFips, string stateAbbreviation)
		{
			CountyName = countyName;
			CountyFips = countyFips;
			FullFips = fullFips;
			StateAbbreviation = stateAbbreviation;
		}

		/// <summary>
		/// County name
		/// </summary>
		public string CountyName { get; internal set; }
		/// <summary>
		/// 3-digit local FIPS code
		/// </summary>
		public string CountyFips { get; internal set; }
		/// <summary>
		/// 5-digit full FIPS code (unique)
		/// </summary>
		public string FullFips { get; internal set; }
		/// <summary>
		/// US State Abbreviation
		/// </summary>
		public string StateAbbreviation { get; internal set; }
	}
}