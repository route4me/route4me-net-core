using Route4MeDB.ApplicationCore.Interfaces;

namespace Route4MeDB.ApplicationCore.Services
{
    public class UriComposer : IUriComposer
    {
        private readonly AddressBookContactSettings _addressBookContactSettings;

        public UriComposer(AddressBookContactSettings addressBookContactSettings) => _addressBookContactSettings = addressBookContactSettings;

        public string ComposePicUri(string uriTemplate)
        {
            return uriTemplate.Replace("http://catalogbaseurltobereplaced", _addressBookContactSettings.AddressBookContactBaseUrl);
        }
    }
}

