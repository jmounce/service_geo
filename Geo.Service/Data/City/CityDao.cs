namespace Geo.Service.Data.City
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Inflection.DataAccess;
	using Inflection.DataAccess.EntityFramework;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// DAO for Cities
	/// </summary>
	public class CityDao : EntityDaoBase<City, int, RuleNull<City>>, ICityDao
	{
		protected override DaoCacheType CacheType => DaoCacheType.None;

		private GeoDbContext Db { get; }

		protected override DbContext CreateContext()
		{
			return Db;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CityDao"/> class.
		/// </summary>
		public CityDao(GeoDbContext db)
			: base(true)
		{
			Db = db;
		}

		/// <inheritdoc />
		public override City GetByKey(int keyCity, bool isReadOnly = true)
		{
			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CityId == keyCity);

				return ExtractFirstFrom(query, isReadOnly);
			}
			catch (Exception ex)
			{
				// re-throw (with additional details)
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<City> GetByCountry(string isoCountry)
		{
			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public City GetById(string idCity)
		{
			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.Id == idCity);

				return query.FirstOrDefaultWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsById(string idCity)
		{
			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.Id == idCity);

				return query.AnyWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<City> GetByName(string isoCountry, string name, bool exactNameMatch)
		{
			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsByName(string isoCountry, string name, bool exactNameMatch)
		{
			IQueryable<City> query = null;
			try
			{
				query = QueryNoIncludes()
					.Where(_ => _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return query.AnyWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<City> GetByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch)
		{
			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.StateIso == isoState && _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch)
		{
			IQueryable<City> query = null;
			try
			{
				query = QueryNoIncludes()
					.Where(_ => _.StateIso == isoState && _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return query.AnyWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<City> GetByCounty(string isoCountry, string fipsCounty)
		{
			if (string.IsNullOrWhiteSpace(fipsCounty)) return null;

			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry && _.Counties.Any(__ => __.County.FullFips == fipsCounty));

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<City> GetByState(string isoCountry, string isoState)
		{
			if (string.IsNullOrWhiteSpace(isoState)) return null;

			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry && _.StateIso == isoState);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<City> GetByZip(string isoCountry, string zip)
		{
			if (string.IsNullOrWhiteSpace(zip)) return null;

			IQueryable<City> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry && _.ZipCodes.Any(__ => __.ZipCode.ZipCode5 == zip));

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<City>(query, ex);
			}
		}

		private IQueryable<City> Query()
		{
			return from zc in Db.Cities
					.Include(_ => _.State)
					.Include(_ => _.ZipCodes).ThenInclude(_ => _.ZipCode)
					.Include(_ => _.Counties).ThenInclude(_ => _.County)
				   select zc;
		}

		private IQueryable<City> QueryNoIncludes()
		{
			return from zc in Db.Cities
				select zc;
		}

		private static IQueryable<City> MatchName(IQueryable<City> query, string name, bool exactNameMatch)
		{
			return exactNameMatch ? query.Where(_ => _.Name.Equals(name)) : query.Where(_ => _.Name.StartsWith(name));
		}

		private City ExtractFirstFrom(IQueryable<City> query, bool isReadOnly = true)
		{
			City city = query.FirstOrDefaultWithTransaction(isReadOnly);
			city?.AttachCountry(true);

			return city;
		}

		private IList<City> ExtractAllFrom(IQueryable<City> query)
		{
			IList<City> cities = query.ToListWithTransaction(true);
			foreach (City city in cities)
			{
				city?.AttachCountry(true);
			}

			return cities;
		}
	}
}