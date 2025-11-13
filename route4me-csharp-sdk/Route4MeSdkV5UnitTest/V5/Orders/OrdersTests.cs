using System;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;

using Order = Route4MeSDK.DataTypes.Order;


namespace Route4MeSdkV5UnitTest.V5.Orders
{
    [TestFixture]
    public class OrdersTests
    {
        [Test]
        public void OrdersArchiveTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
            };
            var result = route4me.ArchiveOrders(parameters, out _);
            Assert.That(result.Items, Is.Not.Empty);
        }

        [Test]
        public async Task OrdersArchiveAsyncTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
            };
            var result = await route4me.ArchiveOrdersAsync(parameters);
            Assert.NotNull(result.Item1);
            Assert.That(result.Item1.Items, Is.Not.Empty);
        }

        [Test]
        public void OrdersHistoryTest()
        {
            var route4meV5 = new Route4MeManagerV5(ApiKeys.ActualApiKey);
            var route4me = new Route4MeManager(ApiKeys.ActualApiKey);

            var order = new Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                CachedLat = 37.9,
                CachedLng = -34.6
            };

            Order createdOrder = route4me.AddOrder(order, out _);

            var parameters = new OrderHistoryParameters()
            {
                OrderId = createdOrder.OrderId,
                TrackingNumber = createdOrder.TrackingNumber
            };

            var result = route4meV5.GetOrderHistory(parameters, out _);

            Assert.That(result.Results, Is.Not.Empty);
        }

        [Test]
        public async Task OrderHistoryAsyncTest()
        {
            var route4meV5 = new Route4MeManagerV5(ApiKeys.ActualApiKey);
            var route4me = new Route4MeManager(ApiKeys.ActualApiKey);

            var order = new Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                CachedLat = 37.9,
                CachedLng = -34.6
            };

            var createdOrder = await route4me.AddOrderAsync(order);

            var parameters = new OrderHistoryParameters()
            {
                OrderId = createdOrder.Item1.OrderId,
                TrackingNumber = createdOrder.Item1.TrackingNumber
            };

            var result = await route4meV5.GetOrderHistoryAsync(parameters);

            Assert.That(result.Item1.Results, Is.Not.Empty);
        }

        [Test]
        public async Task GetOrderAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            var getOrderResponse = await route4me.GetOrderAsync(new GetOrderParameters() { OrderUuid = createdOrder.Item1.OrderUuid });

            Assert.That(getOrderResponse.Item1.OrderUuid, Is.EqualTo(createdOrder.Item1.OrderUuid));

            await route4me.DeleteOrderAsync(getOrderResponse.Item1.OrderUuid);
        }

        [Test]
        public async Task CreateOrderAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            Assert.That(string.IsNullOrEmpty(createdOrder.Item1.OrderUuid), Is.False);

            await route4me.DeleteOrderAsync(createdOrder.Item1.OrderUuid);
        }

        [Test]
        public async Task UpdateOrderAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });
            Assert.That(string.IsNullOrEmpty(createdOrder.Item1.AddressCity), Is.True);
            var cityName = "Madrid";
            createdOrder.Item1.AddressCity = cityName;

            var updateOrderResponse = await route4me.UpdateOrderAsync(createdOrder.Item1);

            var getOrderResponse = await route4me.GetOrderAsync(new GetOrderParameters() { OrderUuid = createdOrder.Item1.OrderUuid });

            Assert.That(getOrderResponse.Item1.AddressCity, Is.EqualTo(cityName));

            await route4me.DeleteOrderAsync(getOrderResponse.Item1.OrderUuid);
        }

        [Test]
        public async Task DeleteOrderAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            Assert.That(string.IsNullOrEmpty(createdOrder.Item1.OrderUuid), Is.False);

            var deleteResponse = await route4me.DeleteOrderAsync(createdOrder.Item1.OrderUuid);

            var getOrderResponse = await route4me.UpdateOrderAsync(createdOrder.Item1);

            Assert.That(string.IsNullOrEmpty(getOrderResponse.Item1.OrderUuid), Is.True);
        }

        [Test]
        public async Task SearchOrderAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            var searchOrderResponse = await route4me.SearchOrdersAsync(new SearchOrdersRequest() { Limit = 1 });

            Assert.That(searchOrderResponse.Item1.Results.Length, Is.EqualTo(1));

            await route4me.DeleteOrderAsync(createdOrder.Item1.OrderUuid);
        }


        [Test]
        public async Task BatchUpdateFilterAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder1 = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            var testAddress2 = "Test Address2 " + (new Random()).Next();
            var createdOrder2 = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = testAddress2,
                AddressAlias = "Test AddressAlias2 " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            await route4me.BatchUpdateFilterAsync(new BatchUpdateFilterOrderRequest() { Data = createdOrder2.Item1, Filters = new FiltersParamRequestBody() { OrderIds = new string[] { createdOrder1.Item1.OrderUuid } } });

            await Task.Delay(TimeSpan.FromSeconds(5));

            var getOrder2 = await route4me.GetOrderAsync(new GetOrderParameters() { OrderUuid = createdOrder2.Item1.OrderUuid });

            Assert.That(getOrder2.Item1.AddressAlias, Is.EqualTo(createdOrder2.Item1.AddressAlias));

            await route4me.DeleteOrderAsync(createdOrder1.Item1.OrderUuid);
            await route4me.DeleteOrderAsync(createdOrder2.Item1.OrderUuid);
        }

        [Test]
        public async Task BatchDeleteAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder1 = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next().ToString(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            var createdOrder2 = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address2 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias2 " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            await route4me.BatchDeleteAsync(new BatchDeleteOrdersRequest() { OrderIds = new[] { createdOrder1.Item1.OrderUuid, createdOrder2.Item1.OrderUuid } });

            var getOrder2 = await route4me.SearchOrdersAsync(new SearchOrdersRequest() { OrderIds = new[] { createdOrder1.Item1.OrderUuid, createdOrder2.Item1.OrderUuid } });

            Assert.That(getOrder2.Item1.Results.Length, Is.Zero);
        }

        [Test]
        public async Task BatchUpdateAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var createdOrder1 = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            var testAddress2 = "Test Address2 " + (new Random()).Next();
            var createdOrder2 = await route4me.CreateOrderAsync(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = testAddress2,
                AddressAlias = "Test AddressAlias2 " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            });

            await route4me.BatchUpdateAsync(new BatchUpdateOrdersRequest() { Data = createdOrder2.Item1, OrderIds = new[] { createdOrder1.Item1.OrderUuid } });

            var getOrder2 = await route4me.GetOrderAsync(new GetOrderParameters() { OrderUuid = createdOrder2.Item1.OrderUuid });

            Assert.That(getOrder2.Item1.AddressAlias, Is.EqualTo(createdOrder2.Item1.AddressAlias));

            await route4me.DeleteOrderAsync(createdOrder1.Item1.OrderUuid);
            await route4me.DeleteOrderAsync(createdOrder2.Item1.OrderUuid);
        }

        [Test]
        public async Task BatchCreateAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var getOrder1 = await route4me.SearchOrdersAsync(new SearchOrdersRequest());

            var batchCreateOrdersResult = await route4me.BatchCreateOrdersAsync(new BatchCreateOrdersRequest()
            {
                Data = new[]
            {
                new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
                {
                    Address1 = "Test Address1 " + (new Random()).Next(),
                    AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                    AddressGeo = new GeoPoint()
                    {
                        Latitude = 37.9,
                        Longitude = -34.6
                    }
                },
                new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
                {
                    Address1 = "Test Address2 " + (new Random()).Next(),
                    AddressAlias = "Test AddressAlias2 " + (new Random()).Next(),
                    AddressGeo = new GeoPoint()
                    {
                        Latitude = 37.9,
                        Longitude = -34.6
                    }
                }
            }
            });

            Assert.That(batchCreateOrdersResult.Item1.Status);

            await Task.Delay(TimeSpan.FromSeconds(5));

            var getOrder2 = await route4me.SearchOrdersAsync(new SearchOrdersRequest());

            Assert.That(getOrder2.Item1.Total - getOrder1.Item1.Total, Is.EqualTo(2));
        }

        [Test]
        public async Task GetCustomUserFieldsAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var customUserFields = await route4me.GetCustomUserFieldsAsync();

            Assert.That(customUserFields.Item1.GetType(), Is.EqualTo(typeof(CustomUserFieldsResponse)));
        }

        [Test]
        public async Task CreateCustomUserFieldsAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var customUserFields1 = await route4me.GetCustomUserFieldsAsync();

            var createdCustomUserFields = await route4me.CreateCustomUserFieldsAsync(new CreateCustomUserFieldRequest()
            {
                OrderCustomFieldLabel = "Test label 2",
                OrderCustomFieldName = "Test name 2",
                OrderCustomFieldType = "Test type 2",
                OrderCustomFieldTypeInfo = JObject.Parse("{\"prop1\": 123}")
            });
            Assert.That(createdCustomUserFields.Item1.Data, Is.Not.Null);

            var customUserFields2 = await route4me.GetCustomUserFieldsAsync();

            Assert.That(customUserFields2.Item1.Data.Length - customUserFields1.Item1.Data.Length, Is.EqualTo(1));

            await route4me.DeleteCustomUserFieldsAsync(createdCustomUserFields.Item1.Data.OrderCustomFieldUuid);
        }

        [Test]
        public async Task UpdateCustomUserFieldsAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);

            var orderCustomFieldName = "Test name 2";
            var createdCustomUserFields = await route4me.CreateCustomUserFieldsAsync(new CreateCustomUserFieldRequest()
            {
                OrderCustomFieldLabel = "Test label 2",
                OrderCustomFieldName = orderCustomFieldName,
                OrderCustomFieldType = "Test type 2",
                OrderCustomFieldTypeInfo = JObject.Parse("{\"prop1\": 123}")
            });
            Assert.That(createdCustomUserFields.Item1.Data, Is.Not.Null);

            var updateCustomUserFieldRequest = new UpdateCustomUserFieldRequest()
            {
                OrderCustomFieldLabel = "Test label 3",
                OrderCustomFieldType = createdCustomUserFields.Item1.Data.OrderCustomFieldType,
                OrderCustomFieldTypeInfo = createdCustomUserFields.Item1.Data.OrderCustomFieldTypeInfo
            };
            var updatedCustomUserFields =
                await route4me.UpdateCustomUserFieldsAsync(createdCustomUserFields.Item1.Data.OrderCustomFieldUuid,
                    updateCustomUserFieldRequest);
            Assert.That(updatedCustomUserFields.Item1.Data.OrderCustomFieldLabel,
                Is.EqualTo(updateCustomUserFieldRequest.OrderCustomFieldLabel));
            Assert.That(updatedCustomUserFields.Item1.Data.OrderCustomFieldName, Is.EqualTo(orderCustomFieldName));

            var customUserFields = await route4me.GetCustomUserFieldsAsync();
            Assert.That(
                customUserFields.Item1.Data
                    .First(x => x.OrderCustomFieldUuid == createdCustomUserFields.Item1.Data.OrderCustomFieldUuid)
                    .OrderCustomFieldLabel, Is.EqualTo(updateCustomUserFieldRequest.OrderCustomFieldLabel));
            Assert.That(
                customUserFields.Item1.Data
                    .First(x => x.OrderCustomFieldUuid == createdCustomUserFields.Item1.Data.OrderCustomFieldUuid)
                    .OrderCustomFieldName, Is.EqualTo(orderCustomFieldName));

            await route4me.DeleteCustomUserFieldsAsync(createdCustomUserFields.Item1.Data.OrderCustomFieldUuid);
        }

        [Test]
        public async Task DeleteCustomUserFieldsAsyncTest()
        {
            var route4me = new OrderManagerV5(ApiKeys.ActualApiKey);
            var createdCustomUserFields = await route4me.CreateCustomUserFieldsAsync(new CreateCustomUserFieldRequest()
            {
                OrderCustomFieldLabel = "Test label 2",
                OrderCustomFieldName = "Test name 2",
                OrderCustomFieldType = "Test type 2",
                OrderCustomFieldTypeInfo = JObject.Parse("{\"prop1\": 123}")
            });
            Assert.That(createdCustomUserFields.Item1.Data, Is.Not.Null);

            await route4me.DeleteCustomUserFieldsAsync(createdCustomUserFields.Item1.Data.OrderCustomFieldUuid);

            var customUserFields = await route4me.GetCustomUserFieldsAsync();

            Assert.That(
                customUserFields.Item1.Data.FirstOrDefault(x =>
                    x.OrderCustomFieldUuid == createdCustomUserFields.Item1.Data.OrderCustomFieldUuid), Is.Null);
        }
    }
}