using System;

namespace Route4MeDbExample
{
    class Program
    {
        /// <summary>
        /// Make sure to replace the 11111111111111111111111111111111 (32 characters) demo API key with your API key.
        /// With the demo API key, the Route4Me system provides only limited functionality.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var runExample = new RunExamples(Route4MeDB.Route4MeDbLibrary.DatabaseProviders.InMemory);

            runExample.c_ApiKey = "11111111111111111111111111111111";

            #region // Run the examples. Note: uncomment a line for executing.
            // runExample.CreateRoute4MeDatabase();

            // runExample.CreateAddressBookContact();

            // runExample.CopyRouteJsonResponseToDatabase();

             runExample.CreateOptimizationAndSaveToDatabase();
            #endregion

            Console.ReadKey();
        }
    }
}
