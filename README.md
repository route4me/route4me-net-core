# Route4Me Route Optimization C# SDK (.net core framework)

### What does Route4Me do?
In layman's terms Route4Me solves complex variations of the [traveling salesman problem](https://blog.route4me.com/traveling-salesman-problem/) and the [vehicle routing problem](https://blog.route4me.com/vehicle-routing-problems-real-life-solutions/). In more technical terms, Route4Me solves HP-hard logistics problems that span several mathematical and scientific disciplines such as industrial engineering, operations research, algorithmic graph theory, computational geometry, combinatorial optimization, fleet management, field service management, [telematics](https://telematics.route4me.com/).

### What does the Route4Me SDK permit me to do?
This SDK makes it easier for you use the Route4Me API. The API has many features, including [route optimization](https://route4me.io/),  and the primary features are related to creating orders and then [creating optimally sequenced driving routes](https://route4me.com) for many drivers.

### Who can use the Route4Me SDK (and API)?
The service is typically used by organizations who must route many drivers to many destinations. In addition to route optimization for new (future) routes, the API can also be used to analyze historical routes, and to distribute routes to field personnel.

### Who is prohibited from using the Route4Me SDK (and API)?
The Route4Me SDK and API cannot be resold or used in a product or system that competes directly with Route4Me. This means that developers cannot resell route optimization services to other businesses or developers. However, developers can integrate our route optimization SDK/API into their software applications. Developers and startups are also permitted to use our software for internal purposes (i.e. a same day delivery startup).


### How does the API/SDK Integration Work?
A Route4Me customer, integrator, or partner incorporates the Route4Me SDK or API into their code base.
Route4Me permits any paying subscriber to interact with every part of its system using it’s API.
The API is RESTful, which means that it’s web based and can be accessed by other programs and machines
The API/SDK should be used to automate the route planning process, or to generate many routes with minimal manual intervention

### Do optimized routes automatically appear inside my Route4Me account?
Every Route4Me SDK instance needs a unique API key. The API key can be retrieved inside your Route4Me.com account, inside the Settings tab called API. When a route is planned, it appears inside the corresponding Route4Me account. Because Route4Me web and mobile accounts are synchronized, the routes will appear in both environments at the same time.

### Can I test the SDK with other addresses without a valid API Key?
No. The sample API key only permits you to optimize routes with the sample address coordinates that are part of this SDK.

### Does the SDK have rate limits?
The number of requests you can make per second is limited by your current subscription plan. Typically, there are different rate limits for these core features:
Address Geocoding & Address Reverse Geocoding
Route Optimization & Management
Viewing a Route

### What is the recommended integration architecture for the Route4Me SDK?
There are two typical integration strategies that we recommend.  Using this SDK, you can make optimization requests and then the SDK polls the Route4Me API to detect state changes as the optimization progresses. Alternatively, you can provide a webhook/callback url, and the API will notify that callback URL every time there is a state change.

### I don't need route management or mobile capabilities. Can I just use the route planning and route optimization API?
There are no additional costs to use the web interface or the mobile application to view your [optimized routes](https://support.route4me.com/route-planner-routes-list/), which mean that you can use only the API without paying extra
for our web app or mobile app.

### How fast is the route Route4Me Optimization Web Service?
Most routes having less than 200 destinations are optimized in 1 second or less. Larger routes having thousands of stops are split into their 
most reasonably optimal geographical regions, and then each of those regions is independently optimized in parallel. Whether you use polling or push,
you will be able to retrieve all the routes optimized that were created from a large optimization problem set.

### Can I disable optimization when planning routes?
Yes. You can send routes with optimization disabled if you want to conveniently see them on a map, or distribute them to your drivers in the order you prefer.

### Can the API be used for aerial vehicles such as drones or self-driving cars?
Yes. The API can accept lat/lng and an unlimited amount metadata per destination (e.g. altitude, weight, pieces, cubic dimension). The metadata will be preserved as passthrough data by our API, so that the receiving service or device will have access to critical data when our API invokes a webhook callback to the device.

### Are all my optimized routes stored permanently stored in the Route4Me database?
Yes. Unless your contract specificies otherwise, all routes are permanently stored in the database and are no longer accessible to you after your subscription is terminated. Route4Me auto-prunes route data
based on your subscription plan, with more expensive plans typically permitting a longer archival period.


### Can I incorporate your API into my mobile application that requires routing, navigation, or route planning?
Route4Me's route planning and optimization technology can only be added into applications that do not directly compete with Route4Me. 
This means the application’s primary capabilities must be unrelated to route optimization, route planning, or navigation.

### Can I pay you to develop a custom algorithm?
Yes

### Can I use your API and resell it to my customers?
White-labeling and private-labeling Route4Me is possible but the deal's licensing terms vary considerably based on customer count, route count, and the level of support that Route4Me should provide to your customers.

### Does the API/SDK have TMS or EDI, or EDI translator capabilities?
Yes

### Can the API/SDK send notifications back to our system using callbacks, notifications, pushes, or webhooks?

Because Route4Me processes all routes asynchronously, Route4Me will immediately notify the endpoint you specify as a route optimization job progresses through each state of the optimization. Every stage of the route optimization process has a unique stage id.

### Does the Route4Me API and SDK work in my country?
Route4Me.com, as well as all of Route4Me’s mobile applications use the Route4Me SDK’s and API.
Since Route4Me works globally, this means that all of Route4Me’s capabilities are available using the SDK’s in almost every country that has well digitized maps.


### Will the Route4Me API/SDK work in my program on Linux/*Nix/Kubernetes/Docker, Windows, Mac?
Customers are encouraged to select their preferred operating system environment. The Route4Me API/SDK will function on any operating system that supports the preferred programming language of the customer. At this point in time, almost every supported SDK can run on any operating system.


### Does the Route4Me API/SDK require me to buy my own servers?
Route4Me has its own computing infrastructure that you can access using the API and SDKs. Customers typically have to run the SDK code on their own computers and/or servers to access this infrastructure.

### Does Route4Me have an on-premise solution?
Route4Me does not currently lease or sell servers, and does not have on-premise appliance solution. This would only be possible in exceptionally unique scenarios.


### Does the Route4Me API/SDK require me to have my own programmers?
The time required to integrate the SDK can be as little as 1 hour or may take several weeks, depending on the number of features being incorporated into the customer’s application and how much integration testing will be done by the client. A programmer’s involvement is almost always required to use Route4Me’s technology when accessing it through the API.


### Examples
Route4Me examples can be found [here](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKTest/)

### Installation instuctions options:

#### NuGet package using Visual Studio (Manage NuGet packages)
1. Open your project in Visual Studio.
2. Right-click on your project in the Solution Explorer and choose "Manage NuGet Packages..."
3. In the NuGet Package Manager, search for the name of the package (Route4MeSDKLibrary) in the "Browse" tab.
4. Select the package and click "Install".

#### NuGet package using .NET CLI: 
1. Open a command prompt.
2. Navigate to your project directory.
3. Run the following command
```shell
dotnet add package Route4MeSDKLibrary
```

####  NuGet package using Visual Studio (Package Manager Console)
1. Go to Tools -> NuGet Package Manager -> Package Manager Console.
2. In the console, type the following command and press Enter:
```shell
Install-Package Route4MeSDKLibrary -ProjectName YourProjectName
```
Replace YourProjectName with the name of your project.

#### NuGet package using csproj directly
1. Open your .csproj file in a text editor.
2. Inside the <ItemGroup> tag, add the following line:
```xml
<PackageReference Include="Route4MeSDKLibrary" Version="YourPackageVersion" />
```
Replace YourPackageName with the name of the package and YourPackageVersion with the version number of the package.

3. Save and close the .csproj file.
4. Open a command prompt, navigate to your project directory and run the following command to restore the package:
```shell
dotnet restore
```

#### From GitHub releases 
1. Navigate to the project's GitHub page.
2. Click on the "Releases" tab.
3. Download the required release.
4. Extract the downloaded file.
5. In your project in Visual Studio, right-click on the Solution Explorer and choose "Add" -> "Existing Project...".
5. Navigate to the location where you extracted the downloaded file, select the .csproj file and click "Open".