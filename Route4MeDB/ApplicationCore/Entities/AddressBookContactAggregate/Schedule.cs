using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    public class Schedule : BaseEntity, IAggregateRoot
    {
        public Schedule() { }

        public Schedule(string scheduleMode, bool blNth)
        {
            switch (scheduleMode)
            {
                case "daily":
                    this.Daily = new ScheduleDaily();
                    this.Mode = "daily";
                    break;
                case "weekly":
                    this.Weekly = new ScheduleWeekly();
                    this.Mode = "weekly";
                    break;
                case "monthly":
                    this.Monthly = new ScheduleMonthly();
                    this.Mode = "monthly";
                    if (blNth) this.Monthly.Nth = new ScheduleMonthlyNth();
                    break;
                case "annually":
                    this.Annually = new ScheduleAnnually();
                    this.Mode = "annually";
                    if (blNth) this.Annually.Nth = new ScheduleMonthlyNth();
                    break;
            }

        }

        public bool Enabled { get; set; }

        public string From { get; set; }

        public string Mode { get; set; }

        internal string _daily { get; set; }

        [NotMapped]
        public ScheduleDaily Daily
        {
            get { return _daily == null ? null : JsonConvert.DeserializeObject<ScheduleDaily>(_daily); }
            set { _daily = JsonConvert.SerializeObject(value); }
        }

        internal string _weekly { get; set; }

        [NotMapped]
        public ScheduleWeekly Weekly
        {
            get { return _weekly == null ? null : JsonConvert.DeserializeObject<ScheduleWeekly>(_weekly); }
            set { _weekly = JsonConvert.SerializeObject(value); }
        }

        internal string _monthly { get; set; }

        [NotMapped]
        public ScheduleMonthly Monthly
        {
            get { return _monthly == null ? null : JsonConvert.DeserializeObject<ScheduleMonthly>(_monthly); }
            set { _monthly = JsonConvert.SerializeObject(value); }
        }

        internal string _annually { get; set; }

        [NotMapped]
        public ScheduleAnnually Annually
        {
            get { return _annually == null ? null : JsonConvert.DeserializeObject<ScheduleAnnually>(_annually); }
            set { _annually = JsonConvert.SerializeObject(value); }
        }

        public bool ValidateScheduleMode(object ScheduleMode)
        {
            return ScheduleMode == null ? false :
                Array.IndexOf(new string[] { "daily", "weekly", "monthly", "annually" }, ScheduleMode.ToString()) >= 0
                ? true : false;
        }

        public bool ValidateScheduleEnabled(object ScheduleEnabled)
        {
            bool blValid = false;
            return bool.TryParse(ScheduleEnabled.ToString(), out blValid) ? true : false;
        }

        public bool ValidateScheduleFrom(object ScheduleFrom)
        {
            DateTime dtOut = DateTime.MinValue;
            return DateTime.TryParseExact(ScheduleFrom.ToString(), "yyyy-MM-dd", new CultureInfo("fr-FR"), DateTimeStyles.None, out dtOut)
                ? true : false;
        }

        public bool ValidateScheduleUseNth(object ScheduleUseNth)
        {
            bool blValid = false;
            return bool.TryParse(ScheduleUseNth.ToString(), out blValid) ? true : false;
        }

        public bool ValidateScheduleEvery(object ScheduleEvery)
        {
            int iEvery = -1;
            return int.TryParse(ScheduleEvery.ToString(), out iEvery) ? true : false;
        }

        public bool ValidateScheduleWeekdays(object Weekdays)
        {
            if (Weekdays == null) return false;

            bool blValid = true;

            string[] arWeekdays = Weekdays.ToString().Split(',');

            foreach (string weekday in arWeekdays)
            {
                int iWeekday = -1;
                if (!int.TryParse(weekday, out iWeekday)) { blValid = false; break; }

                iWeekday = Convert.ToInt32(weekday);
                if (iWeekday > 7 || iWeekday < 1) { blValid = false; break; }
            }

            return blValid;
        }

        public bool ValidateScheduleMonthDays(object ScheduleMonthDays)
        {
            if (ScheduleMonthDays == null) return false;

            bool blValid = true;

            string[] arMonthdays = ScheduleMonthDays.ToString().Split(',');

            foreach (string monthday in arMonthdays)
            {
                int iMonthday = -1;
                if (!int.TryParse(monthday, out iMonthday)) { blValid = false; break; }

                iMonthday = Convert.ToInt32(monthday);
                if (iMonthday > 31 || iMonthday < 1) { blValid = false; break; }
            }

            return blValid;
        }

        public bool ValidateScheduleYearMonths(object ScheduleYearMonths)
        {
            if (ScheduleYearMonths == null) return false;

            bool blValid = true;

            string[] arYearMonth = ScheduleYearMonths.ToString().Split(',');

            foreach (string yearmonth in arYearMonth)
            {
                int iYearmonth = -1;
                if (!int.TryParse(yearmonth, out iYearmonth)) { blValid = false; break; }

                iYearmonth = Convert.ToInt32(yearmonth);
                if (iYearmonth > 12 || iYearmonth < 1) { blValid = false; break; }
            }

            return blValid;
        }


        public bool ValidateScheduleMonthlyMode(object ScheduleMonthlyMode)
        {
            return (ScheduleMonthlyMode == null) ? false :
                Array.IndexOf(new string[] { "dates", "nth" }, ScheduleMonthlyMode.ToString()) >= 0
                ? true : false;
        }


        public bool ValidateScheduleNthN(object ScheduleNthN)
        {
            int iN = -10;
            if (!int.TryParse(ScheduleNthN.ToString(), out iN)) return false;

            iN = Convert.ToInt32(ScheduleNthN);

            return Array.IndexOf(new int[] { 1, 2, 3, 4, 5, -1 }, iN) < 0 ? false : true;
        }


        public bool ValidateScheduleNthWhat(object ScheduleNthWhat)
        {
            int iN = -1;
            if (!int.TryParse(ScheduleNthWhat.ToString(), out iN)) return false;

            iN = Convert.ToInt32(ScheduleNthWhat);

            return Array.IndexOf(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, iN) < 0 ? false : true;
        }
    }
}
