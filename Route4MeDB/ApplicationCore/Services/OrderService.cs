using Route4MeDB.ApplicationCore.Interfaces;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Linq;
using System.Threading.Tasks;
using Route4MeDB.ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Route4MeDB.ApplicationCore.Specifications;

namespace Route4MeDB.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;

        public OrderService(IAsyncRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            //_basketRepository = basketRepository;
            //_itemRepository = itemRepository;
        }

        public async Task<Order> CreateOrderAsync(Order orderParameters)
        {
            //var basket = await _basketRepository.GetByIdAsync(basketId);
            //Guard.Against.NullBasket(basketId, basket);
            //var items = new List<OrderItem>();
            //foreach (var item in basket.Items)
            //{
            //    var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
            //    var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, catalogItem.PictureUri);
            //    var orderItem = new OrderItem(itemOrdered, item.UnitPrice, item.Quantity);
            //    items.Add(orderItem);
            //}
            var order = new Order(orderParameters);

            await _orderRepository.AddAsync(order);

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var orderSpec = new OrderSpecification(orderId);

            var result = _orderRepository.GetByIdAsync(orderSpec).Result;

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<IEnumerable<Order>> GetOrdersByIDsAsync(int[] orderIDs)
        {
            var orderSpec = new OrderSpecification(orderIDs);

            var result = _orderRepository.ListAsync(orderSpec).Result.AsEnumerable<Order>();

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int offset, int limit)
        {
            var orderSpec = new OrderSpecification(offset, limit);

            var result = _orderRepository.ListAsync(orderSpec).Result.AsEnumerable<Order>();

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public Task<IEnumerable<Order>> GetOrdersByInsertedDateAsync(int offset, int limit, string insertedDate)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByScheduledDateAsync(int offset, int limit, string scheduledDate)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveOrdersAsync(int[] orderIDs)
        {
            bool removed = false;
            try
            {
                orderIDs.ToList().ForEach(x =>
                {
                    var orderSpec = new OrderSpecification(x);
                    var order = _orderRepository.GetByIdAsync(orderSpec).Result;
                    _orderRepository.DeleteAsync(order);
                });
                removed = true;
            }
            catch (System.Exception)
            {
                removed = false;
            }

            return await Task.Run(() =>
            {
                return removed;
            });
        }

        public async Task<Order> UpdateOrderAsync(int orderId, Order orderParameters)
        {
            var orderSpec = new OrderSpecification(orderId);

            var order = _orderRepository.GetByIdAsync(orderSpec).Result;

            orderParameters.GetType().GetProperties().ToList()
                .ForEach(async x => {
                    if (x.GetValue(orderParameters)!=null && x.Name!="OrderId") x.SetValue(order, x.GetValue(orderParameters));
                });

            var orderUpdated =_orderRepository.UpdateAsync(order);

            return await Task.Run(() =>
            {
                return orderUpdated;
            });
        }
    }
}
