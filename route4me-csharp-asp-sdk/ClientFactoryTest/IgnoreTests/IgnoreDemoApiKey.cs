using ClientFactoryTest.V4;
using Xunit;

namespace ClientFactoryTest.IgnoreTests
{
    public class IgnoreDemoApiKey : FactAttribute
    {
        public void CheckApiKey(string actualApiKey, string demoApiKey)
        {
            // The optimizations with the Trucking, Multiple Depots,
            // Multiple Drivers allowed only for business and higher account types.
            // Put in the parameter an appropriate API key

            if (actualApiKey == demoApiKey)
            {
                this.Skip = "Use your API key instead of the demo API key to run the tests.";
            }
            else
            {
                this.Skip = "";
            }
        }
    }

    public class IgnoreTheory : TheoryAttribute
    {
        public void CheckApiKey(string actualApiKey, string demoApiKey)
        {
            if (actualApiKey == demoApiKey)
            {
                this.Skip = "Use your API key instead of the demo API key to run the tests.";
            }
            else
            {
                this.Skip = "";
            }
        }
    }
}
