using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using Route4MeSDKLibrary.QueryTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class OrdersGroupTests
    {
        private static string _skip;

        public static readonly string
            c_ApiKey = ApiKeys
                .ActualApiKey; // This group allowed only for business and higher account types --- put in the parameter an appropriate API key

        private static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey; //
        private TestDataRepository _tdr;
        private List<string> _lsOptimizationIDs;
        private readonly List<string> _lsOrderIds = new List<string>();
        private readonly List<Order> _lsOrders = new List<Order>();

        [OneTimeSetUp]
        public void CreateOrderTest()
        {
            if (c_ApiKey == c_ApiKey_1) _skip = "yes";
            else _skip = "no";

            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            _lsOptimizationIDs = new List<string>();
            _tdr = new TestDataRepository();

            var result = _tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver Round Trip generation failed.");

            Assert.IsTrue(_tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.SDRT_optimization_problem_id);

            var dtTomorrow = DateTime.Now + new TimeSpan(1, 0, 0, 0);

            var order = new Order
            {
                Address1 = "Test Address1 " + new Random().Next(),
                AddressAlias = "Test AddressAlias " + new Random().Next(),
                CachedLat = 37.773972,
                CachedLng = -122.431297,
                DayScheduledFor_YYYYMMDD = dtTomorrow.ToString("yyyy-MM-dd"),
                CustomUserFields = new[]
                {
                    new OrderCustomField
                    {
                        OrderCustomFieldId = 93,
                        OrderCustomFieldValue = "false"
                    }
                }
            };

            if (c_ApiKey != c_ApiKey_1)
            {
                // Run the query
                var resultOrder = route4Me.AddOrder(order, out var errorString);

                Assert.IsNotNull(resultOrder, "CreateOrderTest failed. " + errorString);

                _lsOrderIds.Add(resultOrder.OrderId.ToString());
                _lsOrders.Add(resultOrder);
            }
            else
            {
                Assert.AreEqual(c_ApiKey_1, c_ApiKey);
            }
        }

        [Test]
        public void GetOrdersTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParameters = new OrderParameters
            {
                Offset = 0,
                Limit = 10
            };

            var orders = route4Me.GetOrders(
                orderParameters,
                out var total,
                out var errorString);

            Assert.That(orders, Is.InstanceOf<Order[]>(), "GetOrdersTest failed. " + errorString);
        }

        [Test]
        public void GetOrderByIDTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParameters = new OrderParameters
            {
                order_id = _lsOrderIds[0]
            };

            var order = route4Me.GetOrderByID(orderParameters, out var errorString);

            Assert.That(order, Is.InstanceOf<Order>(), "GetOrderByIDTest failed. " + errorString);
        }

        [Test]
        public void GetOrderByInsertedDateTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var InsertedDate = DateTime.Now.ToString("yyyy-MM-dd");

            var oParams = new OrderParameters {DayAddedYYMMDD = InsertedDate};

            var orders = route4Me.SearchOrders(oParams, out var errorString);

            Assert.That(orders, Is.InstanceOf<GetOrdersResponse>(), "GetOrderByInsertedDateTest failed. " + errorString);
        }

        [Test]
        public void GetOrderByScheduledDateTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var dtTomorrow = DateTime.Now + new TimeSpan(1, 0, 0, 0);

            var oParams = new OrderParameters
            {
                ScheduledForYYMMDD = dtTomorrow.ToString("yyyy-MM-dd")
            };

            var orders = route4Me.SearchOrders(oParams, out var errorString);

            Assert.That(orders, Is.InstanceOf<GetOrdersResponse>(), "GetOrderByScheduledDateTest failed. " + errorString);
        }

        [Test]
        public void GetOrdersByScheduleFilter()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var startDate = (DateTime.Now + new TimeSpan(1, 0, 0, 0)).ToString("yyyy-MM-dd");
            var endDate = (DateTime.Now + new TimeSpan(31, 0, 0, 0)).ToString("yyyy-MM-dd");

            var oParams = new OrderFilterParameters
            {
                Limit = 10,
                Filter = new FilterDetails
                {
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new[] {startDate, endDate}
                }
            };

            var orders = route4Me.FilterOrders(oParams, out var errorString);

            Assert.That(orders, Is.InstanceOf<Order[]>(), "GetOrdersByScheduleFilter failed. " + errorString);
        }

        [Test]
        public void FilterOrdersByTrackingNumbers()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var oParams = new OrderFilterParameters
            {
                Limit = 10,
                Filter = new FilterDetails
                {
                    Display = "all",
                    TrackingNumbers = new[] {"TN1"}
                }
            };

            var orders = route4Me.FilterOrders(oParams, out var errorString);

            Assert.That(orders, Is.InstanceOf<Order[]>(), "FilterOrdersByTrackingNumbers failed. " + errorString);
        }

        [Test]
        public void GetOrdersBySpecifiedTextTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var query = "Test Address1";

            var oParams = new OrderParameters
            {
                Query = query,
                Offset = 0,
                Limit = 20
            };

            var orders = route4Me.SearchOrders(oParams, out var errorString);

            Assert.That(orders, Is.InstanceOf<GetOrdersResponse>(), "GetOrdersBySpecifiedTextTest failed. " + errorString);
        }

        [Test]
        public void GetOrdersByCustomFieldsTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var startDate = (DateTime.Now + new TimeSpan(1, 0, 0, 0)).ToString("yyyy-MM-dd");
            var endDate = (DateTime.Now + new TimeSpan(31, 0, 0, 0)).ToString("yyyy-MM-dd");

            var oParams = new OrderFilterParameters
            {
                Limit = 10,
                Filter = new FilterDetails
                {
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new[] {startDate, endDate}
                }
            };

            var orders = route4Me.FilterOrders(oParams, out var errorString);

            Assert.That(orders, Is.InstanceOf<Order[]>(), "GetOrdersByScheduleFilter failed. " + errorString);
        }

        [Test]
        public void UpdateOrderTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            //Order order = null;
            var orderId = _lsOrderIds.Count > 0 ? _lsOrderIds[0] : "";

            Assert.IsFalse(orderId == "", "There is no order for updating.");

            var orderParameters = new OrderParameters
            {
                order_id = orderId
            };

            var order = route4Me.GetOrderByID(orderParameters, out var errorString);

            Assert.IsTrue(
                order != null,
                "There is no order for updating. " + errorString);

            order.ExtFieldLastName = "Updated " + new Random().Next();

            // Run the query
            var updatedOrder = route4Me.UpdateOrder(order, out errorString);

            Assert.IsNotNull(
                updatedOrder,
                "UpdateOrderTest failed. " + errorString);
        }

        [Test]
        public void AddScheduledOrderTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParams = new Order
            {
                Address1 = "318 S 39th St, Louisville, KY 40212, USA",
                CachedLat = 38.259326,
                CachedLng = -85.814979,
                CurbsideLat = 38.259326,
                CurbsideLng = -85.814979,
                AddressAlias = "318 S 39th St 40212",
                AddressCity = "Louisville",
                ExtFieldFirstName = "Lui",
                ExtFieldLastName = "Carol",
                ExtFieldEmail = "lcarol654@yahoo.com",
                ExtFieldPhone = "897946541",
                ExtFieldCustomData = new Dictionary<string, string> {{"order_type", "scheduled order"}},
                DayScheduledFor_YYYYMMDD = "2020-12-20",
                LocalTimeWindowEnd = 39000,
                LocalTimeWindowEnd2 = 46200,
                LocalTimeWindowStart = 37800,
                LocalTimeWindowStart2 = 45000,
                LocalTimezoneString = "America/New_York",
                OrderIcon = "emoji/emoji-bank"
            };

            var newOrder = route4Me.AddOrder(orderParams, out var errorString);

            Assert.IsNotNull(newOrder, "AddScheduledOrdersTest failed. " + errorString);
            Assert.That(newOrder, Is.InstanceOf<Order>(), $"Cannot create the order in the test AddScheduledOrderTest. {errorString}");

            _lsOrders.Add(newOrder);
        }

        [Test]
        public void AddOrderWithDemandsTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParams = new Order
            {
                Address1 = "Some address",
                CachedLat = 48.335991,
                CachedLng = 31.18287,
                Weight = 500.0,
                Cost = 100.0,
                Revenue = 1500.0,
                Cube = 2500.0,
                Pieces = 1500
            };

            var newOrder = route4Me.AddOrder(orderParams, out var errorString);

            Assert.IsNotNull(newOrder, "AddOrderWithDemandsTest failed. " + errorString);
            Assert.That(newOrder, Is.InstanceOf<Order>(), $"Cannot create the order in the test AddOrderWithDemandsTest. {errorString}");

            _lsOrders.Add(newOrder);
        }

        [Test]
        public void AddScheduledOrderAndCheckExtFieldCustomDataTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParams = new Order
            {
                Address1 = "318 S 39th St, Louisville, KY 40212, USA",
                CachedLat = 38.259326,
                CachedLng = -85.814979,
                CurbsideLat = 38.259326,
                CurbsideLng = -85.814979,
                AddressAlias = "318 S 39th St 40212",
                AddressCity = "Louisville",
                ExtFieldFirstName = "Lui",
                ExtFieldLastName = "Carol",
                ExtFieldEmail = "lcarol654@yahoo.com",
                ExtFieldPhone = "897946541",
                ExtFieldCustomData = new Dictionary<string, string> { { "order_type", "scheduled order" } },
                DayScheduledFor_YYYYMMDD = "2020-12-20",
                LocalTimeWindowEnd = 39000,
                LocalTimeWindowEnd2 = 46200,
                LocalTimeWindowStart = 37800,
                LocalTimeWindowStart2 = 45000,
                LocalTimezoneString = "America/New_York",
                OrderIcon = "emoji/emoji-bank"
            };

            var newOrder = route4Me.AddOrder(orderParams, out _);

            Assert.IsNotNull(newOrder.ExtFieldCustomData);
            Assert.IsTrue(newOrder.ExtFieldCustomData.Count == 1);
            Assert.IsTrue(newOrder.ExtFieldCustomData.ContainsKey("order_type"));
            Assert.IsTrue(newOrder.ExtFieldCustomData["order_type"] == "scheduled order");

            _lsOrders.Add(newOrder);
        }

        [Test]
        public void AddOrdersToOptimizationTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var rQueryParams = new OptimizationParameters
            {
                OptimizationProblemID = _tdr.SDRT_optimization_problem_id,
                Redirect = false
            };

            var lsTimeWindowStart = new List<int>();

            var dtCurDate = DateTime.Now + new TimeSpan(1, 0, 0, 0);
            dtCurDate = new DateTime(dtCurDate.Year, dtCurDate.Month, dtCurDate.Day, 8, 0, 0);

            var tsp1000sec = new TimeSpan(0, 0, 1000);

            lsTimeWindowStart.Add((int) R4MeUtils.ConvertToUnixTimestamp(dtCurDate));
            dtCurDate += tsp1000sec;
            lsTimeWindowStart.Add((int) R4MeUtils.ConvertToUnixTimestamp(dtCurDate));
            dtCurDate += tsp1000sec;
            lsTimeWindowStart.Add((int) R4MeUtils.ConvertToUnixTimestamp(dtCurDate));

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    AddressString = "273 Canal St, New York, NY 10013, USA",
                    Latitude = 40.7191558,
                    Longitude = -74.0011966,
                    Alias = "",
                    CurbsideLatitude = 40.7191558,
                    CurbsideLongitude = -74.0011966,
                    IsDepot = true
                },
                new Address
                {
                    AddressString = "106 Liberty St, New York, NY 10006, USA",
                    Alias = "BK Restaurant #: 2446",
                    Latitude = 40.709637,
                    Longitude = -74.011912,
                    CurbsideLatitude = 40.709637,
                    CurbsideLongitude = -74.011912,
                    Email = "",
                    Phone = "(917) 338-1887",
                    FirstName = "",
                    LastName = "",
                    CustomFields = new Dictionary<string, string> {{"icon", null}},
                    Time = 0,
                    TimeWindowStart = lsTimeWindowStart[0],
                    TimeWindowEnd = lsTimeWindowStart[0] + 300,
                    OrderId = 7205705
                },
                new Address
                {
                    AddressString = "325 Broadway, New York, NY 10007, USA",
                    Alias = "BK Restaurant #: 20333",
                    Latitude = 40.71615,
                    Longitude = -74.00505,
                    CurbsideLatitude = 40.71615,
                    CurbsideLongitude = -74.00505,
                    Email = "",
                    Phone = "(212) 227-7535",
                    FirstName = "",
                    LastName = "",
                    CustomFields = new Dictionary<string, string> {{"icon", null}},
                    Time = 0,
                    TimeWindowStart = lsTimeWindowStart[1],
                    TimeWindowEnd = lsTimeWindowStart[1] + 300,
                    OrderId = 7205704
                },
                new Address
                {
                    AddressString = "106 Fulton St, Farmingdale, NY 11735, USA",
                    Alias = "BK Restaurant #: 17871",
                    Latitude = 40.73073,
                    Longitude = -73.459283,
                    CurbsideLatitude = 40.73073,
                    CurbsideLongitude = -73.459283,
                    Email = "",
                    Phone = "(212) 566-5132",
                    FirstName = "",
                    LastName = "",
                    CustomFields = new Dictionary<string, string> {{"icon", null}},
                    Time = 0,
                    TimeWindowStart = lsTimeWindowStart[2],
                    TimeWindowEnd = lsTimeWindowStart[2] + 300,
                    OrderId = 7205703
                }
            };

            #endregion

            var rParams = new RouteParameters
            {
                RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)",
                RouteDate = 1465948800,
                RouteTime = 14400,
                Optimize = "Time",
                AlgorithmType = AlgorithmType.TSP,
                RT = false,
                LockLast = false,
                VehicleId = "",
                DisableOptimization = false
            };

            var dataObject = route4Me.AddOrdersToOptimization(
                rQueryParams,
                addresses,
                rParams,
                out var errorString);

            Assert.IsNotNull(dataObject, "AddOrdersToOptimizationTest failed. " + errorString);
        }

        [Test]
        public void CreateOrderWithCustomFieldTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParams = new Order
            {
                Address1 = "1358 E Luzerne St, Philadelphia, PA 19124, US",
                CachedLat = 48.335991,
                CachedLng = 31.18287,
                DayScheduledFor_YYYYMMDD = "2019-10-11",
                AddressAlias = "Auto test address",
                CustomUserFields = new OrderCustomField[]
                {
                    new OrderCustomField
                    {
                        OrderCustomFieldId = 93,
                        OrderCustomFieldValue = "false"
                    }
                }
            };

            var result = route4Me.AddOrder(orderParams, out var errorString);

            Assert.IsNotNull(result, "AddOrdersToRouteTest failed. " + errorString);

            _lsOrderIds.Add(result.OrderId.ToString());

            _lsOrders.Add(result);
        }

        [Test]
        public void CreateOrderWithOrderTypeTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            // Using of an existing tracking number raises error
            var randomTrackingNumber = R4MeUtils.GenerateRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

            var orderParams = new Order
            {
                Address1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                TrackingNumber = randomTrackingNumber,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var result = route4Me.AddOrder(orderParams, out var errorString);

            var order = result;

            Assert.IsNotNull(order, "CreateOrderWithTrackingNumberTest failed. " + errorString);

            route4Me.RemoveOrders(new[] {order.OrderId.ToString()}, out var errorString2);
        }

        [Test]
        public void CreateWrongOrderTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var randomTrackingNumber = R4MeUtils.GenerateRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

            var orderParams = new Order
            {
                //Address1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                TrackingNumber = randomTrackingNumber,
                AddressStopType = AddressStopType.Delivery.Description()
            };

            var result = route4Me.AddOrder(orderParams, out var errorString);

            if ((result?.OrderId ?? null) != null)
                route4Me.RemoveOrders(new[] {result.OrderId.ToString()}, out var errorString2);

            Assert.IsNull(result, "CreateWrongOrderTest failed. " + errorString);
        }

        [Test]
        public void UpdateOrderWithCustomFieldTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var order = _lsOrders[_lsOrders.Count - 1];

            order.CustomUserFields = new OrderCustomField[]
            {
                new OrderCustomField
                {
                    OrderCustomFieldId = 93,
                    OrderCustomFieldValue = "true"
                }
            };

            var result = route4Me.UpdateOrder(order, out var errorString);

            Assert.IsNotNull(result, "UpdateOrderWithCustomFieldTest failed. " + errorString);
        }

        [Test]
        public void AddOrdersToRouteTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var rQueryParams = new RouteParametersQuery
            {
                RouteId = _tdr.SDRT_route_id,
                Redirect = false
            };

            #region Addresses

            Address[] addresses =
            {
                new Address { OrderId = Int64.Parse(_lsOrderIds[0]) }
            };

            #endregion

            var result = route4Me.AddOrdersToRoute(
                rQueryParams,
                addresses,
                out var errorString);

            Assert.IsNotNull(
                result,
                "AddOrdersToRouteTest failed. " + errorString);
        }

        [Test]
        public void GetOrdersUpdatesTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParams = new Order
            {
                Address1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                ExtFieldCustomData = new Dictionary<string, string>(){ { "Assigned_Inspector", "12345" } }
            };
            var order = route4Me.AddOrder(orderParams, out var errorString1);

            var newAssignedInspector = "123456";
            order.ExtFieldCustomData["Assigned_Inspector"] = newAssignedInspector;
            var updatedOrder = route4Me.UpdateOrder(order, out var errorString3);
            updatedOrder = route4Me.GetOrderByID(new OrderParameters() { order_id = updatedOrder.OrderId.ToString() }, out var errIgnored);

            var secondsIn10mins = 10 * 60;
            var lastKnownTs = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - secondsIn10mins;
            var orders = route4Me.GetOrdersUpdates(new OrderUpdatesParameters(){ LastKnownTs = lastKnownTs }, out var errorString4);

            Assert.That(orders.Data.Where(x => x.OrderId == order.OrderId && x.Data != null && x.Data.TryGetValue("Assigned_Inspector", out var assignedInspector) && assignedInspector == newAssignedInspector).Any);
        }

        [OneTimeTearDown]
        public void RemoveOrdersTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            // Run the query
            var removed = route4Me.RemoveOrders(_lsOrderIds.ToArray(), out var errorString);

            _lsOrders.Clear();
            _lsOrderIds.Clear();

            Assert.IsTrue(removed, "RemoveOrdersTest failed. " + errorString);

            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the testing optimization problem failed.");

            _lsOptimizationIDs.Clear();
        }
    }
}