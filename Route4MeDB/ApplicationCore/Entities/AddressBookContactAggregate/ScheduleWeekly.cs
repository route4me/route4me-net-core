using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    public class ScheduleWeekly : BaseEntity, IAggregateRoot
    {
        public ScheduleWeekly(int _every = 1, int[] _weekdays = null)
        {
            Every = _every;
            if (_weekdays != null) Weekdays = _weekdays;
        }

        public ScheduleWeekly() { }

        public int Every { get; private set; }

        internal string _weekdays { get; set; }

        [NotMapped]
        public int[] Weekdays
        {
            get { return _weekdays == null ? null : JsonConvert.DeserializeObject<int[]>(_weekdays); }
            set { _weekdays = JsonConvert.SerializeObject(value); }
        }
    }
}
