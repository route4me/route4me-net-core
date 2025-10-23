using Route4MeSDKLibrary.QueryTypes.V5.Customers;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of deleting an existing customer.
        /// </summary>
        public void DeleteCustomerV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new CustomerIdParameters()
            {
                CustomerId = "3b41b5b60a1d47ff8ffbc09b7de88ef1" // replace with actual ID
            };

            var result = route4Me.DeleteCustomer(parameters, out var resultResponse);

            if (result != null && (result.Status == true || result.Code == 200))
            {
                Console.WriteLine($"Customer {parameters.CustomerId} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to delete customer {parameters.CustomerId}.");
                PrintTestCustomer(result, resultResponse);
            }
        }
    }
}
