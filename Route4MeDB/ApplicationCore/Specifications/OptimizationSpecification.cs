using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using System.Linq;

namespace Route4MeDB.ApplicationCore.Specifications
{
    public class OptimizationSpecification : BaseSpecification<OptimizationProblem>
    {
        public OptimizationSpecification() :
            base(i => (i.OptimizationProblemDbId != ""))
        {
            AddInclude(o => o.OptimizationProblemDbId);
        }

        public OptimizationSpecification(string[] optimizationDbIds) :
            base(i => (optimizationDbIds.Contains(i.OptimizationProblemDbId)))
        {

        }

        public OptimizationSpecification(string optimizationDbId) :
            base(i => (i.OptimizationProblemDbId == optimizationDbId))
        {
            //AddInclude(o => o.OrderId);
        }

        public OptimizationSpecification(int? offset, int? limit) :
            base(i => (i.OptimizationProblemDbId != "" || (offset.HasValue && limit.HasValue)))
        {
            ApplyPaging((int)offset, (int)limit);
        }
    }
}
