using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeDB.Infrastructure.Data
{
    public class OptimizationRepository : EfRepository<OptimizationProblem>, IOptimizationRepository
    {
        readonly Route4MeDbContext _dbContext;
        public OptimizationRepository(Route4MeDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OptimizationProblem> GetOptimizationByIdAsync(string optimizationDbId)
        {
            return await _dbContext.Optimizations
                .FirstOrDefaultAsync(x => x.OptimizationProblemDbId == optimizationDbId);
        }

        public async Task<IEnumerable<OptimizationProblem>> GetOptimizationsAsync(int offset, int limit)
        {
            var result = _dbContext.Optimizations.ToListAsync().GetAwaiter().GetResult()
                .Skip(offset)
                .Take(limit);

            return await Task.Run(() =>
            {
                return result;
            });
        }

        public async Task<OptimizationProblem> UpdateOptimizationAsync(string optimizationDbId, OptimizationProblem optimizationParameters)
        {
            var optimization = await this.GetOptimizationByIdAsync(optimizationDbId);

            optimizationParameters.GetType().GetProperties()
                .Where(x => x.Name != "OptimizationProblemDbId").ToList()
                .ForEach(x => {
                    x.SetValue(optimization, x.GetValue(optimizationParameters));
                });

            await this.UpdateAsync(optimization);

            return await Task.Run(() =>
            {
                return optimization;
            });
        }

        public async Task<EntityState> RemoveOptimizationAsync(string optimizationDbId)
        {
            var optimization = await this.GetOptimizationByIdAsync(optimizationDbId);

            var optimizationState = await this.DeleteAsync(optimization);

            return await Task.Run(() =>
            {
                return optimizationState;
            });
        }
    }
}
