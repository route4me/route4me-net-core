using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoSqlServerDb : IgnoreNoServerDb
    {
        public IgnoreIfNoSqlServerDb()
        {
            CheckServerDb(DatabaseProviders.MsSql);
        }
    }
}
