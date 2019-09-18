using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IRouteRepository : IAsyncRepository<Route>
    {
        Task<Route> GetRouteByIdAsync(string routeDbId);

        Task<IEnumerable<Route>> GetRoutesAsync(int offset, int limit);

        Task<Route> UpdateRouteAsync(string routeDbId, Route routeParameters);
    }
}
