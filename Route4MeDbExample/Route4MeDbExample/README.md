# Usage Example of the Route4MeDbLibary package (.net core)


This simple console c# (.net core) project demonstrates the functionalities of the Route4MeDbLibrary package.

The project is done with the Visual Studio 2019.


### Project Implementation Steps

1. Create console c# (.net project);

2. Search NuGet for the package Route4MeDbLibrary and install it in the created project;

3. Add to the project the file [RunExamples.cs](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDbExample/Route4MeDbExample/RunExamples.cs).

4. Write in the Program.cs.

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

- CreateRoute4MeDatabase - demonstrates how to create a database with Route4Me data structure in the specified database provider.
- CreateAddressBookContact - demonstrates how to initialize and save in the database an address book contact.
- CopyRouteJsonResponseToDatabase - demonstrates how to import JSON content of the Route4Me route object and save it in the database.
- CreateOptimizationAndSaveToDatabase - demonstrates how to create an optimization in the Route4Me account and simultaneously save it in the database.

Create database in the appropriate database provider if it doesn't exist, set API key and run the project.
