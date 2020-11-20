using Route4MeSDK.DataTypes;
using Route4MeSDK.Examples;
using System;
using System.Collections.Generic;
//using NUnit.Framework;

namespace Route4MeSDKTest
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            var examples = new Route4MeExamples();

            examples.SingleDriverRoute10Stops();
            examples.ResequenceRouteDestinations();
            examples.ResequenceReoptimizeRoute();
            examples.AddRouteDestinations();
            examples.RemoveRouteDestination();
            examples.SingleDriverRoundTrip();
            examples.MoveDestinationToRoute();
            examples.SingleDriverRoundTripGeneric();
            examples.MultipleDepotMultipleDriver();
            examples.MultipleDepotMultipleDriverTimeWindow();
            examples.SingleDepotMultipleDriverNoTimeWindow();
            examples.MultipleDepotMultipleDriverWith24StopsTimeWindow();
            examples.SingleDriverMultipleTimeWindows();
            examples.GetOptimization();

            examples.GetOptimizations();

            examples.AddDestinationToOptimization();
            examples.RemoveDestinationFromOptimization();
            examples.ReOptimization();

            examples.UpdateRoute();
            examples.ReoptimizeRoute();
            examples.GetRoute();
            examples.GetRoutes();
            examples.GetUsers();

            examples.LogCustomActivity();
            examples.GetActivities();

            examples.GetAddress();

            examples.AddAddressNote();
            examples.AddAddressNoteWithFile();
            examples.GetAddressNotes();

            examples.DuplicateRoute();
            examples.DeleteRoutes();

            examples.RemoveOptimization();

            // Address Book
            examples.CreateTestContacts();
            AddressBookContact contact1 = examples.contact1;
            AddressBookContact contact2 = examples.contact2;

            examples.GetAddressBookContacts();
            if (contact1 != null)
            {
                contact1.LastName = "Updated " + (new Random()).Next().ToString();
                examples.UpdateAddressBookContact(contact1);
            }
            else
            {
                Console.WriteLine("contact1 == null. UpdateAddressBookContact not called.");
            }
            List<string> addressIdsToRemove = new List<string>();
            if (contact1 != null)
                addressIdsToRemove.Add(contact1.AddressId.ToString());
            if (contact2 != null)
                addressIdsToRemove.Add(contact2.AddressId.ToString());
            examples.RemoveAddressBookContacts(addressIdsToRemove.ToArray());


            // Avoidance Zones
            string territoryId = examples.AddAvoidanceZone();
            examples.GetAvoidanceZones();
            if (territoryId != null)
                examples.GetAvoidanceZone(territoryId);
            else
                Console.WriteLine("GetAvoidanceZone not called. territoryId == null.");
            if (territoryId != null)
                examples.UpdateAvoidanceZone(territoryId);
            else
                Console.WriteLine("UpdateAvoidanceZone not called. territoryId == null.");
            if (territoryId != null)
                examples.DeleteAvoidanceZone(territoryId);
            else
                Console.WriteLine("DeleteAvoidanceZone not called. territoryId == null.");


            // Orders
            examples.AddOrder();
            examples.GetOrders();
            examples.UpdateOrder();
            examples.RemoveOrders();


            examples.GenericExample();
            examples.GenericExampleShortcut();

            Console.WriteLine("");
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
