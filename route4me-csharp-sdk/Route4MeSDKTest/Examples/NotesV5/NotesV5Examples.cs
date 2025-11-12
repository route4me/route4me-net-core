using System;

using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;

using Route4MeSDKLibrary.Managers;

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
            Console.WriteLine("");
            Console.WriteLine("========== Notes API v5 Examples ==========");
            Console.WriteLine("");

            // 1. Create a single note
            Console.WriteLine("1. Creating a single note...");
            CreateNote();
            Console.WriteLine("");

            // 2. Get note by ID
            Console.WriteLine("2. Getting note by ID...");
            GetNote();
            Console.WriteLine("");

            // 3. Get notes by route
            Console.WriteLine("3. Getting notes by route...");
            GetNotesByRoute();
            Console.WriteLine("");

            // 4. Get notes by destination
            Console.WriteLine("4. Getting notes by destination...");
            GetNotesByDestination();
            Console.WriteLine("");

            // 5. Update a note
            Console.WriteLine("5. Updating a note...");
            UpdateNote();
            Console.WriteLine("");

            // 6. Delete a note
            Console.WriteLine("6. Deleting a note...");
            DeleteNote();
            Console.WriteLine("");

            // 7. Bulk create notes
            Console.WriteLine("7. Bulk creating notes...");
            BulkCreateNotes();
            Console.WriteLine("");

            // 8. Get note custom types
            Console.WriteLine("8. Getting note custom types...");
            GetNoteCustomTypes();
            Console.WriteLine("");

            // 9. Create a note custom type
            Console.WriteLine("9. Creating a note custom type...");
            CreateNoteCustomType();
            Console.WriteLine("");

            // 10. Async examples
            Console.WriteLine("10. Running async examples...");
            CreateNoteAsync();
            GetNoteAsync();
            GetNotesByRouteAsync();
            GetNotesByDestinationAsync();
            UpdateNoteAsync();
            DeleteNoteAsync();
            BulkCreateNotesAsync();
            GetNoteCustomTypesAsync();
            CreateNoteCustomTypeAsync();
            Console.WriteLine("");

            Console.WriteLine("========== Notes API v5 Examples Completed ==========");
            Console.WriteLine("");
        }
    }
}