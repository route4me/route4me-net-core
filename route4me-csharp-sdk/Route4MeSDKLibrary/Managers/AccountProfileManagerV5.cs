using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.Managers
{
    public sealed class AccountProfileManagerV5 : Route4MeManagerBase
    {
        public AccountProfileManagerV5(string apiKey) : base(apiKey)
        {
        }

        public AccountProfileManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        /// <summary>
        ///     Get account profile
        /// </summary>
        /// <param name="failResponse">Error response</param>
        /// <returns>Account profile</returns>
        public AccountProfile GetAccountProfile(out ResultResponse failResponse)
        {
            var parameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<AccountProfile>(parameters,
                R4MEInfrastructureSettingsV5.AccountProfile,
                HttpMethodType.Get,
                out failResponse);

            return result;
        }

        /// <summary>
        ///     Get account profile
        /// </summary>
        /// <returns>Account profile</returns>
        public Task<Tuple<AccountProfile, ResultResponse>> GetAccountProfileAsync()
        {
            var parameters = new GenericParameters();

            return GetJsonObjectFromAPIAsync<AccountProfile>(parameters,
                R4MEInfrastructureSettingsV5.AccountProfile,
                HttpMethodType.Get);
        }

        [Obsolete]
        public string GetAccountPreferredUnit(out ResultResponse failResponse)
        {
            var accountProfile = GetAccountProfile(out failResponse);

            var ownerId = accountProfile.RootMemberId;

            var r4Me = new Route4MeManager(ApiKey);

            var memPars = new MemberParametersV4 { MemberId = ownerId };

            var user = r4Me.GetUserById(memPars, out _);

            var prefUnit = user.PreferredUnits;

            return prefUnit;
        }

        [Obsolete]
        public async Task<Tuple<string, ResultResponse>> GetAccountPreferredUnitAsync()
        {
            var accountProfile = await GetAccountProfileAsync().ConfigureAwait(false);

            var ownerId = accountProfile.Item1.RootMemberId;

            var r4Me = new Route4MeManager(ApiKey);

            var memPars = new MemberParametersV4 { MemberId = ownerId };

            var user = await r4Me.GetUserByIdAsync(memPars).ConfigureAwait(false);

            var prefUnit = user.Item1.PreferredUnits;

            return new Tuple<string, ResultResponse>(prefUnit, accountProfile.Item2);
        }
    }
}