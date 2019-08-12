using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    public class ScheduleAnnually : BaseEntity, IAggregateRoot
    {
        public int Every { get; private set; }

        public bool UseNth { get; private set; }

        internal string _months { get; set; }

        public int[] Months
        {
            get { return _months == null ? null : JsonConvert.DeserializeObject<int[]>(_months); }
            set { _months = JsonConvert.SerializeObject(value); }
        }

        internal string _nth { get; set; }

        [NotMapped]
        public ScheduleMonthlyNth Nth
        {
            get { return _nth == null ? null : JsonConvert.DeserializeObject<ScheduleMonthlyNth>(_nth); }
            set { _nth = JsonConvert.SerializeObject(value); }
        }
    }
}
