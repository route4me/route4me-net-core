using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using System.Linq;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class RouteSpecification : BaseSpecification<Route>
    {
        public RouteSpecification() :
            base(i => (i.RouteDbId != ""))
        {
            AddInclude(o => o.RouteDbId);
        }

        public RouteSpecification(string[] routeDbIds) :
            base(i => (routeDbIds.Contains(i.RouteDbId)))
        {

        }

        public RouteSpecification(string routeDbId) :
            base(i => (i.RouteDbId == routeDbId))
        {
            
        }

        public RouteSpecification(int? offset, int? limit) :
            base(i => (i.RouteDbId != "" || (offset.HasValue && limit.HasValue)))
        {
            ApplyPaging((int)offset, (int)limit);
        }
    }
}
