using Route4MeDB.Route4MeDbLibrary;

namespace Route4MeDB.FunctionalTest
{
    public sealed class IgnoreIfNoLocalDb : IgnoreNoServerDb
    {
        public IgnoreIfNoLocalDb()
        {
            CheckServerDb(DatabaseProviders.LocalDb);
        }
    }
}
