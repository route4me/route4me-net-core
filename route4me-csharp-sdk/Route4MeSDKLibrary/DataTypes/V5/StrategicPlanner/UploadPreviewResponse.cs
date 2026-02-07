using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Response from upload preview operation
    /// </summary>
    [DataContract]
    public sealed class UploadPreviewResponse
    {
        /// <summary>
        /// Array of headers in the file (first row)
        /// </summary>
        [DataMember(Name = "csv_header", EmitDefaultValue = false)]
        public string[] CsvHeader { get; set; }

        /// <summary>
        /// Array of parsed data in the file
        /// </summary>
        [DataMember(Name = "sample_parsed_data", EmitDefaultValue = false)]
        public object[] SampleParsedData { get; set; }

        /// <summary>
        /// Array of sheet names (for Excel files)
        /// </summary>
        [DataMember(Name = "sheets", EmitDefaultValue = false)]
        public string[] Sheets { get; set; }

        /// <summary>
        /// Number of sheet of Excel file
        /// </summary>
        [DataMember(Name = "current_sheet", EmitDefaultValue = false)]
        public int? CurrentSheet { get; set; }

        /// <summary>
        /// Number of found addresses in the file
        /// </summary>
        [DataMember(Name = "address_count", EmitDefaultValue = false)]
        public int? AddressCount { get; set; }

        /// <summary>
        /// Found errors/warnings in the file
        /// </summary>
        [DataMember(Name = "warnings", EmitDefaultValue = false)]
        public object Warnings { get; set; }
    }
}
