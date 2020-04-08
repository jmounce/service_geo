namespace Geo.Service.Controllers
{
	using System.Collections.Generic;
	using Data.City;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Model;
	using System.Linq;
	using Model.Exceptions;

	[ApiController]
	[Route("[controller]")]
	[ProducesErrorResponseType(typeof(GeoServiceException))]
	public class UsCityController : ControllerBase
	{
		private readonly ILogger<UsCityController> _logger;
		private readonly ICityDao _daoCity;

		public UsCityController(
			ILogger<UsCityController> logger,
			ICityDao daoCity)
		{
			_logger = logger;
			_daoCity = daoCity;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<CityUs>> CityByQuery(string name = null, string zip = null, string isoState = null, string fipsCounty = null, bool exactNameMatch = false)
		{
			IList<City> cities;

			if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(isoState))
			{
				cities = _daoCity.GetByStateAndName(Country.CountryUs, isoState, name, exactNameMatch);
				if (cities != null && cities.Any())
				{
					return Ok(City.ToModel(cities));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				cities = _daoCity.GetByName(Country.CountryUs, name, exactNameMatch);
				if (cities != null && cities.Any())
				{
					return Ok(City.ToModel(cities));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(fipsCounty))
			{
				cities = _daoCity.GetByCounty(Country.CountryUs, fipsCounty);
				if (cities != null && cities.Any())
				{
					return Ok(City.ToModel(cities));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(zip))
			{
				cities = _daoCity.GetByZip(Country.CountryUs, zip);
				if (cities != null && cities.Any())
				{
					return Ok(City.ToModel(cities));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(isoState))
			{
				cities = _daoCity.GetByState(Country.CountryUs, isoState);
				if (cities != null && cities.Any())
				{
					return Ok(City.ToModel(cities));
				}
				return NotFound();
			}

			cities = _daoCity.GetByCountry(Country.CountryUs);
			if (cities != null && cities.Any())
			{
				return Ok(City.ToModel(cities));
			}
			return NotFound();
		}

		[HttpHead]
		[Route("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult CityExistsByQuery(string name = null, string isoState = null, bool exactNameMatch = false)
		{
			if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(isoState))
			{
				bool exists = _daoCity.ExistsByStateAndName(Country.CountryUs, isoState, name, exactNameMatch);
				if (exists)
				{
					return Ok();
				}
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				bool exists = _daoCity.ExistsByName(Country.CountryUs, name, exactNameMatch);
				if (exists)
				{
					return Ok();
				}
			}

			return NotFound();
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<CityUs> CityById(string id)
		{
			City city = _daoCity.GetById(id);
			if (city != null)
			{
				return Ok(City.ToModel(city));
			}
			return NotFound();
		}

		[HttpHead]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult CityExistsById(string id)
		{
			if (_daoCity.ExistsById(id))
			{
				return Ok();
			}

			return NotFound();
		}
	}
}