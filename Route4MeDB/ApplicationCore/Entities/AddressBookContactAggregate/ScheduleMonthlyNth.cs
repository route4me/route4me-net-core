using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    [Owned]
    public class ScheduleMonthlyNth
    {
        public ScheduleMonthlyNth(int _n = 1, int _what = 1)
        {
            N = _n;
            What = _what;
        }

        public ScheduleMonthlyNth() { }

        public int N { get; set; }

        public int What { get; set; }
    }
}
