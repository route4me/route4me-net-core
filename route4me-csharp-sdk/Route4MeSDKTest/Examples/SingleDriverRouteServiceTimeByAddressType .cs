using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void SingleDriverRouteServiceTimeByAddressType()
        {
            var route4Me = new Route4MeManager(ActualApiKey);
            OptimizationParameters optParameters = new OptimizationParameters();

            RouteParameters parameters = new RouteParameters();
            parameters.AlgorithmType = AlgorithmType.TSP;
            parameters.StoreRoute = false;
            //parameters.setShareRoute(Boolean.FALSE);
            parameters.RouteName = "Single Driver Route 10 Stops";
            parameters.Optimize = Optimize.Distance.Description();
            parameters.DistanceUnit = DistanceUnit.MI.Description();
            parameters.DeviceType = DeviceType.Web.Description();
            optParameters.Parameters = parameters;

            var serviceTimeByAddressTypeMap = new Dictionary<string, int?>()
            {
                {"STOP_TYPE_1", 60},
                {"STOP_TYPE_2", 120},
                {"STOP_TYPE_3", 180},
                {"STOP_TYPE_4", 300}
            };

            optParameters.Addresses = new[]
            {
                //DEPOT
                new Address
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0
                },
                //STOPS
                new Address
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_1", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_1", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_2", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_2", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_3", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_3", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_4", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_4", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_4", serviceTimeByAddressTypeMap)
                },
                new Address
                {
                    AddressString = "1661 W HILL ST, Louisville, KY, 40210",
                    Latitude = 38.229584,
                    Longitude = -85.783966,
                    Time = ServiceTimeUtils.GetServiceTimeByAddressType("STOP_TYPE_5", serviceTimeByAddressTypeMap)
                }

            };

            try
            {
                DataObject responseObject = route4Me.RunOptimization(optParameters, out var err);
                Console.WriteLine("Optimization Problem ID:" + responseObject.OptimizationProblemId);
                Console.WriteLine("State:" + responseObject.State.Description());
                if (responseObject.Addresses != null)
                {
                    foreach (var sequencedAddress in responseObject.Addresses)
                    {
                        Console.WriteLine(sequencedAddress.SequenceNo + " - " + sequencedAddress.AddressString + "  Service Time: " + sequencedAddress.Time);
                    }
                }
            }
            catch (Exception e)
            {
                //handle exception
                Console.WriteLine(e);
            }
        }
    }
}
