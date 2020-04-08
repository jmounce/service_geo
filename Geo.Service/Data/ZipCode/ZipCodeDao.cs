namespace Geo.Service.Data.ZipCode
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Inflection.DataAccess;
	using Inflection.DataAccess.EntityFramework;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// DAO for ZipCodes
	/// </summary>
	public class ZipCodeDao : EntityDaoBase<ZipCode, int, RuleNull<ZipCode>>, IZipCodeDao
	{
		protected override DaoCacheType CacheType => DaoCacheType.None;

		private GeoDbContext Db { get; }

		protected override DbContext CreateContext()
		{
			return Db;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ZipCodeDao"/> class.
		/// </summary>
		public ZipCodeDao(GeoDbContext db)
			: base(true)
		{
			Db = db;
		}

		/// <inheritdoc />
		public override ZipCode GetByKey(int keyZipCode, bool isReadOnly = true)
		{
			IQueryable<ZipCode> query = null;
			try
			{
				query = Query()
					.Where(_ => _.ZipCodeId == keyZipCode);

				return ExtractFirstFrom(query, isReadOnly);
			}
			catch (Exception ex)
			{
				// re-throw (with additional details)
				throw new EntityQueryException<ZipCode>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<ZipCode> GetByZip(string isoCountry, string zip)
		{
			if (string.IsNullOrWhiteSpace(zip)) return null;

			IQueryable<ZipCode> query = null;
			try
			{
				query = Query()
					.Where(_ => _.ZipCode5 == zip && _.CountryIso == isoCountry);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<ZipCode>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsByZip(string isoCountry, string zip)
		{
			if (string.IsNullOrWhiteSpace(zip)) return false;

			IQueryable<ZipCode> query = null;
			try
			{
				query = QueryNoIncludes()
					.Where(_ => _.ZipCode5 == zip && _.CountryIso == isoCountry);

				return query.AnyWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<ZipCode>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<ZipCode> GetByCountry(string isoCountry)
		{
			IQueryable<ZipCode> query = null;
			try
			{
				query = Query()
					.Where(_ => _.CountryIso == isoCountry);

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<ZipCode>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<ZipCode> GetByCity(string idCity)
		{
			IQueryable<ZipCode> query = null;
			try
			{
				query = Query()
					.Where(_ => _.Cities.Any(_ => _.City.Id == idCity));

				return query.ToListWithTransaction(true);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<ZipCode>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<ZipCode> GetByCounty(string fipsCounty)
		{
			if (string.IsNullOrWhiteSpace(fipsCounty)) return null;

			IQueryable<ZipCode> query = null;
			try
			{
				query = Query()
					.Where(_ => _.Counties.Any(_ => _.County.FullFips == fipsCounty));

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<ZipCode>(query, ex);
			}
		}

		/// <inheritdoc />
		public IList<ZipCode> GetByState(string isoState)
		{
			if (string.IsNullOrWhiteSpace(isoState)) return null;

			IQueryable<ZipCode> query = null;
			try
			{
				query = Query()
					.Where(_ => _.States.Any(_ => _.State.StateIso == isoState));

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<ZipCode>(query, ex);
			}
		}

		private IQueryable<ZipCode> Query()
		{
			return from zc in Db.ZipCodes
					.Include(_ => _.Cities).ThenInclude(_ => _.City)
					.Include(_ => _.Counties).ThenInclude(_ => _.County)
					.Include(_ => _.States).ThenInclude(_ => _.State)
				   select zc;
		}

		private IQueryable<ZipCode> QueryNoIncludes()
		{
			return from zc in Db.ZipCodes
				select zc;
		}

		private ZipCode ExtractFirstFrom(IQueryable<ZipCode> query, bool isReadOnly = true)
		{
			ZipCode zipCode = query.FirstOrDefaultWithTransaction(isReadOnly);
			zipCode?.AttachCountry(true);

			return zipCode;
		}

		private IList<ZipCode> ExtractAllFrom(IQueryable<ZipCode> query)
		{
			IList<ZipCode> zipCodes = query.ToListWithTransaction(true);
			foreach (ZipCode zipCode in zipCodes)
			{
				zipCode?.AttachCountry(true);
			}

			return zipCodes;
		}
	}
}