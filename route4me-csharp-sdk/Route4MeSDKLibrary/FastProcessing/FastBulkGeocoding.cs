using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client.Transports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using AddressBookContact = Route4MeSDK.DataTypes.V5.AddressBookContact;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;
using Socket = Quobject.SocketIoClientDotNet.Client.Socket;

namespace Route4MeSDK.FastProcessing
{
    /// <summary>
    ///     The class for the geocoding of bulk addresses
    /// </summary>
    public class FastBulkGeocoding : Connection
    {
        private FastFileReading _fileReading;
        private bool _geocodedAddressesDownloadingIsDone;
        private bool _largeJsonFileProcessingIsDone;
        private int _loadedAddressesCount;
        private int _nextDownloadStage;
        private int _requestedAddresses;
        private Socket _socket;
        private int _totalCsvChunks;
        private readonly string _apiKey;

        public int CsvChunkSize { get; set; } = 300;
        public int JsonChunkSize { get; set; } = 300;

        public string[] MandatoryFields { get; set; }

        public bool DoGeocoding { get; set; } = false;

        /// <summary>
        ///     Geocode only addresses with empty coordinates (latitude longitude)
        /// </summary>
        public bool GeocodeOnlyEmpty { get; set; } = false;

        public FastBulkGeocoding(string apiKey, bool enableTraceSource = false)
        {
            if (apiKey != "") _apiKey = apiKey;
            Quobject.SocketIoClientDotNet.TraceSourceTools.LogTraceSource.TraceSourceLogging(enableTraceSource);
        }

        /// <summary>
        ///     Upload and geocode large JSON file
        /// </summary>
        /// <param name="fileName">JSON file name</param>
        public void UploadAndGeocodeLargeJsonFile(string fileName)
        {
            _largeJsonFileProcessingIsDone = false;

            _fileReading = new FastFileReading();

            _fileReading.jsonObjectsChunkSize = 200;

            _fileReading.JsonFileChunkIsReady += OnJsonFileChunkIsReady;

            _fileReading.JsonFileReadingIsDone += OnJsonFileReadingIsDone;

            _fileReading.readingChunksFromLargeJsonFile(fileName);
        }

        /// <summary>
        ///     Upload and geocode large JSON file
        /// </summary>
        /// <param name="fileName">JSON file name</param>
        public Task UploadAndGeocodeLargeJsonFileAsync(string fileName)
        {
            _largeJsonFileProcessingIsDone = false;

            _fileReading = new FastFileReading();

            _fileReading.jsonObjectsChunkSize = 200;

            _fileReading.JsonFileChunkIsReadyAsync += OnJsonFileChunkIsReadyAsync;

            _fileReading.JsonFileReadingIsDone += OnJsonFileReadingIsDone;

            return _fileReading.ReadingChunksFromLargeJsonFileAsync(fileName);
        }

        /// <summary>
        ///     Event handler for the JsonFileReadingIsDone event
        /// </summary>
        /// <param name="sender">Event raiser object</param>
        /// <param name="e">Event arguments of the type JsonFileReadingIsDoneArgs</param>
        private void OnJsonFileReadingIsDone(object sender, FastFileReading.JsonFileReadingIsDoneArgs e)
        {
            var isDone = e.IsDone;
            if (isDone)
            {
                _largeJsonFileProcessingIsDone = true;
                if (_geocodedAddressesDownloadingIsDone)
                    OnGeocodingIsFinished(new GeocodingIsFinishedArgs {isFinished = true});
                // fire here event for external (test) code
            }
        }


        /// <summary>
        ///     Event handler for the JsonFileChunkIsReady event
        /// </summary>
        /// <param name="sender">Event raiser object</param>
        /// <param name="e">Event arguments of the type JsonFileChunkIsReadyArgs</param>
        private void OnJsonFileChunkIsReady(object sender, FastFileReading.JsonFileChunkIsReadyArgs e)
        {
            var jsonAddressesChunk = e.AddressesChunk;

            var uploadAddressesResponse = UploadAddressesToTemporaryStorage(jsonAddressesChunk);

            if (uploadAddressesResponse != null)
            {
                var tempAddressesStorageID = uploadAddressesResponse.OptimizationProblemId;
                var addressesInChunk = (int) uploadAddressesResponse.AddressCount;

                if (addressesInChunk < _fileReading.jsonObjectsChunkSize)
                    _requestedAddresses = addressesInChunk; // last chunk

                DownloadGeocodedAddresses(tempAddressesStorageID, addressesInChunk);
            }
        }

