using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Scheduled Order
        /// </summary>
        /// <returns> Added Order </returns>
        public Order AddScheduledOrder()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var order = new Order()
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
                ExtFieldCustomData = new Dictionary<string, string>() { { "order_type", "scheduled order" } },
                DayScheduledFor_YYMMDD = "2017-12-20",
                LocalTimeWindowEnd = 39000,
                LocalTimeWindowEnd2 = 46200,
                LocalTimeWindowStart = 37800,
                LocalTimeWindowStart2 = 45000,
                LocalTimezoneString = "America/New_York",
                OrderIcon = "emoji/emoji-bank"
            };

            // Run the query
            Order resultOrder = route4Me.AddOrder(order, out string errorString);

            Console.WriteLine("");

            if (resultOrder != null)
            {
                Console.WriteLine("AddScheduledOrder executed successfully");

                Console.WriteLine("Order ID: {0}", resultOrder.OrderId);

                return resultOrder;
            }
            else
            {
                Console.WriteLine("AddScheduledOrder error: {0}", errorString);

                return null;
            }
        }
    }
}
