using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeDB.Infrastructure.Data
{
    public class AddressBookContactRepository : EfRepository<AddressBookContact>, IAddressBookContactRepository
    {
        readonly Route4MeDbContext _dbContext;
        public AddressBookContactRepository(Route4MeDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Checks if an address book ontact exists.
        /// </summary>
        /// <param name="addressId">Address book contact ID</param>
        /// <returns>True, if an address book contact exists</returns>
        public bool CheckIfAddressBookContactExists(int addressId)
        {
            return _dbContext.AddressBookContacts.Any(e => e.AddressId == addressId);
        }

        /// <summary>
        /// Gets an address book contact from the database by address ID.
        /// </summary>
        /// <param name="addressId">Address book contact ID</param>
        /// <returns>Address book contact</returns>
        public async Task<AddressBookContact> GetAddressBookContactByIdAsync(int addressId)
        {
            return await _dbContext.AddressBookContacts
                .FirstOrDefaultAsync(x => x.AddressId == addressId);
        }

        /// <summary>
        /// Gets an array of the address book contacts from the database limited by parameters offset and limit.
        /// </summary>
        /// <param name="offset">Only records from that offset will be considered.</param>
        /// <param name="limit">Limit the number of the returned datatable records.</param>
        /// <returns>An array of the address book contacts</returns>
        public async Task<IEnumerable<AddressBookContact>> GetAddressBookContactsAsync(int offset, int limit)
        {
            var result = _dbContext.AddressBookContacts.ToListAsync().GetAwaiter().GetResult()
                .Skip(offset)
                .Take(limit);

            return await Task.Run(() =>
            {
                return result;
            });
        }

        /// <summary>
        /// Gets an array of the address book contacts from the database by array of the address IDs.
        /// </summary>
        /// <param name="contactIDs">An array of the address IDs.</param>
        /// <returns>An array of the address book contacts</returns>
        public async Task<IEnumerable<AddressBookContact>> GetAddressBookContactsAsync(int[] contactIDs)
        {
            var result = _dbContext.AddressBookContacts.Where(x => contactIDs.Contains(x.AddressId))
                .ToListAsync().GetAwaiter().GetResult().AsEnumerable();

            return await Task.Run(() =>
            {
                return result;
            });
        }

        /// <summary>
        /// Gets an array of the address book contacts with the specified fields 
        /// from the database by query text.
        /// </summary>
        /// <param name="query">The query text</param>
        /// <param name="fields"></param>
        /// <returns>An array of the address book contacts</returns>
        public async Task<IEnumerable<dynamic>> GetAddressBookContactsAsync(string query, string[] fields)
        {
            var result = from addressBookContact in _dbContext.AddressBookContacts
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

        /// <summary>
        /// Creates an address book contact by an assress book object parameters as the parameters.
        /// </summary>
        /// <param name="addressBookContactParameters">Address book contact as the input parameters</param>
        /// <returns>Address book contact</returns>
        public async Task<AddressBookContact> CreateAddressBookContactAsync(AddressBookContact addressBookContactParameters)
        {
            var propertyNames = addressBookContactParameters.GetType().GetProperties()
                .ToList().Where(x => x.GetValue(addressBookContactParameters) != null && x.Name!="AddressId")
                .Select(y => y.Name).ToList();

            var addressBookContact = new AddressBookContact(addressBookContactParameters, propertyNames);

            await this.AddAsync(addressBookContact);

            return await Task.Run(() =>
            {
                return addressBookContact;
            });
        }

        /// <summary>
        /// Updates existing address book contact.
        /// </summary>
        /// <param name="addressId">Address ID of the existing address book contact.</param>
        /// <param name="addressBookContactParameters">Address book contact as the input parameters</param>
        /// <returns>Address book contact</returns>
        public async Task<AddressBookContact> UpdateAddressBookContactAsync(int addressId, AddressBookContact addressBookContactParameters)
        {
            var addressBookContact = await this.GetAddressBookContactByIdAsync(addressId);

            addressBookContactParameters.GetType().GetProperties().ToList()
                .ForEach(x =>  {
                    x.SetValue(addressBookContact, x.GetValue(addressBookContactParameters));
                });

            await this.UpdateAsync(addressBookContact);

            return await Task.Run(() =>
            {
                return addressBookContact;
            });
        }

        /// <summary>
        /// Removes from the AddressBookContact dataset repository the address book contacts.
        /// </summary>
        /// <param name="addressIDs"></param>
        /// <returns></returns>
        public async Task<int[]> RemoveAddressBookContactAsync(int[] addressIDs)
        {
            IEnumerable<AddressBookContact> contactsRemove = await this.GetAddressBookContactsAsync(addressIDs);
            List<int> removedAddressIDs = new List<int>();

            contactsRemove.ToList().ForEach(x =>
            {
                this._dbContext.AddressBookContacts.Remove(x);
                removedAddressIDs.Add(x.AddressId);
            });

            return await Task.Run(() =>
            {
                return removedAddressIDs.ToArray();
            });
        }

    }
}