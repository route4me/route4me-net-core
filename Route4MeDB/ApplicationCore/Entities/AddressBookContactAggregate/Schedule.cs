using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    [Owned]
    public class Schedule
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

        public ScheduleDaily Daily { get; set; }

        //[NotMapped]
        //public ScheduleDaily DailyObject
        //{
        //    get { return Daily == null ? null : JsonConvert.DeserializeObject<ScheduleDaily>(Daily); }
        //    set { Daily = JsonConvert.SerializeObject(value); }
        //}

        public ScheduleWeekly Weekly { get; set; }

        //[NotMapped]
        //public ScheduleWeekly WeeklyObject
        //{
        //    get { return Weekly == null ? null : JsonConvert.DeserializeObject<ScheduleWeekly>(Weekly); }
        //    set { Weekly = JsonConvert.SerializeObject(value); }
        //}

        public ScheduleMonthly Monthly { get; set; }

        //[NotMapped]
        //public ScheduleMonthly MonthlyObject
        //{
        //    get { return Monthly == null ? null : JsonConvert.DeserializeObject<ScheduleMonthly>(Monthly); }
        //    set { Monthly = JsonConvert.SerializeObject(value); }
        //}

        public ScheduleAnnually Annually { get; set; }

        //[NotMapped]
        //public ScheduleAnnually AnnuallyObject
        //{
        //    get { return Annually == null ? null : JsonConvert.DeserializeObject<ScheduleAnnually>(Annually); }
        //    set { Annually = JsonConvert.SerializeObject(value); }
        //}

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
