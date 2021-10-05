using Route4MeSDK;
using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class AddressBookContactsFixture : IDisposable
    {
		public readonly string c_ApiKey = ApiKeys.ActualApiKey;

		public AddressBookContact contact1, contact2;

		public AddressBookContact scheduledContact1, scheduledContact1Response;
		public AddressBookContact scheduledContact2, scheduledContact2Response;
		public AddressBookContact scheduledContact3, scheduledContact3Response;
		public AddressBookContact scheduledContact4, scheduledContact4Response;
		public AddressBookContact scheduledContact5, scheduledContact5Response;

        public List<int> lsRemoveContacts;

        public AddressBookContact contactToRemove;


        public AddressBookContactsFixture()
        {
			var route4Me = new Route4MeManager(c_ApiKey);

			lsRemoveContacts = new List<int>();

			var contact = new AddressBookContact()
			{
				FirstName = "Test FirstName " + (new Random()).Next().ToString(),
				Address1 = "Test Address1 " + (new Random()).Next().ToString(),
				CachedLat = 38.024654,
				CachedLng = -77.338814
			};

			// Run the query
			contact1 = route4Me.AddAddressBookContact(contact, out string errorString);

			Assert.True(contact1!=null, "AddAddressBookContactsTest failed. " + errorString);

			int location1 = contact1.AddressId != null ? Convert.ToInt32(contact1.AddressId) : -1;

			if (location1 > 0) lsRemoveContacts.Add(location1);

			var dCustom = new Dictionary<string, string>()
			{
				{"FirstFieldName1", "FirstFieldValue1"},
				{"FirstFieldName2", "FirstFieldValue2"}
			};

			contact = new AddressBookContact()
			{
				FirstName = "Test FirstName " + (new Random()).Next().ToString(),
				Address1 = "Test Address1 " + (new Random()).Next().ToString(),
				CachedLat = 38.024654,
				CachedLng = -77.338814,
				AddressCustomData = dCustom
			};

			contact2 = route4Me.AddAddressBookContact(contact, out errorString);

			Assert.True(contact2!=null, "AddAddressBookContactsTest failed. " + errorString);

			int location2 = contact2.AddressId != null ? Convert.ToInt32(contact2.AddressId) : -1;

			if (location2 > 0) lsRemoveContacts.Add(location2);

			var contactParams = new AddressBookContact()
			{
				FirstName = "Test FirstName Rem" + (new Random()).Next().ToString(),
				Address1 = "Test Address1 Rem " + (new Random()).Next().ToString(),
				CachedLat = 38.02466,
				CachedLng = -77.33882
			};

			contactToRemove = route4Me.AddAddressBookContact(contactParams, out errorString);
		}


        public void Dispose()
        {
			var route4Me = new Route4MeManager(c_ApiKey);

			var lsRemLocations = new List<string>();

			if (lsRemoveContacts.Count > 0)
			{
				foreach (int loc1 in lsRemoveContacts) lsRemLocations.Add(loc1.ToString());

				bool removed = route4Me.RemoveAddressBookContacts(
											lsRemLocations.ToArray(),
											out string errorString);

				Assert.True(removed, "RemoveAddressBookContactsTest failed. " + errorString);
			}
		}
    }
}
