using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5.Notes;

namespace Route4MeSDKLibrary.Managers
{
    /// <summary>
    ///     Manager for Notes API v5.0 operations
    /// </summary>
    public class NotesManagerV5 : Route4MeManagerBase
    {
        public NotesManagerV5(string apiKey) : base(apiKey)
        {
        }

        #region Notes CRUD

        /// <summary>
        ///     Create a new note
        /// </summary>
        /// <param name="request">Note creation request</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created note resource</returns>
        public RouteNoteResource CreateNote(NoteStoreRequest request, out ResultResponse resultResponse)
        {
            // Validate required parameters
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note request cannot be null"}}
                    }
                };
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.RouteId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The route_id is required"}}
                    }
                };
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.StrNoteContents))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note contents (strNoteContents) is required"}}
                    }
                };
                return null;
            }

            return GetJsonObjectFromAPI<RouteNoteResource>(
                request,
                R4MEInfrastructureSettingsV5.Notes,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Create a new note
        /// </summary>
        /// <param name="request">Note creation request</param>
        /// <returns>Created note resource</returns>
        public async Task<Tuple<RouteNoteResource, ResultResponse>> CreateNoteAsync(NoteStoreRequest request)
        {
            // Validate required parameters
            if (request == null)
            {
                return new Tuple<RouteNoteResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note request cannot be null"}}
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(request.RouteId))
            {
                return new Tuple<RouteNoteResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The route_id is required"}}
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(request.StrNoteContents))
            {
                return new Tuple<RouteNoteResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note contents (strNoteContents) is required"}}
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<RouteNoteResource>(
                request,
                R4MEInfrastructureSettingsV5.Notes,
                HttpMethodType.Post);

            return new Tuple<RouteNoteResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Get a note by ID (supports both integer and 32-char hex string)
        /// </summary>
        /// <param name="noteId">Note ID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Note resource</returns>
        public RouteNoteResource GetNote(object noteId, out ResultResponse resultResponse)
        {
            if (noteId == null || string.IsNullOrWhiteSpace(noteId.ToString()))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_id is required"}}
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.NoteById.Replace("{note_id}", noteId.ToString());

            return GetJsonObjectFromAPI<RouteNoteResource>(
                new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Get a note by ID (supports both integer and 32-char hex string)
        /// </summary>
        /// <param name="noteId">Note ID</param>
        /// <returns>Note resource</returns>
        public async Task<Tuple<RouteNoteResource, ResultResponse>> GetNoteAsync(object noteId)
        {
            if (noteId == null || string.IsNullOrWhiteSpace(noteId.ToString()))
            {
                return new Tuple<RouteNoteResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_id is required"}}
                    }
                });
            }

            var url = R4MEInfrastructureSettingsV5.NoteById.Replace("{note_id}", noteId.ToString());

            var result = await GetJsonObjectFromAPIAsync<RouteNoteResource>(
                new GenericParameters(),
                url,
                HttpMethodType.Get);

            return new Tuple<RouteNoteResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Update a note by ID
        /// </summary>
        /// <param name="noteId">Note ID (supports both integer and 32-char hex string)</param>
        /// <param name="request">Note update request</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated note resource</returns>
        public RouteNoteResource UpdateNote(object noteId, NoteUpdateRequest request, out ResultResponse resultResponse)
        {
            if (noteId == null || string.IsNullOrWhiteSpace(noteId.ToString()))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_id is required"}}
                    }
                };
                return null;
            }

            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The update request cannot be null"}}
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.NoteById.Replace("{note_id}", noteId.ToString());

            return GetJsonObjectFromAPI<RouteNoteResource>(
                request,
                url,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Update a note by ID
        /// </summary>
        /// <param name="noteId">Note ID (supports both integer and 32-char hex string)</param>
        /// <param name="request">Note update request</param>
        /// <returns>Updated note resource</returns>
        public async Task<Tuple<RouteNoteResource, ResultResponse>> UpdateNoteAsync(object noteId, NoteUpdateRequest request)
        {
            if (noteId == null || string.IsNullOrWhiteSpace(noteId.ToString()))
            {
                return new Tuple<RouteNoteResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_id is required"}}
                    }
                });
            }

            if (request == null)
            {
                return new Tuple<RouteNoteResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The update request cannot be null"}}
                    }
                });
            }

            var url = R4MEInfrastructureSettingsV5.NoteById.Replace("{note_id}", noteId.ToString());

            var result = await GetJsonObjectFromAPIAsync<RouteNoteResource>(
                request,
                url,
                HttpMethodType.Post);

            return new Tuple<RouteNoteResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Delete a note by ID
        /// </summary>
        /// <param name="noteId">Note ID (supports both integer and 32-char hex string)</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Delete response with status and note_id</returns>
        public DeleteNoteResponse DeleteNote(object noteId, out ResultResponse resultResponse)
        {
            if (noteId == null || string.IsNullOrWhiteSpace(noteId.ToString()))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_id is required"}}
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.NoteById.Replace("{note_id}", noteId.ToString());

            return GetJsonObjectFromAPI<DeleteNoteResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Delete,
                out resultResponse);
        }

        /// <summary>
        ///     Delete a note by ID
        /// </summary>
        /// <param name="noteId">Note ID (supports both integer and 32-char hex string)</param>
        /// <returns>Delete response with status and note_id</returns>
        public async Task<Tuple<DeleteNoteResponse, ResultResponse>> DeleteNoteAsync(object noteId)
        {
            if (noteId == null || string.IsNullOrWhiteSpace(noteId.ToString()))
            {
                return new Tuple<DeleteNoteResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_id is required"}}
                    }
                });
            }

            var url = R4MEInfrastructureSettingsV5.NoteById.Replace("{note_id}", noteId.ToString());

            var result = await GetJsonObjectFromAPIAsync<DeleteNoteResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Delete);

            return new Tuple<DeleteNoteResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Query Notes

        /// <summary>
        ///     Get notes by route ID
        /// </summary>
        /// <param name="routeId">Route ID (32-char hex string)</param>
        /// <param name="page">Page number</param>
        /// <param name="perPage">Items per page</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Collection of notes</returns>
        public RouteNoteCollection GetNotesByRoute(string routeId, int? page, int? perPage, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(routeId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The route_id is required"}}
                    }
                };
                return null;
            }

            // Validate pagination parameters
            if (page.HasValue && page.Value < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Page number must be greater than 0"}}
                    }
                };
                return null;
            }

            if (perPage.HasValue && perPage.Value < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Per page value must be greater than 0"}}
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.NotesByRoute.Replace("{route_id}", routeId);

            var parameters = new GenericParameters();
            if (page.HasValue)
            {
                parameters.ParametersCollection.Add("page", page.Value.ToString());
            }
            if (perPage.HasValue)
            {
                parameters.ParametersCollection.Add("per_page", perPage.Value.ToString());
            }

            return GetJsonObjectFromAPI<RouteNoteCollection>(
                parameters,
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Get notes by route ID
        /// </summary>
        /// <param name="routeId">Route ID (32-char hex string)</param>
        /// <param name="page">Page number</param>
        /// <param name="perPage">Items per page</param>
        /// <returns>Collection of notes</returns>
        public async Task<Tuple<RouteNoteCollection, ResultResponse>> GetNotesByRouteAsync(string routeId, int? page, int? perPage)
        {
            if (string.IsNullOrWhiteSpace(routeId))
            {
                return new Tuple<RouteNoteCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The route_id is required"}}
                    }
                });
            }

            // Validate pagination parameters
            if (page.HasValue && page.Value < 1)
            {
                return new Tuple<RouteNoteCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Page number must be greater than 0"}}
                    }
                });
            }

            if (perPage.HasValue && perPage.Value < 1)
            {
                return new Tuple<RouteNoteCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Per page value must be greater than 0"}}
                    }
                });
            }

            var url = R4MEInfrastructureSettingsV5.NotesByRoute.Replace("{route_id}", routeId);

            var parameters = new GenericParameters();
            if (page.HasValue)
            {
                parameters.ParametersCollection.Add("page", page.Value.ToString());
            }
            if (perPage.HasValue)
            {
                parameters.ParametersCollection.Add("per_page", perPage.Value.ToString());
            }

            var result = await GetJsonObjectFromAPIAsync<RouteNoteCollection>(
                parameters,
                url,
                HttpMethodType.Get);

            return new Tuple<RouteNoteCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Get notes by destination ID
        /// </summary>
        /// <param name="routeDestinationId">Route destination ID (supports both integer and 32-char hex string)</param>
        /// <param name="page">Page number</param>
        /// <param name="perPage">Items per page</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Collection of notes</returns>
        public RouteNoteCollection GetNotesByDestination(object routeDestinationId, int? page, int? perPage, out ResultResponse resultResponse)
        {
            if (routeDestinationId == null || string.IsNullOrWhiteSpace(routeDestinationId.ToString()))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The route_destination_id is required"}}
                    }
                };
                return null;
            }

            // Validate pagination parameters
            if (page.HasValue && page.Value < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Page number must be greater than 0"}}
                    }
                };
                return null;
            }

            if (perPage.HasValue && perPage.Value < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Per page value must be greater than 0"}}
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.NotesByDestination.Replace("{route_destination_id}", routeDestinationId.ToString());

            var parameters = new GenericParameters();
            if (page.HasValue)
            {
                parameters.ParametersCollection.Add("page", page.Value.ToString());
            }
            if (perPage.HasValue)
            {
                parameters.ParametersCollection.Add("per_page", perPage.Value.ToString());
            }

            return GetJsonObjectFromAPI<RouteNoteCollection>(
                parameters,
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Get notes by destination ID
        /// </summary>
        /// <param name="routeDestinationId">Route destination ID (supports both integer and 32-char hex string)</param>
        /// <param name="page">Page number</param>
        /// <param name="perPage">Items per page</param>
        /// <returns>Collection of notes</returns>
        public async Task<Tuple<RouteNoteCollection, ResultResponse>> GetNotesByDestinationAsync(object routeDestinationId, int? page, int? perPage)
        {
            if (routeDestinationId == null || string.IsNullOrWhiteSpace(routeDestinationId.ToString()))
            {
                return new Tuple<RouteNoteCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The route_destination_id is required"}}
                    }
                });
            }

            // Validate pagination parameters
            if (page.HasValue && page.Value < 1)
            {
                return new Tuple<RouteNoteCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Page number must be greater than 0"}}
                    }
                });
            }

            if (perPage.HasValue && perPage.Value < 1)
            {
                return new Tuple<RouteNoteCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"Per page value must be greater than 0"}}
                    }
                });
            }

            var url = R4MEInfrastructureSettingsV5.NotesByDestination.Replace("{route_destination_id}", routeDestinationId.ToString());

            var parameters = new GenericParameters();
            if (page.HasValue)
            {
                parameters.ParametersCollection.Add("page", page.Value.ToString());
            }
            if (perPage.HasValue)
            {
                parameters.ParametersCollection.Add("per_page", perPage.Value.ToString());
            }

            var result = await GetJsonObjectFromAPIAsync<RouteNoteCollection>(
                parameters,
                url,
                HttpMethodType.Get);

            return new Tuple<RouteNoteCollection, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Custom Note Types

        /// <summary>
        ///     Get all custom note types
        /// </summary>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Collection of custom note types</returns>
        public NoteCustomTypeCollection GetNoteCustomTypes(out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<NoteCustomTypeCollection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.NotesCustomTypes,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Get all custom note types
        /// </summary>
        /// <returns>Collection of custom note types</returns>
        public async Task<Tuple<NoteCustomTypeCollection, ResultResponse>> GetNoteCustomTypesAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<NoteCustomTypeCollection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.NotesCustomTypes,
                HttpMethodType.Get);

            return new Tuple<NoteCustomTypeCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Create a custom note type
        /// </summary>
        /// <param name="request">Custom note type creation request</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Collection including the new custom note type</returns>
        public NoteCustomTypeCollection CreateNoteCustomType(NoteCustomTypeStoreRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The custom type request cannot be null"}}
                    }
                };
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.NoteCustomType))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_custom_type is required"}}
                    }
                };
                return null;
            }

            if (request.NoteCustomTypeValues == null || request.NoteCustomTypeValues.Length == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_custom_type_values is required and must contain at least one value"}}
                    }
                };
                return null;
            }

            // Validate note_custom_field_type (should be 1, 2, 3, or 4)
            if (request.NoteCustomFieldType.HasValue &&
                (request.NoteCustomFieldType.Value < 1 || request.NoteCustomFieldType.Value > 4))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The note_custom_field_type must be 1, 2, 3, or 4"}}
                    }
                };
                return null;
            }

            return GetJsonObjectFromAPI<NoteCustomTypeCollection>(
                request,
                R4MEInfrastructureSettingsV5.NotesCustomTypes,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Create a custom note type
        /// </summary>
        /// <param name="request">Custom note type creation request</param>
        /// <returns>Collection including the new custom note type</returns>
        public async Task<Tuple<NoteCustomTypeCollection, ResultResponse>> CreateNoteCustomTypeAsync(
            NoteCustomTypeStoreRequest request)
        {
            if (request == null)
            {
                return new Tuple<NoteCustomTypeCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The custom type request cannot be null" } }
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(request.NoteCustomType))
            {
                return new Tuple<NoteCustomTypeCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The note_custom_type is required" } }
                    }
                });
            }

            if (request.NoteCustomTypeValues == null || request.NoteCustomTypeValues.Length == 0)
            {
                return new Tuple<NoteCustomTypeCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {
                            "Error",
                            new[] { "The note_custom_type_values is required and must contain at least one value" }
                        }
                    }
                });
            }

            // Validate note_custom_field_type (should be 1, 2, 3, or 4)
            if (request.NoteCustomFieldType.HasValue &&
                (request.NoteCustomFieldType.Value < 1 || request.NoteCustomFieldType.Value > 4))
            {
                return new Tuple<NoteCustomTypeCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The note_custom_field_type must be 1, 2, 3, or 4" } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<NoteCustomTypeCollection>(
                request,
                R4MEInfrastructureSettingsV5.NotesCustomTypes,
                HttpMethodType.Post);

            return new Tuple<NoteCustomTypeCollection, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Bulk Create Notes

        /// <summary>
        ///     Bulk create notes
        /// </summary>
        /// <param name="request">Bulk note creation request</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Bulk create response with status and async flag</returns>
        public BulkNotesResponse BulkCreateNotes(NoteStoreBulkRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The bulk note request cannot be null"}}
                    }
                };
                return null;
            }

            if (request.Notes == null || request.Notes.Length == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The notes array cannot be null or empty"}}
                    }
                };
                return null;
            }

            for (int i = 0; i < request.Notes.Length; i++)
            {
                var note = request.Notes[i];

                if (string.IsNullOrWhiteSpace(note.RouteId))
                {
                    resultResponse = new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {$"The route_id is required for note at index {i}"}}
                        }
                    };
                    return null;
                }

                if (string.IsNullOrWhiteSpace(note.StrNoteContents))
                {
                    resultResponse = new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {$"The note contents (strNoteContents) is required for note at index {i}"}}
                        }
                    };
                    return null;
                }
            }

            return GetJsonObjectFromAPI<BulkNotesResponse>(
                request,
                R4MEInfrastructureSettingsV5.NotesBulkCreate,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Bulk create notes asynchronously
        /// </summary>
        /// <param name="request">Bulk note creation request</param>
        /// <returns>Bulk create response with status and async flag</returns>
        public async Task<Tuple<BulkNotesResponse, ResultResponse>> BulkCreateNotesAsync(NoteStoreBulkRequest request)
        {
            if (request == null)
            {
                return new Tuple<BulkNotesResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The bulk note request cannot be null"}}
                    }
                });
            }

            if (request.Notes == null || request.Notes.Length == 0)
            {
                return new Tuple<BulkNotesResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The notes array cannot be null or empty"}}
                    }
                });
            }

            for (int i = 0; i < request.Notes.Length; i++)
            {
                var note = request.Notes[i];

                if (string.IsNullOrWhiteSpace(note.RouteId))
                {
                    return new Tuple<BulkNotesResponse, ResultResponse>(null, new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {$"The route_id is required for note at index {i}"}}
                        }
                    });
                }

                if (string.IsNullOrWhiteSpace(note.StrNoteContents))
                {
                    return new Tuple<BulkNotesResponse, ResultResponse>(null, new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {$"The note contents (strNoteContents) is required for note at index {i}"}}
                        }
                    });
                }
            }

            var result = await GetJsonObjectFromAPIAsync<BulkNotesResponse>(
                request,
                R4MEInfrastructureSettingsV5.NotesBulkCreate,
                HttpMethodType.Post);

            return new Tuple<BulkNotesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion
    }
}