using Route4MeSDKLibrary.QueryTypes.V5.Customers;
using System;
using System.Threading.Tasks;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the asynchronous process of updating an existing customer.
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async Task UpdateCustomerAsyncV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new UpdateCustomerParameters
            {
                CustomerId = "3b41b5b60a1d47ff8ffbc09b7de88ef1", // replace with actual ID
                Name = "Async Updated Test Customer " + DateTime.Now.ToString("HHmmss"),
                ExternalId = "ext-" + DateTime.Now.ToString("yyMMdd"),
                Status = 1
            };

            var (result, resultResponse) = await route4Me.UpdateCustomerAsync(parameters);

            PrintTestCustomer(result, resultResponse);
        }
    }
}
