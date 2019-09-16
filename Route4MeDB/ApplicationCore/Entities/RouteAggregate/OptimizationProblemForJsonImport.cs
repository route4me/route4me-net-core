using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    public class OptimizationProblemForJsonImport : OptimizationProblem
    {
        [Column("user_errors")]
        public new string[] UserErrors { get; set; }

        [Column("parameters")]
        public new RouteParameters Parameters { get; set; }

        [Column("links")]
        public new Dictionary<string, string> Links { get; set; }

        [Column("tracking_history")]
        public new TrackingHistory[] TrackingHistory { get; set; }

        [Column("path")]
        public new DirectionPathPoint[] Path { get; set; }

        public OptimizationProblem GetOptimization()
        {
            var usualOptimization = new OptimizationProblem();
            this.GetType().GetProperties()
                .ToList().Where(x => x.GetValue(this)!=null).ToList()
                .ForEach(x => {
                    switch (x.Name)
                    {
                        case "Parameters":
                            usualOptimization.ParametersObj = this.Parameters;
                            break;
                        case "Links":
                            usualOptimization.LinksObj = this.Links;
                            break;
                        case "UserErrors":
                            usualOptimization.UserErrorsArray = this.UserErrors;
                            break;
                        //case "TrackingHistory":
                        //    usualOptimization.TrackingHistoryArray = this.TrackingHistory;
                        //    break;
                        //case "Path":
                        //    usualOptimization.PathArray = this.Path;
                        //    break;
                        default:
                            x.SetValue(usualOptimization, x.GetValue(this));
                            break;
                    }

                });

            return usualOptimization;
        }
    }

    public class OptimizationParent
    {
        public OptimizationProblemForJsonImport optimizationParent { get; set; }
    }
}
