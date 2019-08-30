using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class AddressBookContactsSpecification : BaseSpecification<AddressBookContact>
    {
        public AddressBookContactsSpecification(int? offset, int? limit) :
            base(i => ((!offset.HasValue || !limit.HasValue)))
        {
            AddInclude(o => o.AddressDbId);
            ApplyPaging((int)offset, (int)limit);
        }
    }
}
