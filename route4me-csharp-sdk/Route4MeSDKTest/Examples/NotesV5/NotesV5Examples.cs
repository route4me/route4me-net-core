using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;
using Route4MeSDKLibrary.Managers;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Comprehensive examples for Notes API v5.0 demonstrating all CRUD operations.
        /// This example shows the complete lifecycle of working with notes in Route4Me.
        /// </summary>
        public void NotesV5Examples()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // Ensure we have a test route
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route for notes examples.");
                return;
            }

            var routeId = SD10Stops_route_id_V5;
            var destinationId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId;

            // 1. Create a Note
            Console.WriteLine("\n=== Creating a Note ===");
            var createRequest = new NoteStoreRequest
            {
                RouteId = routeId,
                StrNoteContents = "Package delivered successfully at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                AddressId = destinationId,
                DeviceType = "web"
            };

            var createdNote = notesManager.CreateNote(createRequest, out var createResponse);

            if (createdNote?.NoteId != null)
            {
                Console.WriteLine("Note created successfully!");
                Console.WriteLine("  Note ID: {0}", createdNote.NoteId);
                Console.WriteLine("  Contents: {0}", createdNote.Contents);
            }
            else
            {
                Console.WriteLine("Failed to create note.");
                PrintFailResponse(createResponse, "CreateNote");
                return;
            }

            // 2. Read - Get the Note by ID
            Console.WriteLine("\n=== Getting Note by ID ===");
            var retrievedNote = notesManager.GetNote(createdNote.NoteId.Value, out var getResponse);

            if (retrievedNote != null)
            {
                Console.WriteLine("Note retrieved successfully!");
                Console.WriteLine("  Note ID: {0}", retrievedNote.NoteId);
                Console.WriteLine("  Contents: {0}", retrievedNote.Contents);
                Console.WriteLine("  Route ID: {0}", retrievedNote.RouteId);
            }
            else
            {
                Console.WriteLine("Failed to retrieve note.");
                PrintFailResponse(getResponse, "GetNote");
            }

            // 3. Update the Note
            Console.WriteLine("\n=== Updating Note ===");
            var updateRequest = new NoteUpdateRequest
            {
                StrNoteContents = "Updated: Package delivered and signed by recipient at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DeviceType = "web"
            };

            var updatedNote = notesManager.UpdateNote(createdNote.NoteId.Value, updateRequest, out var updateResponse);

            if (updatedNote != null)
            {
                Console.WriteLine("Note updated successfully!");
                Console.WriteLine("  Note ID: {0}", updatedNote.NoteId);
                Console.WriteLine("  Updated Contents: {0}", updatedNote.Contents);
            }
            else
            {
                Console.WriteLine("Failed to update note.");
                PrintFailResponse(updateResponse, "UpdateNote");
            }

            // 4. Get Notes by Route
            Console.WriteLine("\n=== Getting Notes by Route ===");
            var routeNotes = notesManager.GetNotesByRoute(routeId, 1, 10, out var routeNotesResponse);

            if (routeNotes != null)
            {
                Console.WriteLine("Route notes retrieved successfully!");
                Console.WriteLine("  Total notes: {0}", routeNotes.Total);
                Console.WriteLine("  Current page: {0}", routeNotes.CurrentPage);

                if (routeNotes.Data != null && routeNotes.Data.Length > 0)
                {
                    foreach (var note in routeNotes.Data)
                    {
                        var contentLength = note.Contents?.Length ?? 0;
                        var displayContent = contentLength > 50 ? note.Contents.Substring(0, 50) : note.Contents;
                        Console.WriteLine("    - Note ID: {0}, Contents: {1}", note.NoteId, displayContent);
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve route notes.");
                PrintFailResponse(routeNotesResponse, "GetNotesByRoute");
            }

            // 5. Get Notes by Destination
            Console.WriteLine("\n=== Getting Notes by Destination ===");
            var destNotes = notesManager.GetNotesByDestination(destinationId, 1, 10, out var destNotesResponse);

            if (destNotes != null)
            {
                Console.WriteLine("Destination notes retrieved successfully!");
                Console.WriteLine("  Total notes: {0}", destNotes.Total);
                Console.WriteLine("  Current page: {0}", destNotes.CurrentPage);

                if (destNotes.Data != null && destNotes.Data.Length > 0)
                {
                    foreach (var note in destNotes.Data)
                    {
                        var contentLength = note.Contents?.Length ?? 0;
                        var displayContent = contentLength > 50 ? note.Contents.Substring(0, 50) : note.Contents;
                        Console.WriteLine("    - Note ID: {0}, Contents: {1}", note.NoteId, displayContent);
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve destination notes.");
                PrintFailResponse(destNotesResponse, "GetNotesByDestination");
            }

            // 6. Get Note Custom Types
            Console.WriteLine("\n=== Getting Note Custom Types ===");
            var customTypes = notesManager.GetNoteCustomTypes(out var customTypesResponse);

            if (customTypes != null)
            {
                Console.WriteLine("Custom types retrieved successfully!");

                if (customTypes.Data != null && customTypes.Data.Length > 0)
                {
                    Console.WriteLine("  Total custom types: {0}", customTypes.Data.Length);

                    foreach (var customType in customTypes.Data)
                    {
                        Console.WriteLine("    - Type: {0} (ID: {1})", customType.NoteCustomType, customType.NoteCustomTypeId);

                        if (customType.NoteCustomTypeValues != null && customType.NoteCustomTypeValues.Length > 0)
                        {
                            Console.WriteLine("      Values: {0}", string.Join(", ", customType.NoteCustomTypeValues));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("  No custom types found.");
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve custom types.");
                PrintFailResponse(customTypesResponse, "GetNoteCustomTypes");
            }

            // 7. Create a Custom Note Type
            Console.WriteLine("\n=== Creating Custom Note Type ===");
            var customTypeRequest = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Delivery Status Example " + DateTime.Now.Ticks,
                NoteCustomTypeValues = new[] { "Delivered", "Refused", "Rescheduled", "Left at Door" },
                NoteCustomFieldType = 1 // Valid values: 1, 2, 3, or 4
            };

            var createdCustomType = notesManager.CreateNoteCustomType(customTypeRequest, out var customTypeCreateResponse);

            if (createdCustomType != null && createdCustomType.Status)
            {
                Console.WriteLine("Custom type created successfully!");

                if (createdCustomType.Data != null && createdCustomType.Data.Length > 0)
                {
                    var newType = createdCustomType.Data[0];
                    Console.WriteLine("  Custom Type ID: {0}", newType.NoteCustomTypeId);
                    Console.WriteLine("  Custom Type Name: {0}", newType.NoteCustomType);
                    Console.WriteLine("  Values: {0}", string.Join(", ", newType.NoteCustomTypeValues ?? new string[] { }));
                }
            }
            else
            {
                Console.WriteLine("Failed to create custom type.");
                PrintFailResponse(customTypeCreateResponse, "CreateNoteCustomType");
            }

            // 8. Delete the Note
            Console.WriteLine("\n=== Deleting Note ===");
            var deleteResult = notesManager.DeleteNote(createdNote.NoteId.Value, out var deleteResponse);

            if (deleteResult != null && deleteResult.Status)
            {
                Console.WriteLine("Note deleted successfully!");
                Console.WriteLine("  Deleted Note ID: {0}", deleteResult.NoteId);
            }
            else
            {
                Console.WriteLine("Failed to delete note.");
                PrintFailResponse(deleteResponse, "DeleteNote");
            }

            Console.WriteLine("\n=== Notes V5 Examples Completed ===");
        }
    }
}
