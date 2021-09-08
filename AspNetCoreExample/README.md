# ASP.NET Core Sample Project


The **AspNetCoreExample** sample project demonstrates the process of creating a new route with 10 destinations and writing information about it to the web page. The project is done based on the Route4Me c# SDK (.net core 3.1). 

The project is done in the Visual Studio 2019.

### Project Implementation Steps

1. Run Visual Studio 2019 and create ASP.NET Core Web App project;

2. In the project creation next step check the options:  
   - Configure for HTTPS
   - Enable Docker
   - Select **Linux** from Docker OS

2. Search NuGet for the library **Route4MeSDKLibrary** and install it in the created project;

3. Add to the project the file **RunExamples.cs** with content:

```
using System;
using System.Collections.Generic;
using System.Text;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace TestRoute4MeSharpSDKCore
{
    public class RunExamples
    {
        public string c_ApiKey { get; set; }

        /// <summary>
        /// The example demonstrates the process of creating a new route with 10 destinations.
        /// </summary>
        public DataObject SingleDriverRoute10Stops(out string errorString)
        {
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
            parameters.Optimize = Optimize.Distance.Description();
            parameters.DistanceUnit = DistanceUnit.MI.Description();
            parameters.DeviceType = DeviceType.Web.Description();

            OptimizationParameters optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            DataObject dataObject = route4Me.RunOptimization(optimizationParameters, out errorString);

            return dataObject;
        }
    }
}
```

4. Modify the method **Configure** in the **Startup.cs** to:

```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
	if (env.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();
	}

	app.UseRouting();

	app.UseEndpoints(endpoints =>
	{
		endpoints.MapGet("/", async context =>
		{
			RunExamples runExamples = new RunExamples();

			// Please, change the demo API key with your own API key
			runExamples.c_ApiKey = "11111111111111111111111111111111";

			var dataObject = runExamples.SingleDriverRoute10Stops(out string errorString);

			string outputMessage = (dataObject != null && dataObject.GetType() == typeof(DataObject))
					? $"Created the optimization with ID={dataObject.OptimizationProblemId} and route name = {dataObject.Parameters.RouteName}"
					: $"Cannot create the optimization - change input data and try again{Environment.NewLine} {errorString}";

			await context.Response.WriteAsync(outputMessage);
		});
	});
}
```

5. Run the project. You can expect to get generated web page (e.g. https://localhost:44389/) with the info in it about created route with the optimized sequence of the destinations.

### Project Dockerization

In order to dockerize the project, to try it in the Linux enironment and run it as a container do following steps:

1. Make sure you have installed Docker Desktop WSL 2;

2. Write a Docker file in the solution core folder with the content:
```
# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
EXPOSE 80
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AspNetCoreExample.dll"]
```

3. Add typical .dockerignore file to the solution folder;

4. Create the Docker image by the CMD (or Shell) command (or make appopriate batch file and run it):
```
docker build -t aspnetapp  .
```

5. Run the Docker container by the CMD (or Shell) command (or make appopriate batch file and run it):
```
docker run -it --rm -p 8080:80 --name AspNetCoreExample aspnetapp
```

6. Open a browser and enter in the address field:
```
https://localhost:8080
```

7. You can expect to see information about created route on the web page.

