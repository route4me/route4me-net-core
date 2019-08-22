using Route4MeDB.ApplicationCore.Interfaces;

namespace Route4MeDB.ApplicationCore.Services
{
    public class UriComposer : IUriComposer
    {
        private readonly Route4MeDbSettings _route4meDbSettings;

        public UriComposer(Route4MeDbSettings route4meDbSettings) => _route4meDbSettings = route4meDbSettings;

        public string ComposePicUri(string uriTemplate)
        {
            //return uriTemplate.Replace("http://catalogbaseurltobereplaced", _route4meDbSettings.AddressBookContactBaseUrl);
            return null;
        }
    }
}

