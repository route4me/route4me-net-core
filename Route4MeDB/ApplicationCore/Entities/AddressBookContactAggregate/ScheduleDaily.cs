using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    public class ScheduleDaily : BaseEntity, IAggregateRoot
    {
        public ScheduleDaily(int _every = 1)
        {
            Every = _every;
        }

        public ScheduleDaily() { }

        public int Every { get; set; }
    }
}
