namespace Geo.Service.Data.State
{
	using System.Collections.Generic;
	using System.Linq;
	using Model;
	using ZipCode;
	using Country = Country.Country;

	public class State
	{
		public int StateId { get; set; }
		public string StateIso { get; set; }
		public string Name { get; set; }
		public string Fips { get; set; }
		public bool IsTerritory { get; set; }
		public string CountryIso { get; set; }
		public Country Country { get; set; }
		public List<ZipCodeState> ZipCodes { get; set; }

		public void AttachCountry(bool navigate)
		{
			Country = Country.CountryMap[CountryIso];
		}

		/// <summary>
		/// Convert internal State to external model
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public static StateUs ToModel(State state)
		{
			if (state == null) return null;

			return new StateUs(
				state.Name, 
				state.StateIso, 
				state.IsTerritory, 
				Country.ToModel(state.Country));
		}

		/// <summary>
		/// Convert internal States to external model
		/// </summary>
		/// <param name="states"></param>
		/// <returns></returns>
		public static IEnumerable<StateUs> ToModel(IEnumerable<State> states)
		{
			return states?.Select(ToModel);
		}

		public static StateSimpleUs ToSimpleModel(State state)
		{
			if (state == null) return null;

			return new StateSimpleUs(
				state.Name,
				state.StateIso,
				state.IsTerritory,
				state.CountryIso);
		}
		public static IEnumerable<StateSimpleUs> ToSimpleModel(IEnumerable<State> states)
		{
			return states?.Select(ToSimpleModel);
		}
	}
}