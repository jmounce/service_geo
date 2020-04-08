namespace Geo.Service.Controllers
{
	using System.Collections.Generic;
	using Data.County;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Model;
	using System.Linq;

	[ApiController]
	[Route("[controller]")]
	public class UsCountyController : ControllerBase
	{
		private readonly ILogger<UsCountyController> _logger;
		private readonly ICountyDao _daoCounty;

		public UsCountyController(
			ILogger<UsCountyController> logger,
			ICountyDao daoCounty)
		{
			_logger = logger;
			_daoCounty = daoCounty;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<CountyUs>> CountyByQuery(string name = null, string zip = null, string isoState = null, bool exactNameMatch = false)
		{
			IList<County> counties;

			if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(isoState))
			{
				County county = _daoCounty.GetByStateAndName(Country.CountryUs, isoState, name, exactNameMatch);
				if (county != null)
				{
					return Ok(County.ToModel(county));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				counties = _daoCounty.GetByName(Country.CountryUs, name, exactNameMatch);
				if (counties != null && counties.Any())
				{
					return Ok(County.ToModel(counties));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(zip))
			{
				counties = _daoCounty.GetByZip(Country.CountryUs, zip);
				if (counties != null && counties.Any())
				{
					return Ok(County.ToModel(counties));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(isoState))
			{
				counties = _daoCounty.GetByState(Country.CountryUs, isoState);
				if (counties != null && counties.Any())
				{
					return Ok(County.ToModel(counties));
				}
				return NotFound();
			}

			counties = _daoCounty.GetCounties(Country.CountryUs);
			if (counties != null && counties.Any())
			{
				return Ok(County.ToModel(counties));
			}
			return NotFound();
		}

		[HttpHead]
		[Route("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult CountyExistsByQuery(string name = null, string isoState = null, bool exactNameMatch = false)
		{
			if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(isoState))
			{
				bool exists = _daoCounty.ExistsByStateAndName(Country.CountryUs, isoState, name, exactNameMatch);
				if (exists)
				{
					return Ok();
				}
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				bool exists = _daoCounty.ExistsByName(Country.CountryUs, name, exactNameMatch);
				if (exists)
				{
					return Ok();
				}
			}

			return NotFound();
		}

		[HttpGet]
		[Route("{fips}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<CountyUs> CountyByFips(string fips)
		{
			County county = _daoCounty.GetByFips(Country.CountryUs, fips);
			if (county != null)
			{
				return Ok(County.ToModel(county));
			}
			return NotFound();
		}

		[HttpHead]
		[Route("{fips}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult CountyExistsByFips(string fips)
		{
			if (_daoCounty.ExistsByFips(Country.CountryUs, fips))
			{
				return Ok();
			}

			return NotFound();
		}
	}
}