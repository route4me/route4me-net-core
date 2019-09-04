using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoSqLiteDb : IgnoreNoServerDb
    {
        public IgnoreIfNoSqLiteDb()
        {
            CheckServerDb(DatabaseProviders.SQLite);
        }
    }
}
