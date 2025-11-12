using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using CsvHelper;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class OptimizationsGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private TestDataRepository _tdr;
        private List<string> _lsOptimizationIDs;
        private List<string> _lsAddressbookContacts;
        private List<string> _lsOrders;

        [OneTimeSetUp]
        public void OptimizationsGroupInitialize()
        {
            _lsOptimizationIDs = new List<string>();
            _lsAddressbookContacts = new List<string>();
            _lsOrders = new List<string>();

            _tdr = new TestDataRepository();

            var result = _tdr.RunOptimizationSingleDriverRoute10Stops();

            Assert.IsTrue(result, "Single Driver 10 stops generation failed.");

            Assert.IsTrue(
                _tdr.SD10Stops_route.Addresses.Length > 0,
                "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.DataObjectSD10Stops.OptimizationProblemId);
        }

        [Test]
        public void GetOptimizationsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var queryParameters = new OptimizationParameters
            {
                Limit = 5,
                Offset = 2
            };

            // Run the query
            var dataObjects = route4Me.GetOptimizations(queryParameters, out var errorString);

            Assert.That(dataObjects, Is.InstanceOf<DataObject[]>(), "GetOptimizationsTest failed. " + errorString);
        }

        [Test]
        public void GetOptimizationsFromDateRangeTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var queryParameters = new OptimizationParameters
            {
                StartDate = "2019-09-15",
                EndDate = "2019-09-20"
            };

            // Run the query
            string errorString;
            var dataObjects = route4Me.GetOptimizations(queryParameters, out errorString);

            Assert.That(dataObjects, Is.InstanceOf<DataObject[]>(), "GetOptimizationsFromDateRangeTest failed. " + errorString);
        }

        [Test]
        public void GetOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var optimizationParameters = new OptimizationParameters
            {
                OptimizationProblemID = _tdr.SD10Stops_optimization_problem_id
            };

            // Run the query
            var dataObject = route4Me.GetOptimization(
                optimizationParameters,
                out var errorString);

            Assert.IsNotNull(
                dataObject,
                "GetOptimizationTest failed. " + errorString);
        }

        [Test]
        public void ReOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var optimizationParameters = new OptimizationParameters
            {
                OptimizationProblemID = _tdr.SD10Stops_optimization_problem_id,
                ReOptimize = true
            };

            // Run the query
            var dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out var errorString);

            _lsOptimizationIDs.Add(dataObject.OptimizationProblemId);

            Assert.IsNotNull(
                dataObject,
                "ReOptimizationTest failed. " + errorString);
        }

        [Test]
        public void UpdateOptimizationDestinationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var address = _tdr.SD10Stops_route.Addresses[3];

            address.FirstName = "UpdatedFirstName";
            address.LastName = "UpdatedLastName";

            var updatedAddress = route4Me.UpdateOptimizationDestination(address, out var _);

            Assert.IsNotNull(
                updatedAddress,
                "UpdateOptimizationDestinationTest failed.");
        }

        [Test]
        public void RemoveOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var result = _tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(
                result,
                "Generation of the route Single Driver Round Trip failed.");

            var opt_id = _tdr.SDRT_optimization_problem_id;
            Assert.IsNotNull(opt_id, "optimizationProblemID is null.");

            string[] OptIDs = { opt_id };

            // Run the query
            var removed = route4Me.RemoveOptimization(OptIDs, out var errorString);

            Assert.IsTrue(removed, "RemoveOptimizationTest failed. " + errorString);
        }

        [Test]
        public void RemoveNotExistingOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            string[] OptIDs = { "not_existing_id" };

            // Run the query
            var removed = route4Me.RemoveOptimization(OptIDs, out var errorString);

            Assert.IsFalse(removed, "RemoveOptimizationTest failed. " + errorString);
        }

        [Test]
        [Obsolete]
        public void HybridOptimizationFrom1000AddressesTest()
        {
            var ApiKey =
                ApiKeys.ActualApiKey; // The addresses in this test not allowed for this API key, you shuld put your valid API key.

            // Comment 2 lines bellow if you have put in the above line your valid key.
            Assert.IsTrue(1 > 0, "");
            if (ApiKey.Length > 10) return;

            var route4Me = new Route4MeManager(ApiKey);

            #region ======= Add scheduled address book locations to an user account ================================

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/addresses_1000.csv";
            var sched0 = new Schedule("daily", false);

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    //int iCount = 0;
                    while (csv.Read())
                    {
                        var lng = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var alias = csv.GetField(2);
                        var address1 = csv.GetField(3);
                        var city = csv.GetField(4);
                        var state = csv.GetField(5);
                        var zip = csv.GetField(6);
                        var phone = csv.GetField(7);
                        //var sched_date = csv.GetField(8);
                        var sched_mode = csv.GetField(8);
                        var sched_enabled = csv.GetField(9);
                        var sched_every = csv.GetField(10);
                        var sched_weekdays = csv.GetField(11);
                        var sched_monthly_mode = csv.GetField(12);
                        var sched_monthly_dates = csv.GetField(13);
                        var sched_annually_usenth = csv.GetField(14);
                        var sched_annually_months = csv.GetField(15);
                        var sched_nth_n = csv.GetField(16);
                        var sched_nth_what = csv.GetField(17);

                        var sAddress = "";

                        if (address1 != null) sAddress += address1.Trim();
                        if (city != null) sAddress += ", " + city.Trim();
                        if (state != null) sAddress += ", " + state.Trim();
                        if (zip != null) sAddress += ", " + zip.Trim();

                        if (sAddress == "") continue;

                        var newLocation = new AddressBookContact();

                        if (lng != null) newLocation.CachedLng = Convert.ToDouble(lng);
                        if (lat != null) newLocation.CachedLat = Convert.ToDouble(lat);
                        if (alias != null) newLocation.AddressAlias = alias.Trim();
                        newLocation.Address1 = sAddress;
                        if (phone != null) newLocation.AddressPhoneNumber = phone.Trim();

                        //newLocation.schedule = new Schedule[]{};
                        if (!sched0.ValidateScheduleMode(sched_mode)) continue;

                        sched0.From = DateTime.Now.ToString("yyyy-MM-dd");

                        var blNth = false;

                        if (sched0.ValidateScheduleMonthlyMode(sched_monthly_mode))
                            if (sched_monthly_mode == "nth")
                                blNth = true;
                        if (sched0.ValidateScheduleUseNth(sched_annually_usenth))
                            if (sched_annually_usenth.ToLower() == "true")
                                blNth = true;

                        var schedule = new Schedule(sched_mode, blNth);

                        var dt = DateTime.Now;
                        //if (schedule.ValidateScheduleMode(sched_mode))
                        //{
                        schedule.Mode = sched_mode;
                        if (schedule.ValidateScheduleEnabled(sched_enabled))
                        {
                            schedule.Enabled = Convert.ToBoolean(sched_enabled);
                            if (schedule.ValidateScheduleEvery(sched_every))
                            {
                                var iEvery = Convert.ToInt32(sched_every);

                                switch (schedule.Mode)
                                {
                                    case "daily":
                                        schedule.Daily.Every = iEvery;
                                        break;
                                    case "weekly":
                                        if (schedule.ValidateScheduleWeekdays(sched_weekdays))
                                        {
                                            schedule.Weekly.Every = iEvery;

                                            var arWeekdays = sched_weekdays.Split(',');
                                            var lsWeekdays = new List<int>();

                                            for (var i = 0; i < arWeekdays.Length; i++)
                                                lsWeekdays.Add(Convert.ToInt32(arWeekdays[i]));
                                            schedule.Weekly.Weekdays = lsWeekdays.ToArray();
                                        }

                                        break;
                                    case "monthly":
                                        if (schedule.ValidateScheduleMonthlyMode(sched_monthly_mode))
                                        {
                                            schedule.Monthly.Every = iEvery;
                                            schedule.Monthly.Mode = sched_monthly_mode;
                                            switch (schedule.Monthly.Mode)
                                            {
                                                case "dates":
                                                    if (schedule.ValidateScheduleMonthDays(sched_monthly_dates))
                                                    {
                                                        var arMonthdays = sched_monthly_dates.Split(',');
                                                        var lsMonthdays = new List<int>();

                                                        for (var i = 0; i < arMonthdays.Length; i++)
                                                            lsMonthdays.Add(Convert.ToInt32(arMonthdays[i]));
                                                        schedule.Monthly.Dates = lsMonthdays.ToArray();
                                                    }

                                                    break;
                                                case "nth":
                                                    if (schedule.ValidateScheduleNthN(sched_nth_n))
                                                        schedule.Monthly.Nth.N = Convert.ToInt32(sched_nth_n);
                                                    if (schedule.ValidateScheduleNthWhat(sched_nth_what))
                                                        schedule.Monthly.Nth.What = Convert.ToInt32(sched_nth_what);
                                                    break;
                                            }
                                        }

                                        break;
                                    case "annually":
                                        if (schedule.ValidateScheduleUseNth(sched_annually_usenth))
                                        {
                                            schedule.Annually.Every = iEvery;
                                            schedule.Annually.UseNth = Convert.ToBoolean(sched_annually_usenth);
                                            if (schedule.Annually.UseNth)
                                            {
                                                if (schedule.ValidateScheduleNthN(sched_nth_n))
                                                    schedule.Annually.Nth.N = Convert.ToInt32(sched_nth_n);
                                                if (schedule.ValidateScheduleNthWhat(sched_nth_what))
                                                    schedule.Annually.Nth.What = Convert.ToInt32(sched_nth_what);
                                            }
                                            else
                                            {
                                                if (schedule.ValidateScheduleYearMonths(sched_annually_months))
                                                {
                                                    var arYearmonths = sched_annually_months.Split(',');
                                                    var lsMonths = new List<int>();

                                                    for (var i = 0; i < arYearmonths.Length; i++)
                                                        lsMonths.Add(Convert.ToInt32(arYearmonths[i]));
                                                    schedule.Annually.Months = lsMonths.ToArray();
                                                }
                                            }
                                        }

                                        break;
                                }
                            }
                        }

                        newLocation.Schedule = new List<Schedule> { schedule }.ToArray();
                        //}

                        //string errorString;
                        var resultContact = route4Me.AddAddressBookContact(newLocation, out var errorString);

                        Assert.IsNotNull(resultContact, "Creation of an addressbook contact failed... " + errorString);

                        if (resultContact != null)
                        {
                            var AddressId = resultContact.AddressId != null ? resultContact.AddressId.ToString() : "";

                            if (AddressId != "") _lsAddressbookContacts.Add(AddressId);
                        }

                        Thread.Sleep(1000);
                    }
                }
            }

            ;

            #endregion

            Thread.Sleep(2000);

            #region ======= Get Hybrid Optimization ================================

            var tsp1day = new TimeSpan(1, 0, 0, 0);
            var lsScheduledDays = new List<string>();
            var curDate = DateTime.Now;

            for (var i = 0; i < 5; i++)
            {
                curDate += tsp1day;
                lsScheduledDays.Add(curDate.ToString("yyyy-MM-dd"));
            }

            #region Addresses

            Address[] Depots =
            {
                new Address
                {
                    AddressString = "2017 Ambler Ave, Abilene, TX, 79603-2239",
                    IsDepot = true,
                    Latitude = 32.474395,
                    Longitude = -99.7447021,
                    CurbsideLatitude = 32.474395,
                    CurbsideLongitude = -99.7447021
                },
                new Address
                {
                    AddressString = "807 Ridge Rd, Alamo, TX, 78516-9596",
                    IsDepot = true,
                    Latitude = 26.170834,
                    Longitude = -98.116201,
                    CurbsideLatitude = 26.170834,
                    CurbsideLongitude = -98.116201
                },
                new Address
                {
                    AddressString = "1430 W Amarillo Blvd, Amarillo, TX, 79107-5505",
                    IsDepot = true,
                    Latitude = 35.221969,
                    Longitude = -101.835288,
                    CurbsideLatitude = 35.221969,
                    CurbsideLongitude = -101.835288
                },
                new Address
                {
                    AddressString = "3611 Ne 24Th Ave, Amarillo, TX, 79107-7242",
                    IsDepot = true,
                    Latitude = 35.236626,
                    Longitude = -101.795117,
                    CurbsideLatitude = 35.236626,
                    CurbsideLongitude = -101.795117
                },
                new Address
                {
                    AddressString = "1525 New York Ave, Arlington, TX, 76010-4723",
                    IsDepot = true,
                    Latitude = 32.720524,
                    Longitude = -97.080195,
                    CurbsideLatitude = 32.720524,
                    CurbsideLongitude = -97.080195
                }
            };

            #endregion

            foreach (var ScheduledDay in lsScheduledDays)
            {
                var hparams = new HybridOptimizationParameters
                {
                    TargetDateString = ScheduledDay,
                    TimezoneOffsetMinutes = -240
                };

                var resultOptimization = route4Me.GetHybridOptimization(hparams, out var errorString1);

                Assert.IsNotNull(resultOptimization, "Get Hybrid Optimization failed... " + errorString1);

                var HybridOptimizationId = "";

                if (resultOptimization != null)
                    HybridOptimizationId = resultOptimization.OptimizationProblemId;
                else
                    continue;

                //============== Add Depot To Hybrid Optimization ===============
                var hDepotParams = new HybridDepotParameters
                {
                    OptimizationProblemId = HybridOptimizationId,
                    DeleteOldDepots = true,
                    NewDepots = new[] { Depots[lsScheduledDays.IndexOf(ScheduledDay)] }
                };

                var addDepotResult = route4Me.AddDepotsToHybridOptimization(hDepotParams, out var errorString3);

                Assert.IsTrue(addDepotResult, "Adding a depot to the Hybrid Optimization failed... " + errorString3);

                Thread.Sleep(5000);

                //============== Reoptimization =================================
                var optimizationParameters = new OptimizationParameters
                {
                    OptimizationProblemID = HybridOptimizationId,
                    ReOptimize = true
                };

                var finalOptimization = route4Me.UpdateOptimization(optimizationParameters, out var errorString2);

                Assert.IsNotNull(finalOptimization, "Update optimization failed... " + errorString1);

                if (finalOptimization != null) _lsOptimizationIDs.Add(finalOptimization.OptimizationProblemId);

                Thread.Sleep(5000);
                //=================================================================
            }

            #endregion

            var removeLocations = _tdr.RemoveAddressBookContacts(_lsAddressbookContacts, ApiKey);

            Assert.IsTrue(removeLocations, "Removing of the addressbook contacts failed...");
        }

        [Test]
        public void HybridOptimizationFrom1000OrdersTest()
        {
            var ApiKey =
                ApiKeys.ActualApiKey; // The addresses in this test not allowed for this API key, you shuld put your valid API key.

            // Comment 2 lines bellow if you have put in the above line your valid key.
            Assert.IsTrue(1 > 0, "");
            if (ApiKey.Length > 10) return;

            var route4Me = new Route4MeManager(ApiKey);

            #region ======= Add scheduled address book locations to an user account ================================

            var sAddressFile = @"Data/CSV/orders_1000.csv";

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var lng = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var alias = csv.GetField(2);
                        var address1 = csv.GetField(3);
                        var city = csv.GetField(4);
                        var state = csv.GetField(5);
                        var zip = csv.GetField(6);
                        var phone = csv.GetField(7);
                        var sched_date = csv.GetField(8);

                        var sAddress = "";

                        if (address1 != null) sAddress += address1.Trim();
                        if (city != null) sAddress += ", " + city.Trim();
                        if (state != null) sAddress += ", " + state.Trim();
                        if (zip != null) sAddress += ", " + zip.Trim();

                        if (sAddress == "") continue;

                        var newOrder = new Order();

                        if (lng != null) newOrder.CachedLat = Convert.ToDouble(lng);
                        if (lat != null) newOrder.CachedLng = Convert.ToDouble(lat);
                        if (alias != null) newOrder.AddressAlias = alias.Trim();
                        newOrder.Address1 = sAddress;
                        if (phone != null) newOrder.ExtFieldPhone = phone.Trim();

                        var dt = DateTime.Now;

                        if (sched_date != null)
                            if (DateTime.TryParse(sched_date, out dt))
                            {
                                dt = Convert.ToDateTime(sched_date);
                                newOrder.DayScheduledFor_YYYYMMDD = dt.ToString("yyyy-MM-dd");
                            }

                        var resultOrder = route4Me.AddOrder(newOrder, out var errorString);
                        Assert.IsNotNull(resultOrder, "Creating of an order failed... " + errorString);

                        if (resultOrder != null)
                        {
                            var OrderId = resultOrder.OrderId.ToString();

                            if (OrderId != "") _lsOrders.Add(OrderId);
                        }

                        Thread.Sleep(1000);
                    }
                }

                ;
            }

            ;

            #endregion

            Thread.Sleep(2000);

            #region ======= Get Hybrid Optimization ================================

            var tsp1day = new TimeSpan(1, 0, 0, 0);
            var lsScheduledDays = new List<string>();
            var curDate = DateTime.Now;

            for (var i = 0; i < 5; i++)
            {
                curDate += tsp1day;
                lsScheduledDays.Add(curDate.ToString("yyyy-MM-dd"));
            }

            Address[] Depots =
            {
                new Address
                {
                    AddressString = "2017 Ambler Ave, Abilene, TX, 79603-2239",
                    IsDepot = true,
                    Latitude = 32.474395,
                    Longitude = -99.7447021,
                    CurbsideLatitude = 32.474395,
                    CurbsideLongitude = -99.7447021
                },
                new Address
                {
                    AddressString = "807 Ridge Rd, Alamo, TX, 78516-9596",
                    IsDepot = true,
                    Latitude = 26.170834,
                    Longitude = -98.116201,
                    CurbsideLatitude = 26.170834,
                    CurbsideLongitude = -98.116201
                },
                new Address
                {
                    AddressString = "1430 W Amarillo Blvd, Amarillo, TX, 79107-5505",
                    IsDepot = true,
                    Latitude = 35.221969,
                    Longitude = -101.835288,
                    CurbsideLatitude = 35.221969,
                    CurbsideLongitude = -101.835288
                },
                new Address
                {
                    AddressString = "3611 Ne 24Th Ave, Amarillo, TX, 79107-7242",
                    IsDepot = true,
                    Latitude = 35.236626,
                    Longitude = -101.795117,
                    CurbsideLatitude = 35.236626,
                    CurbsideLongitude = -101.795117
                },
                new Address
                {
                    AddressString = "1525 New York Ave, Arlington, TX, 76010-4723",
                    IsDepot = true,
                    Latitude = 32.720524,
                    Longitude = -97.080195,
                    CurbsideLatitude = 32.720524,
                    CurbsideLongitude = -97.080195
                }
            };

            foreach (var ScheduledDay in lsScheduledDays)
            {
                var hparams = new HybridOptimizationParameters
                {
                    TargetDateString = ScheduledDay,
                    TimezoneOffsetMinutes = 480
                };

                var resultOptimization = route4Me.GetHybridOptimization(hparams, out var errorString1);

                var HybridOptimizationId = "";

                if (resultOptimization != null)
                {
                    HybridOptimizationId = resultOptimization.OptimizationProblemId;
                    Console.WriteLine("Hybrid optimization generating executed successfully");

                    Console.WriteLine("Generated hybrid optimization ID: {0}", HybridOptimizationId);
                }
                else
                {
                    Console.WriteLine("Hybrid optimization generating error: {0}", errorString1);
                    continue;
                }

                //============== Add Depot To Hybrid Optimization ===============
                var hDepotParams = new HybridDepotParameters
                {
                    OptimizationProblemId = HybridOptimizationId,
                    DeleteOldDepots = true,
                    NewDepots = new[] { Depots[lsScheduledDays.IndexOf(ScheduledDay)] }
                };

                var addDepotResult = route4Me.AddDepotsToHybridOptimization(hDepotParams, out var errorString3);

                Thread.Sleep(5000);

                //============== Reoptimization =================================
                var optimizationParameters = new OptimizationParameters
                {
                    OptimizationProblemID = HybridOptimizationId,
                    ReOptimize = true
                };

                var finalOptimization = route4Me.UpdateOptimization(optimizationParameters, out var errorString2);

                Assert.IsNotNull(finalOptimization, "Update optimization failed... " + errorString1);

                if (finalOptimization != null) _lsOptimizationIDs.Add(finalOptimization.OptimizationProblemId);

                Thread.Sleep(5000);
                //=================================================================
            }

            var removeOrders = _tdr.RemoveOrders(_lsOrders, ApiKey);

            Assert.IsTrue(removeOrders, "Removing of the orders failed...");

            #endregion
        }

        [OneTimeTearDown]
        public void OptimizationsGroupCleanup()
        {
            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(
                result,
                "Removing of the testing optimization problem failed.");
        }
    }
}