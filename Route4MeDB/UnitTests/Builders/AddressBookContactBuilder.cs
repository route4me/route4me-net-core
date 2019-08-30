using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;

namespace Route4MeDB.UnitTests.Builders
{
    public class AddressBookContactBuilder
    {
        private AddressBookContact _addressBookContact;
        public readonly TestData testData = new TestData();

        public class TestData
        {
            public string Address1 => "1604 PARKRIDGE PKWY, Louisville, KY, 40214";
            public string AddressAlias => "1604 PARKRIDGE PKWY 40214";
            public string AddressGroup => "Scheduled daily";

            public string FirstName => "Peter";
            public string LastName => "Newman";
            public string AddressEmail => "pnewman6564@yahoo.com";
            public string AddressPhoneNumber => "65432178";
            public double CachedLat => 38.141598;
            public double CachedLng => -85.793846;
            public string AddressCity => "Louisville";
            public string AddressCustomData = JsonConvert.SerializeObject( new Dictionary<string, string>()
                { { "scheduled", "yes" }, { "service type", "publishing" } });
            public Schedule[] Schedules = new Schedule[]
             {
                new Schedule()
                {
                    Enabled = true,
                    Mode = "daily",
                    Daily = new ScheduleDaily(1)
                }
             };
            public string[] ScheduleBlacklist = new string[]
            {
                "2019-07-21", "2019-07-22", "2019-07-23"
            };
        }

        public AddressBookContactBuilder()
        {
            _addressBookContact = WithDefaultValues();
        }

        public AddressBookContact Build()
        {
            return _addressBookContact;
        }

        public AddressBookContact WithDefaultValues()
        {
            _addressBookContact = new AddressBookContact()
            {
                //Id = new Guid(),
                Address1 = testData.Address1,
                AddressAlias = testData.AddressAlias,
                AddressGroup = testData.AddressGroup,
                FirstName = testData.FirstName,
                LastName = testData.LastName,
                AddressEmail = testData.AddressEmail,
                AddressPhoneNumber = testData.AddressPhoneNumber,
                CachedLat = testData.CachedLat,
                CachedLng = testData.CachedLng,
                AddressCity = testData.AddressCity
            };

            return _addressBookContact;
        }

        public AddressBookContact WithCustomData()
        {
            _addressBookContact = WithDefaultValues();
            _addressBookContact.AddressCustomData = testData.AddressCustomData;
            return _addressBookContact;
        }

        public AddressBookContact WithSchedule()
        {
            _addressBookContact = WithDefaultValues();
            _addressBookContact.Schedules = testData.Schedules;
            return _addressBookContact;
        }

        public AddressBookContact WithScheduleBlacklist()
        {
            _addressBookContact = WithDefaultValues();
            _addressBookContact.ScheduleBlackList = testData.ScheduleBlacklist;
            return _addressBookContact;
        }
    }
}
