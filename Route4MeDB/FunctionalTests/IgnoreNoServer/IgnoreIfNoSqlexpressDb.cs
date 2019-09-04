using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoSqlexpressDb : IgnoreNoServerDb
    {
        public IgnoreIfNoSqlexpressDb()
        {
            CheckServerDb(DatabaseProviders.Sqlexpress);
        }
    }
}
