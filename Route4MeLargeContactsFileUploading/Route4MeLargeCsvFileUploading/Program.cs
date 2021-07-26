using Route4MeSDK;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Route4MeSDK.FastProcessing;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDK.DataTypes.V5;

namespace CsvFileUploading
{
    class Program
    {
        static void Main(string[] args)
        {
            var stPath = AppDomain.CurrentDomain.BaseDirectory;

            string ActualApiKey = ReadSetting("api_keys.actual_api_key").ToString();
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            bool removeContacts = (args != null && args.Length > 0 && args[0] == "remove") ? true : false;

            if (removeContacts)
            {
                string jsonfile = stPath + @"files\remove_contacts.json";

                using (StreamReader file = File.OpenText(jsonfile))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject o = (JObject)JToken.ReadFrom(reader);
                        var query = o["query"].ToString();
                        var address_ids = JsonConvert.DeserializeObject<List<int>>(o["address_ids"].ToString());

                        var offset = JsonConvert.DeserializeObject<int?>(o["offset"].ToString());
                        var limit = JsonConvert.DeserializeObject<int?>(o["limit"].ToString());

                        if (query.Length>0)
                        {
                            var addressBookParameters = new AddressBookParameters();

                            if (query != null && query.Length > 0) addressBookParameters.Query = query;
                            if (offset != null) addressBookParameters.Offset = (uint)offset;
                            if (limit != null) addressBookParameters.Limit = (uint)limit;

                            var response = route4Me.GetAddressBookContacts(
                                            addressBookParameters,
                                            out ResultResponse resultResponse);

                            int[] addressIDs = response.Results
                                                .Where(x => x.AddressId != null)
                                                .Select(x => (int)x.AddressId)
                                                .ToArray();

                            var removed = route4Me.RemoveAddressBookContacts(addressIDs, out resultResponse);

                            Console.WriteLine(resultResponse == null
                                ? addressIDs.Length + " contacts removed from database"
                                : "Cannot remove " + addressIDs.Length + " contacts." + Environment.NewLine +
                                "Exit code: " + (resultResponse?.ExitCode.ToString() ?? "") + Environment.NewLine +
                                "Code: " + (resultResponse?.Code.ToString() ?? "") + Environment.NewLine +
                                "Status: " + (resultResponse?.Status.ToString() ?? "") + Environment.NewLine
                                );

                            if (resultResponse != null)
                            {
                                foreach (var msg in resultResponse.Messages)
                                {
                                    Console.WriteLine(msg.Key + ": " + msg.Value + Environment.NewLine);
                                }
                            }

                            Console.WriteLine("=======================================");
                        }

                        if (address_ids.Count>0)
                        {
                            var removed = route4Me.RemoveAddressBookContacts(address_ids.ToArray(), out ResultResponse resultResponse);

                            Console.WriteLine(resultResponse == null
                                ? address_ids.Count + " contacts removed from database"
                                : "Cannot remove " + address_ids.Count + " contacts." + Environment.NewLine +
                                "Exit code: " + (resultResponse?.ExitCode.ToString() ?? "") + Environment.NewLine +
                                "Code: " + (resultResponse?.Code.ToString() ?? "") + Environment.NewLine +
                                "Status: " + (resultResponse?.Status.ToString() ?? "") + Environment.NewLine
                                );

                            if (resultResponse != null)
                            {
                                foreach (var msg in resultResponse.Messages)
                                {
                                    Console.WriteLine(msg.Key + ": " + msg.Value + Environment.NewLine);
                                }
                            }

                            Console.WriteLine("=======================================");
                        }
                    }
                }

                //Console.WriteLine("Remove Command");
            }
            else
            {
                var fastProcessing = new FastBulkGeocoding(ActualApiKey, false)
                {
                    ChankPause = Convert.ToInt32(ReadSetting("chank_setting.chunk_pause")),
                    CsvChankSize = Convert.ToInt32(ReadSetting("chank_setting.chunk_size"))
                };

                var csvAddressMapping = (ReadSetting("csv_address_mapping") as Dictionary<string, object>)
                                            .ToDictionary(k => k.Key, k => k.Value.ToString());

                FastFileReading.csvAddressMapping = csvAddressMapping;

                var inputFilenames = ReadInputFileNames();

                if (inputFilenames == null)
                {
                    Console.WriteLine("There are no input file names in the file input_files.txt");
                    return;
                }

                foreach (string inputFileName in inputFilenames)
                {
                    Console.WriteLine(Environment.NewLine + "===== Input file: " + inputFileName + "======");
                    string fileName = (inputFileName.Contains(@"\") || inputFileName.Contains(@"/"))
                        ? inputFileName
                        : stPath + @"files\" + inputFileName;

                    Console.WriteLine("Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                    fastProcessing.uploadLargeContactsCsvFile(fileName, out string errorString);

                    Console.WriteLine("End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                }
            }

            Console.WriteLine(Environment.NewLine + "Press any key to close the window");
            string v = Console.ReadLine();
        }

        /// <summary>
        /// Reads settings from the file appsettings.json
        /// </summary>
        /// <param name="key_path">path to a setting key</param>
        /// <returns>A value of a specified key</returns>
        static object ReadSetting(string key_path)
        {
            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            if (key_path.Contains('.'))
            {
                string[] paths = key_path.Split('.');

                var apiKeys = config.GetSection(paths[0]).Get(typeof(Dictionary<string, object>)) as Dictionary<string, object>;

                object keyValue = apiKeys.ContainsKey(paths[1]) ? apiKeys[paths[1]] : null;
                return keyValue;
            }
            else
            {
                object dict = config.GetSection(key_path).Get(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                return dict;
            }
        }

        static string[] ReadInputFileNames()
        {
            var stPath = AppDomain.CurrentDomain.BaseDirectory;

            var fileNames = System.IO.File.ReadAllLines(stPath + @"files\input_files.txt");

            return fileNames;
        }
    }
}
