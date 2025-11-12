using System;

using Route4MeSDKLibrary.QueryTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a paginated list of customers asynchronously.
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetCustomersListAsyncV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new CustomerListParameters()
            {
                Page = 1,
                PerPage = 20
            };

            var (result, resultResponse) = await route4Me.GetCustomersListAsync(parameters);

            PrintTestCustomer(result, resultResponse);
        }
    }
}