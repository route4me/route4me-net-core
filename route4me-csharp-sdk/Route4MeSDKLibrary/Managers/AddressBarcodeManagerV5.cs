using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDKLibrary.Managers
{
    public class AddressBarcodeManagerV5 : Route4MeManagerBase
    {
        public AddressBarcodeManagerV5(string apiKey) : base(apiKey)
        {
        }

        public AddressBarcodeManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        /// <summary>
        ///     Returns address barcodes
        /// </summary>
        /// <param name="getAddressBarcodesParameters">Request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An <see cref="GetAddressBarcodesResponse" /> type object</returns>
        public GetAddressBarcodesResponse GetAddressBarcodes(GetAddressBarcodesParameters getAddressBarcodesParameters,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<GetAddressBarcodesResponse>(getAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address barcodes
        /// </summary>
        /// <param name="getAddressBarcodesParameters">Request parameters</param>
        /// <returns>An <see cref="GetAddressBarcodesResponse" /> type object and <see cref="ResultResponse"/> type object</returns>
        public Task<Tuple<GetAddressBarcodesResponse, ResultResponse>> GetAddressBarcodesAsync(GetAddressBarcodesParameters getAddressBarcodesParameters)
        {
            var response = GetJsonObjectFromAPIAsync<GetAddressBarcodesResponse>(getAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Get);

            return response;
        }

        /// <summary>
        ///     Saves address bar codes
        /// </summary>
        /// <param name="saveAddressBarcodesParameters">The contact parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public StatusResponse SaveAddressBarcodes(SaveAddressBarcodesParameters saveAddressBarcodesParameters,
            out ResultResponse resultResponse)
        {
            saveAddressBarcodesParameters.PrepareForSerialization();

            return GetJsonObjectFromAPI<StatusResponse>(saveAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Saves address bar codes
        /// </summary>
        /// <param name="saveAddressBarcodesParameters">The contact parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> SaveAddressBarcodesAsync(SaveAddressBarcodesParameters saveAddressBarcodesParameters)
        {
            saveAddressBarcodesParameters.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<StatusResponse>(saveAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Post);
        }
    }
}