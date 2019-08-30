using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order orderParameters);

        Task<Order> GetOrderByIdAsync(int orderDbId);

        Task<IEnumerable<Order>> GetOrdersByIDsAsync(int[] orderDbIDs);

        Task<IEnumerable<Order>> GetOrdersAsync(int offset, int limit);

        Task<IEnumerable<Order>> GetOrdersByScheduledDateAsync(int offset, int limit, string scheduledDate);

        Task<IEnumerable<Order>> GetOrdersByInsertedDateAsync(int offset, int limit, string insertedDate);

        Task<Order> UpdateOrderAsync(int ordeDbrId, Order orderParameters);

        Task<bool> RemoveOrdersAsync(int[] orderDbIDs);
    }
}
