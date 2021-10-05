using ClientFactoryTest.V4;

namespace ClientFactoryTest.IgnoreTests
{
    public class FactSkipable : IgnoreDemoApiKey
    {
        public FactSkipable()
        {
            CheckApiKey(ApiKeys.ActualApiKey, ApiKeys.DemoApiKey);
        }
    }
}
