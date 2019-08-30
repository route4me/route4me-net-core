using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Route4MeDB.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Route4MeDB.ApplicationCore.Specifications;
using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;

namespace Route4MeDB.IntegrationTests.LambdaExpressions
{
    public class AddressBookContactLinqQueries
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly AddressBookContactRepository _contactRepository;
        private AddressBookContactBuilder contactBuilder { get; } = new AddressBookContactBuilder();
        private readonly ITestOutputHelper _output;

        public AddressBookContactLinqQueries(ITestOutputHelper output)
        {
            var dbOptions = new DbContextOptionsBuilder<Route4MeDbContext>()
                .UseInMemoryDatabase(databaseName: "Route4MeDB")
                .Options;
            _route4meDbContext = new Route4MeDbContext(dbOptions);
            _contactRepository = new AddressBookContactRepository(_route4meDbContext);
            _output = output;

        }

        [Fact]
        public async Task GetAddressBookContactsAsymc()
        {
            var createdContacts = await Create2Contacts();

            var contacts = _route4meDbContext.AddressBookContacts.Skip(0).Take(2);

            var contactsFromRepo = await _contactRepository.GetAddressBookContactsAsync(0, 1000);

            await contacts.ForEachAsync(x =>
            {
                Assert.Contains<AddressBookContact>(x, contactsFromRepo);
            });
        }

        [Fact]
        public async Task GetAddressBookContactByIdAsync()
        {
            var createdContact = await CreateContact();

            var contactDbId = _route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == createdContact.AddressDbId)
                .FirstOrDefault().AddressDbId;

            var contactSpec = new AddressBookContactSpecification(contactDbId);

            var contactFromRepo = await _contactRepository.GetByIdAsync(contactSpec);

            Assert.Equal<AddressBookContact>(contactFromRepo, createdContact);
        }

        [Fact]
        public async Task GetAddressBookContactsByIdsAsync()
        {
            var createdContacts = await Create2Contacts();

            var createdContactDbIds = new int[] { createdContacts[0].AddressDbId, createdContacts[1].AddressDbId };

            var contacts = _route4meDbContext.AddressBookContacts
                .Where(x => (createdContactDbIds.Contains(x.AddressDbId)));

            var contactsFromRepo = await _contactRepository.GetAddressBookContactsAsync(createdContactDbIds);

            await contacts.ForEachAsync<AddressBookContact>(c =>
            {
                Assert.Contains<AddressBookContact>(c, contactsFromRepo);
            });
        }

        [Fact]
        public async Task UpdateAddressBookContactAsync()
        {
            var createdContact = await CreateContact();

            createdContact.FirstName = "Peter Modified";
            createdContact.LastName = "Newman Modified";

            _route4meDbContext.SaveChanges();

            var contactFromRepo = await _contactRepository
                .GetAddressBookContactByIdAsync(createdContact.AddressDbId);

            Assert.Equal("Peter Modified", contactFromRepo.FirstName);
            Assert.Equal("Newman Modified", contactFromRepo.LastName);
        }

        [Fact]
        public async Task RemoveAddressBookContactAsync()
        {
            var createdContact = await CreateContact();

            int createdContactDbId = createdContact.AddressDbId;

            var contactExistedInRepo = await _contactRepository
                .GetAddressBookContactByIdAsync(createdContactDbId);

            Assert.NotNull(contactExistedInRepo);

            _route4meDbContext.AddressBookContacts.Remove(createdContact);
            await _route4meDbContext.SaveChangesAsync();

            var result = await _contactRepository.GetAddressBookContactByIdAsync(createdContactDbId);

            Assert.Null(result);
        }

        #region // Create Contacts
        private async Task<List<AddressBookContact>> Create2Contacts()
        {
            var firstContactParams = contactBuilder.WithDefaultValues();
            var secondContactParams = contactBuilder.WithCustomData();

            var firstContact = await _route4meDbContext.AddressBookContacts.AddAsync(firstContactParams);
            var secondContact = await _route4meDbContext.AddressBookContacts.AddAsync(secondContactParams);

            _route4meDbContext.SaveChanges();

            return await Task.Run(() =>
            {
                return new List<AddressBookContact>() { firstContact.Entity, secondContact.Entity };
            });

        }

        private async Task<AddressBookContact> CreateContact()
        {
            var contactParams = contactBuilder.WithDefaultValues();

            var contact = await _route4meDbContext.AddressBookContacts.AddAsync(contactParams);

            await _route4meDbContext.SaveChangesAsync();

            return await Task.Run(() =>
            {
                return contact.Entity;
            });

        }

        #endregion
    }
}
