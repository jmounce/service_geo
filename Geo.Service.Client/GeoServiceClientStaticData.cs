namespace Geo.Service.Client
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Model;

	internal class GeoServiceClientStaticData
	{
		public static Country US = new Country( "US", "United States" );
		public static Country CA = new Country( "CA", "Canada" );
		public static Country MX = new Country( "MX", "Mexico" );

		public static ReadOnlyCollection<Country> Countries =
			new ReadOnlyCollection<Country>( new List<Country> { US, CA, MX } );

		public static ReadOnlyCollection<StateUs> StatesUs =
			new ReadOnlyCollection<StateUs>(new List<StateUs>
			{
				new StateUs("Alabama", "AL", false, US),
				new StateUs("Alaska", "AK", false, US),
				new StateUs("Arizona", "AZ", false, US),
				new StateUs("Arkansas", "AR", false, US),
				new StateUs("California", "CA", false, US),
				new StateUs("Colorado", "CO", false, US),
				new StateUs("Connecticut", "CT", false, US),
				new StateUs("Delaware", "DE", false, US),
				new StateUs("Florida", "FL", false, US),
				new StateUs("Georgia", "GA", false, US),
				new StateUs("Hawaii", "HI", false, US),
				new StateUs("Idaho", "ID", false, US),
				new StateUs("Illinois", "IL", false, US),
				new StateUs("Indiana", "IN", false, US),
				new StateUs("Iowa", "IA", false, US),
				new StateUs("Kansas", "KS", false, US),
				new StateUs("Kentucky", "KY", false, US),
				new StateUs("Louisiana", "LA", false, US),
				new StateUs("Maine", "ME", false, US),
				new StateUs("Maryland", "MD", false, US),
				new StateUs("Massachusetts", "MA", false, US),
				new StateUs("Michigan", "MI", false, US),
				new StateUs("Minnesota", "MN", false, US),
				new StateUs("Mississippi", "MS", false, US),
				new StateUs("Missouri", "MO", false, US),
				new StateUs("Montana", "MT", false, US),
				new StateUs("Nebraska", "NE", false, US),
				new StateUs("Nevada", "NV", false, US),
				new StateUs("New Hampshire", "NH", false, US),
				new StateUs("New Jersey", "NJ", false, US),
				new StateUs("New Mexico", "NM", false, US),
				new StateUs("New York", "NY", false, US),
				new StateUs("North Carolina", "NC", false, US),
				new StateUs("North Dakota", "ND", false, US),
				new StateUs("Ohio", "OH", false, US),
				new StateUs("Oklahoma", "OK", false, US),
				new StateUs("Oregon", "OR", false, US),
				new StateUs("Pennsylvania", "PA", false, US),
				new StateUs("Rhode Island", "RI", false, US),
				new StateUs("South Carolina", "SC", false, US),
				new StateUs("South Dakota", "SD", false, US),
				new StateUs("Tennessee", "TN", false, US),
				new StateUs("Texas", "TX", false, US),
				new StateUs("Utah", "UT", false, US),
				new StateUs("Vermont", "VT", false, US),
				new StateUs("Virginia", "VA", false, US),
				new StateUs("Washington", "WA", false, US),
				new StateUs("West Virginia", "WV", false, US),
				new StateUs("Wisconsin", "WI", false, US),
				new StateUs("Wyoming", "WY", false, US),

				new StateUs("American Samoa", "AS", true, US),
				new StateUs("District of Columbia", "DC", true, US),
				new StateUs("Federated States of Micronesia", "FM", true, US),
				new StateUs("Guam", "GU", true, US),
				new StateUs("Marshall Islands", "MH", true, US),
				new StateUs("Northern Mariana Islands", "MP", true, US),
				new StateUs("Palau", "PW", true, US),
				new StateUs("Puerto Rico", "PR", true, US),
				new StateUs("Virgin Islands", "VI", true, US)
			});
	}
}
