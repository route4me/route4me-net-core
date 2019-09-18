using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeDB.Infrastructure.Data
{
    public class RouteRepository : EfRepository<Route>, IRouteRepository
    {
        readonly Route4MeDbContext _dbContext;
        public RouteRepository(Route4MeDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Route> GetRouteByIdAsync(string routeDbId)
        {
            return await _dbContext.Routes
                .FirstOrDefaultAsync(x => x.RouteDbId == routeDbId);
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync(int offset, int limit)
        {
            var result = _dbContext.Routes.ToListAsync().GetAwaiter().GetResult()
                .Skip(offset)
                .Take(limit);

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<Route> UpdateRouteAsync(string routeDbId, Route routeParameters)
        {
            var route = await this.GetRouteByIdAsync(routeDbId);

            routeParameters.GetType().GetProperties()
                .Where(x => x.Name != "RouteDbId").ToList()
                .ForEach(x => {
                    x.SetValue(route, x.GetValue(routeParameters));
                });

            await this.UpdateAsync(route);

            return await Task.Run(() =>
            {
                return route;
            });
        }

        public async Task<EntityState> RemoveRouteAsync(string routeDbId)
        {
            var route = await this.GetRouteByIdAsync(routeDbId);

            var routeState = await this.DeleteAsync(route);

            return await Task.Run(() =>
            {
                return routeState;
            });
        }
    }
}
