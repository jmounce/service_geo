namespace Geo.Service.Data.County
{
	using System.Collections.Generic;
	using System.Linq;
	using City;
	using Inflection.Common;
	using Model;
	using State;
	using ZipCode;
	using Country = Country.Country;

	public class County
	{
		public int CountyId { get; set; }
		public string Name { get; set; }
		public string Fips { get; set; }
		public string FullFips { get; set; }
		public string StateIso { get; set; }
		public State State { get; set; }
		public string CountryIso { get; set; }
		public Country Country { get; set; }
		public IList<CityCounty> Cities { get; set; }
		public IList<ZipCodeCounty> ZipCodes { get; set; }

		public void AttachCountry(bool navigate)
		{
			Country = Country.CountryMap[CountryIso];
			State?.AttachCountry(false);
			if (navigate)
			{
				Cities.ForEach(_ => _.City?.AttachCountry(false));
				ZipCodes.ForEach(_ => _.ZipCode?.AttachCountry(false));
			}
		}

		public static IEnumerable<CountyUs> ToModel(IEnumerable<County> counties)
		{
			return counties.Select(ToModel);
		}

		public static CountyUs ToModel(County county)
		{
			if (county == null) return null;

			return new CountyUs(
				county.Name,
				county.Fips,
				county.FullFips,
				State.ToSimpleModel(county.State),
				City.ToSimpleModel(county.Cities.Select(_ => _.City)),
				ZipCode.ToSimpleModel(county.ZipCodes.Select(_ => _.ZipCode))
			);
		}

		public static CountySimpleUs ToSimpleModel(County county)
		{
			if (county == null) return null;

			return new CountySimpleUs(
				county.Name,
				county.Fips,
				county.FullFips,
				county.StateIso
			);
		}

		public static IEnumerable<CountySimpleUs> ToSimpleModel(IEnumerable<County> counties)
		{
			return counties.Select(ToSimpleModel);
		}
	}
}