using System;
using Route4MeSDK.QueryTypes.V5.Notes;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a custom note type using the API 5 endpoint.
        /// </summary>
        public void CreateNoteCustomType()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            var request = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Delivery Status " + DateTime.Now.Ticks,
                NoteCustomTypeValues = new[] { "Delivered", "Refused", "Rescheduled" },
                NoteCustomFieldType = 1 // Valid values: 1, 2, 3, or 4
            };

            var result = notesManager.CreateNoteCustomType(request, out var resultResponse);

            if (result != null)
            {
                Console.WriteLine("CreateNoteCustomType executed successfully");
                Console.WriteLine("Status: {0}", result.Status);

                if (result.Data != null && result.Data.Length > 0)
                {
                    var customType = result.Data[0];
                    Console.WriteLine("Custom Type ID: {0}", customType.NoteCustomTypeId);
                    Console.WriteLine("Custom Type Name: {0}", customType.NoteCustomType);

                    if (customType.NoteCustomTypeValues != null)
                    {
                        Console.WriteLine("Values: {0}", string.Join(", ", customType.NoteCustomTypeValues));
                    }
                }
            }
            else
            {
                PrintFailResponse(resultResponse, "CreateNoteCustomType");
            }
        }
    }
}
