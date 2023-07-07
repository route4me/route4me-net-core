﻿using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating an optimization 
        /// with single-depot, multi-driver, no time windows options.
        /// </summary>
        public void SingleDepotMultipleDriverNoTimeWindow()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            var addresses = new Address[]
            {
                #region Addresses

                new Address()
                {
                    AddressString = "40 Mercer st, New York, NY",
                    IsDepot = true,
                    Latitude = 40.7213583,
                    Longitude = -74.0013082,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Manhatten Island NYC",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "503 W139 St, NY,NY",
                    Latitude = 40.7109062,
                    Longitude = -74.0091848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "203 grand st, new york, ny",
                    Latitude = 40.7188990,
                    Longitude = -73.9967320,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "119 Church Street",
                    Latitude = 40.7137757,
                    Longitude = -74.0088238,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "broadway street, new york",
                    Latitude = 40.7191551,
                    Longitude = -74.0020849,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Ground Zero, Vesey-Liberty-Church-West Streets New York NY 10038",
                    Latitude = 40.7233126,
                    Longitude = -74.0116602,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "226 ilyssa way staten lsland ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "185 franklin st.",
                    Latitude = 40.7192099,
                    Longitude = -74.0097670,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york city,",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "11 e. broaway 11038",
                    Latitude = 40.7132060,
                    Longitude = -73.9974019,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Brooklyn Bridge, NY",
                    Latitude = 40.7053804,
                    Longitude = -73.9962503,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "World Trade Center Site, NY",
                    Latitude = 40.7114980,
                    Longitude = -74.0122990,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York Stock Exchange, NY",
                    Latitude = 40.7074242,
                    Longitude = -74.0116342,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Wall Street, NY",
                    Latitude = 40.7079825,
                    Longitude = -74.0079781,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Trinity Church, NY",
                    Latitude = 40.7081426,
                    Longitude = -74.0120511,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "World Financial Center, NY",
                    Latitude = 40.7104750,
                    Longitude = -74.0154930,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Federal Hall, NY",
                    Latitude = 40.7073034,
                    Longitude = -74.0102734,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Flatiron Building, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "South Street Seaport, NY",
                    Latitude = 40.7069210,
                    Longitude = -74.0036380,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Rockefeller Center, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "FAO Schwarz, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Woolworth Building, NY",
                    Latitude = 40.7123903,
                    Longitude = -74.0083309,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Met Life Building, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "SOHO/Tribeca, NY",
                    Latitude = 40.7185650,
                    Longitude = -74.0120170,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "MacyГўв‚¬в„ўs, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "City Hall, NY, NY",
                    Latitude = 40.7127047,
                    Longitude = -74.0058663,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Macy&amp;acirc;в‚¬в„ўs, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "1452 potter blvd bayshore ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "55 Church St. New York, NY",
                    Latitude = 40.7112320,
                    Longitude = -74.0102680,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "55 Church St, New York, NY",
                    Latitude = 40.7112320,
                    Longitude = -74.0102680,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "79 woodlawn dr revena ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "135 main st revena ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "250 greenwich st, new york, ny",
                    Latitude = 40.7131590,
                    Longitude = -74.0118890,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "79 grand, new york, ny",
                    Latitude = 40.7216958,
                    Longitude = -74.0024352,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "World trade center\n",
                    Latitude = 40.7116260,
                    Longitude = -74.0107140,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "World trade centern",
                    Latitude = 40.7132910,
                    Longitude = -74.0118350,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "391 broadway new york",
                    Latitude = 40.7183693,
                    Longitude = -74.0027800,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Fletcher street",
                    Latitude = 40.7063954,
                    Longitude = -74.0056353,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "2 Plum LanenPlainview New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "50 Kennedy drivenPlainview New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "7 Crestwood DrivenPlainview New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "85 west street nyc",
                    Latitude = 40.7096460,
                    Longitude = -74.0146140,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York, New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "89 Reade St, New York City, New York 10013",
                    Latitude = 40.7142970,
                    Longitude = -74.0059660,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "100 white st",
                    Latitude = 40.7172477,
                    Longitude = -74.0014351,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "100 white st\n33040",
                    Latitude = 40.7172477,
                    Longitude = -74.0014351,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Canal st and mulberry",
                    Latitude = 40.7170880,
                    Longitude = -73.9986025,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "91-83 111st st\nRichmond hills ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "122-09 liberty avenOzone park ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "80-16 101 avenOzone park ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "6302 woodhaven blvdnRego park ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "39-02 64th stnWoodside ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York City, NY,",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Pine st",
                    Latitude = 40.7069754,
                    Longitude = -74.0089557,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Wall st",
                    Latitude = 40.7079825,
                    Longitude = -74.0079781,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "32 avenue of the Americas, NY, NY",
                    Latitude = 40.7201140,
                    Longitude = -74.0050920,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "260 west broadway, NY, NY",
                    Latitude = 40.7206210,
                    Longitude = -74.0055670,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Long island, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "27 Carley ave\nHuntington ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "17 west neck RdnHuntington ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "206 washington st",
                    Latitude = 40.7131577,
                    Longitude = -74.0126091,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Cipriani new york",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Byshnell Basin. NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "89 Reade St, New York, New York 10013",
                    Latitude = 40.7142970,
                    Longitude = -74.0059660,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "250 Greenwich St, New York, New York 10007",
                    Latitude = 40.7133000,
                    Longitude = -74.0120000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "64 Bowery, New York, New York 10013",
                    Latitude = 40.7165540,
                    Longitude = -73.9962700,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "142-156 Mulberry St, New York, New York 10013",
                    Latitude = 40.7192764,
                    Longitude = -73.9973096,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "80 Spring St, New York, New York 10012",
                    Latitude = 40.7226590,
                    Longitude = -73.9981820,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "182 Duane street ny",
                    Latitude = 40.7170879,
                    Longitude = -74.0101210,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "182 Duane St, New York, New York 10013",
                    Latitude = 40.7170879,
                    Longitude = -74.0101210,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "462 broome street nyc",
                    Latitude = 40.7225800,
                    Longitude = -74.0008980,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "117 mercer street nyc",
                    Latitude = 40.7239679,
                    Longitude = -73.9991585,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Lucca antiques\n182 Duane St, New York, New York 10013",
                    Latitude = 40.7167516,
                    Longitude = -74.0087482,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Room and board\n105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Lucca antiquesn182 Duane St, New York, New York 10013",
                    Latitude = 40.7167516,
                    Longitude = -74.0087482,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Room and boardn105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Lucca antiques 182 Duane st new York ny",
                    Latitude = 40.7170879,
                    Longitude = -74.0101210,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Property\n14 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "101 Crosby street nyc",
                    Latitude = 40.7235730,
                    Longitude = -73.9969540,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Room and board \n105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Propertyn14 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Room and board n105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Mecox gardens\n926 Lexington nyc",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "25 sybil&apos;s crossing Kent lakes, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString =
                        "10149 ASHDALE LANE\t67\t67393253\t\t\tSANTEE\tCA\t92071\t\t280501691\t67393253\tIFI\t280501691\t05-JUN-10\t67393253",
                    Latitude = 40.7143000,
                    Longitude = -74.0067000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "193 Lakebridge Dr, Kings Paark, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "219 west creek",
                    Latitude = 40.7198564,
                    Longitude = -74.0121098,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "14 North Moore Street\nNew York, ny",
                    Latitude = 40.7196970,
                    Longitude = -74.0066100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "14 North Moore StreetnNew York, ny",
                    Latitude = 40.7196970,
                    Longitude = -74.0066100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "14 North Moore Street New York, ny",
                    Latitude = 40.7196970,
                    Longitude = -74.0066100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "30-38 Fulton St, New York, New York 10038",
                    Latitude = 40.7077737,
                    Longitude = -74.0043299,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "73 Spring Street Ny NY",
                    Latitude = 40.7225378,
                    Longitude = -73.9976742,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "119 Mercer Street Ny NY",
                    Latitude = 40.7241390,
                    Longitude = -73.9993110,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "525 Broadway Ny NY",
                    Latitude = 40.7230410,
                    Longitude = -73.9991650,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Church St",
                    Latitude = 40.7154338,
                    Longitude = -74.0075430,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "135 union stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "21101 coffeen stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "215 Washington stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "619 mill stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "3 canel st, new York, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york city new york",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "50 grand street",
                    Latitude = 40.7225780,
                    Longitude = -74.0038019,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Orient ferry, li ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Hilton hotel river head li ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "116 park pl",
                    Latitude = 40.7140565,
                    Longitude = -74.0110155,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "long islans new york",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "1 prospect pointe niagra falls ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York City\tNY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "pink berry ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York City\t NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "10108",
                    Latitude = 40.7143000,
                    Longitude = -74.0067000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Ann st",
                    Latitude = 40.7105937,
                    Longitude = -74.0073715,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Hok 620 ave of Americas new York ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Som 14 wall st nyc",
                    Latitude = 40.7076179,
                    Longitude = -74.0107630,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York ,ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "52 prince st. 10012",
                    Latitude = 40.7235840,
                    Longitude = -73.9961170,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "451 broadway 10013",
                    Latitude = 40.7205177,
                    Longitude = -74.0009557,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Dover street",
                    Latitude = 40.7087886,
                    Longitude = -74.0008644,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Murray st",
                    Latitude = 40.7148929,
                    Longitude = -74.0113349,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "85 West St, New York, New York",
                    Latitude = 40.7096460,
                    Longitude = -74.0146140,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },
                //125left
                new Address()
                {
                    AddressString = "NYC",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "64 trinity place, ny, ny",
                    Latitude = 40.7081649,
                    Longitude = -74.0127168,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "150 broadway ny ny",
                    Latitude = 40.7091850,
                    Longitude = -74.0100330,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Pinegrove Dude Ranch 31 cherrytown Rd Kerhinkson Ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Front street",
                    Latitude = 40.7063990,
                    Longitude = -74.0045493,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "234 canal St new York, NY 10013",
                    Latitude = 40.7177010,
                    Longitude = -73.9999570,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "72 spring street, new york ny 10012",
                    Latitude = 40.7225093,
                    Longitude = -73.9976540,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "150 spring street, new york, ny 10012",
                    Latitude = 40.7242393,
                    Longitude = -74.0014922,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "580 broadway street, new york, ny 10012",
                    Latitude = 40.7244210,
                    Longitude = -73.9970260,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "42 trinity place, new york, ny 10007",
                    Latitude = 40.7074000,
                    Longitude = -74.0135510,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "baco ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Micro Tel Inn Alburn New York",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "20 Cedar Close",
                    Latitude = 40.7068734,
                    Longitude = -74.0078613,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "South street",
                    Latitude = 40.7080184,
                    Longitude = -73.9999414,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "47 Lafayette street",
                    Latitude = 40.7159204,
                    Longitude = -74.0027332,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Newyork",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Ground Zero, NY",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "GROUND ZERO NY",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "33400 SE Harrison",
                    Latitude = 40.7188400,
                    Longitude = -74.0103330,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york, new york",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "8 Greene St, New York, 10013",
                    Latitude = 40.7206160,
                    Longitude = -74.0027600,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "226 w 44st new york city",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "s street seaport 11 fulton st new york city",
                    Latitude = 40.7069150,
                    Longitude = -74.0033215,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "30 Rockefeller Plaza w 49th St New York City",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "30 Rockefeller Plaza 50th St New York City",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "S. Street Seaport 11 Fulton St. New York City",
                    Latitude = 40.7069150,
                    Longitude = -74.0033215,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "30 rockefeller plaza w 49th st, new york city",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "30 rockefeller plaza 50th st, new york city",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "11 fulton st, new york city",
                    Latitude = 40.7069150,
                    Longitude = -74.0033215,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york city ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Big apple",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York new York",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "83-85 Chambers St, New York, New York 10007",
                    Latitude = 40.7148130,
                    Longitude = -74.0068890,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York",
                    Latitude = 40.7145502,
                    Longitude = -74.0071249,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "102 North End Ave NY, NY",
                    Latitude = 40.7147980,
                    Longitude = -74.0159690,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "57 Thompson St, New York, New York 10012",
                    Latitude = 40.7241400,
                    Longitude = -74.0035860,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york city",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "nyc, ny",
                    Latitude = 40.7145502,
                    Longitude = -74.0071249,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York NY",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "285 West Broadway New York, NY 10013",
                    Latitude = 40.7208750,
                    Longitude = -74.0046310,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "100 avenue of the americas New York, NY 10013",
                    Latitude = 40.7233120,
                    Longitude = -74.0043950,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "270 Lafeyette st New York, NY 10012",
                    Latitude = 40.7238790,
                    Longitude = -73.9965270,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "560 Broadway New York, NY 10012",
                    Latitude = 40.7238540,
                    Longitude = -73.9974980,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "42 Wooster St New York, NY 10013",
                    Latitude = 40.7223860,
                    Longitude = -74.0024220,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "42 Wooster StreetNew York, NY 10013-2230",
                    Latitude = 40.7223633,
                    Longitude = -74.0026240,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "504 Broadway, New York, NY 10012",
                    Latitude = 40.7221444,
                    Longitude = -73.9992714,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "426 Broome Street, New York, NY 10013",
                    Latitude = 40.7213295,
                    Longitude = -73.9987121,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "City hall, nyc",
                    Latitude = 40.7122066,
                    Longitude = -74.0055026,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "South street seaport, nyc",
                    Latitude = 40.7069501,
                    Longitude = -74.0030848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Ground zero, nyc",
                    Latitude = 40.7116410,
                    Longitude = -74.0122530,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Ground zero",
                    Latitude = 40.7116410,
                    Longitude = -74.0122530,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Mulberry and canal, NYC",
                    Latitude = 40.7170900,
                    Longitude = -73.9985900,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "World Trade Center, NYC",
                    Latitude = 40.7116670,
                    Longitude = -74.0125000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "South Street Seaport",
                    Latitude = 40.7069501,
                    Longitude = -74.0030848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Wall Street and Nassau Street, NYC",
                    Latitude = 40.7071400,
                    Longitude = -74.0106900,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Trinity Church, NYC",
                    Latitude = 40.7081269,
                    Longitude = -74.0125691,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Federal Hall National Memorial",
                    Latitude = 40.7069515,
                    Longitude = -74.0101638,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Little Italy, NYC",
                    Latitude = 40.7196920,
                    Longitude = -73.9977650,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York, NY",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York City, NY,",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york,ny",
                    Latitude = 40.7145500,
                    Longitude = -74.0071300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Odeon cinema",
                    Latitude = 40.7168300,
                    Longitude = -74.0080300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York City",
                    Latitude = 40.7145500,
                    Longitude = -74.0071300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "52 broadway, ny,ny 1004",
                    Latitude = 40.7065000,
                    Longitude = -74.0123000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "52 broadway, ny,ny 10004",
                    Latitude = 40.7065000,
                    Longitude = -74.0123000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "22 beaver st, ny,ny 10004",
                    Latitude = 40.7048200,
                    Longitude = -74.0121800,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "54 pine st,ny,ny 10005",
                    Latitude = 40.7068600,
                    Longitude = -74.0084900,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "114 liberty st, ny,ny 10006",
                    Latitude = 40.7097700,
                    Longitude = -74.0122000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "215 canal st,ny,ny 10013",
                    Latitude = 40.7174700,
                    Longitude = -73.9989500,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york city ny",
                    Latitude = 40.7145500,
                    Longitude = -74.0071300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "World Trade Center, New York, NY",
                    Latitude = 40.7116700,
                    Longitude = -74.0125000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Chinatown, New York, NY",
                    Latitude = 40.7159600,
                    Longitude = -73.9974100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "101 murray street new york, ny",
                    Latitude = 40.7152600,
                    Longitude = -74.0125100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "nyc",
                    Latitude = 40.7145500,
                    Longitude = -74.0071200,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "510 broadway new york",
                    Latitude = 40.7223400,
                    Longitude = -73.9990160,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "nyc",
                    Latitude = 40.7145502,
                    Longitude = -74.0071249,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Little Italy",
                    Latitude = 40.7196920,
                    Longitude = -73.9977647,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "463 Broadway, New York, NY",
                    Latitude = 40.7210590,
                    Longitude = -74.0006880,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "222 West Broadway, New York, NY",
                    Latitude = 40.7193520,
                    Longitude = -74.0064170,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "270 Lafayette street new York new york",
                    Latitude = 40.7238790,
                    Longitude = -73.9965270,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "New York, NY USA",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "97 Kenmare Street, New York, NY 10012",
                    Latitude = 40.7214370,
                    Longitude = -73.9969110,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "19 Beekman St, New York, New York 10038",
                    Latitude = 40.7107540,
                    Longitude = -74.0062870,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Soho",
                    Latitude = 40.7241404,
                    Longitude = -74.0020213,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Bergen, New York",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "478 Broadway, NY, NY",
                    Latitude = 40.7213360,
                    Longitude = -73.9997710,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "555 broadway, ny, ny",
                    Latitude = 40.7238830,
                    Longitude = -73.9982960,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "375 West Broadway, NY, NY",
                    Latitude = 40.7235000,
                    Longitude = -74.0026020,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "35 howard st, NY, NY",
                    Latitude = 40.7195240,
                    Longitude = -74.0010300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Pier 17 NYC",
                    Latitude = 40.7063660,
                    Longitude = -74.0026890,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "120 Liberty St NYC",
                    Latitude = 40.7097740,
                    Longitude = -74.0124510,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "80 White Street, NY, NY",
                    Latitude = 40.7178340,
                    Longitude = -74.0020520,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Manhattan, NY",
                    Latitude = 40.7144300,
                    Longitude = -74.0061000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "22 read st, ny",
                    Latitude = 40.7142010,
                    Longitude = -74.0044910,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "130 Mulberry St, New York, NY 10013-5547",
                    Latitude = 40.7182880,
                    Longitude = -73.9977110,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "new york city, ny",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "10038",
                    Latitude = 40.7092119,
                    Longitude = -74.0033631,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "11 Wall St, New York, NY 10005-1905",
                    Latitude = 40.7072900,
                    Longitude = -74.0112010,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "89 Reade St, New York, New York 10007",
                    Latitude = 40.7134560,
                    Longitude = -74.0034990,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "265 Canal St, New York, NY 10013-6010",
                    Latitude = 40.7188850,
                    Longitude = -74.0009000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "39 Broadway, New York, NY 10006-3003",
                    Latitude = 40.7133450,
                    Longitude = -73.9961320,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "25 beaver street new york ny",
                    Latitude = 40.7051110,
                    Longitude = -74.0120070,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "100 church street new york ny",
                    Latitude = 40.7130430,
                    Longitude = -74.0096370,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "69 Mercer St, New York, NY 10012-4440",
                    Latitude = 40.7226490,
                    Longitude = -74.0006100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "111 Worth St, New York, NY 10013-4008",
                    Latitude = 40.7159210,
                    Longitude = -74.0034100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "240-248 Broadway, New York, New York 10038",
                    Latitude = 40.7127690,
                    Longitude = -74.0076810,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "12 Maiden Ln, New York, NY 10038-4002",
                    Latitude = 40.7094460,
                    Longitude = -74.0095760,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "291 Broadway, New York, NY 10007-1814",
                    Latitude = 40.7150000,
                    Longitude = -74.0061340,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "55 Liberty St, New York, NY 10005-1003",
                    Latitude = 40.7088430,
                    Longitude = -74.0093840,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "Brooklyn Bridge, NY",
                    Latitude = 40.7063440,
                    Longitude = -73.9974390,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "wall street",
                    Latitude = 40.7063889,
                    Longitude = -74.0094444,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "south street seaport, ny",
                    Latitude = 40.7069501,
                    Longitude = -74.0030848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "little italy, ny",
                    Latitude = 40.7196920,
                    Longitude = -73.9977647,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "47 Pine St, New York, NY 10005-1513",
                    Latitude = 40.7067340,
                    Longitude = -74.0089280,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "22 cortlandt street new york ny",
                    Latitude = 40.7100820,
                    Longitude = -74.0102510,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "105 reade street new york ny",
                    Latitude = 40.7156330,
                    Longitude = -74.0085220,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "2 lafayette street new york ny",
                    Latitude = 40.7140310,
                    Longitude = -74.0038910,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "53 crosby street new york ny",
                    Latitude = 40.7219770,
                    Longitude = -73.9982450,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "2 Lafayette St, New York, NY 10007-1307",
                    Latitude = 40.7140310,
                    Longitude = -74.0038910,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "105 Reade St, New York, NY 10013-3840",
                    Latitude = 40.7156330,
                    Longitude = -74.0085220,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "chinatown, ny",
                    Latitude = 40.7159556,
                    Longitude = -73.9974133,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "250 Broadway, New York, NY 10007-2516",
                    Latitude = 40.7130180,
                    Longitude = -74.0074700,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "156 William St, New York, NY 10038-2609",
                    Latitude = 40.7097970,
                    Longitude = -74.0055770,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "100 Church St, New York, NY 10007-2601",
                    Latitude = 40.7130430,
                    Longitude = -74.0096370,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address()
                {
                    AddressString = "33 Beaver St, New York, NY 10004-2736",
                    Latitude = 40.7050980,
                    Longitude = -74.0117200,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Single Depot, Multiple Driver, No Time Window",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RT = true,
                RouteMaxDuration = 86400,
                VehicleCapacity = 20,
                VehicleMaxDistanceMI = 99999,
                Parts = 4,

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Matrix
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            DataObject dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}