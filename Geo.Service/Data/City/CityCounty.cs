namespace Geo.Service.Data.City
{
	using County;

	public class CityCounty
	{
		public long CityCountyId { get; set; }
		public County County { get; set; }
		public City City { get; set; }
	}
}