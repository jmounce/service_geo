namespace Geo.Service.Client
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Inflection.Rest.Client;
	using Model;
	using Model.Exceptions;

	/// <summary>
	/// Client for Geographical service
	/// </summary>
	public class GeoServiceClient : RestClientBase<GeoServiceException>, IGeoServiceClient
	{
		/// <summary>
		/// Gets US States as a static method
		/// </summary>
		/// <returns></returns>
		public static IReadOnlyList<StateUs> GetStatesUs()
		{
			return GeoServiceClientStaticData.StatesUs;
		}

		public Uri BaseUrl { get; }
		public int TimeoutInMs { get; }

		public GeoServiceClient(Uri baseUrl, int timeoutInMs)
		{
			BaseUrl = baseUrl;
			TimeoutInMs = timeoutInMs;
		}

		/// <inheritdoc />
		protected override RestClient<GeoServiceException> CreateMyClient()
		{
			return RestClient<GeoServiceException>.Create(BaseUrl.ToString(), new RestClientOptions
			{
				TimeoutInMilliseconds = TimeoutInMs
			});

		}

		public async Task<IList<StateUs>> GetStatesAll()
		{
			//			return await MyClient.ReadResourceAsync<IList<StateUs>>("/UsState");
			return await Task.FromResult(GeoServiceClientStaticData.StatesUs);
		}
		public async Task<IList<StateUs>> GetStatesByZipCode(string zip)
		{
			return await MyClient.ReadResourceAsync<IList<StateUs>>($"/UsState?zip={zip}");
		}
		public async Task<IList<StateUs>> GetStatesByCounty(string fipsCounty)
		{
			return await MyClient.ReadResourceAsync<IList<StateUs>>($"/UsState?fipsCounty={fipsCounty}");
		}
		public async Task<StateUs> GetStateByAbbreviation(string abbreviation)
		{
			//			return await MyClient.ReadResourceAsync<StateUs>($"/UsState/{abbreviation}");
			return await Task.FromResult(GeoServiceClientStaticData.StatesUs.FirstOrDefault(_ => abbreviation.Equals(_.Abbreviation, StringComparison.OrdinalIgnoreCase)));
		}
		public async Task<bool> ExistsStateByAbbreviation(string abbreviation)
		{
			//			return await MyClient.ExistsAsync($"/UsState/{abbreviation}");
			return await Task.FromResult(GeoServiceClientStaticData.StatesUs.Any(_ => abbreviation.Equals(_.Abbreviation, StringComparison.OrdinalIgnoreCase)));
		}
		public async Task<StateUs> GetStateByName(string name, bool exactNameMatch = false)
		{
			//			return await MyClient.ReadResourceAsync<StateUs>($"/UsState?name={name}&exactNameMatch={exactNameMatch}");
			return await Task.FromResult(GeoServiceClientStaticData.StatesUs.FirstOrDefault(_ => exactNameMatch ? name.Equals(_.Name, StringComparison.OrdinalIgnoreCase) : _.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase)));
		}
		public async Task<bool> ExistsStateByName(string name, bool exactNameMatch = false)
		{
			//			return await MyClient.ExistsAsync($"/UsState?name={name}&exactNameMatch={exactNameMatch}");
			return await Task.FromResult(GeoServiceClientStaticData.StatesUs.Any(_ => exactNameMatch ? name.Equals(_.Name, StringComparison.OrdinalIgnoreCase) : _.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase)));
		}

		public async Task<IList<CountyUs>> GetCountiesAll()
		{
			return await MyClient.ReadResourceAsync<IList<CountyUs>>("/UsCounty");
		}
		public async Task<IList<CountyUs>> GetCountiesByZipCode(string zip)
		{
			return await MyClient.ReadResourceAsync<IList<CountyUs>>($"/UsCounty?zip={zip}");
		}
		public async Task<IList<CountyUs>> GetCountiesByName(string name, bool exactNameMatch = false)
		{
			return await MyClient.ReadResourceAsync<IList<CountyUs>>($"/UsCounty?name={name}&exactNameMatch={exactNameMatch}");
		}
		public async Task<bool> ExistsCountyByName(string name, bool exactNameMatch = false)
		{
			return await MyClient.ExistsAsync($"/UsCounty?name={name}&exactNameMatch={exactNameMatch}");
		}
		public async Task<CountyUs> GetCountyByStateAndName(string name, string abbreviationState, bool exactNameMatch = false)
		{
			return await MyClient.ReadResourceAsync<CountyUs>($"/UsCounty?name={name}&isoState={abbreviationState}&exactNameMatch={exactNameMatch}");
		}
		public async Task<bool> ExistsCountyByStateAndName(string name, string abbreviationState, bool exactNameMatch = false)
		{
			return await MyClient.ExistsAsync($"/UsCounty?name={name}&isoState={abbreviationState}&exactNameMatch={exactNameMatch}");
		}

		public async Task<IList<CountyUs>> GetCountiesByState(string abbreviationState)
		{
			return await MyClient.ReadResourceAsync<IList<CountyUs>>($"/UsCounty?isoState={abbreviationState}");
		}
		public async Task<CountyUs> GetCountyByFips(string fips)
		{
			return await MyClient.ReadResourceAsync<CountyUs>($"/UsCounty/{fips}");
		}
		public async Task<bool> ExistsCountyByFips(string fips)
		{
			return await MyClient.ExistsAsync($"/UsCounty/{fips}");
		}

		public async Task<IList<ZipCodeUs>> GetZipCodesByCity(string idCity)
		{
			return await MyClient.ReadResourceAsync<IList<ZipCodeUs>>($"/UsZipCode?idCity={idCity}");
		}
		public async Task<IList<ZipCodeUs>> GetZipCodesByCounty(string fipsCounty)
		{
			return await MyClient.ReadResourceAsync<IList<ZipCodeUs>>($"/UsZipCode?fipsCounty={fipsCounty}");
		}
		public async Task<IList<ZipCodeUs>> GetZipCodesByState(string abbreviationState)
		{
			return await MyClient.ReadResourceAsync<IList<ZipCodeUs>>($"/UsZipCode?isoState={abbreviationState}");
		}
		public async Task<IList<ZipCodeUs>> GetZipCodesByCountyAndState(string fipsCounty, string abbreviationState)
		{
			return await MyClient.ReadResourceAsync<IList<ZipCodeUs>>($"/UsZipCode?fipsCounty={fipsCounty}&isoState={abbreviationState}");
		}
		public async Task<ZipCodeUs> GetZipCodeByZip(string zip)
		{
			return await MyClient.ReadResourceAsync<ZipCodeUs>($"/UsZipCode/{zip}");
		}
		public async Task<bool> ExistsZipCodeByZip(string zip)
		{
			return await MyClient.ExistsAsync($"/UsZipCode/{zip}");
		}

		public async Task<IList<CityUs>> GetCitiesAll()
		{
			return await MyClient.ReadResourceAsync<IList<CityUs>>("/UsCity");
		}
		public async Task<IList<CityUs>> GetCitiesByName(string name, bool exactNameMatch = false)
		{
			return await MyClient.ReadResourceAsync<IList<CityUs>>($"/UsCity?name={name}&exactNameMatch={exactNameMatch}");
		}
		public async Task<bool> ExistsCityByName(string name, bool exactNameMatch = false)
		{
			return await MyClient.ExistsAsync($"/UsCity?name={name}&exactNameMatch={exactNameMatch}");
		}
		public async Task<CityUs> GetCityByStateAndName(string name, string abbreviationState, bool exactNameMatch = false)
		{
			return await MyClient.ReadResourceAsync<CityUs>($"/UsCity?name={name}&isoState={abbreviationState}&exactNameMatch={exactNameMatch}");
		}
		public async Task<bool> ExistsCityByStateAndName(string name, string abbreviationState, bool exactNameMatch = false)
		{
			return await MyClient.ExistsAsync($"/UsCity?name={name}&isoState={abbreviationState}&exactNameMatch={exactNameMatch}");
		}
		public async Task<IList<CityUs>> GetCitiesByCounty(string fipsCounty)
		{
			return await MyClient.ReadResourceAsync<IList<CityUs>>($"/UsCity?fipsCounty={fipsCounty}");
		}
		public async Task<IList<CityUs>> GetCitiesByState(string abbreviationState)
		{
			return await MyClient.ReadResourceAsync<IList<CityUs>>($"/UsCity?isoState={abbreviationState}");
		}
		public async Task<IList<CityUs>> GetCitiesByZipCode(string zip)
		{
			return await MyClient.ReadResourceAsync<IList<CityUs>>($"/UsCity?zip={zip}");
		}
		public async Task<CityUs> GetCityById(string idCity)
		{
			return await MyClient.ReadResourceAsync<CityUs>($"/UsCity/{idCity}");
		}
		public async Task<bool> ExistsCityById(string idCity)
		{
			return await MyClient.ExistsAsync($"/UsCity/{idCity}");
		}
	}
}
