using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IAddressBookContactRepository : IAsyncRepository<AddressBookContact>
    {
        Task<AddressBookContact> GetAddressBookContactByIdAsync(int addressId);

        Task<IEnumerable<AddressBookContact>> GetAddressBookContactsAsync(int offset, int limit);

        Task<AddressBookContact> UpdateAddressBookContactAsync(int orderId, AddressBookContact contactParameters);
    }
}
