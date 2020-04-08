namespace Geo.Service.Data.City
{
	using System.Collections.Generic;
	using System.Linq;
	using County;
	using Inflection.Common;
	using Model;
	using State;
	using ZipCode;
	using Country = Country.Country;

	/// <summary>
	/// City
	/// </summary>
	public class City
	{
		public long CityId { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public string StateIso { get; set; }
		public State State { get; set; }
		public string CountryIso { get; set; }
		public Country Country { get; set; }
		public IList<CityCounty> Counties { get; set; }
		public IList<ZipCodeCity> ZipCodes { get; set; }

		public void AttachCountry(bool navigate)
		{
			Country = Country.CountryMap[CountryIso];
			State?.AttachCountry(false);
			if (navigate)
			{
				Counties.ForEach(_ => _.County?.AttachCountry(false));
				ZipCodes.ForEach(_ => _.ZipCode?.AttachCountry(false));
			}
		}

		public static CityUs ToModel(City city)
		{
			if (city == null) return null;

			return new CityUs(
				city.Id, 
				city.Name,
				County.ToSimpleModel(city.Counties.Select(_ => _.County)),
				ZipCode.ToSimpleModel(city.ZipCodes.Select(_ => _.ZipCode)),
				State.ToSimpleModel(city.State));
		}

		public static IEnumerable<CityUs> ToModel(IEnumerable<City> cities)
		{
			return cities.Select(ToModel);
		}

		public static CitySimpleUs ToSimpleModel(City city)
		{
			if (city == null) return null;

			return new CitySimpleUs(city.Id, city.Name, city.StateIso);
		}

		public static IEnumerable<CitySimpleUs> ToSimpleModel(IEnumerable<City> cities)
		{
			return cities.Select(ToSimpleModel);
		}
	}
}