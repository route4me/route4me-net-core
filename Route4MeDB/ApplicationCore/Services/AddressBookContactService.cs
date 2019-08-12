using Route4MeDB.ApplicationCore.Interfaces;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Threading.Tasks;
using Route4MeDB.ApplicationCore.Entities;
using Route4MeDbLibrary.QueryTypes;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
//using Route4MeDB.ApplicationCore.Entities.BasketAggregate;

namespace Route4MeDB.ApplicationCore.Services
{
    public class AddressBookContactService : IAddressBookContactService
    {
        private readonly IAsyncRepository<AddressBookContact> _addressBookContactRepository;
        //private readonly IAsyncRepository<Basket> _basketRepository;
        //private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public AddressBookContactService(
            IAsyncRepository<AddressBookContact> addressBookContactRepository)
        {
            _addressBookContactRepository = addressBookContactRepository;
            //_basketRepository = basketRepository;
            //_itemRepository = itemRepository;
        }

        public async Task CreateAddressBookContactAsync(AddressBookContact addressBookContactParameters)
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
            var addressBookContact = new AddressBookContact(addressBookContactParameters);

            await _addressBookContactRepository.AddAsync(addressBookContact);
        }
    }
}
