using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoBigQueryDb : IgnoreNoServerDb
    {
        public IgnoreIfNoBigQueryDb()
        {
            CheckServerDb(DatabaseProviders.BigQuery);
        }
    }
}
