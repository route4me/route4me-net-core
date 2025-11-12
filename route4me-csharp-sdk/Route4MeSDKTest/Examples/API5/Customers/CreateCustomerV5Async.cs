using System;
using System.Threading.Tasks;

using Route4MeSDKLibrary.DataTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the asynchronous process of creating a new customer.
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async Task CreateCustomerAsyncV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new StoreRequest
            {
                Name = "Async Test Customer " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                ExternalId = Guid.NewGuid().ToString("N"),
                Status = 1,
                Currency = "USD"
            };

            var (result, resultResponse) = await route4Me.CreateCustomerAsync(request);

            PrintTestCustomer(result, resultResponse);
        }
    }
}