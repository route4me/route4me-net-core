using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.TelematicsPlatform;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDKLibrary.Managers
{
    public class TelematicsManagerV5 : Route4MeManagerBase
    {
        public TelematicsManagerV5(string apiKey) : base(apiKey)
        {
        }

        #region Connection

        /// <summary>
        ///     Get all registered telematics connections.
        /// </summary>
        /// <param name="resultResponse">Error response</param>
        /// <returns>An array of the Connection type objects</returns>
        public Connection[] GetTelematicsConnections(out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection[]>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get all registered telematics connections.
        /// </summary>
        /// <returns>An array of the Connection type objects</returns>
        public Task<Tuple<Connection[], ResultResponse>> GetTelematicsConnectionsAsync()
        {
            return GetJsonObjectFromAPIAsync<Connection[]>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get a telematics connection by specified access token
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection GetTelematicsConnectionByToken(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get a telematics connection by specified access token
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>A connection type object</returns>
        public Task<Tuple<Connection, ResultResponse>> GetTelematicsConnectionByTokenAsync(ConnectionParameters connectionParams)
        {
            return GetJsonObjectFromAPIAsync<Connection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Register a telematics connection.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection RegisterTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Register a telematics connection.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>A connection type object</returns>
        public Task<Tuple<Connection, ResultResponse>> RegisterTelematicsConnectionAsync(ConnectionParameters connectionParams)
        {
            return GetJsonObjectFromAPIAsync<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Delete telematics connection account by specified access token.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Removed teleamtics connection</returns>
        public Connection DeleteTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<Connection>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Delete telematics connection account by specified access token.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>Removed teleamtics connection</returns>
        public Task<Tuple<Connection, ResultResponse>> DeleteTelematicsConnectionAsync(ConnectionParameters connectionParams)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                return Task.FromResult(new Tuple<Connection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<Connection>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Update telemetics connection's account
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated teleamtics connection</returns>
        public Connection UpdateTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Put,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update telemetics connection's account
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>Updated teleamtics connection</returns>
        public Task<Tuple<Connection, ResultResponse>> UpdateTelematicsConnectionAsync(ConnectionParameters connectionParams)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                return Task.FromResult(new Tuple<Connection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Put);
        }

        #endregion
    }
}
