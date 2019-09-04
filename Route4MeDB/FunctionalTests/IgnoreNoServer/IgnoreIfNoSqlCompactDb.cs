using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoSqlCompactDb : IgnoreNoServerDb
    {
        public IgnoreIfNoSqlCompactDb()
        {
            CheckServerDb(DatabaseProviders.SqlCompact40);
        }
    }
}
