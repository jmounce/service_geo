namespace Geo.Service.Data.ZipCode
{
	using City;

	public class ZipCodeCity
	{
		public long ZipCodeCityId { get; set; }
		public City City { get; set; }
		public ZipCode ZipCode { get; set; }
	}
}