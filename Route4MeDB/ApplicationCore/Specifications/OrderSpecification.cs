using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Linq;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification() :
            base(i => (i.OrderId>0))
        {
            AddInclude(o => o.OrderId);
        }

        public OrderSpecification(int[] orderIds) :
            base(i => (orderIds.Contains(i.OrderId)))
        {

        }

        public OrderSpecification(int orderId) :
            base(i => (i.OrderId==orderId))
        {
            //AddInclude(o => o.OrderId);
        }

        public OrderSpecification(int? offset, int? limit) :
            base(i => (i.OrderId>0 || (offset.HasValue && limit.HasValue)))
        {
            ApplyPaging((int)offset, (int)limit);
        }
    }
}
