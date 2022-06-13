using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class OrderCustomUserFieldsGroupTests
    {
        private static string Skip;

        private static readonly string
            CApiKey = ApiKeys
                .ActualApiKey; // This group allowed only for business and higher account types --- put in the parameter an appropriate API key

        private static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey; //
        private List<long> _lsOrderCustomUserFieldIDs = new List<long>();

        [OneTimeSetUp]
        public void OrderCustomUserFieldsInitialize()
        {
            if (CApiKey == c_ApiKey_1) Skip = "yes";
            else Skip = "no";

            if (Skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            var orderCustomUserFields = route4Me.GetOrderCustomUserFields(out var errorString);

            long customFieldId;

            if (orderCustomUserFields
                    .Where(x => x.OrderCustomFieldName == "CustomField33")
                    .Count() > 0)
            {
                customFieldId = orderCustomUserFields
                    .Where(x => x.OrderCustomFieldName == "CustomField33")
                    .FirstOrDefault().OrderCustomFieldId;
            }
            else
            {
                var orderCustomFieldParams = new OrderCustomFieldParameters
                {
                    OrderCustomFieldName = "CustomField33",
                    OrderCustomFieldLabel = "Custom Field 33",
                    OrderCustomFieldType = "checkbox",
                    OrderCustomFieldTypeInfo = new Dictionary<string, object>
                    {
                        {"short_label", "cFl33"},
                        {"description", "This is test order custom field"},
                        {"custom field no", 10}
                    }
                };

                var createdCustomField = route4Me.CreateOrderCustomUserField(
                    orderCustomFieldParams,
                    out errorString);


                Assert.That(createdCustomField, Is.InstanceOf<OrderCustomFieldCreateResponse>(), "Cannot initialize the class OrderCustomUserFieldsGroup. " + errorString);

                customFieldId = createdCustomField.Data.OrderCustomFieldId;
            }

            _lsOrderCustomUserFieldIDs = new List<long> {customFieldId};
        }


        [Test]
        public void GetOrderCustomUserFieldsTest()
        {
            if (Skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            var orderCustomUserFields = route4Me.GetOrderCustomUserFields(out var errorString);

            Assert.That(orderCustomUserFields, Is.InstanceOf<OrderCustomField[]>(), "GetOrderCustomUserFieldsTest failed. " + errorString);
        }

        [Test]
        public void CreateOrderCustomUserFieldTest()
        {
            if (Skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            var orderCustomFieldParams = new OrderCustomFieldParameters
            {
                OrderCustomFieldName = "CustomField77",
                OrderCustomFieldLabel = "Custom Field 77",
                OrderCustomFieldType = "checkbox",
                OrderCustomFieldTypeInfo = new Dictionary<string, object>
                {
                    {"short_label", "cFl77"},
                    {"description", "This is test order custom field"},
                    {"custom field no", 11}
                }
            };

            var orderCustomUserField = route4Me.CreateOrderCustomUserField(
                orderCustomFieldParams,
                out var errorString);

            Assert.That(orderCustomUserField, Is.InstanceOf<OrderCustomFieldCreateResponse>(), "CreateOrderCustomUserFieldTest failed. " + errorString);

            _lsOrderCustomUserFieldIDs.Add(orderCustomUserField.Data.OrderCustomFieldId);
        }

        [Test]
        public void UpdateOrderCustomUserFieldTest()
        {
            if (Skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            var orderCustomFieldParams = new OrderCustomFieldParameters
            {
                OrderCustomFieldId = _lsOrderCustomUserFieldIDs[_lsOrderCustomUserFieldIDs.Count - 1],
                OrderCustomFieldLabel = "Custom Field 55",
                OrderCustomFieldType = "checkbox",
                OrderCustomFieldTypeInfo = new Dictionary<string, object>
                {
                    {"short_label", "cFl55"},
                    {"description", "This is updated test order custom field"},
                    {"custom field no", 12}
                }
            };

            var orderCustomUserField = route4Me.UpdateOrderCustomUserField(
                orderCustomFieldParams,
                out var errorString);

            Assert.That(orderCustomUserField, Is.InstanceOf<OrderCustomFieldCreateResponse>(), "UpdateOrderCustomUserFieldTest failed. " + errorString);

            Assert.AreEqual(
                "Custom Field 55",
                orderCustomUserField.Data.OrderCustomFieldLabel,
                "UpdateOrderCustomUserFieldTest failed. " + errorString);
        }

        [Test]
        public void RemoveOrderCustomUserFieldTest()
        {
            if (Skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            var orderCustomFieldId = _lsOrderCustomUserFieldIDs[_lsOrderCustomUserFieldIDs.Count - 1];

            var orderCustomFieldParams = new OrderCustomFieldParameters
            {
                OrderCustomFieldId = orderCustomFieldId
            };

            var response = route4Me.RemoveOrderCustomUserField(
                orderCustomFieldParams,
                out var errorString);

            Assert.IsTrue(
                response.Affected == 1,
                "RemoveOrderCustomUserFieldTest failed. " + errorString);

            _lsOrderCustomUserFieldIDs.Remove(orderCustomFieldId);
        }

        [OneTimeTearDown]
        public void RemoveOrderCustomUserFields()
        {
            if (Skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            foreach (var customFieldId in _lsOrderCustomUserFieldIDs)
            {
                var customFieldParam = new OrderCustomFieldParameters
                {
                    OrderCustomFieldId = customFieldId
                };

                var removeResult = route4Me.RemoveOrderCustomUserField(
                    customFieldParam,
                    out var errorString);

                Assert.IsTrue(
                    removeResult.Affected == 1,
                    "Cannot remove order customuser field with id=" + customFieldId + ". " + errorString);
            }

            _lsOrderCustomUserFieldIDs.Clear();
        }
    }
}