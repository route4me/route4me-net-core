using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System.Linq;
using Xunit;

namespace Route4MeDB.UnitTests.ApplicationCore.Entities.AddressBookContactTests
{
    public class AddressBookContactInitializingTest
    {
        [Fact]
        public void CreateEmptyAddressBookContact()
        {
            var newContact = new AddressBookContact();
            Assert.Null(newContact.Address1);
            Assert.Null(newContact.Address2);
            Assert.Null(newContact.AddressAlias);
            Assert.True(double.TryParse(newContact.CachedLat.ToString(), out _));
            Assert.True(double.TryParse(newContact.CachedLng.ToString(), out _));
            Assert.True(int.TryParse(newContact.AddressDbId.ToString(), out _));
        }

        [Fact]
        public void CreateAddressBookContactWithOnlyMandatoryFields()
        {
            var newContact = new AddressBookContact("1358 E Luzerne St, Philadelphia, PA 19124, US", 48.335991, 31.18287
                );

            Assert.Equal("1358 E Luzerne St, Philadelphia, PA 19124, US", newContact.Address1);
            Assert.Null(newContact.Address2);
            Assert.Null(newContact.AddressAlias);
            Assert.Equal(48.335991, newContact.CachedLat);
            Assert.Equal(48.335991, newContact.CachedLat);
        }
    }
}
