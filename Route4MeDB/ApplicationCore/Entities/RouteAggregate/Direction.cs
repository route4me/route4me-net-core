using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class Direction : IAggregateRoot
    {
        public Direction() { }

        [Key, Column("direction_id")]
        public long DirectionId { get; set; }

        [Column("route_id", TypeName = "varchar(32)")]
        public string RouteId { get; set; }

        ///<summary>
        /// Starting location of a direction. See <see cref="DirectionLocation"/>
        ///</summary>
        [Column("location")]
        public string Location { get; set; }

        [NotMapped]
        public DirectionLocation LocationObj
        {
            get { return Location == null ? null : JsonConvert.DeserializeObject<DirectionLocation>(Location); }
            set { Location = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// The diection steps. See <see cref="DirectionStep"/>
        /// </summary>
        [Column("steps")]
        public string Steps { get; set; }

        [NotMapped]
        public DirectionStep[] StepsArray
        {
            get { return Steps == null ? null : JsonConvert.DeserializeObject<DirectionStep[]>(Steps); }
            set { Steps = JsonConvert.SerializeObject(value); }
        }

        public string RouteDbId { get; set; }
        [ForeignKey("RouteDbId")]
        public Route Route { get; set; }

        // TO DO: Adjust later
        //public string OptimizationProblemDbId { get; set; }
        //[ForeignKey("FK_Directions_OptimizationProblemDbId")]
        //public OptimizationProblem optimizationProblem { get; set; }
    }
}
