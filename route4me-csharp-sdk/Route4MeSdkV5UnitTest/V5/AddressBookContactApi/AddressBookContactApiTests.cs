﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSdkV5UnitTest.V5;
using Xunit;
using Xunit.Abstractions;

namespace Route4MeSdkV5UnitTest.AddressBookContactApi
{
    public class AddressBookContactApiTests : IDisposable
    {
        static string c_ApiKey = ApiKeys.ActualApiKey;

        private readonly ITestOutputHelper _output;

        static List<AddressBookContact> lsCreatedContacts;

        public AddressBookContactApiTests(ITestOutputHelper output)
        {
            _output = output;

            #region Create Test Contacts

            var route4Me = new Route4MeManagerV5(c_ApiKey);

            lsCreatedContacts = new List<AddressBookContact>();

            var contactParams = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact1 = route4Me.AddAddressBookContact(contactParams, out ResultResponse resultResponse);

            lsCreatedContacts.Add(contact1);


            contactParams = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024664,
                CachedLng = -77.338834,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact2 = route4Me.AddAddressBookContact(contactParams, out resultResponse);

            lsCreatedContacts.Add(contact2);


            contactParams = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024684,
                CachedLng = -77.338854,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact3 = route4Me.AddAddressBookContact(contactParams, out resultResponse);

            lsCreatedContacts.Add(contact3);

            #endregion
        }

        public void Dispose()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var lsRemLocations = new List<string>();

            if (lsCreatedContacts.Count > 0)
            {
                //foreach (int loc1 in lsRemoveContacts) lsRemLocations.Add(loc1.ToString());

                var contactIDs = lsCreatedContacts.Where(x => (x != null && x.AddressId != null)).Select(x => (int)(x.AddressId));

                bool removed = route4Me.RemoveAddressBookContacts(
                                                contactIDs.ToArray(),
                                                out ResultResponse resultResponse);

                Assert.Null(resultResponse);
            }
        }

        [Fact]
        public void GetAddressBookContactsTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var addressBookParameters = new AddressBookParameters()
            {
                Limit = 1,
                Offset = 16
            };

            // Run the query
            var response = route4Me.GetAddressBookContacts(
                                    addressBookParameters,
                                    out ResultResponse resultResponse);

            Assert.IsType<AddressBookContactsResponse>(response);
        }

        [Fact]
        public void GetAddressBookContactByIdTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            int addressId = (int)lsCreatedContacts[lsCreatedContacts.Count - 1].AddressId;

            var response = route4Me.GetAddressBookContactById(addressId, out ResultResponse resultResponse);

            Assert.IsType<AddressBookContact>(response);
        }

        [Fact]
        public void GetAddressBookContactsByIDsTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            int addressId1 = (int)lsCreatedContacts[lsCreatedContacts.Count - 1].AddressId;
            int addressId2 = (int)lsCreatedContacts[lsCreatedContacts.Count - 2].AddressId;

            int[] addressIDs = new int[] { addressId1, addressId2 };

            var response = route4Me.GetAddressBookContactsByIds(addressIDs, out ResultResponse resultResponse);

            Assert.IsType<AddressBookContactsResponse>(response);
        }

        [Fact]
        public void DeleteAddressBookContacts()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var lsSize = lsCreatedContacts.Count - 1;

            int[] addressIDs = new int[] { (int)lsCreatedContacts[lsSize - 1].AddressId, (int)lsCreatedContacts[lsSize - 2].AddressId };

            var response = route4Me.RemoveAddressBookContacts(addressIDs, out ResultResponse resultResponse);

            Assert.Null(resultResponse);

            lsCreatedContacts.RemoveAt(lsSize - 1);
            lsCreatedContacts.RemoveAt(lsSize - 2);
        }

        [Fact]
        public void CreateAddressBookContact()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var contactParams = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact = route4Me.AddAddressBookContact(contactParams, out ResultResponse resultResponse);
            Assert.IsType<AddressBookContact>(contact);

            lsCreatedContacts.Add(contact);
        }

        [Fact]
        public void BatchCreatingAddressBookContacts()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var lsContacts = new List<AddressBookContact>()
            {
                new AddressBookContact()
                {
                    FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                    Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                    CachedLat = 38.024754,
                    CachedLng = -77.338914,
                    AddressStopType = AddressStopType.PickUp.Description()
                },
                new AddressBookContact()
                {
                    FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                    Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                    CachedLat = 38.024554,
                    CachedLng = -77.338714,
                    AddressStopType = AddressStopType.PickUp.Description()
                }
            };

            var contactParams = new Route4MeManagerV5.BatchCreatingAddressBookContactsRequest()
            {
                Data = lsContacts.ToArray()
            };

            var response = route4Me.BatchCreateAdressBookContacts(contactParams, out ResultResponse resultResponse);

            Assert.IsType<StatusResponse>(response);
            Assert.True(response.status);
            //foreach (var cont in response.results) lsCreatedContacts.Add(cont);
        }
    }
}
