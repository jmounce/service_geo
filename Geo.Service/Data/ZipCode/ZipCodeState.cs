namespace Geo.Service.Data.ZipCode
{
	using State;

	public class ZipCodeState
	{
		public long ZipCodeStateId { get; set; }
		public State State { get; set; }
		public ZipCode ZipCode { get; set; }
	}
}