using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Linq;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class AddressBookContactSpecification : BaseSpecification<AddressBookContact>
    {
        public AddressBookContactSpecification() :
            base(i => (i.AddressDbId > 0))
        {
            AddInclude(o => o.AddressDbId);
        }

        public AddressBookContactSpecification(int? addressDbId) :
            base(i => (i.AddressDbId == addressDbId))
        {
            //AddInclude(o => o.AddressId);
        }

        public AddressBookContactSpecification(int[] addressDbIds) :
            base(i => (addressDbIds.Contains(i.AddressDbId)))
        {

        }

        public AddressBookContactSpecification(int? offset, int? limit) :
            base(i => (i.AddressDbId > 0 || (offset.HasValue && limit.HasValue)))
        {
            ApplyPaging((int)offset, (int)limit);
        }
    }
}
