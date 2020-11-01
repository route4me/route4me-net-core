using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add a scheduled address book contact
        /// </summary>
        public void AddScheduledAddressBookContact()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var lsRemoveContacts = new List<string>();

            #region // Add a location, scheduled daily.
            Schedule sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            AddressBookContact scheduledContact1 = new AddressBookContact()
            {
                Address1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                AddressAlias = "1604 PARKRIDGE PKWY 40214",
                AddressGroup = "Scheduled daily",
                FirstName = "Peter",
                LastName = "Newman",
                AddressEmail = "pnewman6564@yahoo.com",
                AddressPhoneNumber = "65432178",
                CachedLat = 38.141598,
                CachedLng = -85.793846,
                AddressCity = "Louisville",
                AddressCustomData = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "publishing" } },
                Schedule = new List<Schedule>() { sched1 }
            };

            var scheduledContact1Response = route4Me
                .AddAddressBookContact(scheduledContact1, out string errorString);

            int location1 = scheduledContact1Response.AddressId != null 
                ? Convert.ToInt32(scheduledContact1Response.AddressId) 
                : -1;

            if (location1 > 0)
            {
                lsRemoveContacts.Add(location1.ToString());
                Console.WriteLine("A location with the daily scheduling was created. AddressId: {0}", location1);
            }
            else Console.WriteLine("Creating if a location with daily scheduling failed");
            #endregion

            #region // Add a location, scheduled weekly.
            Schedule sched2 = new Schedule("weekly", false)
            {
                Enabled = true,
                Weekly = new ScheduleWeekly(1, new int[] { 1, 2, 3, 4, 5 })
            };

            AddressBookContact scheduledContact2 = new AddressBookContact()
            {
                Address1 = "1407 MCCOY, Louisville, KY, 40215",
                AddressAlias = "1407 MCCOY 40215",
                AddressGroup = "Scheduled weekly",
                FirstName = "Bart",
                LastName = "Douglas",
                AddressEmail = "bdouglas9514@yahoo.com",
                AddressPhoneNumber = "95487454",
                CachedLat = 38.202496,
                CachedLng = -85.786514,
                AddressCity = "Louisville",
                ServiceTime = 600,
                Schedule = new List<Schedule>() { sched2 }
            };

            var scheduledContact2Response = route4Me.AddAddressBookContact(scheduledContact2, out errorString);

            int location2 = scheduledContact2Response.AddressId != null ? Convert.ToInt32(scheduledContact2Response.AddressId) : -1;

            if (location2 > 0)
            {
                lsRemoveContacts.Add(location2.ToString());
                Console.WriteLine("A location with the weekly scheduling was created. AddressId: {0}", location2);
            }
            else Console.WriteLine("Creating if a location with weekly scheduling failed");

            #endregion

            #region // Add a location, scheduled monthly (dates mode).
            Schedule sched3 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(_every: 1, _mode: "dates", _dates: new int[] { 20, 22, 23, 24, 25 })
            };

            AddressBookContact scheduledContact3 = new AddressBookContact()
            {
                Address1 = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                Address2 = "4806 BELLEVUE AVE, Louisville, KY, 40215",
                AddressAlias = "4805 BELLEVUE AVE 40215",
                AddressGroup = "Scheduled monthly",
                FirstName = "Bart",
                LastName = "Douglas",
                AddressEmail = "bdouglas9514@yahoo.com",
                AddressPhoneNumber = "95487454",
                CachedLat = 38.178844,
                CachedLng = -85.774864,
                AddressCountryId = "US",
                AddressStateId = "KY",
                AddressZip = "40215",
                AddressCity = "Louisville",
                ServiceTime = 750,
                Schedule = new List<Schedule>() { sched3 },
                Color = "red"
            };

            var scheduledContact3Response = route4Me.AddAddressBookContact(scheduledContact3, out errorString);

            int location3 = scheduledContact3Response.AddressId != null ? Convert.ToInt32(scheduledContact3Response.AddressId) : -1;

            if (location3 > 0)
            {
                lsRemoveContacts.Add(location3.ToString());
                Console.WriteLine("A location with the monthly scheduling (mode 'dates') was created. AddressId: {0}", location3);
            }
            else Console.WriteLine("Creating if a location with monthly scheduling (mode 'dates') failed");
            #endregion

            #region // Add a location, scheduled monthly (nth mode).
            Schedule sched4 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(_every: 1, _mode: "nth", _nth: new Dictionary<int, int>() { { 1, 4 } })
            };

            AddressBookContact scheduledContact4 = new AddressBookContact()
            {
                Address1 = "730 CECIL AVENUE, Louisville, KY, 40211",
                AddressAlias = "730 CECIL AVENUE 40211",
                AddressGroup = "Scheduled monthly",
                FirstName = "David",
                LastName = "Silvester",
                AddressEmail = "dsilvester5874@yahoo.com",
                AddressPhoneNumber = "36985214",
                CachedLat = 38.248684,
                CachedLng = -85.821121,
                AddressCity = "Louisville",
                ServiceTime = 450,
                AddressCustomData = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "library" } },
                Schedule = new List<Schedule>() { sched4 },
                AddressIcon = "emoji/emoji-bus"
            };

            var scheduledContact4Response = route4Me.AddAddressBookContact(scheduledContact4, out errorString);

            int location4 = scheduledContact4Response.AddressId != null ? Convert.ToInt32(scheduledContact4Response.AddressId) : -1;

            if (location4 > 0)
            {
                lsRemoveContacts.Add(location4.ToString());
                Console.WriteLine("A location with the monthly scheduling (mode 'nth') was created. AddressId: {0}", location4);
            }
            else Console.WriteLine("Creating if a location with monthly scheduling (mode 'nth') failed");
            #endregion

            #region // Add a location with the daily scheduling and blacklist.
            Schedule sched5 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            AddressBookContact scheduledContact5 = new AddressBookContact()
            {
                Address1 = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                AddressAlias = "4629 HILLSIDE DRIVE 40216",
                AddressGroup = "Scheduled daily",
                FirstName = "Kim",
                LastName = "Shandor",
                AddressEmail = "kshand8524@yahoo.com",
                AddressPhoneNumber = "9874152",
                CachedLat = 38.176067,
                CachedLng = -85.824638,
                AddressCity = "Louisville",
                AddressCustomData = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "appliance" } },
                Schedule = new List<Schedule>() { sched5 },
                ScheduleBlacklist = new string[] { "2017-12-22", "2017-12-23" },
                ServiceTime = 300
            };

            var scheduledContact5Response = route4Me.AddAddressBookContact(scheduledContact5, out errorString);

            int location5 = scheduledContact5Response.AddressId != null ? Convert.ToInt32(scheduledContact5Response.AddressId) : -1;

            if (location5 > 0)
            {
                lsRemoveContacts.Add(location5.ToString());
                Console.WriteLine("A location with the blacklist was created. AddressId: {0}", location5);
            }
            else Console.WriteLine("Creating of a location with the blacklist failed");
            #endregion

            var removed = route4Me.RemoveAddressBookContacts(lsRemoveContacts.ToArray(), out errorString);

            if ((bool)removed)
                Console.WriteLine("The added testing address book locations were removed successfuly");
            else
                Console.WriteLine("Removing of the added testing address book locations failed");
        }
    }
}
