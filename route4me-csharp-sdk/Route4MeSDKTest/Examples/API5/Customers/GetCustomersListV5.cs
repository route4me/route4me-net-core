using Route4MeSDKLibrary.QueryTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a paginated list of customers.
        /// </summary>
        public void GetCustomersListV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new CustomerListParameters()
            {
                Page = 1,
                PerPage = 20
            };

            var result = route4Me.GetCustomersList(parameters, out var resultResponse);

            PrintTestCustomer(result, resultResponse);
        }
    }
}