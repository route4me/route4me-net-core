using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Collections.Generic;

namespace Route4MeDB.UnitTests.Builders
{
    public class OrderBuilder
    {
        private Order _order;
        public readonly TestData testData = new TestData();

        public class TestData
        {
            public string Address1 = "318 S 39th St, Louisville, KY 40212, USA";
            public double CachedLat = 38.259326;
            public double CachedLng = -85.814979;
            public double CurbsideLat = 38.259326;
            public double CurbsideLng = -85.814979;
            public string AddressAlias = "318 S 39th St 40212";
            public string AddressCity = "Louisville";
            public string EXT_FIELD_first_name = "Lui";
            public string EXT_FIELD_last_name = "Carol";
            public string EXT_FIELD_email = "lcarol654@yahoo.com";
            public string EXT_FIELD_phone = "897946541";
            public Dictionary<string, string> EXT_FIELD_custom_datas = new Dictionary<string, string>() { { "order_type", "scheduled order" } };
            public string DayScheduledForYyMmDd = "2017-12-20";
            public int LocalTimeWindowEnd = 39000;
            public int LocalTimeWindowEnd2 = 46200;
            public int LocalTimeWindowStart = 37800;
            public int LocalTimeWindowStart2 = 45000;
            public string LocalTimezoneString = "America/New_York";
            public string OrderIcon = "emoji/emoji-bank";
        }

        public OrderBuilder()
        {
            _order = WithDefaultValues();
        }

        public Order Build()
        {
            return _order;
        }

        public Order WithDefaultValues()
        {
            _order = new Order()
            {
                Address1 = testData.Address1,
                CachedLat = testData.CachedLat,
                CachedLng = testData.CachedLng,
                CurbsideLat = testData.CurbsideLat,
                CurbsideLng = testData.CurbsideLng,
                AddressAlias = testData.AddressAlias,
                AddressCity = testData.AddressCity,
                EXT_FIELD_first_name = testData.EXT_FIELD_first_name,
                EXT_FIELD_last_name = testData.EXT_FIELD_last_name,
                EXT_FIELD_email = testData.EXT_FIELD_email,
                EXT_FIELD_phone = testData.EXT_FIELD_phone,
               // EXT_FIELD_custom_datas = new Dictionary<string, string> { { "cust field", "cust value" } },
                DayScheduledForYyMmDd = testData.DayScheduledForYyMmDd,
                LocalTimeWindowEnd = testData.LocalTimeWindowEnd,
                LocalTimeWindowEnd2 = testData.LocalTimeWindowEnd2,
                LocalTimeWindowStart = testData.LocalTimeWindowStart,
                LocalTimeWindowStart2 = testData.LocalTimeWindowStart2,
                LocalTimezoneString = testData.LocalTimezoneString,
                OrderIcon = testData.OrderIcon
            };

            return _order;
        }

        public Order WithCustomData()
        {
            _order = WithDefaultValues();
            _order.EXT_FIELD_custom_datas = testData.EXT_FIELD_custom_datas;
            return _order;
        }
    }
}