        private async Task OnJsonFileChunkIsReadyAsync(FastFileReading.JsonFileChunkIsReadyArgs arg)
        {
            var jsonAddressesChunk = arg.AddressesChunk;

            var uploadAddressesResponse = await UploadAddressesToTemporaryStorageAsync(jsonAddressesChunk).ConfigureAwait(false);

            if (uploadAddressesResponse != null)
            {
                var tempAddressesStorageID = uploadAddressesResponse.OptimizationProblemId;
                var addressesInChunk = (int)uploadAddressesResponse.AddressCount;

                if (addressesInChunk < _fileReading.jsonObjectsChunkSize)
                    _requestedAddresses = addressesInChunk; // last chunk

                DownloadGeocodedAddresses(tempAddressesStorageID, addressesInChunk);
            }
        }

        public void UploadLargeContactsCsvFile(string fileName, out string errorString)
        {
            errorString = null;
            _totalCsvChunks = 0;

            if (!File.Exists(fileName))
            {
                errorString = "The file " + fileName + " doesn't exist.";
                return;
            }

            _fileReading = new FastFileReading();

            _fileReading.csvObjectsChunkSize = CsvChunkSize;
            _fileReading.jsonObjectsChunkSize = JsonChunkSize;

            _fileReading.CsvFileChunkIsReady += FileReading_CsvFileChunkIsReady;

            _fileReading.CsvFileReadingIsDone += FileReading_CsvFileReadingIsDone;

            _fileReading.readingChunksFromLargeCsvFile(fileName, out errorString);

            if ((errorString?.Length ?? 0) > 0)
                Console.WriteLine("Contacts file uploading canceled:" + Environment.NewLine + errorString);
        }

        private void FileReading_CsvFileReadingIsDone(object sender, FastFileReading.CsvFileReadingIsDoneArgs e)
        {
            var isDone = e.IsDone;
            if (isDone)
            {
                Parallel.ForEach(e.Packages,
                    new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount}, CsvFileChunkIsReady);
            }
        }

