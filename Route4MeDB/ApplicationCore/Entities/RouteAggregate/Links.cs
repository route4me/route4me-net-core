using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class Links
    {
        /// <summary>
        /// API URL Route GET call for the current route
        /// </summary>
        [Column("route")]
        public string Route { get; set; }

        /// <summary>
        /// A Link to the GET operation for the optimization problem
        /// </summary>
        [Column("view")]
        public string View { get; set; }

        /// <summary>
        /// The optimization problem ID
        /// </summary>
        [Column("optimization_problem_id", TypeName ="varchar(250)")]
        public string OptimizationProblemId { get; set; }
    }
}
