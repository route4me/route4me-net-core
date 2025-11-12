using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Asynchronous batch geocoding
        /// </summary>
        /// <param name="geoParams">Geocoding parameters</param>
        public void BatchGeocodingForward332Async()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = @"1771 15th Street, Mobile, AL 35243
5100 N 9Th Avenue, Pensacola, FL 35243
3436 Bel Air Mall, Mobile, AL 35243
2600 BEACH BOULEVARD, BILOXI, MS 35243
900 Commons Drive, Dothan, AL 35243
4225 Legendary Drive, Destin, FL 35243
15600 Starfish Street, Panama City Beach, FL 35243
1654 AIRPORT BLVD UNIT 140, PENSACOLA, FL 35243
99 ELGIN PKWY NE STE 1A, FORT WALTON BCH, FL 35243
SP C-101 4300 LEGENDARY DR., DESTIN, FL 35243
200 BLUEFISH DR, PANAMA CITY BCH, FL 35243
30500 STATE HWY 181, SPANISH FORT, AL 35243
10735 FACTORY SHOPS BLVD, GULFPORT, MS 35243
900 COMMONS DRIVE, DOTHAN, AL 35243
2601 S McKenzie Street, Foley, AL 35243
3201 AIRPORT BLVD, MOBILE, AL 35243
535 W. 23rd Street, Panama City, FL 35243
3869 PROMENADE PARKWAY, DIBERVILLE, MS 35243
4601 Montgomery Hwy, Dothan, AL 35243
4737 US-90, Pace, FL 35243
300 MARY ESTHER BLVD, MARY ESTHER, FL 35243
5100 N 9TH AVE, PENSACOLA, FL 35243
2600 BEACH BLVD, BILOXI, MS 35243
3277 BELAIR MALL, MOBILE, AL 35243
2601 S MCKENZIE STREET, FOLEY, AL 35243
10000 FACTORY SHOP BLVD, GULFPORT, MS 35243
4126 LENGENDARY DRIVE, DESTIN, FL 35243
5100 NORTH 9TH AVE, PENSACOLA, FL 35243
5100 N 9Th Avenue, Pensacola, FL 35243
3436 Bel Air Mall, Mobile, AL 35243
2600 BEACH BOULEVARD, BILOXI, MS 35243
900 Commons Drive, Dothan, AL 35243
4225 Legendary Drive, Destin, FL 35243
15600 Starfish Street, Panama City Beach, FL 35243
1544 GOVERNORS SQUARE BLVD UNIT 9, TALLAHASSEE, FL 35243
15700 PCB PKWY #220, PANAMA CITY BCH, FL 35243
2528 S MCKENZIE ST, FOLEY, AL 35243
3500 ROSS CREEK CIRCLE, DOTHAN, AL 35243
4429 COMMONS DR E, DESTIN, FL 35243
535 HAWKINS AVE, PANAMA CITY, FL 35243
535 HAWKINS AVE, DAPHNE, AL 35243
2601 S McKenzie St, Foley, AL 35243
2601 S McKenzie St, Foley, AL 35243
10562 Emerald Coast Parkway W, Destin, FL 35243
5100 N 9th Ave, Pensacola, FL 35243
5100 N 9th Ave, Pensacola, FL 35243
10562 Emerald Coast Parkway W, Destin, FL 35243
300 Mary Esther Blvd, Mary Esther, FL 35243
3720 Hardy St, Fairhope, AL 35243
2620 CREIGHTON RD.  #501, PENSACOLA, FL 35243
1765 E NINE MILE RD STE 8, PENSACOLA, FL 35243
3755 GULF BREEZE PKWY #B, GULF BREEZE, FL 35243
4791 HWY 90, PACE, FL 35243
6342 Highway 11 N, Panama City, FL 35243
2296 Martin Luther King Jr Blvd, Panama City, FL 35243
6235 N Davis Hwy, Saraland, AL 35243
6235 N Davis Hwy, Crestview, FL 35243
6235 N Davis Hwy, Mobile, AL 35243
34737 Emerald Coast Pkwy, Destin, FL 35243
685 Schillinger Rd S, Mobile, AL 35243
685 Schillinger Rd S, Pascagoula, MS 35243
6548 Caroline St, Foley, AL 35243
621 W 23RD STREET, PANAMA CITY, FL 35243
6342 Highway 11 N, Panama City Beach, FL 35243
3659 airport blvd, Mobile, AL 35243
1030 US-331, DeFuniak Springs, FL 35243
5090 N 9th Ave, Pensacola, FL 35243
550  MARY ESTHER CUT OFF, FT WALTON BEACH, FL 35243
4447 COMMONS DR E #K102, DESTIN, FL 35243
15600 PANAMA CITY BEC PKY, PANAMA CITY, FL 35243
3377 S FERDON BLVD., CRESTVIEW, FL 35243
6342 Highway 11 N, Panama City, FL 35243
1000 E 23rd St, Suite A7-A8, Panama City, FL 35243
603 US-90, Bay St. Louis, MS 35243
603 US-90, Gulfport, MS 35243
603 US-90, Destin, FL 35243
6342 Highway 11 N, Daphne, AL 35243
6342 Highway 11 N, Fairhope, AL 35243
6342 Highway 11 N, Foley, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Santa Rosa Beach, FL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Panama City Beach, FL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Panama City Beach, FL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Santa Rosa Beach, FL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Fairhope, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Foley, AL 35243
6235 N Davis Hwy, Gulf Breeze, FL 35243
685 Schillinger Rd S, Chipley, FL 35243
6548 Caroline St, Fort Walton Beach, FL 35243
1030 US-331, Niceville, FL 35243
1030 US-331, Panama City Beach, FL 35243
603 US-90, Panama City, FL 35243
4358 Old Shell Rd, Mobile, AL 35243
3720 Hardy St, Hattiesburg, MS 35243
6342 Highway 11 N, Santa Rosa Beach, FL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Daphne, AL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Panama City Beach, FL 35243
6342 Highway 11 N, Daphne, AL 35243
6342 Highway 11 N, Fairhope, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Foley, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6235 N Davis Hwy, Gulf Breeze, FL 35243
685 Schillinger Rd S, Chipley, FL 35243
6548 Caroline St, Fort Walton Beach, FL 35243
1030 US-331, Panama City Beach, FL 35243
1030 US-331, Niceville, FL 35243
603 US-90, Panama City, FL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Santa Rosa Beach, FL 35243
6342 Highway 11 N, Panama City, FL 35243
2601 S McKenzie Street, Foley, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Panama City Beach, FL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Foley, AL 35243
6342 Highway 11 N, Fairhope, AL 35243
6342 Highway 11 N, Daphne, AL 35243
6342 Highway 11 N, Mobile, AL 35243
400 N Navy Blvd, Pensacola, FL 35243
6235 N Davis Hwy, Pensacola, FL 35243
6235 N Davis Hwy, Saraland, AL 35243
6235 N Davis Hwy, Crestview, FL 35243
6235 N Davis Hwy, Mobile, AL 35243
2296 Martin Luther King Jr Blvd, Panama City, FL 35243
34737 Emerald Coast Pkwy, Destin, FL 35243
1030 US-331, DeFuniak Springs, FL 35243
3659 airport blvd, Mobile, AL 35243
5090 N 9th Ave, Pensacola, FL 35243
603 US-90, Bay St. Louis, MS 35243
603 US-90, Gulfport, MS 35243
603 US-90, Destin, FL 35243
685 Schillinger Rd S, Mobile, AL 35243
685 Schillinger Rd S, Pascagoula, MS 35243
6548 Caroline St, Milton, FL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Santa Rosa Beach, FL 35243
6342 Highway 11 N, Panama City, FL 35243
6342 Highway 11 N, Panama City Beach, FL 35243
6342 Highway 11 N, Daphne, AL 35243
6342 Highway 11 N, Fairhope, AL 35243
6342 Highway 11 N, Foley, AL 35243
6342 Highway 11 N, Mobile, AL 35243
1325 Commerce Dr, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
400 N Navy Blvd, Pensacola, FL 35243
6235 N Davis Hwy, Pensacola, FL 35243
6548 Caroline St, Milton, FL 35243
6548 Caroline St, Foley, AL 35243
8994 PENSACOLA BLVD, PENSACOLA, FL 35243
6245 N DAVIS HWY S 2, PENSACOLA, FL 35243
WALTON PLAZA, DEFUNIAK SPRINGS, FL 35243
2515 S FERDON BLVD, CRESTVIEW, FL 35243
2560 DOUGLAS HWY, BREWTON, AL 35243
6271 W HWY 90, MILTON, FL 35243
151 NINTH AVENUE, FOLEY, AL 35243
714 MCMEANS AVENUE, BAY MINETTE, AL 35243
151 LINDBERG AVENUE, ATMORE, AL 35243
400 NORTH NAVY BLVD  UNIT 6, PENSACOLA, FL 35243
5100 N. 9th Avenue, Pensacola, FL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Fairhope, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Mobile, AL 35243
6342 Highway 11 N, Foley, AL 35243
6342 Highway 11 N, Daphne, AL 35243
6342 Highway 11 N, Santa Rosa Beach, FL 35243
1544 GOVERNORS SQUARE BLVD UNIT 9, TALLAHASSEE, FL 35243
4429 COMMONS DR E, FOLEY, AL 35243
3500 ROSS CREEK CIRCLE, DOTHAN, AL 35243
99 ELGIN PKWY NE STE 1A, PANAMA CITY, FL 35243
99 ELGIN PKWY NE STE 1A, DAPHNE, AL 35243
SP C-101 4300 LEGENDARY DR., DESTIN, FL 35243
200 BLUEFISH DR, PANAMA CITY BCH, FL 35243
30500 STATE HWY 181, SPANISH FORT, AL 35243
10735 FACTORY SHOPS BLVD, GULFPORT, MS 35243
900 COMMONS DRIVE, DOTHAN, AL 35243
2601 S McKenzie Street, Foley, AL 35243
3201 AIRPORT BLVD, MOBILE, AL 35243
535 W. 23rd Street, Panama City, FL 35243
3869 PROMENADE PARKWAY, DIBERVILLE, MS 35243
4601 Montgomery Hwy, Dothan, AL 35243
4737 US-90, Pace, FL 35243
300 MARY ESTHER BLVD, MARY ESTHER, FL 35243
5100 N 9TH AVE, PENSACOLA, FL 35243
2600 BEACH BLVD, BILOXI, MS 35243
3277 BELAIR MALL, MOBILE, AL 35243
2601 S MCKENZIE STREET, FOLEY, AL 35243
10000 FACTORY SHOP BLVD, GULFPORT, MS 35243
4126 LENGENDARY DRIVE, DESTIN, FL 35243
5100 N 9TH AVE, STE A123B, PENSACOLA, FL 35243
8938 PENSACOLA BLVD, PENSACOLA, FL 35243
4600 MOBILE HWY.  SUITE 4, PENSACOLA, FL 35243
4877 US-90, Pace, FL 35243
2532 S McKenzie St, Foley, AL 35243
6850 US-90, Daphne, AL 35243
3869 Promenade Pkwy, D'Iberville, MS 35243
15224 Crossroads Pkwy, Gulfport, MS 35243
7765 Airport Blvd, Mobile, AL 35243
3250 Airport Blvd, Mobile, AL 35243
6342 Highway 11 N, Gulfport, MS 35243
6342 Highway 11 N, Ocean Springs, MS 35243
6342 Highway 11 N, D'Iberville, MS 35243
6342 Highway 11 N, D'Iberville, MS 35243
6342 Highway 11 N, Ocean Springs, MS 35243
6342 Highway 11 N, Gulfport, MS 35243
6342 Highway 11 N, Gulfport, MS 35243
6342 Highway 11 N, D'Iberville, MS 35243
6342 Highway 11 N, Ocean Springs, MS 35243
1336 Sherman Ave, Gulfport, MS 35243
6342 Highway 11 N, D'Iberville, MS 35243
6342 Highway 11 N, Ocean Springs, MS 35243
6342 Highway 11 N, Gulfport, MS 35243
6342 Highway 11 N, Ocean Springs, MS 35243
6342 Highway 11 N, D'Iberville, MS 35243
6342 Highway 11 N, Gulfport, MS 35243
6342 Highway 11 N, D'Iberville, MS 35243
6342 Highway 11 N, Ocean Springs, MS 35243
1771 15th Street, Mobile, AL 35243
1771 15th Street, Mobile, AL 35243
1771 15th Street, Mobile, AL 35243
1771 15th Street, Mobile, AL 35243
1771 15th Street, Mobile, AL 35243
6342 Highway 11 N, Picayune, MS 35243
1310 Tingle Circle East, Mobile, AL 35243
6342 Highway 11 N, Picayune, MS 35243
1310 Tingle Circle East, Mobile, AL 35243
6342 Highway 11 N, Picayune, MS 35243
6342 Highway 11 N, Picayune, MS 35243
6342 Highway 11 N, Picayune, MS 35243
6342 Highway 11 N, Picayune, MS 35243
10676 Emerald Coast Parkway, Destin, FL 35243
300 Mary Esther Blvd, Mobile, AL 35243
300 Mary Esther Blvd, Mobile, AL 35243
740 SCHILLINGER RD S #A2, MOBILE, AL 35243
3764 AIRPORT BLVD, MOBILE, AL 35243
5300 Halls Mill Rd, Suite B, Mobile, AL 35243
6880 US HWY 90 SUITE C-8, DAPHNE, AL 35243
163 W 9TH AVE STE 163, FOLEY, AL 35243
1097 INDUSTRIAL PKWY #B, SARALAND, AL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Milton, FL 35243
6342 Highway 11 N, Niceville, FL 35243
6342 Highway 11 N, Crestview, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Gulf Breeze, FL 35243
6342 Highway 11 N, Navarre, FL 35243
6342 Highway 11 N, Fort Walton Beach, FL 35243
6342 Highway 11 N, Cantonment, FL 35243
6342 Highway 11 N, Cantonment, FL 35243
6342 Highway 11 N, Cantonment, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Cantonment, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Niceville, FL 35243
6342 Highway 11 N, Fort Walton Beach, FL 35243
6342 Highway 11 N, Navarre, FL 35243
6342 Highway 11 N, Gulf Breeze, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Milton, FL 35243
6342 Highway 11 N, Crestview, FL 35243
6342 Highway 11 N, Niceville, FL 35243
6342 Highway 11 N, Crestview, FL 35243
90 Palm Blvd N, Milton, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Gulf Breeze, FL 35243
6342 Highway 11 N, Navarre, FL 35243
6342 Highway 11 N, Fort Walton Beach, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Niceville, FL 35243
6342 Highway 11 N, Navarre, FL 35243
6342 Highway 11 N, Fort Walton Beach, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Gulf Breeze, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Crestview, FL 35243
6342 Highway 11 N, Milton, FL 35243
6342 Highway 11 N, Cantonment, FL 35243
6342 Highway 11 N, Milton, FL 35243
6342 Highway 11 N, Crestview, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Gulf Breeze, FL 35243
6342 Highway 11 N, Navarre, FL 35243
6342 Highway 11 N, Fort Walton Beach, FL 35243
6342 Highway 11 N, Niceville, FL 35243
6342 Highway 11 N, Cantonment, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Niceville, FL 35243
6342 Highway 11 N, Fort Walton Beach, FL 35243
6342 Highway 11 N, Navarre, FL 35243
6342 Highway 11 N, Gulf Breeze, FL 35243
6342 Highway 11 N, Pensacola, FL 35243
6342 Highway 11 N, Crestview, FL 35243
6342 Highway 11 N, Milton, FL 35243
5100 N 9th Ave, Panama City Beach, FL 35243
10562 Emerald Coast Parkway W, Miramar Beach, FL 35243
10562 Emerald Coast Parkway W, Miramar Beach, FL 35243
5100 N 9th Ave, Destin, FL 35243
542 Hawkins Point Dr, Panama City, FL 35243
5100 N 9th Ave, Destin, FL 35243
5100 N 9th Ave, Panama City Beach, FL 35243
10562 Emerald Coast Parkway W, Miramar Beach, FL 35243",
                ExportFormat = "json"
            };

            //Run the query
            var result = route4Me.BatchGeocodingAsync(geoParams).GetAwaiter().GetResult();


            if (result.Item2.Length < 1 && result.Item1.Length > 120)
            {
                var geocodedFilteredAddresses = FilterGeocodedAddresses(result.Item1);
                Console.WriteLine($"Filtered Geocoded Addresses: {geocodedFilteredAddresses.Count}");
            }
            else
            {
                Console.WriteLine("Cannot geocode the addresses");
            }
        }

        List<GeocodingResponse> FilterGeocodedAddresses(string jsonResult)
        {
            jsonResult = jsonResult.Trim();

            var geocodedAddresses = R4MeUtils.ReadObjectNew<GeocodingResponse[]>(jsonResult);

            if (geocodedAddresses.Length < 1) return null;

            var geocodedAddressesFiltered = new List<GeocodingResponse>()
            {
                geocodedAddresses[0]
            };

            bool previousSkipped = false;

            // Filter the geocoded addresses by similarity to the original address
            for (int i = 1; i < geocodedAddresses.Length; i++)
            {
                if (geocodedAddresses[i].Original == geocodedAddresses[i - 1].Original &&
                    geocodedAddresses[i].Address != geocodedAddresses[i - 1].Address &&
                    !previousSkipped)
                {
                    int similarPevious = ComputeLevenshteinDistance(
                        geocodedAddresses[i - 1].Original,
                        geocodedAddresses[i - 1].Address);

                    int similarCurrent = ComputeLevenshteinDistance(
                        geocodedAddresses[i].Original,
                        geocodedAddresses[i].Address);

                    if (similarPevious > similarCurrent)
                        geocodedAddressesFiltered[geocodedAddressesFiltered.Count - 1] = geocodedAddresses[i];

                    previousSkipped = true;
                    continue;
                }

                ;

                geocodedAddressesFiltered.Add(geocodedAddresses[i]);
                previousSkipped = false;
            }

            Console.WriteLine($"geocodedAddresses length: {geocodedAddresses.Length}");

            return geocodedAddressesFiltered;
        }

        /// <summary>
        /// Returns similarity of source string to target string.
        /// Less is better.
        /// </summary>
        int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }
    }
}