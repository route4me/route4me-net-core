using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void MergeRoutes(MergeRoutesQuery mergeRoutesParameters)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            bool result = route4Me.MergeRoutes(mergeRoutesParameters, out string errorString);

            Console.WriteLine("");

            if (result)
            {
                Console.WriteLine("MergeRoutes executed successfully, {0} routes merged", mergeRoutesParameters.RouteIds);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("MergeRoutes error {0}", errorString);
            }
        }
    }
}
