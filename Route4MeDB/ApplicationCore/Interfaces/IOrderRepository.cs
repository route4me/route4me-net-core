using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(int orderDbId);
    }
}

