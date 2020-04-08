namespace Geo.Service.Data.CityAlias
{
	using City;

	/// <summary>
	/// City Alias name
	/// </summary>
	public class CityAlias
	{
		public long CityAliasId { get; set; }
		public string Alias { get; set; }
		public string Abbreviation { get; set; }
		public City City { get; set; }
	}
}