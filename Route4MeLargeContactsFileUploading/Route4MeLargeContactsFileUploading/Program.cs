using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.FastProcessing;
using Route4MeSDK.QueryTypes.V5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Route4MeLargeContactsFileUploading
{
    class Program
    {
        static void Main(string[] args)
        {
            var stPath = AppDomain.CurrentDomain.BaseDirectory;

            //args = new string[] { "geocode", "only_empty" };
            //args = new string[] { "geocode", "all" };
            //args = new string[] { "remove", "" };

            //args = new string[] {
            //    "--API_KEY",
            //    "51d0c0701ce83855c9f62d0440096e7c",
            //    "--CSV",
            //    "files/for_address_removing.csv",
            //    "--COLUMN_NAME",
            //    "uid"
            //};

            string ActualApiKey = ReadSetting("api_keys.actual_api_key").ToString();
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            bool removeContacts = (args != null && args.Length > 0 && args[0] == "remove") ? true : false;

            bool validateContacts = (args != null && args.Length > 0 && args[0] == "validate") ? true : false;

            bool geocode = (args != null && args.Length > 0 && args[0] == "geocode") ? true : false;
            bool only_empty = (args != null && args.Length > 1 && args[1] == "only_empty") ? true : false;

            bool alternativeRemoveCommand = false;

            if (IsAlternativeArgs(args))
            {
                if (args.Length != 6)
                {
                    Console.WriteLine($"Remove command canceled.{Environment.NewLine}Alternative CMD command args should be 4");
                    return;
                }

                if (args[0].ToLower() != "--api_key" || args[2].ToLower() != "--csv" || args[4].ToLower() != "--column_name")
                {
                    Console.WriteLine($"Remove command canceled.{Environment.NewLine}" +
                        $"Proper format of the alternative remove command is: {Environment.NewLine}" +
                        $"Route4MeLargeContactsFileUploading.exe --API_KEY XXXXXX --CSV file_name.csv --COLUMN_NAME col_uid");
                    return;
                }

                string apiKeyPattern = @"^[a-zA-Z\d]{32}$";
                var rgxApiKey = new Regex(apiKeyPattern);
                var maths = rgxApiKey.Match(args[1]);

                if (!maths.Success)
                {
                    Console.WriteLine($"Remove command canceled.{Environment.NewLine}Wrong API key - should be 32 symbols (letters and digits)");
                    return;
                }

                string csvFilePattern = @"\.csv$";
                var rgxcsvFile = new Regex(csvFilePattern);
                var maths2 = rgxcsvFile.Match(args[3].ToLower());

                if (!maths.Success)
                {
                    Console.WriteLine($"Remove command canceled.{Environment.NewLine}Wrong CSV file name - should the extension .csv");
                    return;
                }

                if (args[5].Length<1)
                {
                    Console.WriteLine($"Remove command canceled.{Environment.NewLine}Column name should contain at last 1 letter");
                    return;
                }

                alternativeRemoveCommand = true;

                removeContacts = true;

                ActualApiKey = args[1].ToString();
            }

            if (removeContacts || validateContacts)
            {
                geocode = false;
                only_empty = false;
            }

             if (removeContacts)
            {
                string jsonfile = !alternativeRemoveCommand ? stPath + @"files/remove_contacts.json" : "";

                using (StreamReader file = !alternativeRemoveCommand ? File.OpenText(jsonfile) : null)
                {
                    using (JsonTextReader reader = !alternativeRemoveCommand ? new JsonTextReader(file) : null)
                    {
                        JObject o = !alternativeRemoveCommand ? (JObject)JToken.ReadFrom(reader) : null;
                        var query = !alternativeRemoveCommand ? o["query"].ToString() : null;
                        var address_ids = !alternativeRemoveCommand ? JsonConvert.DeserializeObject<List<int>>(o["address_ids"].ToString()) : null;

                        var offset = !alternativeRemoveCommand ? JsonConvert.DeserializeObject<int?>(o["offset"].ToString()) : null;
                        var limit = !alternativeRemoveCommand ? JsonConvert.DeserializeObject<int?>(o["limit"].ToString()) : null;

                        var csv_file_column = !alternativeRemoveCommand ? JsonConvert.DeserializeObject<JObject>(o["csv_file_column"].ToString()) : null;

                        var remove_file_name = !alternativeRemoveCommand 
                            ? (csv_file_column != null ? csv_file_column["file_name"].ToString() : null) 
                            : args[3].ToString();
                        var remove_col_name = !alternativeRemoveCommand 
                            ? (csv_file_column != null ? csv_file_column["column_name"].ToString() : null) 
                            : args[5].ToString();

                        if ((query?.Length ?? 0) > 0)
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

                        if ((address_ids?.Count ?? 0) > 0)
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

                        if ((remove_file_name?.Length ?? 0)>4 && (remove_col_name?.Length ?? 0) > 0)
                        {
                            remove_file_name = stPath + remove_file_name;

                            using (TextReader csv_reader = File.OpenText(remove_file_name))
                            {
                                using (var csv = new CsvReader(csv_reader))
                                {
                                    bool headerRead = csv.ReadHeader();

                                    if (!headerRead || !csv.FieldHeaders.Contains(remove_col_name))
                                    {
                                        Console.WriteLine($"The file {remove_file_name} does not contain the column {remove_col_name}");
                                        return;
                                    }

                                    int colIndex = Array.IndexOf(csv.FieldHeaders, remove_col_name);

                                    var columnValues = new List<string>();

                                    while (csv.Read())
                                    {
                                        var colValue = csv.GetField(colIndex)?.ToString() ?? null;

                                        if (colValue != null && !columnValues.Contains(colValue))
                                            columnValues.Add(csv.GetField(colIndex).ToString());
                                    }
                                    var route4MeApi4 = new Route4MeManager(ActualApiKey);

                                    var addressIDs = route4MeApi4.GetAddressBookContactsByCustomField(
                                        remove_col_name,
                                        columnValues.ToArray(),
                                        out string errorString
                                     );

                                    Console.WriteLine(addressIDs != null ? "Address ID 0: " + addressIDs[0] : errorString);

                                    if ((addressIDs?.Length ?? 0) < 1)
                                    {
                                        Console.WriteLine($"Cannot retrieve address IDs by custom field {remove_col_name}");
                                        return;
                                    }

                                    var removed = route4Me.RemoveAddressBookContacts(
                                        addressIDs.Select(x => (int)x).ToArray(),
                                        out ResultResponse resultResponse);

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
                            }
                        }
                    }
                }

                //Console.WriteLine("Remove Command");
            }
            else if (validateContacts)
            {
                var fastValidating = new FastValidateData(ActualApiKey, false)
                {
                    ChankPause = Convert.ToInt32(ReadSetting("chank_setting.chunk_pause")),
                    CsvChankSize = Convert.ToInt32(ReadSetting("chank_setting.chunk_size"))
                };

                var csvAddressMapping = (ReadSetting("csv_address_mapping") as Dictionary<string, object>)
                                            .ToDictionary(k => k.Key, k => k.Value.ToString());

                fastValidating.MandatoryFields = csvAddressMapping.Values.ToArray();

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

                    Console.WriteLine("Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    fastValidating.readLargeContactsCsvFile(fileName, out string errorString);

                    Console.WriteLine("End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                Console.WriteLine(Environment.NewLine + "Press any key to close the window");
                string v1 = Console.ReadLine();
            }
            else
            {
                var fastProcessing = new FastBulkGeocoding(ActualApiKey, false)
                {
                    ChankPause = Convert.ToInt32(ReadSetting("chank_setting.chunk_pause")),
                    CsvChankSize = Convert.ToInt32(ReadSetting("chank_setting.chunk_size")),
                    DoGeocoding = geocode,
                    GeocodeOnlyEmpty = only_empty
                };

                //if (geocode) fastProcessing.DoGeocoding = true;
                //if (only_empty) fastProcessing.GeocodeOnlyEmpty = true;

                var csvAddressMapping = (ReadSetting("csv_address_mapping") as Dictionary<string, object>)
                                            .ToDictionary(k => k.Key, k => k.Value.ToString());

                FastFileReading.csvAddressMapping = csvAddressMapping;

                fastProcessing.MandatoryFields = csvAddressMapping.Values.ToArray();

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

                    Console.WriteLine("Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    fastProcessing.uploadLargeContactsCsvFile(fileName, out string errorString);

                    Console.WriteLine("End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }

            Console.WriteLine(Environment.NewLine + "Press any key to close the window");
            string v = Console.ReadLine();
        }

        static bool IsAlternativeArgs(string[] args)
        {
            if ((args?.Length ?? 0) < 1) return false;

            return (args.Where(x => x.Contains("--"))?.Count() ?? 0) > 0 ? true : false;
        }

        /// <summary>
        /// Reads settings from the file appsettings.json
        /// </summary>
        /// <param name="key_path">path to a setting key</param>
        /// <returns>A value of a specified key</returns>
        static object ReadSetting(string key_path)
        {
            var stPath = AppDomain.CurrentDomain.BaseDirectory;

            string jsonfile = stPath + @"appsettings.json";

            using (StreamReader file = File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject appSettings = (JObject)JToken.ReadFrom(reader);

                    if (key_path.Contains('.'))
                    {
                        string[] paths = key_path.Split('.');

                        if (paths.Length == 2)
                        {
                            try
                            {
                                var value1 = appSettings[paths[0]] as JObject;
                                var value2 = value1[paths[1]];
                                return value2;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Wrong appsettings key path: {key_path}. {ex.Message}");
                                return null;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Wrong appsettings key: {key_path}.");
                            return null;
                        }
                    }
                    else
                    {
                        var dictValue = appSettings[key_path] as JObject;

                        return dictValue.ToObject<Dictionary<string, object>>();
                    }
                }
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
