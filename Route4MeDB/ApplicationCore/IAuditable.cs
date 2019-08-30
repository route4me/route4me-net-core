using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore
{
    /// <summary>
    /// This interface determines what will be automatically tracked.
    /// </summary>
    public interface IAuditable
    {
        Guid Id { get; set; }

        DateTime? CreatedDate { get; set; }

        DateTime? ModifiedDate { get; set; }

        String LastModifiedBy { get; set; }

        bool IsInactive { get; set; }
    }

    public enum EntityStateChangeTypeEnum
    {
        Added,
        Deleted,
        Modified,
    }

    public class Audit
    {
        public Guid Id { get; set; }

        public Guid? EntityId { get; set; }

        public string User { get; set; }

        public String Entity { get; set; }

        public DateTime DateTime { get; set; }

        public string ColumnName { get; set; }

        public String OldValue { get; set; }

        public String NewValue { get; set; }

    }
}
