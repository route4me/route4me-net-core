using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Route4MeDB.Infrastructure.Data
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        private new readonly Route4MeDbContext _dbContext;
        public OrderRepository(Route4MeDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Checks if an order exists.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <returns>True, if an order exists</returns>
        public bool CheckIfOrderExists(int orderId)
        {
            return _dbContext.Orders.Any(e => e.OrderId == orderId);
        }

        /// <summary>
        /// Gets an order from the Order entity by order ID.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <returns>Order</returns>
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        /// <summary>
        /// Gets an array of the orders from the Orders entity limited by parameters offset and limit.
        /// </summary>
        /// <param name="offset">Only records from that offset will be considered.</param>
        /// <param name="limit">Limit the number of the returned datatable records.</param>
        /// <returns>An array of the orders</returns>
        public async Task<IEnumerable<Order>> GetOrdersAsync(int offset, int limit)
        {
            var result = _dbContext.Orders.ToListAsync().GetAwaiter().GetResult()
                .Skip(offset)
                .Take(limit);

            return await Task.Run(() =>
            {
                return result;
            });
        }

        /// <summary>
        /// Gets an array of the orders from the Orders entity by comma-delimited list of the order IDs.
        /// </summary>
        /// <param name="orderIDs">An array of the order IDs.</param>
        /// <returns>An array of the orders</returns>
        public async Task<IEnumerable<Order>> GetOrdersAsync(int[] orderIDs)
        {
            var result = _dbContext.Orders.Where(x => orderIDs.Contains(x.OrderId))
                .ToListAsync().GetAwaiter().GetResult().AsEnumerable();

            return await Task.Run(() =>
            {
                return result;
            });
        }

        /// <summary>
        /// Gets an array of the orders with the specified fields 
        /// from the Orders entity limited by the parameters: offset and limit.
        /// </summary>
        /// <param name="offset">Only records from that offset will be considered.</param>
        /// <param name="limit">Limit the number of the returned datatable records.</param>
        /// <param name="fields"></param>
        /// <returns>An array of the orders</returns>
        public async Task<IEnumerable<dynamic>> GetOrdersAsync(string[] fields, int? offset, int? limit)
        {
            string from = limit==null ? " FROM Orders" : " FROM Orders LIMIT "+limit;
            from += offset == 0 ? "" : "," + offset;

            var result = _dbContext.Orders
                .FromSql("SELECT {0} {1}", string.Join(",",fields), from)
                .OrderBy(b => b.OrderId);

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<IEnumerable<Order>> GetOrdersByInsertedDateAsync(DateTime insertedDate, int? offset, int? limit)
        {
            var result = _dbContext.Orders
                .Where(x => x.DayAddedYyMmDd == insertedDate.ToString("yyyy-MM-dd"))
                .ToList();

            if (offset!=null) result.Skip((int)offset);
            if (limit != null) result.Take((int)limit);

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<IEnumerable<Order>> GetOrdersByScheduledDateAsync(DateTime scheduleedDate, int? offset, int? limit)
        {
            var result = _dbContext.Orders
                .Where(x => x.DayScheduledForYyMmDd == scheduleedDate.ToString("yyyy-MM-dd"))
                .ToList();

            if (offset != null) result.Skip((int)offset);
            if (limit != null) result.Take((int)limit);

            return await Task.Run(() =>
            {
                return result;
            });
        }

        /// <summary>
        /// Creates an order by an order parameters as the parameters.
        /// </summary>
        /// <param name="orderParameters">Order as the input parameters</param>
        /// <returns>Order</returns>
        public async Task<Order> CreateOrderAsync(Order orderParameters)
        {
            var order = new Order(orderParameters);

            await this.AddAsync(order);

            return await Task.Run(() =>
            {
                return order;
            });
        }

        /// <summary>
        /// Updates existing order.
        /// </summary>
        /// <param name="orderId">Order ID of the existing order.</param>
        /// <param name="orderParameters">Order as the input parameters</param>
        /// <returns>AOrder</returns>
        public async Task<Order> UpdateOrderAsync(int orderId, Order orderParameters)
        {
            var order = await this.GetOrderByIdAsync(orderId);

            orderParameters.GetType().GetProperties().ToList()
                .ForEach(x => {
                    x.SetValue(order, x.GetValue(orderParameters));
                });

            var updatedOrder = await this.UpdateAsync(order);

            return await Task.Run(() =>
            {
                return updatedOrder;
            });
        }

        /// <summary>
        /// Removes from the Orders entity the orders.
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public async Task<int[]> RemoveOrderAsync(int[] orderIDs)
        {
            IEnumerable<Order> ordersRemove = await this.GetOrdersAsync(orderIDs);
            List<int> removedOrderIDs = new List<int>();

            ordersRemove.ToList().ForEach(x =>
            {
                this._dbContext.Orders.Remove(x);
                removedOrderIDs.Add(x.OrderId);
            });

            return await Task.Run(() =>
            {
                return removedOrderIDs.ToArray();
            });
        }
    }
}
