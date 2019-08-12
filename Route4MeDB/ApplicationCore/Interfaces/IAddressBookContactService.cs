using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IAddressBookContactService
    {
        Task CreateAddressBookContactAsync(AddressBookContact addressBookContactParameters);
    }
}
