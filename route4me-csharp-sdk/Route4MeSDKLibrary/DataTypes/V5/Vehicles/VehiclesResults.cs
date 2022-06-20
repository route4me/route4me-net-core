using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Response schema for some bulk operations
    /// </summary>
    [DataContract]
    public sealed class VehiclesResults : GenericParameters
    {
        /// <summary>
        ///     Vehicle data
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public Vehicle[] Results { get; set; }
    }
}
