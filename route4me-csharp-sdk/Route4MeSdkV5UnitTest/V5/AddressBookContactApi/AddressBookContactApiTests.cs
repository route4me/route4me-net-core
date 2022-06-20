using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.AddressBookContactApi
{
    [TestFixture]
    public class AddressBookContactApiTests
    {
        private static readonly string ApiKey = ApiKeys.ActualApiKey;

        private static List<AddressBookContact> lsCreatedContacts;


        [SetUp]
        public void Setup()
        {
            #region Create Test Contacts

            var route4Me = new Route4MeManagerV5(ApiKey);

            lsCreatedContacts = new List<AddressBookContact>();

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact1 = route4Me.AddAddressBookContact(contactParams, out var resultResponse);

            lsCreatedContacts.Add(contact1);


            contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024664,
                CachedLng = -77.338834,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact2 = route4Me.AddAddressBookContact(contactParams, out resultResponse);

            lsCreatedContacts.Add(contact2);


            contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024684,
                CachedLng = -77.338854,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact3 = route4Me.AddAddressBookContact(contactParams, out resultResponse);

            lsCreatedContacts.Add(contact3);

            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var lsRemLocations = new List<string>();

            if (lsCreatedContacts.Count > 0)
            {
                var contactIDs = lsCreatedContacts.Where(x => x != null && x.AddressId != null)
                    .Select(x => (long)x.AddressId);

                var removed = route4Me.RemoveAddressBookContacts(
                    contactIDs.ToArray(),
                    out var resultResponse);

                Assert.Null(resultResponse);
            }
        }

        [Test]
        public void GetAddressBookContactsTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Limit = 1,
                Offset = 16
            };

            // Run the query
            var response = route4Me.GetAddressBookContacts(
                addressBookParameters,
                out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContactsResponse)));
        }

        [Test]
        public void GetAddressBookContactByIdTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressId = (int)lsCreatedContacts[lsCreatedContacts.Count - 1].AddressId;

            var response = route4Me.GetAddressBookContactById(addressId, out var resultResponse);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContact)));
        }

        [Test]
        public void GetAddressBookContactsByIDsTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressId1 = (long)lsCreatedContacts[lsCreatedContacts.Count - 1].AddressId;
            var addressId2 = (long)lsCreatedContacts[lsCreatedContacts.Count - 2].AddressId;

            long[] addressIDs = { addressId1, addressId2 };

            var response = route4Me.GetAddressBookContactsByIds(addressIDs, out var resultResponse);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContactsResponse)));
        }

        [Test]
        public void GetDepotsFromAddressBookTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            // Run the query
            var response = route4Me.GetDepotsFromAddressBook(out ResultResponse resultResponse);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContact[])));
        }

        [Test]
        public void DeleteAddressBookContacts()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var lsSize = lsCreatedContacts.Count - 1;

            long[] addressIDs =
                {(long) lsCreatedContacts[lsSize - 1].AddressId, (long) lsCreatedContacts[lsSize - 2].AddressId};

            var response = route4Me.RemoveAddressBookContacts(addressIDs, out var resultResponse);

            Assert.Null(resultResponse);

            lsCreatedContacts.RemoveAt(lsSize - 1);
            lsCreatedContacts.RemoveAt(lsSize - 2);
        }

        [Test]
        public void CreateAddressBookContact()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact = route4Me.AddAddressBookContact(contactParams, out _);
            Assert.That(contact.GetType(), Is.EqualTo(typeof(AddressBookContact)));

            lsCreatedContacts.Add(contact);
        }

        [Test]
        public void BatchCreatingAddressBookContacts()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var lsContacts = new List<AddressBookContact>
            {
                new AddressBookContact
                {
                    FirstName = "Test FirstName " + new Random().Next(),
                    Address1 = "Test Address1 " + new Random().Next(),
                    CachedLat = 38.024754,
                    CachedLng = -77.338914,
                    AddressStopType = AddressStopType.PickUp.Description()
                },
                new AddressBookContact
                {
                    FirstName = "Test FirstName " + new Random().Next(),
                    Address1 = "Test Address1 " + new Random().Next(),
                    CachedLat = 38.024554,
                    CachedLng = -77.338714,
                    AddressStopType = AddressStopType.PickUp.Description()
                }
            };

            var mandatoryFields = new[]
            {
                R4MeUtils.GetPropertyName(() => lsContacts[0].FirstName),
                R4MeUtils.GetPropertyName(() => lsContacts[0].Address1),
                R4MeUtils.GetPropertyName(() => lsContacts[0].CachedLat),
                R4MeUtils.GetPropertyName(() => lsContacts[0].CachedLng),
                R4MeUtils.GetPropertyName(() => lsContacts[0].AddressStopType)
            };

            var contactParams = new BatchCreatingAddressBookContactsRequest
            {
                Data = lsContacts.ToArray()
            };

            var response =
                route4Me.BatchCreateAdressBookContacts(contactParams, mandatoryFields, out var resultResponse);

            Assert.That(response.GetType(), Is.EqualTo(typeof(StatusResponse)));
            Assert.True(response.status);
        }
    }
}