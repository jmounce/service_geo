namespace Geo.Service.Data.County
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Inflection.DataAccess;
	using Inflection.DataAccess.EntityFramework;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// Used for accessing County-related data
	/// </summary>
	public class CountyDao : EntityDaoBase<County, int, RuleNull<County>>, ICountyDao
	{
		/// <inheritdoc />
		protected override DaoCacheType CacheType => DaoCacheType.None;

		public GeoDbContext Db { get; set; }

		/// <inheritdoc />
		public CountyDao(GeoDbContext context)
		{
			Db = context;
		}

		/// <inheritdoc />
		protected override DbContext CreateContext()
		{
			// DB Context is initialized through the constructor
			return Db;
		}

		/// <inheritdoc />
		public override County GetByKey(int key, bool isReadOnly = true)
		{
			SetProxyCreationEnabled(true);
			IQueryable<County> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountyId == key);

				return ExtractFirstFrom(query, isReadOnly);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsByFips(string isoCountry, string fipsCounty)
		{
			IQueryable<County> query = null;
			try
			{
				query = QueryNoIncludes()
					.Where(_ => _.FullFips == fipsCounty && _.CountryIso == isoCountry);

				return query.AnyWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public County GetByFips(string isoCountry, string fipsCounty)
		{
			IQueryable<County> query = null;
			try
			{
				query = Query()
						.Where(_ => _.FullFips == fipsCounty && _.CountryIso == isoCountry);

				return ExtractFirstFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsByName(string isoCountry, string name, bool exactNameMatch)
		{
			IQueryable<County> query = null;
			try
			{
				query = QueryNoIncludes()
					.Where(_ => _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return query.AnyWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch)
		{
			IQueryable<County> query = null;
			try
			{
				query = QueryNoIncludes()
					.Where(_ => _.StateIso == isoState && _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return query.AnyWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<County> GetByName(string isoCountry, string name, bool exactNameMatch)
		{
			IQueryable<County> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public County GetByStateAndName(string isoCountry, string isoState, string name, bool exactNameMatch)
		{
			IQueryable<County> query = null;
			try
			{
				query = Query()
					.Where(_ => _.StateIso == isoState && _.CountryIso == isoCountry);
				query = MatchName(query, name, exactNameMatch);

				return ExtractFirstFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<County> GetCounties(string isoCountry)
		{
			IQueryable<County> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<County> GetByState(string isoCountry, string isoState)
		{
			IQueryable<County> query = null;
			try
			{
				query = Query()
					.Where(_ => _.StateIso == isoState && _.CountryIso == isoCountry);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		public IList<County> GetByZip(string isoCountry, string zip)
		{
			if (string.IsNullOrWhiteSpace(zip)) return null;

			IQueryable<County> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry && _.ZipCodes.Any(__ => __.ZipCode.ZipCode5 == zip));

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<County>(query, ex);
			}
		}

		private IQueryable<County> Query()
		{
			return from c in Db.Counties
					.Include(_ => _.State)
					.Include(_ => _.Cities).ThenInclude(_ => _.City)
					.Include(_ => _.ZipCodes).ThenInclude(_ => _.ZipCode)
				   select c;
		}

		private IQueryable<County> QueryNoIncludes()
		{
			return from c in Db.Counties
				   select c;
		}

		private static IQueryable<County> MatchName(IQueryable<County> query, string name, bool exactNameMatch)
		{
			return exactNameMatch ? query.Where(_ => _.Name.Equals(name)) : query.Where(_ => _.Name.StartsWith(name));
		}

		private County ExtractFirstFrom(IQueryable<County> query, bool isReadOnly = true)
		{
			County county = query.FirstOrDefaultWithTransaction(isReadOnly);
			county?.AttachCountry(true);

			return county;
		}

		private IList<County> ExtractAllFrom(IQueryable<County> query)
		{
			IList<County> counties = query.ToListWithTransaction(true);
			foreach (County county in counties)
			{
				county?.AttachCountry(true);
			}

			return counties;
		}
	}
}