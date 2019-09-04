using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoPostgreSqlDb : IgnoreNoServerDb
    {
        public IgnoreIfNoPostgreSqlDb()
        {
            CheckServerDb(DatabaseProviders.PostgreSql);
        }
    }
}
