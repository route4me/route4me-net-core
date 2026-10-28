using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.Customers;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    internal class CustomerImportanceTests
    {
        private const string PriorityId = "B97C3EEE6997A58B70C06E60EA0E5CF9";
        private const string CustomerId = "0E3A2F6C1B5D4E7F8A9B0C1D2E3F4A5B";

        [Test]
        public void SerializeOptimizationParameters_WithImportance_IncludesItInPayload()
        {
            var optimizationParameters = new OptimizationParameters
            {
                Addresses = new[]
                {
                    new Address
                    {
                        AddressString = "754 5th Ave New York, NY 10019",
                        Latitude = 40.7636197,
                        Longitude = -73.9744388,
                        Importance = 2,
                        CustomerId = CustomerId
                    }
                },
                Parameters = new RouteParameters { UseCustomerImportance = true }
            };

            var json = R4MeUtils.SerializeObjectToJson(optimizationParameters);

            Assert.That(json, Does.Contain("\"use_customer_importance\":true"));
            Assert.That(json, Does.Contain("\"importance\":2"));
            Assert.That(json, Does.Contain("\"customer_id\":\"" + CustomerId + "\""));
        }

        [Test]
        public void SerializeV5RouteParameters_SendsFalseAndOmitsNull()
        {
            var off = R4MeUtils.SerializeObjectToJson(
                new Route4MeSDK.DataTypes.V5.RouteParameters { UseCustomerImportance = false });
            var notSet = R4MeUtils.SerializeObjectToJson(new Route4MeSDK.DataTypes.V5.RouteParameters());
            var address = R4MeUtils.SerializeObjectToJson(
                new Route4MeSDK.DataTypes.V5.Address { Importance = 3, CustomerId = CustomerId });

            Assert.That(off, Does.Contain("\"use_customer_importance\":false"));
            Assert.That(notSet, Does.Not.Contain("use_customer_importance"));
            Assert.That(address, Does.Contain("\"importance\":3"));
            Assert.That(address, Does.Contain("\"customer_id\":\"" + CustomerId + "\""));
        }

        [Test]
        public void SerializeCustomerRequests_IncludePriority()
        {
            var customer = R4MeUtils.SerializeObjectToJson(new StoreRequest { Name = "Acme", PriorityId = PriorityId });
            var priority = R4MeUtils.SerializeObjectToJson(new CustomerPriorityRequest { Name = "High", SortOrder = 0 });

            Assert.That(customer, Does.Contain("\"priority_id\":\"" + PriorityId + "\""));
            Assert.That(priority, Does.Contain("\"name\":\"High\""));
            Assert.That(priority, Does.Contain("\"sort_order\":0"));
        }

        [Test]
        public void DeserializeCustomerPriorityResponses()
        {
            var list = R4MeUtils.ReadObjectNew<CustomerPriorityListResponse>(
                "{\"data\":[{\"customer_priority_id\":\"" + PriorityId + "\",\"name\":\"High\",\"sort_order\":1}]}");
            var customer = R4MeUtils.ReadObjectNew<CustomerResource>(
                "{\"customer_id\":\"C1\",\"priority_id\":\"" + PriorityId + "\",\"priority_label\":\"High\"}");

            Assert.That(list.Data, Has.Length.EqualTo(1));
            Assert.That(list.Data[0].CustomerPriorityId, Is.EqualTo(PriorityId));
            Assert.That(list.Data[0].SortOrder, Is.EqualTo(1));
            Assert.That(customer.PriorityId, Is.EqualTo(PriorityId));
            Assert.That(customer.PriorityLabel, Is.EqualTo("High"));
        }
    }
}