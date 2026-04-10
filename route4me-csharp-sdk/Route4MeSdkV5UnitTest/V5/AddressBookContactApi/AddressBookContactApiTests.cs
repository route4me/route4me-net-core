using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes;
using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.AddressBookContact;

using AddressBookContact = Route4MeSDK.DataTypes.V5.AddressBookContact;
using AddressBookContactsResponse = Route4MeSDK.DataTypes.V5.AddressBookContactsResponse;
using AddressBookParameters = Route4MeSDK.QueryTypes.V5.AddressBookParameters;
using AddressStopType = Route4MeSDK.DataTypes.V5.AddressStopType;
using StatusResponse = Route4MeSDK.DataTypes.V5.StatusResponse;

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
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact1 = route4Me.AddAddressBookContact(contactParams, out var resultResponse);

            lsCreatedContacts.Add(contact1);

            contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
                CachedLat = 38.024664,
                CachedLng = -77.338834,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact2 = route4Me.AddAddressBookContact(contactParams, out resultResponse);

            lsCreatedContacts.Add(contact2);


            contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
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
        public void GetAddressBookContactsWithRequestBodyTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressBookParameters = new AddressBookContactsBodyRequest()
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
        public void GetAddressBookContactsPaginatedTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressBookParametersPaginated = new AddressBookParametersPaginated()
            {
                Display = "all",
                Page = 0,
                PerPage = 10
            };

            // Run the query
            var response = route4Me.GetAddressBookContactsPaginated(
                addressBookParametersPaginated,
                out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContactsResponse)));
        }

        [Test]
        public void GetAddressBookContactsBodyPaginatedTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressBookParametersPaginated = new AddressBookContactsBodyPaginatedRequest()
            {
                Page = 0,
                PerPage = 10
            };

            // Run the query
            var response = route4Me.GetAddressBookContactsPaginated(
                addressBookParametersPaginated,
                out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContactsResponse)));
        }

        [Test]
        public void GetAddressBookContactsClusteringTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressBookParametersPaginated = new AddressBookParametersClustering()
            {
            };

            // Run the query
            var response = route4Me.GetAddressBookContactsClustering(
                addressBookParametersPaginated,
                out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContactsClusteringResponse)));
        }

        [Test]
        public void GetAddressBookContactsClusteringBodyTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var addressBookParametersPaginated = new AddressBookParametersClusteringBodyRequest()
            {
                Clustering = new Clustering() { Precision = 5 }
            };

            // Run the query
            var response = route4Me.GetAddressBookContactsClustering(
                addressBookParametersPaginated,
                out _);

            Assert.That(response.GetType(), Is.EqualTo(typeof(AddressBookContactsClusteringResponse)));
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
        public void DeleteAddressBookContactsTest()
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
        [Ignore("HTTP 403 error, need account with appropriate permissions")]
        public void DeleteAddressBookContactsByAreasTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
                CachedLat = 15.222,
                CachedLng = -17.333,
                AddressStopType = AddressStopType.PickUp.Description()
            };
            var contact = route4Me.AddAddressBookContact(contactParams, out var err1);

            lsCreatedContacts.Add(contact);

            route4Me.RemoveAddressBookContactsByAreas(new AddressBookContactsFilter()
            {
                SelectedAreas = new SelectedArea[]
                {
                    new SelectedArea()
                    {
                        Type = SelectedAreasType.Circle.Description(),
                        Value = new SelectedAreasValueCircle()
                        {
                            Center = new GeoPoint()
                            {
                                Latitude = 15.222,
                                Longitude = -17.333
                            },
                            Distance = 1
                        }
                    }
                }
            }, out var err3);

            Assert.That(err3, Is.Null);
        }

        [Test]
        public void GetCustomFieldsTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var res = route4Me.GetCustomFields(out var resultResponse);

            Assert.That(res.GetType(), Is.EqualTo(typeof(CustomFieldsResponse)));
        }

        [Test]
        public void CreateAddressBookContactTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                ScheduleBlacklist = new string[] { "2022-06-28" },
                AddressStopType = AddressStopType.PickUp.Description(),
                AddressCustomData = new Dictionary<string, string>() { { "key1", "value1" } }
            };

            var contact = route4Me.AddAddressBookContact(contactParams, out ResultResponse resultResponse);
            Assert.That(contact.GetType(), Is.EqualTo(typeof(AddressBookContact)));

            lsCreatedContacts.Add(contact);
        }

        [Test]
        public void BatchCreatingAddressBookContactsTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var lsContacts = new List<AddressBookContact>
            {
                new AddressBookContact
                {
                    FirstName = "Test FirstName " + new Random().Next(0, 1000),
                    Address1 = "Test Address1 " + new Random().Next(0, 1000),
                    CachedLat = 38.024754,
                    CachedLng = -77.338914,
                    AddressStopType = AddressStopType.PickUp.Description()
                },
                new AddressBookContact
                {
                    FirstName = "Test FirstName " + new Random().Next(0, 1000),
                    Address1 = "Test Address1 " + new Random().Next(0, 1000),
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
                route4Me.BatchCreateAddressBookContacts(contactParams, mandatoryFields, out var resultResponse);

            Assert.That(response.GetType(), Is.EqualTo(typeof(StatusResponse)));
            Assert.True(response.Status);

            lsCreatedContacts.AddRange(lsContacts);
        }

        [Test]
        public void UpdateAddressBookContactTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact = route4Me.AddAddressBookContact(contactParams, out _);
            Assert.That(contact.GetType(), Is.EqualTo(typeof(AddressBookContact)));

            lsCreatedContacts.Add(contact);

            var email = "zozo6534654gfhfghfgsdfsd@gmail.com";
            contact.AddressEmail = email;

            var result = route4Me.UpdateAddressBookContact(contact.AddressId.Value, contact, out _);

            var updated = route4Me.GetAddressBookContactsByIds(new[] { result.AddressId.Value }, out _);

            Assert.That(updated.Results[0].AddressEmail, Is.EqualTo(email));
        }

        [Test]
        public void BatchUpdateAddressBookContactTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var contactParams1 = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contactParams2 = new AddressBookContact
            {
                FirstName = "Test FirstName 2 " + new Random().Next(0, 1000),
                Address1 = "Test Address1 2 " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact1 = route4Me.AddAddressBookContact(contactParams1, out var err1);
            var contact2 = route4Me.AddAddressBookContact(contactParams2, out var err2);

            lsCreatedContacts.Add(contact1);
            lsCreatedContacts.Add(contact2);

            route4Me.BatchUpdateAddressBookContact(new AddressBookContactMultiple()
            {
                AddressIds = new[] { contact1.AddressId.Value, contact2.AddressId.Value, },
                FirstName = "Test FirstName 3 " + new Random().Next(0, 1000),
                Address1 = contactParams1.Address1,
                CachedLat = contact1.CachedLat,
                CachedLng = contact1.CachedLng,
                AddressStopType = contact1.AddressStopType
            }, out var err3);

            var updated = route4Me.GetAddressBookContactsByIds(new[] { contact1.AddressId.Value, contact2.AddressId.Value }, out _);

            Assert.That(updated.Results[0].FirstName.Contains("Test FirstName 3 "));
            Assert.That(updated.Results[1].FirstName.Contains("Test FirstName 3 "));
        }

        [Test]
        [Ignore("HTTP 403 error, need account with appropriate permissions")]
        public void UpdateAddressBookContactByAreasTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            var contactParams1 = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(0, 1000),
                Address1 = "Test Address1 " + new Random().Next(0, 1000),
                CachedLat = 38.1111,
                CachedLng = -77.2222,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contactParams2 = new AddressBookContact
            {
                FirstName = "Test FirstName 2 " + new Random().Next(0, 1000),
                Address1 = "Test Address1 2 " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var contact1 = route4Me.AddAddressBookContact(contactParams1, out var err1);
            var contact2 = route4Me.AddAddressBookContact(contactParams2, out var err2);

            lsCreatedContacts.Add(contact1);
            lsCreatedContacts.Add(contact2);

            route4Me.UpdateAddressBookContactByAreas(new UpdateAddressBookContactByAreasRequest()
            {
                Data = new AddressBookContact()
                {
                    FirstName = "Test FirstName 3 " + new Random().Next(0, 1000),
                    Address1 = contact2.Address1,
                    CachedLat = contact1.CachedLat,
                    CachedLng = contact1.CachedLng,
                    AddressStopType = contact1.AddressStopType
                },
                Filter = new AddressBookContactsFilter()
                {
                    Center = new GeoPoint()
                    {
                        Latitude = 38.1111,
                        Longitude = -77.2222
                    }
                }

            }, out var err3);

            Assert.That(err3, Is.Null);
        }

        [Test]
        [Ignore("HTTP 403 error, need account with appropriate permissions")]
        public void ExportAddressesByAreasTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);


            var result = route4Me.ExportAddressesByAreas(new AddressExportByAreasParameters()
            {
                Filter = new AddressExportByAreasFilter()
                {
                    Filename = "ExportAddresses " + DateTime.Now.ToString("yyMMddHHmmss"),
                    SelectedAreas = new SelectedArea[]
                    {
                        new SelectedArea()
                        {
                            Type = SelectedAreasType.Circle.Description(),
                            Value = new SelectedAreasValueCircle()
                            {
                                Center = new GeoPoint()
                                {
                                    Latitude =  40.00015,
                                    Longitude = 80.00028
                                },
                                Distance = 100000
                            }
                        }
                    }
                }
            }, out var err);

            Assert.That(result.IsSuccessStatusCode);
        }

        [Test]
        [Ignore("HTTP 403 error, need account with appropriate permissions")]
        public void ExportAddressesByAreaIdsTest()
        {
            var route4Me = new Route4MeManager(ApiKey);

            var territories = route4Me.GetTerritories(new AvoidanceZoneQuery(),
                out string errorString);

            var route4MeV5 = new Route4MeManagerV5(ApiKey);

            route4MeV5.ExportAddressesByAreaIds(new AddressExportByAreaIdsParameters()
            {
                TerritoryIds = new string[] { territories.First().TerritoryId },
                Filename = "ExpAddrByAreaIdes " + DateTime.Now.ToString("yyMMddHHmmss")
            }, out var res);

            Assert.That(res.Status);
        }



        [Test]
        public void AddressBookContactCustomDataTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            // Create custom data with multiple key-value pairs
            var customData = new Dictionary<string, string>
            {
                { "customer_id", "CUST-12345" },
                { "order_number", "ORD-98765" },
                { "delivery_notes", "Leave at back door" },
                { "priority", "high" }
            };

            // Create a contact with custom data
            var contactParams = new AddressBookContact
            {
                FirstName = "CustomData Test " + new Random().Next(0, 1000),
                Address1 = "Test Address " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description(),
                AddressCustomData = customData
            };

            var createdContact = route4Me.AddAddressBookContact(contactParams, out var createError);

            Assert.That(createError, Is.Null, "Failed to create contact with custom data");
            Assert.That(createdContact, Is.Not.Null, "Created contact should not be null");
            Assert.That(createdContact.AddressId, Is.Not.Null, "Created contact should have an AddressId");

            lsCreatedContacts.Add(createdContact);

            // Retrieve the contact and verify custom data was persisted
            var retrievedContact = route4Me.GetAddressBookContactById(
                createdContact.AddressId.Value,
                out var retrieveError);

            Assert.That(retrieveError, Is.Null, "Failed to retrieve contact");
            Assert.That(retrievedContact, Is.Not.Null, "Retrieved contact should not be null");
            Assert.That(retrievedContact.AddressCustomData, Is.Not.Null, "Custom data should not be null");

            // Verify the custom data values
            var retrievedCustomData = retrievedContact.AddressCustomData as Dictionary<string, string>;
            Assert.That(retrievedCustomData, Is.Not.Null, "Custom data should be a Dictionary<string, string>");
            Assert.That(retrievedCustomData.ContainsKey("customer_id"), Is.True, "Custom data should contain 'customer_id'");
            Assert.That(retrievedCustomData["customer_id"], Is.EqualTo("CUST-12345"), "customer_id value should match");
            Assert.That(retrievedCustomData.ContainsKey("order_number"), Is.True, "Custom data should contain 'order_number'");
            Assert.That(retrievedCustomData["order_number"], Is.EqualTo("ORD-98765"), "order_number value should match");
            Assert.That(retrievedCustomData.ContainsKey("delivery_notes"), Is.True, "Custom data should contain 'delivery_notes'");
            Assert.That(retrievedCustomData["delivery_notes"], Is.EqualTo("Leave at back door"), "delivery_notes value should match");
            Assert.That(retrievedCustomData.ContainsKey("priority"), Is.True, "Custom data should contain 'priority'");
            Assert.That(retrievedCustomData["priority"], Is.EqualTo("high"), "priority value should match");
        }

        [Test]
        public void UpdateAddressBookContactCustomDataTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            // Create initial custom data
            var initialCustomData = new Dictionary<string, string>
            {
                { "initial_key", "initial_value" }
            };

            // Create a contact with initial custom data
            var contactParams = new AddressBookContact
            {
                FirstName = "Update Test " + new Random().Next(0, 1000),
                Address1 = "Test Address " + new Random().Next(0, 1000),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressStopType = AddressStopType.PickUp.Description(),
                AddressCustomData = initialCustomData
            };

            var createdContact = route4Me.AddAddressBookContact(contactParams, out var createError);

            Assert.That(createError, Is.Null, "Failed to create contact");
            Assert.That(createdContact, Is.Not.Null);

            lsCreatedContacts.Add(createdContact);

            // Update custom data with new values
            var updatedCustomData = new Dictionary<string, string>
            {
                { "updated_key", "updated_value" },
                { "new_field", "new_value" }
            };

            createdContact.AddressCustomData = updatedCustomData;

            var updatedContact = route4Me.UpdateAddressBookContact(
                createdContact.AddressId.Value,
                createdContact,
                out var updateError);

            Assert.That(updateError, Is.Null, "Failed to update contact custom data");

            // Retrieve and verify updated custom data
            var retrievedContact = route4Me.GetAddressBookContactById(
                createdContact.AddressId.Value,
                out var retrieveError);

            Assert.That(retrieveError, Is.Null, "Failed to retrieve updated contact");
            Assert.That(retrievedContact.AddressCustomData, Is.Not.Null, "Updated custom data should not be null");

            var retrievedCustomData = retrievedContact.AddressCustomData as Dictionary<string, string>;
            Assert.That(retrievedCustomData, Is.Not.Null, "Custom data should be a Dictionary<string, string>");
            Assert.That(retrievedCustomData.ContainsKey("updated_key"), Is.True, "Custom data should contain 'updated_key'");
            Assert.That(retrievedCustomData["updated_key"], Is.EqualTo("updated_value"), "updated_key value should match");
            Assert.That(retrievedCustomData.ContainsKey("new_field"), Is.True, "Custom data should contain 'new_field'");
            Assert.That(retrievedCustomData["new_field"], Is.EqualTo("new_value"), "new_field value should match");
        }
    }
}