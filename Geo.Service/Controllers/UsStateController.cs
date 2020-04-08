namespace Geo.Service.Controllers
{
	using System.Collections.Generic;
	using Data.County;
	using Data.State;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Model;
	using System.Linq;

	[ApiController]
	[Route("[controller]")]
	public class UsStateController : ControllerBase
	{
		private readonly ILogger<UsStateController> _logger;
		private readonly IStateDao _daoState;
		private readonly ICountyDao _daoCounty;

		public UsStateController(
			ILogger<UsStateController> logger,
			IStateDao daoState,
			ICountyDao daoCounty)
		{
			_logger = logger;
			_daoState = daoState;
			_daoCounty = daoCounty;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<StateUs>> StateByQuery(string fipsCounty = null, string zip = null, string name = null, bool exactNameMatch = false)
		{
			IList<State> states;

			if (!string.IsNullOrWhiteSpace(fipsCounty))
			{
				County county = _daoCounty.GetByFips(Country.CountryUs, fipsCounty);
				if (county != null)
				{
					string isoState = county.StateIso;
					State state = _daoState.GetByIso(Country.CountryUs, isoState);
					if (state != null)
					{
						return Ok(State.ToModel(state));
					}
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(zip))
			{
				states = _daoState.GetByZip(Country.CountryUs, zip);
				if (states != null && states.Any())
				{
					return Ok(State.ToModel(states));
				}
				return NotFound();
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				states = _daoState.GetByName(Country.CountryUs, name, exactNameMatch);
				if (states != null && states.Any())
				{
					return Ok(State.ToModel(states));
				}
				return NotFound();
			}

			states = _daoState.GetStates(Country.CountryUs);
			if (states != null && states.Any())
			{
				return Ok(State.ToModel(states));
			}
			return NotFound();
		}

		[HttpHead]
		[Route("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult StateExistsByQuery(string name = null, bool exactNameMatch = false)
		{
			if (!string.IsNullOrWhiteSpace(name))
			{
				bool exists = _daoState.ExistsByName(Country.CountryUs, name, exactNameMatch);
				if (exists)
				{
					return Ok();
				}
			}
			return NotFound();
		}

		[HttpGet]
		[Route("{isoState}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<StateUs> StateByAbbreviation(string isoState)
		{
			string isoStateToUse = isoState.ToUpper();
			
			State state = _daoState.GetByIso(Country.CountryUs, isoStateToUse);
			if (state != null)
			{
				return Ok(State.ToModel(state));
			}
			return NotFound();
		}

		[HttpHead]
		[Route("{isoState}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult StateExistsByAbbreviation(string isoState)
		{
			string isoStateToUse = isoState.ToUpper();

			if (_daoState.ExistsByIso(Country.CountryUs, isoStateToUse))
			{
				return Ok();
			}

			return NotFound();
		}
	}
}