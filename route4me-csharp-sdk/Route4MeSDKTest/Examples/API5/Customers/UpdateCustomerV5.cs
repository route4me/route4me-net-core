using System;

using Route4MeSDKLibrary.QueryTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating an existing customer.
        /// </summary>
        public void UpdateCustomerV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new UpdateCustomerParameters
            {
                CustomerId = "3b41b5b60a1d47ff8ffbc09b7de88ef1", // replace with actual ID
                Name = "Updated Test Customer " + DateTime.Now.ToString("HHmmss"),
                ExternalId = "ext-" + DateTime.Now.ToString("yyMMdd"),
                Status = 1
            };

            var result = route4Me.UpdateCustomer(parameters, out var resultResponse);

            PrintTestCustomer(result, resultResponse);
        }
    }
}