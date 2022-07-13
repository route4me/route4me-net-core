using Route4MeSDK.DataTypes.V5;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates process of removing the address book contacts asynchronously.
        /// </summary>
        public async void RemoveAddressBookContactsAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            CreateTestContactsV5();

            long[] addressIDs = new long[] 
            {
                (long)contact51.AddressId, 
                (long)contact52.AddressId 
            };

            // Run the query
            var removingResult = await route4Me.RemoveAddressBookContactsAsync(addressIDs);

            if (removingResult.Item3!=null)
            {
                string jobId = removingResult.Item3;

                var jobStatus = route4Me.GetContactsJobStatus(jobId, out ResultResponse resultResponse);
            }

            Console.WriteLine((removingResult?.Item1?.IsSuccessStatusCode ?? false)
                ? addressIDs.Length + " contacts removed from database"
                : "Cannot remove " + addressIDs.Length + " contacts." + Environment.NewLine +
                "Exit code: " + (removingResult?.Item2?.ExitCode.ToString() ?? "") + Environment.NewLine +
                "Code: " + (removingResult?.Item2?.Code.ToString() ?? "") + Environment.NewLine +
                "Status: " + (removingResult?.Item2?.Status.ToString() ?? "") + Environment.NewLine
                );

            if (removingResult.Item2 != null)
            {
                foreach (var msg in removingResult.Item2.Messages)
                {
                    Console.WriteLine(msg.Key + ": " + msg.Value + Environment.NewLine);
                }
            }

            Console.WriteLine("=======================================");
        }
    }
}
