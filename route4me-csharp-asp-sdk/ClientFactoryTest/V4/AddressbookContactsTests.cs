using ClientFactoryTest.IgnoreTests;
using ClientFactoryTest.V4.Fixtures;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK;
using Route4MeSDK.Controllers;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Route4MeSDK.Services.Route4MeApi4Service;

namespace ClientFactoryTest.V4
{
    public class AddressbookContactsTests : IClassFixture<AddressBookContactsFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient _httpClient;

        private Route4MeApi4Controller r4mController;

        private Route4MeApi4Service r4mApi4Service;

        private readonly ITestOutputHelper _output;

        private readonly AddressBookContactsFixture _fixture;

        public AddressbookContactsTests(AddressBookContactsFixture fixture, IServer server, ITestOutputHelper output)
        {
            _httpClientFactory = ((TestServer)server).Services.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            r4mApi4Service = new Route4MeApi4Service(_httpClient);
            r4mController = new Route4MeApi4Controller(r4mApi4Service, _httpClient);

            _output = output;

            _fixture = fixture;
            _output = output;
        }

        [FactSkipable]
        public async Task AddCustomDataToContactTest()
        {
            _fixture.contact1.AddressCustomData = new Dictionary<string, string>()
            {
                {"Service type", "publishing"},
                {"Facilities", "storage" },
                {"Parking", "temporarry" }
            };

            // Run the query
            var updatableProperties = new List<string>()
            {
                "AddressId", "AddressCustomData"
            };

            var updatedContactResult = await r4mController.UpdateAddressBookContact(
                                                    _fixture.contact1,
                                                    updatableProperties);

            Assert.True(updatedContactResult != null,
                        $"AddCustomDataToContactTest failed. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(updatedContactResult.Item2));

            Assert.IsType<AddressBookContact>(updatedContactResult.Item1);
        }

        [FactSkipable]
        public async Task AddScheduledAddressBookContactsTest()
        {
			#region // Add a location, scheduled daily.
			var sched1 = new Schedule("daily", false)
			{
				Enabled = true,
				Mode = "daily",
				Daily = new ScheduleDaily(1)
			};

			_fixture.scheduledContact1 = new AddressBookContact()
			{
				Address1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
				AddressAlias = "1604 PARKRIDGE PKWY 40214",
				AddressGroup = "Scheduled daily",
				FirstName = "Peter",
				LastName = "Newman",
				AddressEmail = "pnewman6564@yahoo.com",
				AddressPhoneNumber = "65432178",
				CachedLat = 38.141598,
				CachedLng = -85.793846,
				AddressCity = "Louisville",
				AddressCustomData = new Dictionary<string, string>()
										{
											{ "scheduled", "yes" },
											{ "service type", "publishing" }
										},
				Schedule = new List<Schedule>() { sched1 }
			};

			var scheduledContact1Result = await r4mController.AddAddressBookContact(
														_fixture.scheduledContact1);

			Assert.True(scheduledContact1Result != null,
						$"AddScheduledAddressBookContactsTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(scheduledContact1Result.Item2));

			_fixture.scheduledContact1Response = scheduledContact1Result.Item1;

			int location1 = _fixture.scheduledContact1Response.AddressId != null
							? Convert.ToInt32(_fixture.scheduledContact1Response.AddressId)
							: -1;

			if (location1 > 0) _fixture.lsRemoveContacts.Add(location1);
			#endregion

			#region // Add a location, scheduled weekly.
			Schedule sched2 = new Schedule("weekly", false)
			{
				Enabled = true,
				Weekly = new ScheduleWeekly(1, new int[] { 1, 2, 3, 4, 5 })
			};

			_fixture.scheduledContact2 = new AddressBookContact()
			{
				Address1 = "1407 MCCOY, Louisville, KY, 40215",
				AddressAlias = "1407 MCCOY 40215",
				AddressGroup = "Scheduled weekly",
				FirstName = "Bart",
				LastName = "Douglas",
				AddressEmail = "bdouglas9514@yahoo.com",
				AddressPhoneNumber = "95487454",
				CachedLat = 38.202496,
				CachedLng = -85.786514,
				AddressCity = "Louisville",
				ServiceTime = 600,
				Schedule = new List<Schedule>() { sched2 }
			};

			var scheduledContact2Result = await r4mController.AddAddressBookContact(_fixture.scheduledContact2);

			Assert.True(scheduledContact2Result != null,
						$"AddScheduledAddressBookContactsTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(scheduledContact2Result.Item2));

			_fixture.scheduledContact2Response = scheduledContact2Result.Item1;

			int location2 = _fixture.scheduledContact2Response.AddressId != null
							? Convert.ToInt32(_fixture.scheduledContact2Response.AddressId)
							: -1;

			if (location2 > 0) _fixture.lsRemoveContacts.Add(location2);

			#endregion

			#region // Add a location, scheduled monthly (dates mode).
			Schedule sched3 = new Schedule("monthly", false)
			{
				Enabled = true,
				Monthly = new ScheduleMonthly(
					_every: 1,
					_mode: "dates",
					_dates: new int[] { 20, 22, 23, 24, 25 })
			};

			_fixture.scheduledContact3 = new AddressBookContact()
			{
				Address1 = "4805 BELLEVUE AVE, Louisville, KY, 40215",
				Address2 = "4806 BELLEVUE AVE, Louisville, KY, 40215",
				AddressAlias = "4805 BELLEVUE AVE 40215",
				AddressGroup = "Scheduled monthly",
				FirstName = "Bart",
				LastName = "Douglas",
				AddressEmail = "bdouglas9514@yahoo.com",
				AddressPhoneNumber = "95487454",
				CachedLat = 38.178844,
				CachedLng = -85.774864,
				AddressCountryId = "US",
				AddressStateId = "KY",
				AddressZip = "40215",
				AddressCity = "Louisville",
				ServiceTime = 750,
				Schedule = new List<Schedule>() { sched3 },
				Color = "red"
			};

			var scheduledContact3Result = await r4mController.AddAddressBookContact(
															_fixture.scheduledContact3);

			Assert.True(scheduledContact3Result != null,
						$"AddScheduledAddressBookContactsTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(scheduledContact3Result.Item2));

			_fixture.scheduledContact3Response = scheduledContact3Result.Item1;

			int location3 = _fixture.scheduledContact3Response.AddressId != null
							? Convert.ToInt32(_fixture.scheduledContact3Response.AddressId)
							: -1;

			if (location3 > 0) _fixture.lsRemoveContacts.Add(location3);
			#endregion

			#region // Add a location, scheduled monthly (nth mode).

			Schedule sched4 = new Schedule("monthly", false)
			{
				Enabled = true,
				Monthly = new ScheduleMonthly(
					_every: 1,
					_mode: "nth",
					_nth: new Dictionary<int, int>() { { 1, 4 } })
			};

			_fixture.scheduledContact4 = new AddressBookContact()
			{
				Address1 = "730 CECIL AVENUE, Louisville, KY, 40211",
				AddressAlias = "730 CECIL AVENUE 40211",
				AddressGroup = "Scheduled monthly",
				FirstName = "David",
				LastName = "Silvester",
				AddressEmail = "dsilvester5874@yahoo.com",
				AddressPhoneNumber = "36985214",
				CachedLat = 38.248684,
				CachedLng = -85.821121,
				AddressCity = "Louisville",
				ServiceTime = 450,
				AddressCustomData = new Dictionary<string, string>()
								{
									{ "scheduled", "yes" },
									{ "service type", "library" }
								},
				Schedule = new List<Schedule>() { sched4 },
				AddressIcon = "emoji/emoji-bus"
			};

			var scheduledContact4Result = await r4mController.AddAddressBookContact(
														_fixture.scheduledContact4);

			Assert.True(scheduledContact4Result != null,
						$"AddScheduledAddressBookContactsTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(scheduledContact4Result.Item2));

			_fixture.scheduledContact4Response = scheduledContact4Result.Item1;

			int location4 = _fixture.scheduledContact4Response.AddressId != null
							? Convert.ToInt32(_fixture.scheduledContact4Response.AddressId)
							: -1;

			if (location4 > 0) _fixture.lsRemoveContacts.Add(location4);

			#endregion

			#region // Add a location with the daily scheduling and blacklist.

			Schedule sched5 = new Schedule("daily", false)
			{
				Enabled = true,
				Mode = "daily",
				Daily = new ScheduleDaily(1)
			};

			_fixture.scheduledContact5 = new AddressBookContact()
			{
				Address1 = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
				AddressAlias = "4629 HILLSIDE DRIVE 40216",
				AddressGroup = "Scheduled daily",
				FirstName = "Kim",
				LastName = "Shandor",
				AddressEmail = "kshand8524@yahoo.com",
				AddressPhoneNumber = "9874152",
				CachedLat = 38.176067,
				CachedLng = -85.824638,
				AddressCity = "Louisville",
				AddressCustomData = new Dictionary<string, string>()
								{
									{ "scheduled", "yes" },
									{ "service type", "appliance" }
								},
				Schedule = new List<Schedule>() { sched5 },
				ScheduleBlacklist = new string[] {
					(DateTime.Now + new TimeSpan(10,0,0,0)).ToString("yyy-MM-dd"),
					(DateTime.Now + new TimeSpan(11,0,0,0)).ToString("yyy-MM-dd")
				},
				ServiceTime = 300
			};

			var scheduledContact5Result = await r4mController.AddAddressBookContact(
															_fixture.scheduledContact5);

			Assert.True(scheduledContact5Result != null,
						$"AddScheduledAddressBookContactsTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(scheduledContact5Result.Item2));

			_fixture.scheduledContact5Response = scheduledContact5Result.Item1;

			int location5 = _fixture.scheduledContact5Response.AddressId != null
							? Convert.ToInt32(_fixture.scheduledContact5Response.AddressId)
							: -1;

			if (location5 > 0) _fixture.lsRemoveContacts.Add(location5);
			#endregion
		}

		[FactSkipable]
		public async Task UpdateAddressBookContactTest()
        {
			Assert.True(_fixture.contact1!=null, "contact1 is null.");

			_fixture.contact1.AddressGroup = "Updated";
			_fixture.contact1.ScheduleBlacklist = new string[] { "2020-03-14", "2020-03-15" };

			_fixture.contact1.AddressCustomData = new Dictionary<string, string>
			{
				{"key1", "value1" }, {"key2", "value2" }
			};

			_fixture.contact1.LocalTimeWindowStart = 25400;
			_fixture.contact1.LocalTimeWindowEnd = 26000;
			_fixture.contact1.AddressCube = 5;
			_fixture.contact1.AddressPieces = 6;
			_fixture.contact1.AddressRevenue = 700;
			_fixture.contact1.AddressWeight = 80;
			_fixture.contact1.AddressPriority = 9;

			var updatableProperties = new List<string>()
			{
				"AddressId", "AddressGroup", "ScheduleBlacklist",
				"AddressCustomData", "LocalTimeWindowStart", "LocalTimeWindowEnd",
				"AddressCube","AddressPieces","AddressRevenue","AddressWeight","AddressPriority"
			};

			// Run the query
			var updateContactResult = await r4mController.UpdateAddressBookContact(
												_fixture.contact1,
												updatableProperties);

			Assert.True((updateContactResult?.Item1 ?? null) != null,
						$"UpdateAddressBookContactTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(updateContactResult.Item2));

			Assert.IsType<AddressBookContact>(updateContactResult.Item1);

			Assert.NotNull(updateContactResult.Item1.ScheduleBlacklist);
			Assert.NotNull(updateContactResult.Item1.LocalTimeWindowStart);
			Assert.NotNull(updateContactResult.Item1.LocalTimeWindowEnd);
			Assert.Equal(5, updateContactResult.Item1.AddressCube);
			Assert.Equal(6, updateContactResult.Item1.AddressPieces);
			Assert.Equal(700, updateContactResult.Item1.AddressRevenue);
			Assert.Equal(80, updateContactResult.Item1.AddressWeight);
			Assert.Equal(9, updateContactResult.Item1.AddressPriority);

			_fixture.contact1.ScheduleBlacklist = null;
			_fixture.contact1.AddressCustomData = null;
			_fixture.contact1.LocalTimeWindowStart = null;
			_fixture.contact1.LocalTimeWindowEnd = null;
			_fixture.contact1.AddressCube = null;
			_fixture.contact1.AddressPieces = null;
			_fixture.contact1.AddressRevenue = null;
			_fixture.contact1.AddressWeight = null;
			_fixture.contact1.AddressPriority = null;

			var updatedContact1 = await r4mController.UpdateAddressBookContact(
													_fixture.contact1,
													updatableProperties);

			Assert.True((updatedContact1?.Item1 ?? null) != null,
						$"UpdateAddressBookContactTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(updatedContact1.Item2));

			Assert.Null(updatedContact1.Item1.ScheduleBlacklist);
			Assert.Null(updatedContact1.Item1.LocalTimeWindowStart);
			Assert.Null(updatedContact1.Item1.LocalTimeWindowEnd);
			Assert.Null(updatedContact1.Item1.AddressCube);
			Assert.Null(updatedContact1.Item1.AddressPieces);
			Assert.Null(updatedContact1.Item1.AddressRevenue);
			Assert.Null(updatedContact1.Item1.AddressWeight);
			Assert.Null(updatedContact1.Item1.AddressPriority);
		}

		[FactSkipable]
		public async Task UpdateWholeAddressBookContactTest()
        {
			Assert.NotNull(_fixture.contact1);

			// Create contact clone in the memory
			var contactClone = R4MeUtils.ObjectDeepClone<AddressBookContact>(_fixture.contact1);

			// Modify the parameters of the contactClone
			contactClone.AddressGroup = "Updated";
			contactClone.ScheduleBlacklist = new string[] 
			{
				(DateTime.Now + new TimeSpan(10,0,0,0)).ToString("yyy-MM-dd"),
				(DateTime.Now + new TimeSpan(11,0,0,0)).ToString("yyy-MM-dd")
			};

			contactClone.AddressCustomData = new Dictionary<string, string>
			{
				{"key1", "value1" }, 
				{"key2", "value2" }
			};

			contactClone.LocalTimeWindowStart = 25400;
			contactClone.LocalTimeWindowEnd = 26000;
			contactClone.AddressCube = 5;
			contactClone.AddressPieces = 6;
			contactClone.AddressRevenue = 700;
			contactClone.AddressWeight = 80;
			contactClone.AddressPriority = 9;

			var sched1 = new Schedule("daily", false)
			{
				Enabled = true,
				Mode = "daily",
				Daily = new ScheduleDaily(1)
			};

			contactClone.Schedule = new List<Schedule>() { sched1 };

			var updateContact1Result = await r4mController.UpdateAddressBookContact(contactClone, _fixture.contact1);

			Assert.True((updateContact1Result?.Item1 ?? null) != null,
						$"UpdateAddressBookContactTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(updateContact1Result.Item2));

			Assert.IsType<AddressBookContact>(updateContact1Result.Item1);

			_fixture.contact1 = updateContact1Result.Item1;

			Assert.NotNull(_fixture.contact1.ScheduleBlacklist);
			Assert.NotNull(_fixture.contact1.LocalTimeWindowStart);
			Assert.NotNull(_fixture.contact1.LocalTimeWindowEnd);
			Assert.Equal(5,  _fixture.contact1.AddressCube);
			Assert.Equal(6, _fixture.contact1.AddressPieces);
			Assert.Equal(700, _fixture.contact1.AddressRevenue);
			Assert.Equal(80, _fixture.contact1.AddressWeight);
			Assert.Equal(9, _fixture.contact1.AddressPriority);

			contactClone = R4MeUtils.ObjectDeepClone<AddressBookContact>(_fixture.contact1);

			contactClone.ScheduleBlacklist = null;
			contactClone.AddressCustomData = null;
			contactClone.LocalTimeWindowStart = null;
			contactClone.LocalTimeWindowEnd = null;
			contactClone.AddressCube = null;
			contactClone.AddressPieces = null;
			contactClone.AddressRevenue = null;
			contactClone.AddressWeight = null;
			contactClone.AddressPriority = null;

			 var updateCloneResult = await r4mController.UpdateAddressBookContact(contactClone, _fixture.contact1);

			Assert.True((updateCloneResult?.Item1 ?? null) != null,
						$"UpdateAddressBookContactTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(updateCloneResult.Item2));

			Assert.IsType<AddressBookContact>(updateCloneResult.Item1);

			_fixture.contact1 = updateCloneResult.Item1;

			Assert.NotNull(_fixture.contact1);
			Assert.Null(_fixture.contact1.ScheduleBlacklist);
			Assert.Null(_fixture.contact1.LocalTimeWindowStart);
			Assert.Null(_fixture.contact1.LocalTimeWindowEnd);
			Assert.Null(_fixture.contact1.AddressCube);
			Assert.Null(_fixture.contact1.AddressPieces);
			Assert.Null(_fixture.contact1.AddressRevenue);
			Assert.Null(_fixture.contact1.AddressWeight);
			Assert.Null(_fixture.contact1.AddressPriority);
		}

		[FactSkipable]
		public async Task SearchLocationsByTextTest()
        {
			var addressBookParameters = new AddressBookParameters
			{
				Query = "Test Address1",
				Offset = 0,
				Limit = 20
			};

			// Run the query
			var searchLocationsResult = await r4mController.GetAddressBookLocation(
														addressBookParameters);

			Assert.True((searchLocationsResult?.Item1 ?? null) != null,
						$"SearchLocationsByTextTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(searchLocationsResult.Item2));

			Assert.IsType<GetAddressBookContactsResponse>(searchLocationsResult.Item1);


			Assert.True(searchLocationsResult.Item1.Total>0);
		}

		[FactSkipable]
		public async Task SearchLocationsByIDsTest()
        {
			Assert.NotNull(_fixture.contact1);
			Assert.NotNull(_fixture.contact2);

			string addresses = _fixture.contact1.AddressId + "," + _fixture.contact2.AddressId;

			var addressBookParameters = new AddressBookParameters
			{
				AddressId = addresses
			};

			// Run the query
			var searchLocationsResult = await r4mController.GetAddressBookLocation(
														addressBookParameters);

			Assert.True((searchLocationsResult?.Item1 ?? null) != null,
						$"SearchLocationsByIDsTest failed. Error: {Environment.NewLine}" +
						r4mApi4Service.ErrorResponseToString(searchLocationsResult.Item2));

			Assert.IsType<GetAddressBookContactsResponse>(searchLocationsResult.Item1);

			Assert.True(searchLocationsResult.Item1.Total > 0);
		}

		[FactSkipable]
		public async Task GetSpecifiedFieldsSearchTextTest()
        {
			var addressBookParameters = new AddressBookParameters
			{
				Query = "FirstFieldValue1",
				Fields = "first_name,address_email,schedule_blacklist,schedule,address_custom_data,address_1",
				Offset = 0,
				Limit = 20
			};

			// Run the query
			var SearchResult = await r4mController.SearchAddressBookLocation(
										addressBookParameters);

			Assert.True((SearchResult?.Item1 ?? null) != null,
					$"GetSpecifiedFieldsSearchTextTest failed. Error: {Environment.NewLine}" +
					r4mApi4Service.ErrorResponseToString(SearchResult.Item2));

			Assert.IsType<SearchAddressBookLocationResponse>(SearchResult.Item1);

			Assert.IsType<uint>(SearchResult.Item1.Total);
			Assert.IsType<List<object[]>>(SearchResult.Item1.Results);
		}

		[FactSkipable]
		public async Task GetAddressBookContactsTest()
        {
			var addressBookParameters = new AddressBookParameters()
			{
				Limit = 10,
				Offset = 0
			};

			// Run the query
			var contactsResult = await r4mController.GetAddressBookLocation(
														addressBookParameters);

			Assert.True((contactsResult?.Item1 ?? null) != null,
					$"GetAddressBookContactsTest failed. Error: {Environment.NewLine}" +
					r4mApi4Service.ErrorResponseToString(contactsResult.Item2));

			Assert.IsType<GetAddressBookContactsResponse>(contactsResult.Item1);

			Assert.IsType<AddressBookContact[]>(contactsResult.Item1.Results);
		}

		[FactSkipable]
		public async Task RemoveAddressbookContactsTest()
        {
			var removedResult = await r4mController.RemoveAddressBookContacts(
									new string[] { _fixture.contactToRemove.AddressId.ToString() });

			Assert.True((removedResult?.Item1 ?? null) != null,
					$"RemoveAddressbookContactsTest failed. Error: {Environment.NewLine}" +
					r4mApi4Service.ErrorResponseToString(removedResult.Item2));

			Assert.IsType<StatusResponse>(removedResult.Item1);

			Assert.True(removedResult.Item1.Status);
		}

		[FactSkipable]
		public async Task SearchRoutedLocationsTest()
        {
			var addressBookParameters = new AddressBookParameters
			{
				Display = "routed",
				Offset = 0,
				Limit = 20
			};

			// Run the query
			var contactsResult = await r4mController.GetAddressBookLocation(
														addressBookParameters);

			Assert.True((contactsResult?.Item1 ?? null) != null,
					$"SearchRoutedLocationsTest failed. Error: {Environment.NewLine}" +
					r4mApi4Service.ErrorResponseToString(contactsResult.Item2));

			Assert.IsType<GetAddressBookContactsResponse>(contactsResult.Item1);
		}
	}
}
