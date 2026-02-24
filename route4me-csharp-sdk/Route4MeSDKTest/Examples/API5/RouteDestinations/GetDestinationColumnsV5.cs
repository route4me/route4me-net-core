using System;

using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Example: read and update the destination column configuration (API V5).
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets the current destination column configuration for a given configurator key,
        /// then updates the column order by moving the first column to the end.
        /// Uses GET /route-destinations/columns and PUT /route-destinations/columns.
        /// </summary>
        public void GetDestinationColumnsV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);
            const string tag = "GetDestinationColumnsV5";

            // Replace with a real key from your account
            const string configuratorKey = "route_columns_config_key";

            Console.WriteLine("");

            // Step 1: get current columns
            Console.WriteLine("{0} Step 1: Getting destination columns...", tag);
            var columnsResult = route4Me.GetDestinationColumns(configuratorKey, out ResultResponse resp1);

            if (resp1 != null)
            {
                Console.WriteLine("{0} Step 1 failed.", tag);
                PrintFailResponse(resp1, tag);
                return;
            }

            Console.WriteLine("{0} Step 1 OK. Categories: {1}, Current order count: {2}",
                tag,
                columnsResult.Categories?.Length ?? 0,
                columnsResult.Order?.Length ?? 0);

            if (columnsResult.Order != null)
            {
                Console.WriteLine("  Current order:");
                foreach (var col in columnsResult.Order)
                    Console.WriteLine("    {0}", col);
            }

            if (columnsResult.Order == null || columnsResult.Order.Length < 2)
            {
                Console.WriteLine("{0}: Not enough columns to reorder; skipping Step 2.", tag);
                return;
            }

            // Step 2: move first column to the end
            var newOrder = new string[columnsResult.Order.Length];
            for (int i = 1; i < columnsResult.Order.Length; i++)
                newOrder[i - 1] = columnsResult.Order[i];
            newOrder[columnsResult.Order.Length - 1] = columnsResult.Order[0];

            Console.WriteLine("{0} Step 2: Updating column order...", tag);
            var editRequest = new EditDestinationColumnsRequest
            {
                ColumnsConfiguratorKey = configuratorKey,
                Order = newOrder
            };

            var editResult = route4Me.EditDestinationColumns(editRequest, out ResultResponse resp2);

            if (resp2 != null)
            {
                Console.WriteLine("{0} Step 2 failed.", tag);
                PrintFailResponse(resp2, tag);
                return;
            }

            Console.WriteLine("{0} Step 2 OK. Updated order count: {1}",
                tag, editResult.Order?.Length ?? 0);

            if (editResult.Order != null)
            {
                Console.WriteLine("  New order:");
                foreach (var col in editResult.Order)
                    Console.WriteLine("    {0}", col);
            }

            Console.WriteLine("{0} Done.", tag);
        }
    }
}
