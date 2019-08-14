using Route4MeDB.ApplicationCore.Interfaces;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;
using Route4MeDB.ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;

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

        public async Task CreateOrderAsync(Order orderParameters)
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
        }
    }
}
