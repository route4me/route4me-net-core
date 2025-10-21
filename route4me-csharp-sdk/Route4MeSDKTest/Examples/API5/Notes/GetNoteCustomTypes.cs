using System;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting all note custom types using the API 5 endpoint.
        /// </summary>
        public void GetNoteCustomTypes()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            var result = notesManager.GetNoteCustomTypes(out var resultResponse);

            if (result != null)
            {
                Console.WriteLine("GetNoteCustomTypes executed successfully");

                if (result.Data != null && result.Data.Length > 0)
                {
                    Console.WriteLine("Total custom types: {0}", result.Data.Length);

                    foreach (var customType in result.Data)
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
                PrintFailResponse(resultResponse, "GetNoteCustomTypes");
            }
        }
    }
}
