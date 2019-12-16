using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.UnitTests.Builders;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Specifications;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Route4MeDB.FunctionalTests.InMemoryDb
{
    public class DatabaseContactsFixture : DatabaseFixtureBase
    {
        public DatabaseContactsFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.InMemory;

            GetDbContext(DatabaseProviders.InMemory);

            _addressBookContactRepository = new AddressBookContactRepository(_route4meDbContext);
        }

        public AddressBookContactRepository _addressBookContactRepository;
    }

    public class AddressBookContactTests : IDisposable, IClassFixture<DatabaseContactsFixture>
    {
        private readonly ITestOutputHelper _output;
        DatabaseContactsFixture fixture;
        public IConfigurationRoot Configuration { get; }

        public AddressBookContactTests(DatabaseContactsFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            _output = output;
        }

        public void Dispose()
        {
            //_route4meDbContext.Dispose();
        }

        [Fact]
        public async void GetAddressBookContactsTest()
        {
            var addresDbIDs = new List<int>();

            var firstAddressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(firstAddressBookContact);
            int firstAddressDbId = firstAddressBookContact.AddressDbId;
            addresDbIDs.Add(firstAddressDbId);

            var secondAddressBookContact = fixture.addressBookContactBuilder.WithSchedule();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(secondAddressBookContact);
            int secondAddressDbId = secondAddressBookContact.AddressDbId;
            addresDbIDs.Add(secondAddressDbId);

            await fixture._route4meDbContext.SaveChangesAsync();

            var contacts = fixture._route4meDbContext.AddressBookContacts
                .OrderByDescending(x => x.AddressDbId)
                .Skip(0).Take(2);

            var linqContacts = fixture._route4meDbContext.AddressBookContacts
                .Where(x => addresDbIDs.Contains(x.AddressDbId)).ToList<AddressBookContact>();

            foreach (var linqContact in linqContacts)
            {
                Assert.Contains(linqContact, contacts);
            }
        }

        [Fact]
        public async void ImportJsonDataToDataBaseTest()
        {
            string testDataFile = @"TestData/one_complex_contact.json";

            DataExchangeHelper dataExchange = new DataExchangeHelper();

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                AddressBookContact importedContact = dataExchange.ConvertSdkJsonContentToEntity<AddressBookContact>(jsonContent, out string errorString);

                fixture._route4meDbContext.AddressBookContacts.Add(importedContact);

                await fixture._route4meDbContext.SaveChangesAsync();
                int addressDbId = importedContact.AddressDbId;

                var addressSpec = new AddressBookContactSpecification(addressDbId);

                var addressBookContactFromRepo = await fixture.r4mdbManager.ContactsRepository.GetByIdAsync(addressSpec);

                Assert.IsType<AddressBookContact>(addressBookContactFromRepo);
            }
        }

        [Fact]
        public async void ExportContactEntityToSdkContactObject()
        {
            var addressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            var CreatedContact = await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);
            await fixture._route4meDbContext.SaveChangesAsync();

            DataExchangeHelper dataExchange = new DataExchangeHelper();

            var sdkContact = dataExchange.ConvertEntityToSDK<Route4MeSDK.DataTypes.AddressBookContact>(CreatedContact.Entity, out string errorString);

            Assert.IsType<Route4MeSDK.DataTypes.AddressBookContact>(sdkContact);
        }

        [Fact]
        public async void GetsExistingAddressBookContactAsync()
        {
            var existingAddressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            fixture._route4meDbContext.AddressBookContacts.Add(existingAddressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();
            int addressDbId = existingAddressBookContact.AddressDbId;
            //_output.WriteLine($"AddressDbId: {addressDbId}");

            var addressSpec = new AddressBookContactSpecification(addressDbId);

            var addressBookContactFromRepo = await fixture.r4mdbManager.ContactsRepository.GetByIdAsync(addressSpec);

            Assert.Equal(fixture.addressBookContactBuilder.testData.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(fixture.addressBookContactBuilder.testData.LastName, addressBookContactFromRepo.LastName);
        }

        [Fact]
        public async void UpdateAddressBookContactAsync()
        {
            var addressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);
            int firstAddressDbId = addressBookContact.AddressDbId;

            await fixture._route4meDbContext.SaveChangesAsync();

            addressBookContact.FirstName = "Peter Modified";
            addressBookContact.LastName = "Newman Modified";

            var updatedContact = await fixture.r4mdbManager.ContactsRepository
                .UpdateAddressBookContactAsync(addressBookContact.AddressDbId, addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressId == updatedContact.AddressId).FirstOrDefault();

            Assert.Equal(updatedContact.Address1, linqContact.Address1);
            Assert.Equal(updatedContact.AddressId, linqContact.AddressId);
        }

        [Fact]
        public async void CustomDataTestAsync()
        {
            string customDataJsonString = JsonConvert.SerializeObject(fixture.addressBookContactBuilder.testData.AddressCustomDataDic);
            var addressBookContact = fixture.addressBookContactBuilder.WithCustomData();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == addressBookContact.AddressDbId).FirstOrDefault();

            Assert.Equal(customDataJsonString, linqContact.AddressCustomData);
        }

        [Fact]
        public async void ScheduleBlackListTestAsync()
        {
            var scheduleBlackList = fixture.addressBookContactBuilder.testData.ScheduleBlacklistArray;
            var addressBookContact = fixture.addressBookContactBuilder.WithScheduleBlacklist();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == addressBookContact.AddressDbId).FirstOrDefault();

            Assert.Equal<string[]>(scheduleBlackList, linqContact.ScheduleBlackListArray);
        }

        [Fact]
        public async void ScheduleTestAsync()
        {
            var schedules = fixture.addressBookContactBuilder.testData.SchedulesArray;
            var addressBookContact = fixture.addressBookContactBuilder.WithSchedule();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == addressBookContact.AddressDbId).FirstOrDefault();

            Assert.Equal(addressBookContact.Schedules, linqContact.Schedules);
        }

        [Fact]
        public async void RemoveAddressBookContactAsync()
        {
            var addressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var createdContactDbId = addressBookContact.AddressDbId;

            var removedContacts = await fixture._addressBookContactRepository
                .RemoveAddressBookContactAsync(new int[] { createdContactDbId });

            await fixture._route4meDbContext.SaveChangesAsync();

            Assert.Equal(removedContacts[0], createdContactDbId);

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == createdContactDbId).FirstOrDefault();

            Assert.Null(linqContact);
        }
    }
}
