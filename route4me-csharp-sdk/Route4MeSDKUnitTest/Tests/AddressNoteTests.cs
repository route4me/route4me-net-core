using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class AddressNoteTests
    {
        [Test]
        public void DeserializeAddressNoteWithNullLatLngTest()
        {
            // lat/lng are nullable in the DB, so a note can come back with null coordinates.
            var json = "{\"note_id\":1,\"route_id\":\"r\",\"route_destination_id\":2,\"lat\":null,\"lng\":null}";

            var note = R4MeUtils.ReadObjectNew<AddressNote>(json);

            Assert.IsNotNull(note);
            Assert.IsNull(note.Latitude);
            Assert.IsNull(note.Longitude);
        }
    }
}
