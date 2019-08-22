using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Linq;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class AddressBookContactSpecification : BaseSpecification<AddressBookContact>
    {
        public AddressBookContactSpecification() :
            base(i => (i.AddressId > 0))
        {
            AddInclude(o => o.AddressId);
        }

        public AddressBookContactSpecification(int? addressId) :
            base(i => (i.AddressId == addressId))
        {
            //AddInclude(o => o.AddressId);
        }

        public AddressBookContactSpecification(int[] addressIds) :
            base(i => (addressIds.Contains(i.AddressId)))
        {

        }

        public AddressBookContactSpecification(int? offset, int? limit) :
            base(i => (i.AddressId > 0 || (offset.HasValue && limit.HasValue)))
        {
            ApplyPaging((int)offset, (int)limit);
        }
    }
}
