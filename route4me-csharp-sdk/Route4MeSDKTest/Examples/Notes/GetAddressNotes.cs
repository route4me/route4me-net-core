using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetAddressNotes(string routeId, int routeDestinationId)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var noteParameters = new NoteParameters()
            {
                RouteId = routeId,
                AddressId = routeDestinationId
            };

            // Run the query
            AddressNote[] notes = route4Me.GetAddressNotes(noteParameters, out string errorString);

            Console.WriteLine("");

            if (notes != null)
            {
                Console.WriteLine("GetAddressNotes executed successfully, {0} notes returned", notes.Length);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("GetAddressNotes error: {0}", errorString);
                Console.WriteLine("");
            }
        }
    }
}

