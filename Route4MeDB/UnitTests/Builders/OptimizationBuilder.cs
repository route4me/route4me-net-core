using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using Route4MeDB.Route4MeDbLibrary;
using System.IO;

namespace Route4MeDB.UnitTests.Builders
{
    public class OptimizationBuilder
    {
        public OptimizationProblem _optimization;
        public readonly OptimizationTestData optimizationTestData;
        string testDataFile;
        JsonSerializerSettings settings;

        DataExchangeHelper dataExchange = new DataExchangeHelper();

        public class OptimizationTestData
        {

        }

        public OptimizationBuilder()
        {
            optimizationTestData = new OptimizationTestData();
            settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public OptimizationProblem OptimizationWith24Stops()
        {
            testDataFile = @"TestData/mdmd_optimization_24stops_RESPONSE.json";

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                _optimization = dataExchange.ConvertSdkJsonContentToEntity<OptimizationProblem>(jsonContent, out string errorString);
            }

            return _optimization;
        }

        public OptimizationProblem OptimizationWithSingleDriverRoundtrip()
        {
            testDataFile = @"TestData/sdrt_optimization_RESPONSE.json";

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                _optimization = dataExchange.ConvertSdkJsonContentToEntity<OptimizationProblem>(jsonContent, out string errorString);
            }

            return _optimization;
        }

        public OptimizationProblem OptimizationWith10Stops()
        {
            testDataFile = @"TestData/sd_optimization_10stops_RESPONSE.json";

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore
                };

                _optimization = dataExchange.ConvertSdkJsonContentToEntity<OptimizationProblem>(jsonContent, out string errorString);
            }

            return _optimization;
        }

        public OptimizationProblem OptimizationWithCustomFields()
        {
            testDataFile = @"TestData/optimization_with_custom_fields.json";

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                _optimization = dataExchange.ConvertSdkJsonContentToEntity<OptimizationProblem>(jsonContent, out string errorString);
            }

            return _optimization;
        }

    }
}
