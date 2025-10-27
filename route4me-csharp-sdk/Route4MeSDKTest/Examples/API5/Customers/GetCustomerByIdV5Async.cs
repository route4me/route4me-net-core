using Route4MeSDKLibrary.QueryTypes.V5.Customers;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a single customer by ID.
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetCustomerByIdV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new CustomerIdParameters()
            {
                CustomerId = "3b41b5b60a1d47ff8ffbc09b7de88ef1"
            };

            var (result, resultResponse) = await route4Me.GetCustomerByIdAsync(parameters);

            PrintTestCustomer(result, resultResponse);
        }
    }
}
