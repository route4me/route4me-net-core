using System.Collections.Generic;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class AddressBookGroupsGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private AddressBookGroup _group1, _group2;

        private readonly List<string> _lsGroups = new List<string>();

        [OneTimeSetUp]
        public void AddressBookGroupsInitialize()
        {
            _group1 = CreateAddreessBookGroup(out var errorString);

            Assert.IsNotNull(_group1, "AddressBookGroupsInitialize failed. " + errorString);

            _group2 = CreateAddreessBookGroup(out errorString);

            Assert.IsNotNull(_group2, "AddressBookGroupsInitialize failed. " + errorString);

            _lsGroups.Add(_group2.GroupId);
        }

        [Test]
        public void GetAddressBookGroupsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var groups = route4Me.GetAddressBookGroups(
                addressBookGroupParameters,
                out var errorString);

            Assert.That(groups, Is.InstanceOf<AddressBookGroup[]>(), "GetAddressBookGroupsTest failed. " + errorString);
        }

        [Test]
        public void GetAddressBookGroupTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                GroupId = _group2.GroupId
            };

            // Run the query
            var addressBookGroup = route4Me.GetAddressBookGroup(
                addressBookGroupParameters,
                out var errorString);

            Assert.That(addressBookGroup, Is.InstanceOf<AddressBookGroup>(), "GetAddressBookGroupTest failed. " + errorString);
        }

        [Test]
        public void GetAddressBookContactsByGroupTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                groupID = _group2.GroupId
            };

            // Run the query
            var addressBookGroup = route4Me.GetAddressBookContactsByGroup(addressBookGroupParameters,
                out var errorString);

            Assert.That(addressBookGroup, Is.InstanceOf<AddressBookSearchResponse>(), "GetAddressBookContactsByGroupTest failed. " + errorString);
        }

        [Test]
        public void SearchAddressBookContactsByFilterTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var filterParam = new AddressBookGroupFilterParameter
            {
                Query = "623CEE8BCD50B75A66268AAC11E552FD",
                Display = "all"
            };

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                Fields = new[] {"address_id", "address_1", "address_group"},
                offset = 0,
                limit = 10,
                filter = filterParam
            };

            // Run the query
            var results = route4Me.SearchAddressBookContactsByFilter(
                addressBookGroupParameters,
                out var errorString);

            Assert.That(results, Is.InstanceOf<AddressBookContactsResponse>(), "GetAddressBookContactsByGroupTest failed. " + errorString);
        }

        [Test]
        public void UpdateAddressBookGroupTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupRule = new AddressBookGroupRule
            {
                ID = "address_1",
                Field = "address_1",
                Operator = "not_equal",
                Value = "qwerty1234567"
            };

            var addressBookGroupFilter = new AddressBookGroupFilter
            {
                Condition = "AND",
                Rules = new[] {addressBookGroupRule}
            };

            var addressBookGroupParameters = new AddressBookGroup
            {
                GroupId = _group2.GroupId,
                GroupColor = "cd74e6",
                Filter = addressBookGroupFilter
            };

            // Run the query
            var addressBookGroup = route4Me.UpdateAddressBookGroup(
                addressBookGroupParameters,
                out var errorString);

            Assert.IsNotNull(
                addressBookGroup,
                "UpdateAddressBookGroupTest failed. " + errorString);
        }

        [Test]
        public void AddAddressBookGroupTest()
        {
            var addressBookGroup = CreateAddreessBookGroup(out var errorString);

            Assert.IsNotNull(addressBookGroup, "AddAddreessBookGroupTest failed. " + errorString);

            _lsGroups.Add(addressBookGroup.GroupId);
        }

        private static AddressBookGroup CreateAddreessBookGroup(out string errorString)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupRule = new AddressBookGroupRule
            {
                ID = "address_1",
                Field = "address_1",
                Operator = "not_equal",
                Value = "qwerty123456"
            };

            var addressBookGroupFilter = new AddressBookGroupFilter
            {
                Condition = "AND",
                Rules = new[] {addressBookGroupRule}
            };

            var addressBookGroupParameters = new AddressBookGroup
            {
                GroupName = "All Group",
                GroupColor = "92e1c0",
                Filter = addressBookGroupFilter
            };

            // Run the query
            var addressBookGroup = route4Me.AddAddressBookGroup(
                addressBookGroupParameters,
                out errorString);

            return addressBookGroup;
        }

        private static StatusResponse DeleteAddressBookGroup(string remeoveGroupId, out string errorString)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressGroupParams = new AddressBookGroupParameters
            {
                groupID = remeoveGroupId
            };

            var status = route4Me.RemoveAddressBookGroup(
                addressGroupParams,
                out errorString);
            return status;
        }

        [Test]
        public void RemoveAddressBookGroupTest()
        {
            var response = DeleteAddressBookGroup(_group1.GroupId, out var errorString);

            Assert.IsTrue(response.Status, "RemoveAddressBookGroupTest failed. " + errorString);
        }

        [OneTimeTearDown]
        public void AddressBookGroupsGroupCleanup()
        {
            foreach (var curGroupId in _lsGroups)
            {
                var resposne = DeleteAddressBookGroup(curGroupId, out _);

                Assert.IsTrue(
                    resposne.Status,
                    "Removing of the address book group with group ID = " + curGroupId + " failed.");
            }
        }
    }
}