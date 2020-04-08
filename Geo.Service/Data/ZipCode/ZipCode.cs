namespace Geo.Service.Data.ZipCode
{
	using System.Collections.Generic;
	using System.Linq;
	using City;
	using County;
	using Inflection.Common;
	using Model;
	using State;
	using Country = Country.Country;

	public class ZipCode
	{
		public long ZipCodeId { get; set; }
		public string ZipCode5 { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
		public string CountryIso { get; set; }
		public Country Country { get; set; }
		public IList<ZipCodeCity> Cities { get; set; }
		public IList<ZipCodeCounty> Counties { get; set; }
		public IList<ZipCodeState> States { get; set; }

		public void AttachCountry(bool navigate)
		{
			Country = Country.CountryMap[CountryIso];
			if (navigate)
			{
				States.ForEach(_ => _.State?.AttachCountry(false));
				Counties.ForEach(_ => _.County?.AttachCountry(false));
				Cities.ForEach(_ => _.City?.AttachCountry(false));
			}
		}

		public static ZipCodeUs ToModel(ZipCode zipCode)
		{
			if (zipCode == null) return null;

			return new ZipCodeUs(
				zipCode.ZipCode5,
				zipCode.Latitude,
				zipCode.Longitude,
				City.ToSimpleModel(zipCode.Cities.Select(_ => _.City)),
				County.ToSimpleModel(zipCode.Counties.Select(_ => _.County)),
				State.ToSimpleModel(zipCode.States.Select(_ => _.State)),
				Country.ToModel(zipCode.Country));
		}

		public static IEnumerable<ZipCodeUs> ToModel(IEnumerable<ZipCode> zipCodes)
		{
			return zipCodes.Select(ToModel);
		}

		public static ZipCodeSimpleUs ToSimpleModel(ZipCode zipCode)
		{
			if (zipCode == null) return null;

			return new ZipCodeSimpleUs(zipCode.ZipCode5, zipCode.Latitude, zipCode.Longitude);
		}

		public static IEnumerable<ZipCodeSimpleUs> ToSimpleModel(IEnumerable<ZipCode> zipCodes)
		{
			return zipCodes.Select(ToSimpleModel);
		}
	}
}