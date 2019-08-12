using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    public class ScheduleMonthlyNth : BaseEntity, IAggregateRoot
    {
        public ScheduleMonthlyNth(int _n = 1, int _what = 1)
        {
            N = _n;
            What = _what;
        }

        public ScheduleMonthlyNth() { }

        public int N { get; private set; }

        public int What { get; private set; }
    }
}
