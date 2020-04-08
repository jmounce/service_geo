namespace Geo.Service.Controllers
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using Data.ZipCode;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Model;

	[ApiController]
	[Route("[controller]")]
	public class UsZipCodeController : ControllerBase
	{
		private readonly ILogger<UsZipCodeController> _logger;
		private readonly IZipCodeDao _daoZipCode;

		public UsZipCodeController(
			ILogger<UsZipCodeController> logger,
			IZipCodeDao daoZipCode)
		{
			_logger = logger;
			_daoZipCode = daoZipCode;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<ZipCodeUs>> ZipCodeByQuery(string fipsCounty = null, string isoState = null, string idCity = null)
		{
			IList<ZipCode> zipCodes;

			if (!string.IsNullOrWhiteSpace(fipsCounty))
			{
				zipCodes = _daoZipCode.GetByCounty(fipsCounty);
				if (zipCodes != null && zipCodes.Any())
				{
					return Ok(ZipCode.ToModel(zipCodes));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(isoState))
			{
				string isoStateToUse = isoState.ToUpper();
				zipCodes = _daoZipCode.GetByState(isoStateToUse);
				if (zipCodes != null && zipCodes.Any())
				{
					return Ok(ZipCode.ToModel(zipCodes));
				}
				return NotFound();
			}

			if (!string.IsNullOrWhiteSpace(idCity))
			{
				zipCodes = _daoZipCode.GetByCity(idCity);
				if (zipCodes != null && zipCodes.Any())
				{
					return Ok(ZipCode.ToModel(zipCodes));
				}
				return NotFound();
			}

			zipCodes = _daoZipCode.GetByCountry(Country.CountryUs);
			if (zipCodes != null && zipCodes.Any())
			{
				return Ok(ZipCode.ToModel(zipCodes));
			}
			return NotFound();
		}

		[HttpGet]
		[Route("{zip}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ZipCodeUs> ZipCodeByZip(string zip)
		{
			IList<ZipCode> zipCode = _daoZipCode.GetByZip(Country.CountryUs, zip);
			IList<ZipCodeUs> zipCodeByZip = ZipCode.ToModel(zipCode).ToList();
			if (zipCodeByZip.Any())
			{
				Debug.Assert(zipCodeByZip.Count == 1);
				return Ok(zipCodeByZip.First());
			}
			return NotFound();
		}

		[HttpHead]
		[Route("{zip}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult ZipCodeExistsByZip(string zip)
		{
			bool exists = _daoZipCode.ExistsByZip(Country.CountryUs, zip);
			if (exists)
			{
				return Ok();
			}

			return NotFound();
		}
	}
}