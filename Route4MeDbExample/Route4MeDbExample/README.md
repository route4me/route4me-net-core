# Usage Example of the Route4MeDbLibary package (.net core)


This simple console c# (.net core) project demonstrates the functionalities of the Route4MeDbLibrary package.

The project is done with the Visual Studio 2019.


### Project Implementation Steps

1. Create console c# (.net project);

2. Search NuGet for the package Route4MeDbLibrary and install it in the created prohject;

3. Add to the project the file RunExamples.cs with content:

```
using System;
using System.Linq;
using System.IO;
using OptimizationProblemEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.OptimizationProblem;
using RouteEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Route;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeDbExample
{
    /// <summary>
    /// The examples demostrate functionalities of the Route4MeDbLibrary package.
    /// </summary>
    public class RunExamples
    {
        public string c_ApiKey { get; set; }

        Route4MeDbManager route4MeDb = new Route4MeDbManager();
        bool AppSetingsFileExists;

        DataExchangeHelper exchangeHelper = new DataExchangeHelper();

        /// <summary>
        /// The class constructor.
        /// </summary>
        public RunExamples(DatabaseProviders databaseProvider)
        {
            AppSetingsFileExists = true;
            route4MeDb.AppSettingsFileCreated += Manager_AppSettingsFileCreated;
            route4MeDb.AssignDatabaseProvider(databaseProvider);
        }

        /// <summary>
        /// Handler for the creating event of the appSettings.json file with demo connection strings.
        /// The file will be created if it doesn't exist. In this case, it will be created and the application will be canceled.
        /// After creation, you can find the file appSettings.json in the core folder of the application and 
        /// change demo connection strings with the actual connections strings.
        /// </summary>
        /// <param name="sender">The event sender object</param>
        /// <param name="e">Event args</param>
        private void Manager_AppSettingsFileCreated(object sender, AppSettingsFileCreatedEventArgs e)
        {
            AppSetingsFileExists = false;
            Console.WriteLine("App file created ? " + e.AppSettingsFileCreated + ", Message Text -> " + e.MessageText);
        }

        /// <summary>
        /// Creates address book contact object (in memory) and saves it in the database.
        /// </summary>
        public void CreateAddressBookContact()
        {
            if (!AppSetingsFileExists) return;

            var contact = new Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate.AddressBookContact()
            {
                Address1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                AddressAlias = "1604 PARKRIDGE PKWY 40214. " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                AddressGroup = "Scheduled daily",
                FirstName = "Peter",
                LastName = "Newman",
                AddressEmail = "pnewman6564@yahoo.com",
                AddressPhoneNumber = "65432178",
                CachedLat = 38.141598,
                CachedLng = -85.793846,
                AddressCity = "Louisville"
            };

            var contactsRepo = new AddressBookContactRepository(route4MeDb.Route4MeContext);
            contactsRepo.AddAsync(contact);
            route4MeDb.Route4MeContext.SaveChangesAsync();

            bool contactExists = route4MeDb.ContactsRepository.CheckIfAddressBookContactExists(contact.AddressDbId);

            Console.WriteLine(contactExists ? "The contact created" : "The contact not created");

            Console.WriteLine("Press eny key to close the window");
        }
        
        /// <summary>
        /// Imports JSON content of the Route4Me route object and saves it in the database.
        /// </summary>
        public async void CopyRouteJsonResponseToDatabase()
        {
            if (!AppSetingsFileExists) return;

            string testDataFile = @"TestData/route_plain_with_10_stops.json";

            StreamReader reader = new StreamReader(testDataFile);
            var jsonContent = reader.ReadToEnd();
            reader.Close();

            var importedRoute = exchangeHelper.ConvertSdkJsonContentToEntity<RouteEntity>(jsonContent, out string errorString);

            var routesRepo = new RouteRepository(route4MeDb.Route4MeContext);

            var savedRoute = await routesRepo.AddAsync(importedRoute);

            await route4MeDb.Route4MeContext.SaveChangesAsync();


            var result = route4MeDb.Route4MeContext.Routes
                .Where(x => x.RouteDbId == savedRoute.RouteDbId).FirstOrDefault();

            Console.WriteLine("Imported route ID = " + importedRoute.RouteId);
            Console.WriteLine("Imported route name = " + importedRoute.ParametersObj.RouteName);

            Console.WriteLine("Saved route ID = " + savedRoute.RouteId);
            Console.WriteLine("Saved route name = " + savedRoute.ParametersObj.RouteName);

            Console.WriteLine("Found route DB ID = " + result.RouteDbId);
            Console.WriteLine("Found route name = " + result.ParametersObj.RouteName);
        }

        /// <summary>
        /// Creates an optimization in the Route4Me account and simultaneously saves it in the database.
        /// </summary>
        public async void CreateOptimizationAndSaveToDatabase()
        {
            if (!AppSetingsFileExists) return;

            #region // Create an optimization problem with the Route4Me SDK in the Route4Me account.
            Console.WriteLine("SingleDriverRoute10Stops");

            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
            {
                #region Addresses

                new Address() { AddressString = "151 Arbor Way Milledgeville GA 31061",
                                //indicate that this is a departure stop
                                //single depot routes can only have one departure depot 
                                IsDepot = true, 
                                
                                //required coordinates for every departure and stop on the route
                                Latitude = 33.132675170898,
                                Longitude = -83.244743347168,
                                
                                //the expected time on site, in seconds. this value is incorporated into the optimization engine
                                //it also adjusts the estimated and dynamic eta's for a route
                                Time = 0, 
                                
                                
                                //input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest
                                CustomFields = new Dictionary<string, string>() {{"color", "red"}, {"size", "huge"}}
                },

                new Address() { AddressString = "230 Arbor Way Milledgeville GA 31061",
                                Latitude = 33.129695892334,
                                Longitude = -83.24577331543,
                                Time = 0 },

                new Address() { AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                                Latitude = 33.143497,
                                Longitude = -83.224487,
                                Time = 0 },

                new Address() { AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude = 33.141784667969,
                                Longitude = -83.237518310547,
                                Time = 0 },

                new Address() { AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude = 33.141086578369,
                                Longitude = -83.238258361816,
                                Time = 0 },

                new Address() { AddressString =  "131 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude = 33.142036437988,
                                Longitude = -83.238845825195,
                                Time = 0 },

                new Address() { AddressString =  "138 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude = 33.14307,
                                Longitude = -83.239334,
                                Time = 0 },

                new Address() { AddressString =  "139 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude = 33.142734527588,
                                Longitude = -83.237442016602,
                                Time = 0 },

                new Address() { AddressString =  "145 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude = 33.143871307373,
                                Longitude = -83.237342834473,
                                Time = 0 },

                new Address() { AddressString =  "221 Blake Cir Milledgeville GA 31061",
                                Latitude = 33.081462860107,
                                Longitude = -83.208511352539,
                                Time = 0 }

                #endregion
            };

            // Set parameters
            RouteParameters parameters = new RouteParameters();

            parameters.AlgorithmType = AlgorithmType.TSP;
            parameters.RouteName = "Single Driver Route 10 Stops";
            parameters.RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1));
            parameters.RouteTime = 60 * 60 * 7;
            parameters.Optimize = R4MeUtils.Description(Optimize.Distance);
            parameters.DistanceUnit = R4MeUtils.Description(DistanceUnit.MI);
            parameters.DeviceType = R4MeUtils.Description(DeviceType.Web);

            OptimizationParameters optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            string errorString;
            DataObject dataObject = route4Me.RunOptimization(optimizationParameters, out errorString);
            #endregion

            // Check the result
            if (dataObject == null)
            {
                Console.WriteLine("SingleDriverRoute10Stops failed: " + errorString);
                return;
            }
            else
            {
                Console.WriteLine("Created optimization problem ID = "+dataObject.OptimizationProblemId);
            }

            #region // Save the optimization in the dataase
            OptimizationProblemEntity optimizationEntity =  exchangeHelper.ConvertSdkOptimizationToEntity(dataObject);

            var optimizationsRepo = new OptimizationRepository(route4MeDb.Route4MeContext);

            var savedOptimization = await optimizationsRepo.AddAsync(optimizationEntity);

            await route4MeDb.Route4MeContext.SaveChangesAsync();

            Console.WriteLine("Saved optimization problem ID = " + savedOptimization.OptimizationProblemId);
            Console.WriteLine("Saved optimization problem DB ID = " + savedOptimization.OptimizationProblemDbId);

            #endregion
        }

    }
}
```

4. Write in the Program.cs the code:

```
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
```

5. The module RunExamples contains 4 examlples (methods):

1. CreateRoute4MeDatabase - demonstrates how to create a database with Route4Me data structure in the specified database provider.
2. CreateAddressBookContact - demonstrates how to initialize and save in the database address book contact.
3. CopyRouteJsonResponseToDatabase - demonstrates how to import JSON content of the Route4Me route object and save it in the database.
4. CreateOptimizationAndSaveToDatabase - demonstrates how to create an optimization in the Route4Me account and simultaneously save it in the database.

Create database in the appropriate database provider if it doesn't exist, set API key and run the project.
