using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class AddressBookGroupFixture : IDisposable
    {
        public readonly string c_ApiKey = ApiKeys.ActualApiKey;

        public AddressBookGroup group1, group2;

        static readonly List<string> lsGroups = new List<string>();

        public AddressBookGroupFixture()
        {
            group1 = CreateAddreessBookGroup(out string errorString);

            Assert.NotNull(group1);

            group2 = CreateAddreessBookGroup(out errorString);

            Assert.NotNull(group2);

            lsGroups.Add(group2.GroupId);
        }

        public void Dispose()
        {
            foreach (string curGroupID in lsGroups)
            {
                StatusResponse resposne = DeleteAddreessBookGroup(curGroupID, out string errorString);

                Assert.True(resposne.Status,
                    "Removing of the address book group with group ID = " + curGroupID + " failed.");
            }
        }

        private StatusResponse DeleteAddreessBookGroup(string remeoveGroupID, out string errorString)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressGroupParams = new AddressBookGroupParameters()
            {
                groupID = remeoveGroupID
            };

            StatusResponse status = route4Me.RemoveAddressBookGroup(
                                                addressGroupParams,
                                                out errorString);
            return status;
        }

        private AddressBookGroup CreateAddreessBookGroup(out string errorString)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupRule = new AddressBookGroupRule()
            {
                ID = "address_1",
                Field = "address_1",
                Operator = "not_equal",
                Value = "qwerty123456"
            };

            var addressBookGroupFilter = new AddressBookGroupFilter()
            {
                Condition = "AND",
                Rules = new AddressBookGroupRule[] { addressBookGroupRule }
            };

            var addressBookGroupParameters = new AddressBookGroup()
            {
                GroupName = "All Group",
                GroupColor = "92e1c0",
                Filter = addressBookGroupFilter
            };

            // Run the query
            AddressBookGroup addressBookGroup = route4Me.AddAddressBookGroup(
                                                            addressBookGroupParameters,
                                                            out errorString);

            return addressBookGroup;
        }
    }
}
