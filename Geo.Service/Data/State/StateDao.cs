namespace Geo.Service.Data.State
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Inflection.DataAccess;
	using Inflection.DataAccess.EntityFramework;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// Used for accessing State-related data
	/// </summary>
	public class StateDao : EntityDaoBase<State, int, RuleNull<State>>, IStateDao
	{
		/// <inheritdoc />
		protected override DaoCacheType CacheType => DaoCacheType.None;

		public GeoDbContext Db { get; set; }

		/// <inheritdoc />
		public StateDao(GeoDbContext context)
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
		public override State GetByKey(int key, bool isReadOnly = true)
		{
			State retVal = null;

			if (isReadOnly)
			{
				IList<State> all = GetAll();
				if (all != null)
				{
					retVal = all.FirstOrDefault(c => c.StateId == key);
				}
			}
			else
			{
				SetProxyCreationEnabled(true);
				IQueryable<State> query = null;
				try
				{
					query = from state in Db.States
							where state.StateId == key
							select state;

					retVal = ExtractFirstFrom(query, false);
				}
				catch (Exception ex)
				{
					throw new EntityQueryException<State>(query, ex);
				}
			}

			return retVal;
		}

		public IList<State> GetByZip(string isoCountry, string zip)
		{
			if (string.IsNullOrWhiteSpace(zip)) return null;

			IQueryable<State> query = null;
			try
			{
				query = QueryWithZips()
					.Where(_ => _.CountryIso == isoCountry && _.ZipCodes.Any(__ => __.ZipCode.ZipCode5 == zip));

				return ExtractAllFrom(query);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<State>(query, ex);
			}
		}

		/// <inheritdoc />
		public bool ExistsByIso(string isoCountry, string isoState)
		{
			return GetAll().Any(_ => _.StateIso == isoState);
		}

		/// <inheritdoc />
		public State GetByIso(string isoCountry, string isoState)
		{
			return GetAll()?.FirstOrDefault(_ => _.StateIso == isoState);
		}

		/// <inheritdoc />
		public IList<State> GetStates(string isoCountry)
		{
			return GetAll()?.Where(_ => _.CountryIso == isoCountry && !_.IsTerritory).ToList();
		}

		/// <inheritdoc />
		public IList<State> GetTerritories(string isoCountry)
		{
			return GetAll()?.Where(_ => _.CountryIso == isoCountry && _.IsTerritory).ToList();
		}

		public IList<State> GetByName(string isoCountry, string name, bool exactNameMatch)
		{
			IList<State> states = GetAll();
			if (states == null) return null;

			return exactNameMatch 
				? states.Where(_ => _.CountryIso == isoCountry && _.Name.Equals(name)).ToList() 
				: states.Where(_ => _.CountryIso == isoCountry && _.Name.StartsWith(name)).ToList();
		}

		public bool ExistsByName(string isoCountry, string name, bool exactNameMatch)
		{
			IList<State> states = GetAll();
			if (states == null) return false;

			return exactNameMatch
				? states.Any(_ => _.CountryIso == isoCountry && _.Name.Equals(name))
				: states.Any(_ => _.CountryIso == isoCountry && _.Name.StartsWith(name));
		}

		/// <summary>
		/// Gets all records
		/// </summary>
		private IList<State> GetAll(bool isReadOnly = true)
		{
			IList<State> retVal = GetFromCache<IList<State>>(CacheNamespace, isReadOnly);
			if (retVal == null)
			{
				retVal = QueryForAll(isReadOnly);

				if (CanUseCache(isReadOnly))
				{
					PutIntoCache(CacheNamespace, retVal, isReadOnly);
				}
			}

			return retVal;
		}

		/// <summary>
		/// Queries for all.
		/// </summary>
		/// <param name="isReadOnly">if set to <c>true</c> [is read only].</param>
		private IList<State> QueryForAll(bool isReadOnly)
		{
			SetProxyCreationEnabled(!isReadOnly);
			IQueryable<State> query = null;
			try
			{
				query = from state in Db.States
						select state;

				return ExtractAllFrom(query, isReadOnly);
			}
			catch (Exception ex)
			{
				throw new EntityQueryException<State>(query, ex);
			}
		}

		private IQueryable<State> QueryWithZips()
		{
			return from c in Db.States
					.Include(_ => _.ZipCodes).ThenInclude(_ => _.ZipCode)
				   select c;
		}

		private State ExtractFirstFrom(IQueryable<State> query, bool isReadOnly = true)
		{
			State state = query.FirstOrDefaultWithTransaction(isReadOnly);
			state?.AttachCountry(true);

			return state;
		}

		private IList<State> ExtractAllFrom(IQueryable<State> query, bool isReadOnly = true)
		{
			IList<State> states = query.ToListWithTransaction(isReadOnly);
			foreach (State county in states)
			{
				county?.AttachCountry(true);
			}

			return states;
		}
	}
}