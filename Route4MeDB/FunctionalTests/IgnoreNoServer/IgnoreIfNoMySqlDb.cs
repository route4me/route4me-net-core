using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoMySqlDb : IgnoreNoServerDb
    {
        public IgnoreIfNoMySqlDb()
        {
            CheckServerDb(DatabaseProviders.LocalDb);
        }
    }
}