        private void FileReading_CsvFileChunkIsReady(object sender, FastFileReading.CsvFileChunkIsReadyArgs e)
        {
            if (e.TotalResult.Count > 15)
            {
                Parallel.ForEach(e.TotalResult,
                    new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount}, CsvFileChunkIsReady);

                e.TotalResult.Clear();
            }
        }

        private void CsvFileChunkIsReady(List<AddressBookContact> contactsChunk)
        {
            var route4Me = new Route4MeManagerV5(_apiKey);
            var route4MeV4 = new Route4MeManager(_apiKey);

            if (DoGeocoding && contactsChunk != null)
            {
                var contactsToGeocode = new Dictionary<int, AddressBookContact>();

                if (GeocodeOnlyEmpty)
                {
                    var emptyContacts = contactsChunk.Where(x => x.CachedLat == 0 && x.CachedLng == 0);
                    if (emptyContacts != null)
                        foreach (var econt in emptyContacts)
                            contactsToGeocode.Add(contactsChunk.IndexOf(econt), econt);
                }
                else
                {
                    for (var i = 0; i < (contactsChunk?.Count ?? 0); i++) contactsToGeocode.Add(i, contactsChunk[i]);
                }

                var lsAddressesToGeocode = contactsToGeocode
                    .Select(x => x.Value)
                    .Select(x => x.Address1 +
                                 ((x?.AddressCity?.Length ?? 0) > 0 ? ", " + x.AddressCity : "") +
                                 ((x?.AddressStateId?.Length ?? 0) > 0 ? ", " + x.AddressStateId : "") +
                                 ((x?.AddressZip?.Length ?? 0) > 0 ? ", " + x.AddressZip : "") +
                                 ((x?.AddressCountryId?.Length ?? 0) > 0 ? ", " + x.AddressCountryId : "")
                    )
                    .ToList();

                var addressesToGeocode = string.Join(Environment.NewLine, lsAddressesToGeocode);

                var geoParams = new GeocodingParameters
                {
                    Addresses = addressesToGeocode,
                    ExportFormat = "json"
                };

                var geocodedAddresses = route4MeV4.BatchGeocoding(geoParams, out var errorString);

                if ((geocodedAddresses?.Length ?? 0) > 50)
                {
                    var geocodedObjects =
                        JsonConvert.DeserializeObject<GeocodingResponse[]>(geocodedAddresses).ToList();

                    // If returned objects not equal to input contacts, remove with duplicated original
                    if (geocodedObjects != null && geocodedObjects.Count > contactsToGeocode.Count)
                    {
                        var dupicates = new List<GeocodingResponse>();

                        for (var i = 1; i < geocodedObjects.Count; i++)
                            if (geocodedObjects[i].Original == geocodedObjects[i - 1].Original)
                                dupicates.Add(geocodedObjects[i]);

                        foreach (var duplicate in dupicates) geocodedObjects.Remove(duplicate);
                    }

                    if (geocodedObjects != null && geocodedObjects.Count == contactsToGeocode.Count)
                    {
                        var indexList = contactsToGeocode.Keys.ToList();

                        for (var i = 0; i < geocodedObjects.Count; i++)
                        {
                            contactsChunk[indexList[i]].CachedLat = geocodedObjects[i].Lat;
                            contactsChunk[indexList[i]].CachedLng = geocodedObjects[i].Lng;
                        }
                    }
                }
            }

            var contactParams = new Route4MeManagerV5.BatchCreatingAddressBookContactsRequest
            {
                Data = contactsChunk.ToArray()
            };

            var response = route4Me.BatchCreateAdressBookContacts(
                contactParams,
                MandatoryFields,
                out var resultResponse);

            if (response?.status ?? false) _totalCsvChunks += contactsChunk.Count;

            Console.WriteLine(
                response?.status ?? false
                    ? _totalCsvChunks + " address book contacts added to database"
                    : "Faild to add " + contactsChunk.Count + " address book contacts");

            if (!(response?.status ?? false))
            {
                Console.WriteLine("Exit code: " + resultResponse.ExitCode + Environment.NewLine +
                                  "Code: " + resultResponse.Code + Environment.NewLine +
                                  "Status: " + resultResponse.Status + Environment.NewLine
                );

                foreach (var msg in resultResponse.Messages)
                    Console.WriteLine(msg.Key + ": " + string.Join(", ", msg.Value));

                Console.WriteLine("Start address: " + contactsChunk[0].Address1);
                Console.WriteLine("End address: " + contactsChunk[contactsChunk.Count - 1].Address1);
                Console.WriteLine("-------------------------------");
            }
        }

        /// <summary>
        ///     Upload JSON addresses to a temporary storage
        /// </summary>
        /// <param name="streamSource">Input stream source - file name or JSON text</param>
        /// <returns>Response object of the type uploadAddressesToTemporaryStorageResponse</returns>
        public Route4MeManager.UploadAddressesToTemporaryStorageResponse UploadAddressesToTemporaryStorage(
            string streamSource)
        {
            var route4Me = new Route4MeManager(_apiKey);

            string jsonText;

            if (streamSource.Contains("{") && streamSource.Contains("}"))
                jsonText = streamSource;
            else
                jsonText = ReadJsonTextFromLargeJsonFileOfAddresses(streamSource);

            var uploadResponse =
                route4Me.UploadAddressesToTemporaryStorage(jsonText, out _);


            if (uploadResponse == null || !uploadResponse.Status) return null;

            return uploadResponse;
        }

        /// <summary>
        ///     Upload JSON addresses to a temporary storage
        /// </summary>
        /// <param name="streamSource">Input stream source - file name or JSON text</param>
        /// <returns>Response object of the type uploadAddressesToTemporaryStorageResponse</returns>
        public async Task<Route4MeManager.UploadAddressesToTemporaryStorageResponse> UploadAddressesToTemporaryStorageAsync(
            string streamSource)
        {
            var route4Me = new Route4MeManager(_apiKey);

            string jsonText;

            if (streamSource.Contains("{") && streamSource.Contains("}"))
                jsonText = streamSource;
            else
                jsonText = ReadJsonTextFromLargeJsonFileOfAddresses(streamSource);

            var uploadResponse =
                await route4Me.UploadAddressesToTemporaryStorageAsync(jsonText).ConfigureAwait(false);


            if (uploadResponse == null || !uploadResponse.Item1.Status) return null;

            return uploadResponse.Item1;
        }

        /// <summary>
        ///     Geocode and download the addresses from the temporary storage.
        /// </summary>
        /// <param name="temporaryAddressesStorageID">ID of the temporary storage</param>
        /// <param name="addressesInFile">Chunk size of the addresses to be geocoded</param>
        public void DownloadGeocodedAddresses(string temporaryAddressesStorageID, int addressesInFile)
        {
            //bool done = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12
                                                   | (SecurityProtocolType) 12288;

            _geocodedAddressesDownloadingIsDone = false;

            var savedAddresses = new List<AddressGeocoded>();

            _requestedAddresses = addressesInFile;

            using (var manualResetEvent = new ManualResetEvent(false))
            {
                var options = CreateOptions();
                options.Path = "/socket.io";
                options.Host = "validator.route4me.com/";
                options.AutoConnect = true;
                options.IgnoreServerCertificateValidation = true;
                options.Timeout = 60000;
                options.Upgrade = true;
                options.ForceJsonp = true;
                options.Transports = ImmutableList.Create(Polling.NAME, WebSocket.NAME);


                var uri = CreateUri();
                _socket = IO.Socket(uri, options);


                _socket.On("error", message =>
                {
                    Debug.Print("Error -> " + message);
                    manualResetEvent.Set();
                    _socket.Disconnect();
                });

                _socket.On(Socket.EVENT_ERROR, e =>
                {
                    var exception = (EngineIOException)e;
                    Console.WriteLine("EVENT_ERROR. " + exception.Message);
                    Console.WriteLine("BASE EXCEPTION. " + exception.GetBaseException());
                    Console.WriteLine("DATA COUNT. " + exception.Data.Count);
                    _socket.Disconnect();
                    manualResetEvent.Set();
                });

                _socket.On(Socket.EVENT_MESSAGE, message =>
                {
                });

                _socket.On("data", d =>
                {
                });

                _socket.On(Socket.EVENT_CONNECT, () =>
                {
                });

                _socket.On(Socket.EVENT_DISCONNECT, () =>
                {
                });

                _socket.On(Socket.EVENT_RECONNECT_ATTEMPT, () =>
                {
                });

                _socket.On("addresses_bulk", addresses_chunk =>
                {

                    var jsonChunkText = addresses_chunk.ToString();

                    var errors = new List<string>();

                    var jsonSettings = new JsonSerializerSettings
                    {
                        Error = delegate (object sender, ErrorEventArgs errorArgs)
                        {
                            errors.Add(errorArgs.ErrorContext.Error.Message);
                            errorArgs.ErrorContext.Handled = true;
                        },
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    var addressesChunk = JsonConvert.DeserializeObject<AddressGeocoded[]>(jsonChunkText, jsonSettings);

                    if (errors.Count > 0)
                    {
                        Debug.Print("Json serializer errors:");
                        foreach (var errMessage in errors) Debug.Print(errMessage);
                    }

                    if (addressesChunk != null)
                    {
                        savedAddresses = savedAddresses.Concat(addressesChunk).ToList();
                        _loadedAddressesCount += addressesChunk.Length;
                    }

                    if (_loadedAddressesCount == _nextDownloadStage)
                        Download(_loadedAddressesCount, temporaryAddressesStorageID);

                    if (_loadedAddressesCount == _requestedAddresses)
                    {
                        _socket.Emit("disconnect", temporaryAddressesStorageID);
                        _loadedAddressesCount = 0;
                        var addressesChunkGeocodedArgs = new AddressesChunkGeocodedArgs { lsAddressesChunkGeocoded = savedAddresses };
                        OnAddressesChunkGeocoded(addressesChunkGeocodedArgs);

                        manualResetEvent.Set();

                        _geocodedAddressesDownloadingIsDone = true;

                        if (_largeJsonFileProcessingIsDone)
                            OnGeocodingIsFinished(new GeocodingIsFinishedArgs { isFinished = true });

                        _socket.Close();
                    }
                });

                _socket.On("geocode_progress", message =>
                {
                    var progressMessage = JsonConvert.DeserializeObject<clsProgress>(message.ToString());

                    if (progressMessage.total == progressMessage.done)
                    {
                        if (_requestedAddresses == default) _requestedAddresses = progressMessage.total;
                            Download(0, temporaryAddressesStorageID);
                    }
                });

                var jobj = new JObject();
                jobj.Add("temporary_addresses_storage_id", temporaryAddressesStorageID);
                jobj.Add("force_restart", true);

                var args = new List<object>();
                args.Add(jobj);

                try
                {
                    _socket.Emit("geocode", args);
                }
                catch (Exception ex)
                {
                    Debug.Print("Socket connection failed. " + ex.Message);
                }

                manualResetEvent.WaitOne();
            }
        }

        /// <summary>
        ///     Download chunk of the geocoded addresses
        /// </summary>
        /// <param name="start">Download addresses starting from</param>
        /// <param name="temporaryAddressesStorageId">ID of the temporary storage</param>
        public void Download(int start, string temporaryAddressesStorageId)
        {
            var bufferFailSafeMaxAddresses = 100;
            var chunkSize = (int) Math.Round((decimal) Math.Min(200, Math.Max(10, _requestedAddresses / 100)));
            var chunksLimit = (int) Math.Ceiling((decimal) (bufferFailSafeMaxAddresses / chunkSize));

            var maxAddressesToBeDownloaded = chunkSize * chunksLimit;
            _nextDownloadStage = _loadedAddressesCount + maxAddressesToBeDownloaded;

            // from_index = (chunks_limit * chunk_size);
            var jobj = new JObject();

            jobj.Add("temporary_addresses_storage_id", temporaryAddressesStorageId);
            jobj.Add("from_index", start);
            jobj.Add("chunks_limit", chunksLimit);
            jobj.Add("chunk_size", chunkSize);

            var _args = new List<object>();
            _args.Add(jobj);
            //var data = Quobject.SocketIoClientDotNet.Parser.Packet.Args2JArray(_args);

            _socket.Emit("download", _args);
        }

        public string ReadJsonTextFromLargeJsonFileOfAddresses(string sFileName)
        {
            var fileRead = new FastFileReading();

            return fileRead != null ? fileRead.readJsonTextFromFile(sFileName) : string.Empty;
        }


        #region // Addresses chunk's geocoding is finished event handler

        public event EventHandler<AddressesChunkGeocodedArgs> AddressesChunkGeocoded;

        protected virtual void OnAddressesChunkGeocoded(AddressesChunkGeocodedArgs e)
        {
            var handler = AddressesChunkGeocoded;

            if (handler != null) handler(this, e);
        }

        public class AddressesChunkGeocodedArgs : EventArgs
        {
            public List<AddressGeocoded> lsAddressesChunkGeocoded { get; set; }
        }

        public delegate void AddressesChunkGeocodedEventHandler(object sender, AddressesChunkGeocodedArgs e);

        #endregion

        #region // geocoding is finished event handler

        public event EventHandler<GeocodingIsFinishedArgs> GeocodingIsFinished;

        protected virtual void OnGeocodingIsFinished(GeocodingIsFinishedArgs e)
        {
            var handler = GeocodingIsFinished;

            if (handler != null) handler(this, e);
        }

        public class GeocodingIsFinishedArgs : EventArgs
        {
            public bool isFinished { get; set; }
        }

        public delegate void GeocodingIsFinishedEventHandler(object sender, AddressesChunkGeocodedArgs e);

        #endregion
    }

    /// <summary>
    ///     Response class of the received event about geocoding progress
    /// </summary>
    internal class clsProgress
    {
        public int total { get; set; }

        public int done { get; set; }
    }
}