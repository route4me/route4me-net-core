using System;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Example: get the list of filterable/sortable fields for route destinations (API V5).
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets all available destination fields (name, type, kind) that can be used
        /// in filter and sort parameters.
        /// Uses GET /route-destinations/list/fields.
        /// </summary>
        public void GetDestinationFieldsV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);
            const string tag = "GetDestinationFieldsV5";

            Console.WriteLine("");

            var result = route4Me.GetDestinationFields(out ResultResponse resultResponse);

            if (resultResponse == null && result != null)
            {
                Console.WriteLine("{0} executed successfully. Fields count: {1}",
                    tag, result.Fields?.Length ?? 0);

                if (result.Fields != null)
                {
                    foreach (var field in result.Fields)
                    {
                        Console.WriteLine("  value={0,-40} label={1,-35} kind={2,-10} type={3}",
                            field.Value, field.Label, field.Kind, field.Type);
                    }
                }
            }
            else
            {
                PrintFailResponse(resultResponse, tag);
            }
        }
    }
}