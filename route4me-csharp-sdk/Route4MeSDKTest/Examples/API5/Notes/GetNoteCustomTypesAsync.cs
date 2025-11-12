using System;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting all note custom types asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetNoteCustomTypesAsync()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            var result = await notesManager.GetNoteCustomTypesAsync();

            if (result.Item1 != null)
            {
                Console.WriteLine("GetNoteCustomTypesAsync executed successfully");

                if (result.Item1.Data != null && result.Item1.Data.Length > 0)
                {
                    Console.WriteLine("Total custom types: {0}", result.Item1.Data.Length);

                    foreach (var customType in result.Item1.Data)
                    {
                        Console.WriteLine("  Type: {0}", customType.NoteCustomType);
                        Console.WriteLine("    ID: {0}", customType.NoteCustomTypeId);

                        if (customType.NoteCustomTypeValues != null && customType.NoteCustomTypeValues.Length > 0)
                        {
                            Console.WriteLine("    Values: {0}", string.Join(", ", customType.NoteCustomTypeValues));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No custom types found.");
                }
            }
            else
            {
                PrintFailResponse(result.Item2, "GetNoteCustomTypesAsync");
            }
        }
    }
}