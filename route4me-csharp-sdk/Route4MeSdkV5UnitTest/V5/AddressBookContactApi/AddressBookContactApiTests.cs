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
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private static List<AddressBookContact> _lsCreatedContacts;

        [SetUp]
        public void Setup()
        {
            #region Create Test Contacts

            var route4Me = new Route4MeManagerV5(CApiKey);

            _lsCreatedContacts = new List<AddressBookContact>();

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact1 = route4Me.AddAddressBookContact(contactParams, out _);

            _lsCreatedContacts.Add(contact1);


            contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024664,
                CachedLng = -77.338834,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact2 = route4Me.AddAddressBookContact(contactParams, out _);

            _lsCreatedContacts.Add(contact2);


            contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024684,
                CachedLng = -77.338854,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact3 = route4Me.AddAddressBookContact(contactParams, out _);

            _lsCreatedContacts.Add(contact3);

            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            if (_lsCreatedContacts.Count > 0)
            {
                var contactIDs = _lsCreatedContacts.Where(x => x != null && x.AddressId != null)
                    .Select(x => (long) x.AddressId);

                route4Me.RemoveAddressBookContacts(
                    contactIDs.ToArray(),
                    out var resultResponse);

                Assert.Null(resultResponse);
            }
        }

        [Test]
        public void GetAddressBookContactsTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

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
            var route4Me = new Route4MeManagerV5(CApiKey);

            var addressId = (int) _lsCreatedContacts[_lsCreatedContacts.Count - 1].AddressId;

            var response = route4Me.GetAddressBookContactById(addressId, out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContact)));
        }

        [Test]
        public void GetAddressBookContactsByIDsTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var addressId1 = (long)_lsCreatedContacts[_lsCreatedContacts.Count - 1].AddressId;
            var addressId2 = (long) _lsCreatedContacts[_lsCreatedContacts.Count - 2].AddressId;

            long[] addressIDs = {addressId1, addressId2};

            var response = route4Me.GetAddressBookContactsByIds(addressIDs, out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContactsResponse)));
        }

        [Test]
        public void GetDepotsFromAddressBookTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            // Run the query
            var response = route4Me.GetDepotsFromAddressBook(out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContact[])));
        }

        [Test]
        public void DeleteAddressBookContacts()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var lsSize = _lsCreatedContacts.Count - 1;

            long[] addressIDs =
                {(long) _lsCreatedContacts[lsSize - 1].AddressId, (long) _lsCreatedContacts[lsSize - 2].AddressId};

            route4Me.RemoveAddressBookContacts(addressIDs, out var resultResponse);

            Assert.Null(resultResponse);

            _lsCreatedContacts.RemoveAt(lsSize - 1);
            _lsCreatedContacts.RemoveAt(lsSize - 2);
        }

        [Test]
        public void CreateAddressBookContact()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

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

            _lsCreatedContacts.Add(contact);
        }

        [Test]
        public void BatchCreatingAddressBookContacts()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

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
                route4Me.BatchCreateAdressBookContacts(contactParams, mandatoryFields, out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(StatusResponse)));
            Assert.True(response.status);
            //foreach (var cont in response.results) lsCreatedContacts.Add(cont);
        }
    }
}