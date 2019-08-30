using Route4MeDB.ApplicationCore.Interfaces;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Threading.Tasks;
using Route4MeDB.ApplicationCore.Entities;
using Route4MeDbLibrary.QueryTypes;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using System.Linq;
using Route4MeDB.ApplicationCore.Specifications;
//using Route4MeDB.ApplicationCore.Entities.BasketAggregate;

namespace Route4MeDB.ApplicationCore.Services
{
    public class AddressBookContactService : IAddressBookContactService
    {
        private readonly IAsyncRepository<AddressBookContact> _addressBookContactRepository;

        public AddressBookContactService(
            IAsyncRepository<AddressBookContact> addressBookContactRepository)
        {
            _addressBookContactRepository = addressBookContactRepository;

        }

        public async Task<AddressBookContact> CreateAddressBookContactAsync(AddressBookContact addressBookContactParameters)
        {
            var propertyNames = addressBookContactParameters.GetType().GetProperties()
                .ToList().Where(x => x.GetValue(addressBookContactParameters) != null && x.Name != "AddressId")
                .Select(y => y.Name).ToList();

            var addressBookContact = new AddressBookContact(addressBookContactParameters, propertyNames);

            return await _addressBookContactRepository.AddAsync(addressBookContact);
        }

        public async Task<AddressBookContact> GetAddressBookContactByIdAsync(int addressDbId)
        {
            var addressSpec = new AddressBookContactSpecification(addressDbId);
            return await _addressBookContactRepository.GetByIdAsync(addressSpec);
        }

        public async Task<IEnumerable<AddressBookContact>> GetAddressBookContactsAsync(int offset, int limit)
        {
            var contactSpec = new AddressBookContactSpecification(offset, limit);

            var result = _addressBookContactRepository.ListAsync(contactSpec).Result.AsEnumerable<AddressBookContact>();

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<IEnumerable<AddressBookContact>> GetAddressBookContactsByIdsAsync(int[] contactDbIDs)
        {
            var contactSpec = new AddressBookContactSpecification(contactDbIDs);

            var result = _addressBookContactRepository.ListAsync(contactSpec).Result.AsEnumerable<AddressBookContact>();

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<IEnumerable<dynamic>> GetAddressBookContactsAsync(string query, string[] fields)
        {
            var result = from addressBookContact in _addressBookContactRepository.ListAllAsync().Result
                         where addressBookContact.Address1.Contains(query) ||
                         addressBookContact.Address2.Contains(query) ||
                         addressBookContact.AddressAlias.Contains(query) ||
                         addressBookContact.AddressCustomData.Contains(query)
                         select new { addressBookContact.FirstName, addressBookContact.LastName };

            return await Task.Run(() =>
            {
                return result;
            });
        }


        public async Task<AddressBookContact> UpdateAddressBookContactAsync(int addressDbId, AddressBookContact addressBookContactParameters)
        {
            //var addressBookContact = await this.GetAddressBookContactByIdAsync(addressId);
            var contactSpec = new AddressBookContactSpecification(addressDbId);
            var addressBookContact = _addressBookContactRepository.GetByIdAsync(contactSpec).Result;

            addressBookContactParameters.GetType().GetProperties().ToList()
                .ForEach(async x => {
                    if (x.GetValue(addressBookContactParameters) != null && x.Name != "AddressDbId")
                        x.SetValue(addressBookContact, x.GetValue(addressBookContactParameters));
                });

            await this._addressBookContactRepository.UpdateAsync(addressBookContact);

            return await Task.Run(() =>
            {
                return addressBookContact;
            });
        }

        public async Task<int[]> RemoveAddressBookContactAsync(int[] addressDbIDs)
        {
            IEnumerable<AddressBookContact> contactsRemove = await this.GetAddressBookContactsByIdsAsync(addressDbIDs);
            var removedAddressDbIDs = new List<int>();

            contactsRemove.ToList().ForEach(x =>
            {
                this._addressBookContactRepository.DeleteAsync(x);
                removedAddressDbIDs.Add(x.AddressDbId);
            });

            return await Task.Run(() =>
            {
                return removedAddressDbIDs.ToArray();
            });
        }
    }
}
