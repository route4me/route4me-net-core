using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    [Owned]
    public class ScheduleMonthly : BaseEntity, IAggregateRoot
    {
        public ScheduleMonthly() { }

        public ScheduleMonthly(int _every = 1, string _mode = "dates", int[] _dates = null, Dictionary<int, int> _nth = null)
        {
            Every = _every;
            Mode = _mode;

            if (_dates != null) Dates = _dates;

            if (_nth != null)
            {
                int _n = -1;
                int _what = -1;

                _nth.ToList().ForEach(kv1 =>
                {
                    _n = kv1.Key;
                    _what = kv1.Value;
                });

                if (_n != -1 && _what != -1)
                {
                    this.Nth = new ScheduleMonthlyNth(_n, _what);
                }
            }
        }

        public int Every { get; set; }

        public string Mode { get; set; }

        internal string _dates { get; set; }

        [NotMapped]
        public int[] Dates
        {
            get { return _dates == null ? null : JsonConvert.DeserializeObject<int[]>(_dates); }
            set { _dates = JsonConvert.SerializeObject(value); }
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
