using System;
using System.Collections.Generic;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class AddressBookContactsGroupTest
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private AddressBookContact _contact1, _contact2;

        private readonly List<long> _lsRemoveContacts = new List<long>();
        private AddressBookContact _scheduledContact1, _scheduledContact1Response;
        private AddressBookContact _scheduledContact2, _scheduledContact2Response;
        private AddressBookContact _scheduledContact3, _scheduledContact3Response;
        private AddressBookContact _scheduledContact4, _scheduledContact4Response;
        private AddressBookContact _scheduledContact5, _scheduledContact5Response;        
        private AddressBookContact _contactToRemove;

        private TestDataRepository _tdr;
        private List<string> _lsOptimizationIDs;

        [OneTimeSetUp]
        public void AddAddressBookContactsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            string scheduledDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            var sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                From = scheduledDate,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            #region Create 1st Contact

            var contact = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                Schedule = new List<Schedule>() { sched1 }
            };

            // Run the query
            _contact1 = route4Me.AddAddressBookContact(contact, out var errorString);

            Assert.IsNotNull(_contact1, "AddAddressBookContactsTest failed. " + errorString);

            var location1 = _contact1.AddressId != null ? Convert.ToInt64(_contact1.AddressId) : -1;

            if (location1 > 0) _lsRemoveContacts.Add(location1);

            #endregion

            #region Create 2nd Contact

            var dCustom = new Dictionary<string, string>
            {
                {"FirstFieldName1", "FirstFieldValue1"},
                {"FirstFieldName2", "FirstFieldValue2"}
            };
            contact = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressCustomData = dCustom,
                Schedule = new List<Schedule>() { sched1 }
            };

            _contact2 = route4Me.AddAddressBookContact(contact, out errorString);

            Assert.IsNotNull(_contact2, "AddAddressBookContactsTest failed. " + errorString);

            var location2 = _contact2.AddressId != null ? Convert.ToInt64(_contact2.AddressId) : -1;

            if (location2 > 0) _lsRemoveContacts.Add(location2);

            #endregion

            #region Create 3rd Contact for removing

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName Rem" + new Random().Next(),
                Address1 = "Test Address1 Rem " + new Random().Next(),
                CachedLat = 38.02466,
                CachedLng = -77.33882
            };

            _contactToRemove = route4Me.AddAddressBookContact(contactParams, out errorString);

            if ((_contactToRemove?.AddressId ?? 0) > 0) _lsRemoveContacts.Add((long)_contactToRemove.AddressId);

            #endregion

            _lsOptimizationIDs = new List<string>();

            #region Create Single Driver 10 Stops Route
            _tdr = new TestDataRepository();

            var result = _tdr.RunOptimizationSingleDriverRoute10Stops();

            Assert.IsTrue(result, "Single Driver 10 Stops generation failed.");

            _lsOptimizationIDs.Add(_tdr.SD10Stops_optimization_problem_id);

            #endregion

            #region Dynamically insert the created contacts to a route

            var addressToInsert1 = new Address()
            {
                ContactId = _contact1.AddressId,
                Latitude = _contact1.CachedLat,
                Longitude = _contact1.CachedLng
            };

            var addressToInsert2 = new Address()
            {
                ContactId = _contact2.AddressId,
                Latitude = _contact2.CachedLat,
                Longitude = _contact2.CachedLng
            };

            _tdr.SD10Stops_route = _tdr.AddAddressesToRoute(
                new Address[] { addressToInsert1, addressToInsert2 }, 
                _tdr.SD10Stops_route_id);

            #endregion
        }

        [Test]
        public void AddCustomDataToContactTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            _contact1.AddressCustomData = new Dictionary<string, string>
            {
                {"Service type", "publishing"},
                {"Facilities", "storage"},
                {"Parking", "temporarry"}
            };

            // Run the query
            var updatableProperties = new List<string>
            {
                "AddressId", "AddressCustomData"
            };

            var updatedContact = route4Me.UpdateAddressBookContact(
                _contact1,
                updatableProperties,
                out var errorString);

            Assert.IsNotNull(
                updatedContact.AddressCustomData,
                "AddCustomDataToContactTest failed. " + errorString);
        }

        [Test]
        public void AddScheduledAddressBookContactsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            #region // Add a location, scheduled daily.

            var sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            _scheduledContact1 = new AddressBookContact
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
                AddressCustomData = new Dictionary<string, string>
                {
                    {"scheduled", "yes"},
                    {"service type", "publishing"}
                },
                Schedule = new List<Schedule> {sched1}
            };

            _scheduledContact1Response = route4Me.AddAddressBookContact(
                _scheduledContact1,
                out var errorString);

            Assert.IsNotNull(
                _scheduledContact1Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location1 = _scheduledContact1Response.AddressId != null
                ? Convert.ToInt32(_scheduledContact1Response.AddressId)
                : -1;

            if (location1 > 0) _lsRemoveContacts.Add(location1);

            #endregion

            #region // Add a location, scheduled weekly.

            var sched2 = new Schedule("weekly", false)
            {
                Enabled = true,
                Weekly = new ScheduleWeekly(1, new[] {1, 2, 3, 4, 5})
            };

            _scheduledContact2 = new AddressBookContact
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
                Schedule = new List<Schedule> {sched2}
            };

            _scheduledContact2Response = route4Me.AddAddressBookContact(_scheduledContact2, out errorString);

            Assert.IsNotNull(
                _scheduledContact2Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location2 = _scheduledContact2Response.AddressId != null
                ? Convert.ToInt32(_scheduledContact2Response.AddressId)
                : -1;

            if (location2 > 0) _lsRemoveContacts.Add(location2);

            #endregion

            #region // Add a location, scheduled monthly (dates mode).

            var sched3 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(
                    1,
                    "dates",
                    new[] {20, 22, 23, 24, 25})
            };

            _scheduledContact3 = new AddressBookContact
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
                Schedule = new List<Schedule> {sched3},
                Color = "red"
            };

            _scheduledContact3Response = route4Me.AddAddressBookContact(
                _scheduledContact3,
                out errorString);

            Assert.IsNotNull(
                _scheduledContact3Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location3 = _scheduledContact3Response.AddressId != null
                ? Convert.ToInt32(_scheduledContact3Response.AddressId)
                : -1;

            if (location3 > 0) _lsRemoveContacts.Add(location3);

            #endregion

            #region // Add a location, scheduled monthly (nth mode).

            var sched4 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(
                    1,
                    "nth",
                    _nth: new Dictionary<int, int> {{1, 4}})
            };

            _scheduledContact4 = new AddressBookContact
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
                AddressCustomData = new Dictionary<string, string>
                {
                    {"scheduled", "yes"},
                    {"service type", "library"}
                },
                Schedule = new List<Schedule> {sched4},
                AddressIcon = "emoji/emoji-bus"
            };

            _scheduledContact4Response = route4Me.AddAddressBookContact(
                _scheduledContact4,
                out errorString);

            Assert.IsNotNull(
                _scheduledContact4Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location4 = _scheduledContact4Response.AddressId != null
                ? Convert.ToInt32(_scheduledContact4Response.AddressId)
                : -1;

            if (location4 > 0) _lsRemoveContacts.Add(location4);

            #endregion

            #region // Add a location with the daily scheduling and blacklist.

            var sched5 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            _scheduledContact5 = new AddressBookContact
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
                AddressCustomData = new Dictionary<string, string>
                {
                    {"scheduled", "yes"},
                    {"service type", "appliance"}
                },
                Schedule = new List<Schedule> {sched5},
                ScheduleBlacklist = new[] {"2017-12-22", "2017-12-23"},
                ServiceTime = 300
            };

            _scheduledContact5Response = route4Me.AddAddressBookContact(
                _scheduledContact5,
                out errorString);

            Assert.IsNotNull(
                _scheduledContact5Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location5 = _scheduledContact5Response.AddressId != null
                ? Convert.ToInt32(_scheduledContact5Response.AddressId)
                : -1;

            if (location5 > 0) _lsRemoveContacts.Add(location5);

            #endregion
        }

        [Test]
        public void UpdateAddressBookContactTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            Assert.IsNotNull(_contact1, "contact1 is null.");

            _contact1.AddressGroup = "Updated";
            _contact1.ScheduleBlacklist = new[] {"2020-03-14", "2020-03-15"};
            _contact1.AddressCustomData = new Dictionary<string, string>
            {
                {"key1", "value1"}, {"key2", "value2"}
            };
            _contact1.LocalTimeWindowStart = 25400;
            _contact1.LocalTimeWindowEnd = 26000;
            _contact1.AddressCube = 5;
            _contact1.AddressPieces = 6;
            _contact1.AddressRevenue = 700;
            _contact1.AddressWeight = 80;
            _contact1.AddressPriority = 9;

            var updatableProperties = new List<string>
            {
                "AddressId", "AddressGroup", "ScheduleBlacklist",
                "AddressCustomData", "LocalTimeWindowStart", "LocalTimeWindowEnd",
                "AddressCube", "AddressPieces", "AddressRevenue", "AddressWeight", "AddressPriority"
            };

            // Run the query
            var updatedContact = route4Me.UpdateAddressBookContact(
                _contact1,
                updatableProperties,
                out var errorString);

            Assert.IsNotNull(
                updatedContact,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                updatedContact.ScheduleBlacklist,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                updatedContact.LocalTimeWindowStart,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                updatedContact.LocalTimeWindowEnd,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressCube == 5,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressPieces == 6,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressRevenue == 700,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressWeight == 80,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressPriority == 9,
                "UpdateAddressBookContactTest failed. " + errorString);

            _contact1.ScheduleBlacklist = null;
            _contact1.AddressCustomData = null;
            _contact1.LocalTimeWindowStart = null;
            _contact1.LocalTimeWindowEnd = null;
            _contact1.AddressCube = null;
            _contact1.AddressPieces = null;
            _contact1.AddressRevenue = null;
            _contact1.AddressWeight = null;
            _contact1.AddressPriority = null;

            var updatedContact1 = route4Me.UpdateAddressBookContact(
                _contact1,
                updatableProperties,
                out var errorString1);

            Assert.IsNotNull(
                updatedContact1,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.ScheduleBlacklist,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.LocalTimeWindowStart,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.LocalTimeWindowEnd,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressCube,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressPieces,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressRevenue,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressWeight,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressPriority,
                "UpdateAddressBookContactTest failed. " + errorString);
        }

        [Test]
        public void UpdateWholeAddressBookContactTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            Assert.IsNotNull(_contact1, "contact1 is null..");

            // Create contact clone in the memory
            var contactClone = R4MeUtils.ObjectDeepClone(_contact1);

            // Modify the parameters of the contactClone
            contactClone.AddressGroup = "Updated";
            contactClone.ScheduleBlacklist = new[] {"2020-03-14", "2020-03-15"};
            contactClone.AddressCustomData = new Dictionary<string, string>
            {
                {"key1", "value1"}, {"key2", "value2"}
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

            contactClone.Schedule = new List<Schedule> {sched1};

            _contact1 = route4Me.UpdateAddressBookContact(contactClone, _contact1, out var errorString);

            Assert.IsNotNull(
                _contact1,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                _contact1.ScheduleBlacklist,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                _contact1.LocalTimeWindowStart,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                _contact1.LocalTimeWindowEnd,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                _contact1.AddressCube == 5,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                _contact1.AddressPieces == 6,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                _contact1.AddressRevenue == 700,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                _contact1.AddressWeight == 80,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                _contact1.AddressPriority == 9,
                "UpdateWholeAddressBookContactTest failed. " + errorString);

            contactClone = R4MeUtils.ObjectDeepClone(_contact1);

            contactClone.ScheduleBlacklist = null;
            contactClone.AddressCustomData = null;
            contactClone.LocalTimeWindowStart = null;
            contactClone.LocalTimeWindowEnd = null;
            contactClone.AddressCube = null;
            contactClone.AddressPieces = null;
            contactClone.AddressRevenue = null;
            contactClone.AddressWeight = null;
            contactClone.AddressPriority = null;

            _contact1 = route4Me.UpdateAddressBookContact(contactClone, _contact1, out errorString);

            Assert.IsNotNull(
                _contact1,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.ScheduleBlacklist,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.LocalTimeWindowStart,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.LocalTimeWindowEnd,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.AddressCube,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.AddressPieces,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.AddressRevenue,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.AddressWeight,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                _contact1.AddressPriority,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
        }

        [Test]
        public void SearchLocationsByTextTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Query = "Test Address1",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.That(contacts, Is.InstanceOf<AddressBookContact[]>(), "SearchLocationsByTextTest failed. " + errorString);

            Assert.IsNotNull(total, "SearchLocationsByTextTest failed.");
        }

        [Test]
        public void SearchLocationsByIDsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            Assert.IsNotNull(_contact1, "contact1 is null.");
            Assert.IsNotNull(_contact2, "contact2 is null.");

            var addresses = _contact1.AddressId + "," + _contact2.AddressId;

            var addressBookParameters = new AddressBookParameters
            {
                AddressId = addresses
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.That(contacts, Is.InstanceOf<AddressBookContact[]>(), "SearchLocationsByIDsTest failed. " + errorString);

            Assert.IsNotNull(total, "SearchLocationsByIDsTest failed.");
        }

        [Test]
        public void GetSpecifiedFieldsSearchTextTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Query = "FirstFieldValue1",
                Fields = "first_name,address_email,schedule_blacklist,schedule,address_custom_data,address_1",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            if (addressBookParameters.Fields != null)
            {
                var response = route4Me.SearchAddressBookLocation(
                    addressBookParameters,
                    out var contactsFromObjects,
                    out var errorString);

                Assert.That(response.Total, Is.InstanceOf<uint>(), "GetSpecifiedFieldsSearchTextTest failed. " + errorString);
                Assert.That(contactsFromObjects, Is.InstanceOf<List<AddressBookContact>>(), "GetSpecifiedFieldsSearchTextTest failed. " + errorString);
            }
            else
            {
                var response = route4Me.GetAddressBookContacts(
                    addressBookParameters,
                    out _,
                    out var errorString);
                Assert.That(response, Is.InstanceOf<AddressBookContact[]>(), "GetSpecifiedFieldsSearchTextTest failed. " + errorString);
            }
        }

        [Test]
        public void GetAddressBookContactsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.That(contacts, Is.InstanceOf<AddressBookContact[]>(), "GetAddressBookContactsTest failed. " + errorString);

            Assert.IsNotNull(total, "GetAddressBookContactsTest failed.");
        }

        [Test]
        public void RemoveAddressBookContactsTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var removed = route4Me.RemoveAddressBookContacts(
                new[] {_contactToRemove.AddressId.ToString()},
                out var errorString);

            Assert.IsTrue(removed,
                "Cannot remove the address book contact." + Environment.NewLine + errorString);
        }

        [Test]
        public void SearchRoutedLocationsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Display = "routed",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.That(contacts, Is.InstanceOf<AddressBookContact[]>(), "SearchRoutedLocationsTest failed. " + errorString);

            Assert.IsNotNull(total, "SearchRoutedLocationsTest failed.");
        }

        [OneTimeTearDown]
        public void AddressbookContactsGroupCleanup()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var lsRemLocations = new List<string>();

            if (_lsRemoveContacts.Count > 0)
            {
                foreach (var loc1 in _lsRemoveContacts) lsRemLocations.Add(loc1.ToString());

                var removed = route4Me.RemoveAddressBookContacts(
                    lsRemLocations.ToArray(),
                    out var errorString);

                Assert.IsTrue(removed, "RemoveAddressBookContactsTest failed. " + errorString);
            }

            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the testing optimization problem failed.");
        }
    }
}