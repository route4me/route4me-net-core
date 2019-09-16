using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IOptimizationRepository : IAsyncRepository<OptimizationProblem>
    {
        Task<OptimizationProblem> GetOptimizationByIdAsync(string optimizationDbId);

        Task<IEnumerable<OptimizationProblem>> GetOptimizationsAsync(int offset, int limit);

        Task<OptimizationProblem> UpdateOptimizationAsync(string optimizationDbId, OptimizationProblem optimizationParameters);
    }
}
