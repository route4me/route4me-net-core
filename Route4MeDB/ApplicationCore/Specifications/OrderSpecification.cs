using Route4MeDB.ApplicationCore.Entities.OrderAggregate;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification() :
            base(i => (i.Id>0))
        {
            AddInclude(o => o.OrderId);
        }

        public OrderSpecification(int? orderId) : 
            base(i => (!orderId.HasValue || i.OrderId==orderId))
        {

        }
    }
}
