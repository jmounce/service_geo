namespace Geo.Service.Data.ZipCode
{
	using County;

	public class ZipCodeCounty
	{
		public long ZipCodeCountyId { get; set; }
		public County County { get; set; }
		public ZipCode ZipCode { get; set; }
	}
}