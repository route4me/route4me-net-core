using System;

using Route4MeSDKLibrary.QueryTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of deleting an existing customer asynchronously.
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void DeleteCustomerAsyncV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new CustomerIdParameters()
            {
                CustomerId = "3b41b5b60a1d47ff8ffbc09b7de88ef1" // replace with actual ID
            };

            var tupleResult = await route4Me.DeleteCustomerAsync(parameters);

            var result = tupleResult.Item1;
            var resultResponse = tupleResult.Item2;

            if (result != null && (result.Status == true || result.Code == 200))
            {
                Console.WriteLine($"Customer {parameters.CustomerId} deleted successfully (async).");
            }
            else
            {
                Console.WriteLine($"Failed to delete customer {parameters.CustomerId} (async).");
                PrintTestCustomer(result, resultResponse);
            }
        }
    }
}