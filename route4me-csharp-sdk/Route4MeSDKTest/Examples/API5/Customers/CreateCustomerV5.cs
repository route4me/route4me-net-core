using System;
using System.Linq;

using Route4MeSDKLibrary.DataTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a new customer.
        /// </summary>
        public void CreateCustomerV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new StoreRequest
            {
                Name = "Test Customer " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                ExternalId = Guid.NewGuid().ToString("N"),
                Status = 1,
                // Required: the team member accountable for the customer.
                AccountablePersonId = route4Me.GetTeamMembers(out _)?.FirstOrDefault()?.MemberId
            };

            var customer = route4Me.CreateCustomer(request, out var resultResponse);

            PrintTestCustomer(customer, resultResponse);
        }
    }
}