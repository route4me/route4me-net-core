using static Route4MeDB.ApplicationCore.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigurePathesToNext(EntityTypeBuilder<PathToNext> builder)
        {


        }
    }
}
