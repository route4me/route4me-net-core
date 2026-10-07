using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.Routes;

[TestFixture]
public class RouteParametersTruckSpeedCapTests
{
    [Test]
    public void SerializeOptimizationRequest_WithTruckSpeedCap_SendsTruckSpeedCapInParameters()
    {
        var optimizationParameters = new OptimizationParameters
        {
            Parameters = new RouteParameters
            {
                TruckSpeedCap = 80
            }
        };

        // Same serializer the SDK uses for the request body
        var json = JObject.Parse(R4MeUtils.SerializeObjectToJson(optimizationParameters));

        Assert.That(json["parameters"]?["truck_speed_cap"]?.Value<double>(), Is.EqualTo(80.0));
    }

    [Test]
    public void SerializeRouteParameters_WithTruckSpeedCap_IncludesTruckSpeedCapInPayload()
    {
        var routeParameters = new RouteParameters
        {
            TravelMode = TravelMode.Trucking.Description(),
            TruckSpeedCap = 55.5
        };

        var json = JObject.Parse(R4MeUtils.SerializeObjectToJson(routeParameters, true));

        Assert.That(json["truck_speed_cap"]?.Value<double>(), Is.EqualTo(55.5));
    }

    [Test]
    public void SerializeRouteParameters_WithoutTruckSpeedCap_OmitsTruckSpeedCap()
    {
        var routeParameters = new RouteParameters
        {
            TravelMode = TravelMode.Trucking.Description()
        };

        var json = JObject.Parse(R4MeUtils.SerializeObjectToJson(routeParameters, true));

        Assert.That(json.ContainsKey("truck_speed_cap"), Is.False);
    }

    [Test]
    public void DeserializeRouteParameters_WithTruckSpeedCap_ReadsTruckSpeedCap()
    {
        var json = "{\"travel_mode\":\"Trucking\",\"truck_speed_cap\":80}";

        var routeParameters = R4MeUtils.ReadObjectNew<RouteParameters>(json);

        Assert.That(routeParameters, Is.Not.Null);
        Assert.That(routeParameters.TruckSpeedCap, Is.EqualTo(80.0));
    }

    [TestCase(-1.0)]
    [TestCase(double.NaN)]
    [TestCase(double.PositiveInfinity)]
    public void ValidateRouteParameters_WithInvalidTruckSpeedCap_FailsValidation(double truckSpeedCap)
    {
        var routeParameters = new RouteParameters
        {
            TruckSpeedCap = truckSpeedCap
        };

        var isValid = Validator.TryValidateObject(
            routeParameters,
            new ValidationContext(routeParameters),
            new List<ValidationResult>(),
            true);

        Assert.That(isValid, Is.False);
    }

    // 0 is valid: the API treats it as no cap
    [TestCase(0.0)]
    [TestCase(80.0)]
    public void ValidateRouteParameters_WithValidTruckSpeedCap_PassesValidation(double truckSpeedCap)
    {
        var routeParameters = new RouteParameters
        {
            TruckSpeedCap = truckSpeedCap
        };

        var isValid = Validator.TryValidateObject(
            routeParameters,
            new ValidationContext(routeParameters),
            new List<ValidationResult>(),
            true);

        Assert.That(isValid, Is.True);
    }
}