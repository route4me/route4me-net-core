using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Route4MeDB.FunctionalTest;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Route4MeDB.ApplicationCore.Specifications;

namespace Route4MeDB.FunctionalTests.PostgreSqlDb
{
    public class DatabaseContactsFixture : DatabaseFixtureBase
    {
        public DatabaseContactsFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.PostgreSql;

            GetDbContext(DatabaseProviders.PostgreSql);

            _addressBookContactRepository = new AddressBookContactRepository(_route4meDbContext);
        }

        public readonly AddressBookContactRepository _addressBookContactRepository;
    }

    public class AddressBookContactTests : FactAttribute, IClassFixture<DatabaseContactsFixture>, IDisposable
    {
        public IConfigurationRoot Configuration { get; }
        DatabaseContactsFixture fixture;
        private readonly ITestOutputHelper _output;

        public AddressBookContactTests(DatabaseContactsFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            _output = output;
        }

        public void Dispose()
        {
            //this.Dispose();
        }

        [IgnoreIfNoPostgreSqlDb]
        public async Task GetAddressBookContactsTest()
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

            var contacts = fixture._route4meDbContext.AddressBookContacts.Skip(0).Take(2);

            var linqContacts = fixture._route4meDbContext.AddressBookContacts
                .Where(x => addresDbIDs.Contains(x.AddressDbId)).ToList<AddressBookContact>();

            foreach (var linqContact in linqContacts)
            {
                Assert.Contains(linqContact, contacts);
            }
        }

        [IgnoreIfNoPostgreSqlDb]
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

        [IgnoreIfNoPostgreSqlDb]
        public async Task GetExistingAddressBookContactAsync()
        {
            var firstAddressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(firstAddressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            int createdAddressDbId = firstAddressBookContact.AddressDbId;

            var addressBookContactFromRepo = await fixture._addressBookContactRepository
                .GetAddressBookContactByIdAsync(createdAddressDbId);

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => createdAddressDbId == x.AddressDbId).FirstOrDefault();

            Assert.Equal(createdAddressDbId, addressBookContactFromRepo.AddressDbId);
            Assert.Equal(firstAddressBookContact.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(firstAddressBookContact.LastName, addressBookContactFromRepo.LastName);
            Assert.Equal(firstAddressBookContact.FirstName, linqContact.FirstName);
            Assert.Equal(firstAddressBookContact.LastName, linqContact.LastName);
        }

        [IgnoreIfNoPostgreSqlDb]
        public async Task UpdateAddressBookContactAsync()
        {
            var addressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            addressBookContact.FirstName = "Peter Modified";
            addressBookContact.LastName = "Newman Modified";

            var updatedContact = await fixture._addressBookContactRepository
                .UpdateAddressBookContactAsync(addressBookContact.AddressDbId, addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == updatedContact.AddressDbId).FirstOrDefault();

            Assert.Equal<AddressBookContact>(updatedContact, linqContact);
        }

        [IgnoreIfNoPostgreSqlDb]
        public async Task CustomDataTestAsync()
        {
            string customDataJsonString = JsonConvert.SerializeObject(fixture.addressBookContactBuilder.testData.AddressCustomDataDic);
            var addressBookContact = fixture.addressBookContactBuilder.WithCustomData();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == addressBookContact.AddressDbId).FirstOrDefault();

            Assert.Equal(customDataJsonString, linqContact.AddressCustomData);
        }

        [IgnoreIfNoPostgreSqlDb]
        public async Task ScheduleBlackListTestAsync()
        {
            var scheduleBlackList = fixture.addressBookContactBuilder.testData.ScheduleBlacklistArray;
            var addressBookContact = fixture.addressBookContactBuilder.WithScheduleBlacklist();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == addressBookContact.AddressDbId).FirstOrDefault();

            Assert.Equal<string[]>(scheduleBlackList, linqContact.ScheduleBlackListArray);
        }

        [IgnoreIfNoPostgreSqlDb]
        public async Task ScheduleTestAsync()
        {
            var schedules = fixture.addressBookContactBuilder.testData.SchedulesArray;
            var addressBookContact = fixture.addressBookContactBuilder.WithSchedule();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == addressBookContact.AddressDbId).FirstOrDefault();

            Assert.Equal(addressBookContact.Schedules, linqContact.Schedules);
        }

        [IgnoreIfNoPostgreSqlDb]
        public async Task RemoveAddressBookContactAsync()
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
