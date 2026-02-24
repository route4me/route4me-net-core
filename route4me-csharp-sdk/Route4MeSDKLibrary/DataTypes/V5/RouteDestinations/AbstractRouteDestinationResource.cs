using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Base route destination resource containing all common stop-level fields.
    /// Returned by the route-destinations API endpoints.
    /// </summary>
    [DataContract]
    public class AbstractRouteDestinationResource
    {
        /// <summary>Integer ID of the route destination (stop).</summary>
        [DataMember(Name = "route_destination_id")]
        public int? RouteDestinationId { get; set; }

        /// <summary>Planned service time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "service_time")]
        public string ServiceTime { get; set; }

        /// <summary>Actual service time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "actual_service_time")]
        public string ActualServiceTime { get; set; }

        /// <summary>Reported on-site time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "reported_time_on_site")]
        public string ReportedTimeOnSite { get; set; }

        /// <summary>Geofence-detected service time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "geofence_detected_service_time")]
        public string GeofenceDetectedServiceTime { get; set; }

        /// <summary>Time arrived on time, formatted as "HHh:MMm".</summary>
        [DataMember(Name = "arrival_on_time")]
        public string ArrivalOnTime { get; set; }

        /// <summary>Time departed on time, formatted as "HHh:MMm".</summary>
        [DataMember(Name = "departure_on_time")]
        public string DepartureOnTime { get; set; }

        /// <summary>Running service time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "running_service_time")]
        public string RunningServiceTime { get; set; }

        /// <summary>Running travel time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "running_travel_time")]
        public string RunningTravelTime { get; set; }

        /// <summary>Running wait time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "running_wait_time")]
        public string RunningWaitTime { get; set; }

        /// <summary>Running break time formatted as "HHh:MMm".</summary>
        [DataMember(Name = "running_break_time")]
        public string RunningBreakTime { get; set; }

        /// <summary>Projected wait time before time-window opens, formatted as "HHh:MMm:SSs".</summary>
        [DataMember(Name = "projected_wait_time_before_tw_open")]
        public string ProjectedWaitTimeBeforeTwOpen { get; set; }

        /// <summary>Estimated wait time before time-window opens, formatted as "HHh:MMm:SSs".</summary>
        [DataMember(Name = "estimated_wait_time_before_tw_open")]
        public string EstimatedWaitTimeBeforeTwOpen { get; set; }

        /// <summary>Arrival deviation in seconds, formatted as "HHh:MMm:SSs".</summary>
        [DataMember(Name = "arrival_deviation_seconds")]
        public string ArrivalDeviationSeconds { get; set; }

        /// <summary>Departure deviation in seconds, formatted as "HHh:MMm:SSs".</summary>
        [DataMember(Name = "departure_deviation_seconds")]
        public string DepartureDeviationSeconds { get; set; }

        /// <summary>"Yes" if the stop is a depot; otherwise "No".</summary>
        [DataMember(Name = "is_depot")]
        public string IsDepot { get; set; }

        /// <summary>"Yes" if any time windows were violated; otherwise "No".</summary>
        [DataMember(Name = "time_windows_violated")]
        public string TimeWindowsViolated { get; set; }

        /// <summary>"Yes" if the stop has been visited; otherwise "No".</summary>
        [DataMember(Name = "is_visited")]
        public string IsVisited { get; set; }

        /// <summary>"Yes" if the stop has been departed; otherwise "No".</summary>
        [DataMember(Name = "is_departed")]
        public string IsDeparted { get; set; }

        /// <summary>"Yes" if the stop is a pickup; otherwise "No".</summary>
        [DataMember(Name = "pickup")]
        public string Pickup { get; set; }

        /// <summary>"Yes" if the stop is a dropoff; otherwise "No".</summary>
        [DataMember(Name = "dropoff")]
        public string Dropoff { get; set; }

        /// <summary>"Yes" if this is a joint stop; otherwise "No".</summary>
        [DataMember(Name = "joint")]
        public string Joint { get; set; }

        /// <summary>"Yes" if this is a bundle stop; otherwise "No".</summary>
        [DataMember(Name = "bundle")]
        public string Bundle { get; set; }

        /// <summary>Comma-separated list of required driver skills.</summary>
        [DataMember(Name = "skills")]
        public string Skills { get; set; }

        /// <summary>Running distance to this stop, formatted with unit (e.g., "12.34 mi").</summary>
        [DataMember(Name = "running_distance")]
        public string RunningDistance { get; set; }

        /// <summary>Stop status label (e.g., "Completed"). Null if not yet assigned.</summary>
        [DataMember(Name = "stop_status_id")]
        public string StopStatusId { get; set; }

        /// <summary>Workflow identifier associated with this stop.</summary>
        [DataMember(Name = "workflow_id")]
        public int? WorkflowId { get; set; }

        /// <summary>Stop type (e.g., "Dropoff", "Pickup", "Service").</summary>
        [DataMember(Name = "address_stop_type")]
        public string AddressStopType { get; set; }

        /// <summary>Customer billing type code.</summary>
        [DataMember(Name = "customer_billing_type")]
        public int? CustomerBillingType { get; set; }

        /// <summary>Human-readable label for the customer billing type.</summary>
        [DataMember(Name = "customer_billing_type_label")]
        public string CustomerBillingTypeLabel { get; set; }

        /// <summary>
        /// Destination-level custom key/value data pairs.
        /// Use <see cref="GetDestinationCustomFields"/> and the list/combined endpoints to read,
        /// and the PUT /columns endpoint body to specify which fields are returned.
        /// </summary>
        [DataMember(Name = "custom_fields")]
        public Dictionary<string, string> CustomFields { get; set; }

        /// <summary>Notes attached to this destination.</summary>
        [DataMember(Name = "notes")]
        public List<RouteDestinationNote> Notes { get; set; }
    }
}
