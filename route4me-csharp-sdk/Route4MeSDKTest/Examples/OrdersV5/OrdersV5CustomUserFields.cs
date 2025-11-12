using Newtonsoft.Json.Linq;

using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void OrdersV5CustomUserFields()
        {
            var route4Me = new OrderManagerV5(ActualApiKey);

            // Create
            var createdCustomUserFields = route4Me.CreateCustomUserFields(new CreateCustomUserFieldRequest()
            {
                OrderCustomFieldLabel = "Test label 2",
                OrderCustomFieldName = "Test name 2",
                OrderCustomFieldType = "Test type 2",
                OrderCustomFieldTypeInfo = JObject.Parse("{\"prop1\": 123}")
            }, out _);

            //Read 
            var customUserFields = route4Me.GetCustomUserFields(out var resultResponse);

            //Update
            var updateCustomUserFieldRequest = new UpdateCustomUserFieldRequest()
            {
                OrderCustomFieldLabel = "Test label 3",
                OrderCustomFieldType = createdCustomUserFields.Data.OrderCustomFieldType,
                OrderCustomFieldTypeInfo = createdCustomUserFields.Data.OrderCustomFieldTypeInfo
            };
            var updatedCustomUserFields = route4Me.UpdateCustomUserFields(createdCustomUserFields.Data.OrderCustomFieldUuid, updateCustomUserFieldRequest, out _);

            // Delete
            route4Me.DeleteCustomUserFields(createdCustomUserFields.Data.OrderCustomFieldUuid, out _);
        }
    }
}