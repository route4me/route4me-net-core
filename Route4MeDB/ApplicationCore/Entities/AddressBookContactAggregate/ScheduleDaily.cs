using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    [Owned]
    public class ScheduleDaily
    {
        public ScheduleDaily(int _every = 1)
        {
            Every = _every;
        }

        public ScheduleDaily() { }

        public int Every { get; set; }
    }
}
