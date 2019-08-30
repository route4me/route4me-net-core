using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Linq;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification() :
            base(i => (i.OrderDbId>0))
        {
            AddInclude(o => o.OrderDbId);
        }

        public OrderSpecification(int[] orderDbIds) : 
            base(i => (orderDbIds.Contains(i.OrderDbId)))
        {
            
        }

        public OrderSpecification(int orderDbId) :
            base(i => (i.OrderDbId==orderDbId))
        {
            //AddInclude(o => o.OrderId);
        }

        public OrderSpecification(int? offset, int? limit) :
            base(i => (i.OrderDbId>0 || (offset.HasValue && limit.HasValue)))
        {
            ApplyPaging((int)offset, (int)limit);
        }
    }
}
