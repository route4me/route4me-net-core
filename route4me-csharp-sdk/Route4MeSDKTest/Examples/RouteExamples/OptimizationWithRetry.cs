using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Robust optimization example with 100 stops, retry logic, and detailed logging.
        /// </summary>
        public void OptimizationWith100StopsAndRetry()
        {
            // 1. Initialize the Manager
            // Note: In a real app, you might inject a logger here. For this console example, we use standard Console output.
            var route4Me = new Route4MeManager(ActualApiKey);

            Console.WriteLine("=== Robust Optimization Example with 100 Stops ===\n");

            // 2. Prepare Data (100 Addresses)
            Address[] addresses = Get100Addresses();

            var parameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = new RouteParameters
                {
                    AlgorithmType = AlgorithmType.CVRP_TW_SD,
                    Parts = 10,
                    RouteName = "Robust Optimization 100 Stops",
                    RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Now.AddDays(1)),
                    RouteTime = 60 * 60 * 7, // 7:00 AM
                    Optimize = "Distance"
                },
                Redirect = false
            };

            Console.WriteLine($"✓ Prepared {addresses.Length} addresses.");
            Console.WriteLine($"✓ Route Name: {parameters.Parameters.RouteName}");

            // 3. Submit Optimization with Retry Logic
            Console.WriteLine("\nSubmitting optimization request...");
            var stopwatch = Stopwatch.StartNew();

            DataObject dataObject = route4Me.RunOptimizationWithRetry(
                parameters,
                out Route4MeSDK.DataTypes.V5.ResultResponse failureResponse,
                maxRetries: 3
            );

            stopwatch.Stop();

            // 4. Handle Submission Results
            if (dataObject == null)
            {
                Console.WriteLine("\n✗ Optimization submission FAILED.");
                if (failureResponse != null && failureResponse.Messages != null)
                {
                    Console.WriteLine("  Error Details:");
                    foreach (var kvp in failureResponse.Messages)
                    {
                        Console.WriteLine($"    {kvp.Key}: {string.Join(", ", kvp.Value)}");
                    }
                }
                else
                {
                    Console.WriteLine("  Unknown error (Network issue or Infrastructure failure).");
                }
                return;
            }

            // 5. Success - Monitor Progress
            string optimizationId = dataObject.OptimizationProblemId;
            Console.WriteLine($"\n✓ Submission Successful! (Latency: {stopwatch.ElapsedMilliseconds}ms)");
            Console.WriteLine($"  Optimization ID: {optimizationId}");
            Console.WriteLine($"  Initial State: {dataObject.State}");

            // Poll until optimized
            if (dataObject.State != OptimizationState.Optimized)
            {
                Console.WriteLine("\nPolling for completion...");
                stopwatch.Restart();

                bool isOptimized = false;
                int maxPolls = 60; // Wait up to 60 seconds (usually takes <10s for 100 stops)

                for (int i = 0; i < maxPolls; i++)
                {
                    Thread.Sleep(1000); // Poll every second

                    var result = route4Me.GetOptimization(
                        new OptimizationParameters { OptimizationProblemID = optimizationId },
                        out string errorString
                    );

                    if (result != null)
                    {
                        Console.Write($"\r  Poll #{i + 1}: State = {result.State}      ");

                        if (result.State == OptimizationState.Optimized) // State 4
                        {
                            dataObject = result;
                            isOptimized = true;
                            break;

                        }
                        else if (result.State == OptimizationState.Error) // State 5
                        {
                            Console.WriteLine("\n\n✗ Optimization failed during processing (State: Error).");
                            return;
                        }
                        else if (result.State == OptimizationState.NoSolution) // State 8
                        {
                            Console.WriteLine("\n\n✗ Optimization failed: No Solution (State: NoSolution).");
                            return;
                        }
                    }
                }

                stopwatch.Stop();
                if (!isOptimized)
                {
                    Console.WriteLine("\n\n⚠ Timed out waiting for optimization to complete.");
                    return;
                }
                Console.WriteLine($"\n✓ Optimization Completed! (Processing Time: {stopwatch.Elapsed.TotalSeconds:F2}s)");
            }

            // 6. Log Results
            if (dataObject.Routes != null && dataObject.Routes.Length > 0)
            {
                Console.WriteLine($"\nRoutes Created: {dataObject.Routes.Length}");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine($"{"Route ID",-36} | {"Stops",-6} | {"Distance",-10} | {"Duration",-10}");
                Console.WriteLine(new string('-', 80));

                foreach (var route in dataObject.Routes)
                {
                    double dist = route.TripDistance ?? 0;
                    long dur = route.PlannedTotalRouteDuration ?? 0;

                    Console.WriteLine($"{route.RouteId,-36} | {route.Addresses.Length,-6} | {dist:F2}mi    | {FormatDuration(dur),-10}");
                }
                Console.WriteLine(new string('-', 80));
            }
            else
            {
                Console.WriteLine("\n⚠ Optimization finished but no routes were returned.");
            }
        }

        private string FormatDuration(long seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return $"{t.Hours:D2}h:{t.Minutes:D2}m";
        }

        private Address[] Get100Addresses()
        {
            return new Address[]
            {
                new Address { AddressString = "449 Schiller st Elizabeth, NJ, 07206", Alias = "Depot", IsDepot = true, Latitude = 40.6608211, Longitude = -74.1827578 },
                new Address { AddressString = "24 Convenience Store LLC, 2519 Pacific Ave, Atlantic City, NJ, 08401", Alias = "24 Convenience Store LLC", IsDepot = false, Latitude = 39.355035, Longitude = -74.441433 },
                new Address { AddressString = "24/7, 1406 Atlantic Ave, Atlantic City, NJ, 08401", Alias = "24/7", IsDepot = false, Latitude = 39.361713, Longitude = -74.428145 },
                new Address { AddressString = "6-12 Convienece, 1 South Main Street, Marlboro, NJ, 07746", Alias = "6-12 Convienece", IsDepot = false, Latitude = 40.314719, Longitude = -74.248578 },
                new Address { AddressString = "6Th Ave Groc, 214 6th Ave, Newark, NJ, 07102", Alias = "6Th Ave Groc", IsDepot = false, Latitude = 40.756385, Longitude = -74.187419 },
                new Address { AddressString = "76 Express Mart, 46 Ryan Rd, Manalapan, NJ, 07726", Alias = "76 Express Mart", IsDepot = false, Latitude = 40.301426, Longitude = -74.259929 },
                new Address { AddressString = "801 W Groc, 801 N Indiana, Atlantic City, NJ, 08401", Alias = "801 W Groc", IsDepot = false, Latitude = 39.368782, Longitude = -74.438739 },
                new Address { AddressString = "91 Farmers Market, 34 Lanes Mill Road, Bricktown, NJ, 08724", Alias = "91 Farmers Market", IsDepot = false, Latitude = 40.095338, Longitude = -74.144739 },
                new Address { AddressString = "A&L Mini, 103 Central Ave, Newark, NJ, 07103", Alias = "A&L Mini", IsDepot = false, Latitude = 40.763848, Longitude = -74.228196 },
                new Address { AddressString = "AC Deli & Food Market, 3104 Pacific Ave, Atlantic City, NJ, 08401", Alias = "AC Deli & Food Market", IsDepot = false, Latitude = 39.351864, Longitude = -74.448293 },
                new Address { AddressString = "AC Food Market & Deli 2, 2401 Pacific Ave, Atlantic City, NJ, 08401", Alias = "AC Food Market & Deli 2", IsDepot = false, Latitude = 39.357207, Longitude = -74.440922 },
                new Address { AddressString = "Ag Mini, 503 Gregory Ave, Passaic, NJ, 07055", Alias = "Ag Mini", IsDepot = false, Latitude = 40.864225, Longitude = -74.139027 },
                new Address { AddressString = "Alexca Groc, 525 Elizabeth Ave, Newark, NJ, 07108", Alias = "Alexca Groc", IsDepot = false, Latitude = 40.708466, Longitude = -74.201882 },
                new Address { AddressString = "Alpha And Omega, 404 Oriental Ave, Atlantic City, NJ, 08401", Alias = "Alpha And Omega", IsDepot = false, Latitude = 39.36423, Longitude = -74.414019 },
                new Address { AddressString = "Always at Home Adult Day care, 8a Jocama Blvd, OldBridge, NJ, 08857", Alias = "Always at Home Adult Day care", IsDepot = false, Latitude = 40.37812, Longitude = -74.305547 },
                new Address { AddressString = "AM PM, 1338 Atlantic Ave, Atlantic City, NJ, 08401", Alias = "AM PM", IsDepot = false, Latitude = 39.362037, Longitude = -74.427806 },
                new Address { AddressString = "Amaury Groc, 84 North Walnut, East Orange, NJ, 07021", Alias = "Amaury Groc", IsDepot = false, Latitude = 40.76518, Longitude = -74.211008 },
                new Address { AddressString = "American Way Food, 2005 Route 35 North, Oakhurst, NJ, 07755", Alias = "American Way Food", IsDepot = false, Latitude = 40.263924, Longitude = -74.040861 },
                new Address { AddressString = "Amezquita, 126 Gouvernor St, Paterson, NJ, 07524", Alias = "Amezquita", IsDepot = false, Latitude = 40.922167, Longitude = -74.163824 },
                new Address { AddressString = "Anita's Corner Deli, 664 Brace Avenue, Perth Amboy, NJ, 08861", Alias = "Anita's Corner Deli", IsDepot = false, Latitude = 40.524289, Longitude = -74.287035 },
                new Address { AddressString = "Anthony's Pizza, 65 Church Street, Keansburg, NJ, 07734", Alias = "Anthony's Pizza", IsDepot = false, Latitude = 40.441791, Longitude = -74.133082 },
                new Address { AddressString = "Antonia's Café, 47 3rd Avenue, Long Branch, NJ, 07740", Alias = "Antonia's Café", IsDepot = false, Latitude = 40.302707, Longitude = -73.987299 },
                new Address { AddressString = "AP Grocery, 534 Broadway, Elmwood Park, NJ, 07407", Alias = "AP Grocery", IsDepot = false, Latitude = 40.918104, Longitude = -74.151194 },
                new Address { AddressString = "Ashley Groc, 506 Clinton St, Newark, NJ, 07108", Alias = "Ashley Groc", IsDepot = false, Latitude = 40.721587, Longitude = -74.201352 },
                new Address { AddressString = "Atlantic Bagel Co, 113 E River Road, Rumson, NJ, 07760", Alias = "Atlantic Bagel Co 1", IsDepot = false, Latitude = 40.371677, Longitude = -73.999631 },
                new Address { AddressString = "Atlantic Bagel Co, 283 Route 35, Middletown, NJ, 07701", Alias = "Atlantic Bagel Co 2", IsDepot = false, Latitude = 40.366843, Longitude = -74.08326 },
                new Address { AddressString = "Atlantic Bagel Co, 642 Newman spring Rd, Lincroft, NJ, 07738", Alias = "Atlantic Bagel Co 3", IsDepot = false, Latitude = 40.366843, Longitude = -74.08326 },
                new Address { AddressString = "Atlantic Bagel Co, 74 1st Avenue, Atlantic Highlands, NJ, 07732", Alias = "Atlantic Bagel Co 4", IsDepot = false, Latitude = 40.4138, Longitude = -74.037514 },
                new Address { AddressString = "Atlantic City Fuel, 864 N Main St, Pleasantville, NJ, 08232", Alias = "Atlantic City Fuel", IsDepot = false, Latitude = 39.403741, Longitude = -74.511303 },
                new Address { AddressString = "Atlantic City Gas, 8006 Black Horse Pike, Pleasantville, NJ, 08232", Alias = "Atlantic City Gas", IsDepot = false, Latitude = 39.380853, Longitude = -74.495093 },
                new Address { AddressString = "Awan Convience, 3701 Vetnor Ave, Atlantic City, NJ, 08401", Alias = "Awan Convience", IsDepot = false, Latitude = 39.351437, Longitude = -74.455519 },
                new Address { AddressString = "Babes Corner, 132 Sumner Avenue, Seaside Heights, NJ, 08751", Alias = "Babes Corner", IsDepot = false, Latitude = 39.941312, Longitude = -74.074916 },
                new Address { AddressString = "Bagel Mania, 347 Matawan Rd, Lawrence Harbor, NJ, 08879", Alias = "Bagel Mania", IsDepot = false, Latitude = 40.430159, Longitude = -74.251723 },
                new Address { AddressString = "Bagel One, 700 Old Bridge Tpke, South River, NJ, 08882", Alias = "Bagel One 1", IsDepot = false, Latitude = 40.462466, Longitude = -74.402633 },
                new Address { AddressString = "Bagel One, 777 Washington Road, Parlin, NJ, 08859", Alias = "Bagel One 2", IsDepot = false, Latitude = 40.462783, Longitude = -74.327999 },
                new Address { AddressString = "Bagel Station, 168 Monmouth St, Red Bank, NJ, 07721", Alias = "Bagel Station", IsDepot = false, Latitude = 40.348985, Longitude = -74.073624 },
                new Address { AddressString = "Barry Mini Mart, 498 12th st, Paterson, NJ, 07504", Alias = "Barry Mini Mart", IsDepot = false, Latitude = 40.91279, Longitude = -74.138676 },
                new Address { AddressString = "Best Tropical Grocery 2, 284 Verona Ave, Passaic, NJ, 07055", Alias = "Best Tropical Grocery 2", IsDepot = false, Latitude = 40.782701, Longitude = -74.166163 },
                new Address { AddressString = "Bevaquas, 305 Port Monmouth Rd, Middleton, NJ, 07748", Alias = "Bevaquas", IsDepot = false, Latitude = 40.442036, Longitude = -74.116429 },
                new Address { AddressString = "Bhavani, 1050 Route 9 South, Old Bridge, NJ, 08859", Alias = "Bhavani", IsDepot = false, Latitude = 40.452799, Longitude = -74.299858 },
                new Address { AddressString = "Big Hamilton Grocery, 117 Hamilton Avenue, Paterson, NJ, 07514", Alias = "Big Hamilton Grocery", IsDepot = false, Latitude = 40.920487, Longitude = -74.166298 },
                new Address { AddressString = "BP Gas Station, 409 Rt 46 West, Newark, NJ, 07104", Alias = "BP Gas Station 1", IsDepot = false, Latitude = 40.893342, Longitude = -74.107102 },
                new Address { AddressString = "BP Gas Station, 780 Market St, Newark, NJ, 07112", Alias = "BP Gas Station 2", IsDepot = false, Latitude = 40.905749, Longitude = -74.147813 },
                new Address { AddressString = "Bray Ave Deli, 190 Bray Ave, Middletown, NJ, 07748", Alias = "Bray Ave Deli", IsDepot = false, Latitude = 40.436711, Longitude = -74.111739 },
                new Address { AddressString = "Brennans Deli, 4 W River Rd, Rumson, NJ, 07760", Alias = "Brennans Deli", IsDepot = false, Latitude = 40.374892, Longitude = -74.013428 },
                new Address { AddressString = "Brick Convenience, 438 Mantoloking Road, Brick, NJ, 08723", Alias = "Brick Convenience", IsDepot = false, Latitude = 40.045475, Longitude = -74.094392 },
                new Address { AddressString = "Brothers Produce, 327 East Railway Ave, Passaic, NJ, 07055", Alias = "Brothers Produce", IsDepot = false, Latitude = 40.891322, Longitude = -74.149694 },
                new Address { AddressString = "Brown Bag Convience, 297 Spotswood Englishtown Rd, Monroe, NJ, 08831", Alias = "Brown Bag Convience", IsDepot = false, Latitude = 40.380837, Longitude = -74.388253 },
                new Address { AddressString = "Butler Food Store, 109 Easton Avenue, New Brunswick, NJ, 08901", Alias = "Butler Food Store", IsDepot = false, Latitude = 40.499122, Longitude = -74.451908 },
                new Address { AddressString = "Café Columbia, 495 Mcbride Ave, Irvington, NJ, 07111", Alias = "Café Columbia", IsDepot = false, Latitude = 40.734721, Longitude = -74.223831 },
                new Address { AddressString = "Café Sical, 56 Obert Street, South River, NJ, 08882", Alias = "Café Sical", IsDepot = false, Latitude = 40.45067, Longitude = -74.380567 },
                new Address { AddressString = "Calis General Str, 2701 Atlantic Ave, Atlantic City, NJ, 08401", Alias = "Calis General Str", IsDepot = false, Latitude = 39.35569, Longitude = -74.444721 },
                new Address { AddressString = "Carolyn Park Ave Groc, 76 Park Ave, Hackensack, NJ, 07601", Alias = "Carolyn Park Ave Groc", IsDepot = false, Latitude = 40.888972, Longitude = -74.045214 },
                new Address { AddressString = "Cavo Crepe, 122 North Michigan Avenue, Atlantic City, NJ, 08401", Alias = "Cavo Crepe", IsDepot = false, Latitude = 39.361182, Longitude = -74.437285 },
                new Address { AddressString = "Ccs - New Vista, 300 Broadway, Cedar Grove, NJ, 07009", Alias = "Ccs - New Vista", IsDepot = false, Latitude = 40.76121, Longitude = -74.169224 },
                new Address { AddressString = "Ccs- Fountainview, 527 River Avenue, Lakewood, NJ, 08701", Alias = "Ccs- Fountainview", IsDepot = false, Latitude = 40.074549, Longitude = -74.215903 },
                new Address { AddressString = "Ccs-Tallwoods, 18 Butler Blvd, Bayville, NJ, 08721", Alias = "Ccs-Tallwoods", IsDepot = false, Latitude = 39.887461, Longitude = -74.156648 },
                new Address { AddressString = "Cedar 15, 501 Atlantic Ave, Atlantic City, NJ, 08401", Alias = "Cedar 15", IsDepot = false, Latitude = 39.368863, Longitude = -74.416528 },
                new Address { AddressString = "Cedar Meat Market, 6407 Vetnor Avenue, Vetnor, NJ, 08406", Alias = "Cedar Meat Market", IsDepot = false, Latitude = 39.338153, Longitude = -74.482597 },
                new Address { AddressString = "Center City Deli, 1714 Atlantic Ave, Atlantic City, NJ, 08401", Alias = "Center City Deli", IsDepot = false, Latitude = 39.360264, Longitude = -74.432264 },
                new Address { AddressString = "Charlie'S Deli, 164 Port Monmouth, Keansburg, NJ, 07734", Alias = "Charlie'S Deli", IsDepot = false, Latitude = 40.441981, Longitude = -74.12276 },
                new Address { AddressString = "Chikeeza, 1305 Baltic Ave, Atlantic City, NJ, 08401", Alias = "Chikeeza", IsDepot = false, Latitude = 39.365509, Longitude = -74.429001 },
                new Address { AddressString = "Choice Food, 182 Route 35 North, Keyport, NJ, 07735", Alias = "Choice Food", IsDepot = false, Latitude = 40.449313, Longitude = -74.236787 },
                new Address { AddressString = "Circle K, 102 Bay Avenue, Highlands, NJ, 07732", Alias = "Circle K 1", IsDepot = false, Latitude = 40.400419, Longitude = -73.984715 },
                new Address { AddressString = "Circle K, 2001 Ridgeway Road, Toms River, NJ, 08757", Alias = "Circle K 2", IsDepot = false, Latitude = 40.006828, Longitude = -74.242188 },
                new Address { AddressString = "Circle K, 220 Oceangate Drive, Bayville, NJ, 08721", Alias = "Circle K 3", IsDepot = false, Latitude = 39.916714, Longitude = -74.15386 },
                new Address { AddressString = "Citgi Come and Go, 519 Route 33, Millstone, NJ, 08535", Alias = "Citgi Come and Go", IsDepot = false, Latitude = 40.260143, Longitude = -74.409921 },
                new Address { AddressString = "Citgo Gas Station, 473 Broadway, Paterson, NJ, 07501", Alias = "Citgo Gas Station", IsDepot = false, Latitude = 40.918597, Longitude = -74.154093 },
                new Address { AddressString = "Citrus Rest, 305 Main St, W Paterson, NJ, 07424", Alias = "Citrus Rest", IsDepot = false, Latitude = 40.887559, Longitude = -74.041441 },
                new Address { AddressString = "City Farm, 294 North 8th St, Paterson, NJ, 07501", Alias = "City Farm", IsDepot = false, Latitude = 40.933369, Longitude = -74.172208 },
                new Address { AddressString = "City Mkt, 26 S Main St, Pleasantville, NJ, 08232", Alias = "City Mkt", IsDepot = false, Latitude = 39.391235, Longitude = -74.522571 },
                new Address { AddressString = "Clinton News, 31 Clinton Street, Passaic, NJ, 07055", Alias = "Clinton News", IsDepot = false, Latitude = 40.735982, Longitude = -74.169955 },
                new Address { AddressString = "Collins Convience, 201 E Collins Ave, Galloway, NJ, 08025", Alias = "Collins Convience", IsDepot = false, Latitude = 39.491728, Longitude = -74.503715 },
                new Address { AddressString = "Community Deli, 546 Market St, East Orange, NJ, 07042", Alias = "Community Deli", IsDepot = false, Latitude = 40.911747, Longitude = -74.155516 },
                new Address { AddressString = "Convenience Corner, 355 Monmouth Road, West Long Branch, NJ, 07764", Alias = "Convenience Corner", IsDepot = false, Latitude = 40.2842, Longitude = -74.02012 },
                new Address { AddressString = "Correita El Paisa, 326 21st Ave, Paterson, NJ, 07514", Alias = "Correita El Paisa", IsDepot = false, Latitude = 40.906704, Longitude = -74.158671 },
                new Address { AddressString = "Cositas Ricas, 535 21st Ave, Paterson, NJ, 07504", Alias = "Cositas Ricas", IsDepot = false, Latitude = 40.90601, Longitude = -74.150362 },
                new Address { AddressString = "Country Farm, 1320 Seagirt Avenue, Seagirt, NJ, 08750", Alias = "Country Farm", IsDepot = false, Latitude = 40.135683, Longitude = -74.062333 },
                new Address { AddressString = "Country Farms, 125 Main Street, Bradley Beach, NJ, 07720", Alias = "Country Farms 1", IsDepot = false, Latitude = 40.200035, Longitude = -74.019095 },
                new Address { AddressString = "Country Farms, 2588 Tilton Rd, Egg Harbor, NJ, 08234", Alias = "Country Farms 2", IsDepot = false, Latitude = 39.416868, Longitude = -74.569141 },
                new Address { AddressString = "Country Farms, 3122 Route 88, Point Pleasant, NJ, 08742", Alias = "Country Farms 3", IsDepot = false, Latitude = 40.079909, Longitude = -74.083889 },
                new Address { AddressString = "Country Farms, 892 Fisher Blvd, Toms River, NJ, 08753", Alias = "Country Farms 4", IsDepot = false, Latitude = 39.973935, Longitude = -74.137087 },
                new Address { AddressString = "Country Food Market, 921 Atlantic City Blvd, Bayville, NJ, 08721", Alias = "Country Food Market", IsDepot = false, Latitude = 39.882705, Longitude = -74.164435 },
                new Address { AddressString = "Country Store Raceway, 454 Rt 33 West, Millstone, NJ, 07726", Alias = "Country Store Raceway", IsDepot = false, Latitude = 40.258843, Longitude = -74.398019 },
                new Address { AddressString = "Crossroads Deli, 479 Route 79, Morganville, NJ, 07751", Alias = "Crossroads Deli", IsDepot = false, Latitude = 40.383938, Longitude = -74.241525 },
                new Address { AddressString = "Crystal Express Deli, 308 Ernston Road, Parlin, NJ, 08859", Alias = "Crystal Express Deli", IsDepot = false, Latitude = 40.458048, Longitude = -74.305937 },
                new Address { AddressString = "Crystal Kitchen, 1600 1600 Perrinville Rd, Monroe, NJ, 08831", Alias = "Crystal Kitchen", IsDepot = false, Latitude = 40.316134, Longitude = -74.440031 },
                new Address { AddressString = "Deal Food, 112 Norwood Ave, Deal, NJ, 67723", Alias = "Deal Food", IsDepot = false, Latitude = 40.253485, Longitude = -74.000852 },
                new Address { AddressString = "Dehuit Market, 70 Market Street, Passaic, NJ, 07055", Alias = "Dehuit Market", IsDepot = false, Latitude = 40.863711, Longitude = -74.116357 },
                new Address { AddressString = "Delta Gas, 100 Madison Avenue, Lakewood, NJ, 08701", Alias = "Delta Gas", IsDepot = false, Latitude = 40.091107, Longitude = -74.216751 },
                new Address { AddressString = "Demarcos, 3809 Crossan Ave, Atlantic City, NJ, 08401", Alias = "Demarcos", IsDepot = false, Latitude = 39.358413, Longitude = -74.462155 },
                new Address { AddressString = "Dios Fe Ezperanza, 548 Market St, Orange, NJ, 07050", Alias = "Dios Fe Ezperanza", IsDepot = false, Latitude = 40.768005, Longitude = -74.232605 },
                new Address { AddressString = "Dollar Variety, 292 Main St, Paterson, NJ, 07502", Alias = "Dollar Variety", IsDepot = false, Latitude = 40.915152, Longitude = -74.173859 },
                new Address { AddressString = "Doms Cherry Street Deli, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701", Alias = "Doms Cherry Street Deli", IsDepot = false, Latitude = 40.332559, Longitude = -74.074423 },
                new Address { AddressString = "Doniele Lotz, 206-220 First Ave, Asbury Park, NJ, 07712", Alias = "Doniele Lotz", IsDepot = false, Latitude = 40.219227, Longitude = -74.003708 },
                new Address { AddressString = "Dover Market, 3920 Vetnor Avenue, Atlantic City, NJ, 08401", Alias = "Dover Market", IsDepot = false, Latitude = 39.349847, Longitude = -74.457832 },
                new Address { AddressString = "Dunkin Donuts 117-02, 545 Chancellor Ave, Paterson, NJ, 07513", Alias = "Dunkin Donuts 117-02", IsDepot = false, Latitude = 40.713875, Longitude = -74.229677 },
                new Address { AddressString = "El Apache, 44 East Front Street, Keyport, NJ, 07735", Alias = "El Apache", IsDepot = false, Latitude = 40.438094, Longitude = -74.199867 },
                new Address { AddressString = "El Bohio, 510 Park Ave, Paterson, NJ, 07504", Alias = "El Bohio", IsDepot = false, Latitude = 40.913352, Longitude = -74.143493 },
                new Address { AddressString = "El Colmado Supermarket, 126 Hope Street, Passaic, NJ, 07055", Alias = "El Colmado Supermarket", IsDepot = false, Latitude = 40.867712, Longitude = -74.122705 },
                new Address { AddressString = "El Paisa, 471 21st Ave, Irvington, NJ, 07111", Alias = "El Paisa", IsDepot = false, Latitude = 40.906332, Longitude = -74.153318 },
                new Address { AddressString = "El Poblano Deli & Grocery, 1241 Lakewood Rd, Toms River, NJ, 08753", Alias = "El Poblano Deli & Grocery", IsDepot = false, Latitude = 39.985037, Longitude = -74.20969 },
                new Address { AddressString = "El Triangulo, 156 Hawthorne Ave, Paterson, NJ, 07514", Alias = "El Triangulo", IsDepot = false, Latitude = 40.949274, Longitude = -74.149605 },
                new Address { AddressString = "Eliany Groc, 146 Sherman Ave, Newark, NJ, 07108", Alias = "Eliany Groc", IsDepot = false, Latitude = 40.720118, Longitude = -74.186768 },
            };
        }
    }
}