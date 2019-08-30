using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IAddressBookContactService
    {
        Task<AddressBookContact> CreateAddressBookContactAsync(AddressBookContact addressBookContactParameters);

        Task<AddressBookContact> GetAddressBookContactByIdAsync(int addressDbId);

        Task<IEnumerable<AddressBookContact>> GetAddressBookContactsAsync(int offset, int limit);

        Task<IEnumerable<AddressBookContact>> GetAddressBookContactsByIdsAsync(int[] contactDbIDs);

        //Task<IEnumerable<dynamic>> GetAddressBookContactsAsync(string query, string[] fields);

        Task<AddressBookContact> UpdateAddressBookContactAsync(int addressDbId, AddressBookContact addressBookContactParameters);

        Task<int[]> RemoveAddressBookContactAsync(int[] addressDbIDs);
    }
}
