DROP TABLE IF EXISTS [RouteParameters];

CREATE TABLE [RouteParameters] (
[route_id] [nvarchar](32)
,[optimization_problem_id] [nvarchar](32)
,[is_upload] [bit]
,[rt] [bit]
,[disable_optimization] [bit]
,[route_name] [nvarchar](250)
,[route_date] [bigint]
,[route_time] [bigint]
,[optimize] [nvarchar](32)
,[lock_last] [bit]
,[vehicle_capacity] [int]
,[vehicle_max_distance_mi] [int]
,[subtour_max_revenue] [int]
,[vehicle_max_cargo_volume] NUMERIC(8, 2)
,[vehicle_max_cargo_weight] NUMERIC(8, 2)
,[distance_unit] [nvarchar](8)
,[travel_mode] [nvarchar](32) NOT NULL
,[avoid] [nvarchar](32) NOT NULL
,[avoidance_zones] [nvarchar](MAX)
,[vehicle_id] [nvarchar](32)
,[dev_lat] NUMERIC(10, 7)
,[dev_lng] NUMERIC(10, 7)
,[route_max_duration] [int]
,[route_email] [nvarchar](250)
,[route_type] [nvarchar](8)
,[metric] [int]
,[algorithm_type] [int]
,[member_id] [int]
,[ip] [bigint]
,[dm] [int]
,[dirm] [int]
,[parts] [int]
,[parts_min] [int]
,[device_type] [nvarchar](32)
,[has_trailer] [bit]
,[first_drive_then_wait_between_stops] [bit]
,[trailer_weight_t] NUMERIC(8, 2)
,[limited_weight_t] NUMERIC(8, 2)
,[weight_per_axle_t] NUMERIC(8, 2)
,[truck_height] NUMERIC(8, 2)
,[truck_width] NUMERIC(8, 2)
,[truck_length] NUMERIC(8, 2)
,[min_tour_size] [int]
,[max_tour_size] [int] 
,[optimization_quality] [int]
,[uturn] [int]
,[leftturn] [int]
,[rightturn] [int]
,[override_addresses] [nvarchar]
) ON [PRIMARY]
GO


DROP TABLE IF EXISTS [Addresses];

CREATE TABLE [Addresses] (
[route_destination_id] [int] NOT NULL
,[alias] [nvarchar](250)
,[member_id] [int]
,[first_name] [nvarchar](64)
,[last_name] [nvarchar](64)
,[address] [nvarchar](250)
,[address_stop_type] [nvarchar](16)
,[is_depot] [bit]
,[timeframe_violation_state] [int]
,[timeframe_violation_time] [bigint]
,[timeframe_violation_rate] [double]
,[lat] [double]
,[lng] [double]
,[route_id] [nvarchar](32)
,[original_route_id] [nvarchar](32)
,[optimization_problem_id] [nvarchar](32)
,[route_name] [nvarchar](255)
,[sequence_no] [int]
,[geocoded] [bit]
,[preferred_geocoding] [int]
,[failed_geocoding] [bit]
,[geocodings] [nvarchar](MAX)
,[contact_id] [int]
,[is_visited] [bit]
,[is_departed] [bit]
,[timestamp_last_visited] [bigint]
,[timestamp_last_departed] [bigint]
,[visited_lat] [double]
,[visited_lng] [double]
,[departed_lat] [double]
,[departed_lng] [double]
,[group] [nvarchar](128)
,[customer_po] [nvarchar](128)
,[invoice_no] [nvarchar](128)
,[reference_no] [nvarchar](128)
,[order_no] [nvarchar](128)
,[order_id] [int]
,[weight] NUMERIC(8, 2)
,[cost] NUMERIC(8, 2)
,[revenue] NUMERIC(8, 2)
,[cube] NUMERIC(8, 2)
,[pieces] [int]
,[email] [nvarchar](128)
,[phone] [nvarchar](128)
,[destination_note_count] [int]
,[drive_time_to_next_destination] [int]
,[abnormal_traffic_time_to_next_destination] [int]
,[uncongested_time_to_next_destination] [int]
,[traffic_time_to_next_destination] [int]
,[distance_to_next_destination] [double]
,[generated_time_window_start] [int]
,[generated_time_window_end] [int]
,[channel_name] [nvarchar](250)
,[time_window_start] [int]
,[time_window_end] [int]
,[time_window_start_2] [int] NOT NULL
,[time_window_end_2] [int] NOT NULL
,[wait_time_to_next_destination] [int] NOT NULL
,[geofence_detected_visited_timestamp] [bigint]
,[geofence_detected_departed_timestamp] [bigint]
,[geofence_detected_service_time] [int] NOT NULL
,[geofence_detected_visited_lat] [double]
,[geofence_detected_visited_lng] [double]
,[geofence_detected_departed_lat] [double]
,[geofence_detected_departed_lng] [double]
,[time] [int]
,[notes] [nvarchar](MAX)
,[path_to_next] [nvarchar](MAX)
,[priority] [int]
,[curbside_lat] [double]
,[curbside_lng] [double]
,[custom_fields] [nvarchar](MAX)
,[custom_fields_str_json] [nvarchar](250)
,[custom_fields_config] [nvarchar](MAX)
,[custom_fields_config_str_json] [nvarchar](MAX) 
,[tracking_number] [nvarchar](64),
,CONSTRAINT [PK_RouteDestinationID] PRIMARY KEY ([route_destination_id])
) ON [PRIMARY]
GO


DROP TABLE IF EXISTS [AddressManifest];

CREATE TABLE [AddressManifest] (
[route_destination_id] [int] NOT NULL
,[running_service_time] [int]
,[running_travel_time] [int]
,[running_wait_time] [int]
,[running_distance] [double]
,[fuel_from_start] [double]
,[fuel_cost_from_start] [double]
,[projected_arrival_time_ts] [int]
,[projected_departure_time_ts] [int]
,[actual_arrival_time_ts] [int]
,[actual_departure_time_ts] [int]
,[estimated_arrival_time_ts] [int]
,[estimated_departure_time_ts] [int]
,[time_impact] [int]
,CONSTRAINT [PK_AddressManifestID] PRIMARY KEY ([route_destination_id])
) ON [PRIMARY]
GO


DROP TABLE IF EXISTS [Geocoding];

CREATE TABLE [Geocoding] (
[route_destination_id] [int] NOT NULL
,[key] [nvarchar](250) 
,[name] [nvarchar](250)
,[bbox] [nvarchar](MAX)
,[lat] [double]
,[lng] [double]
,[confidence] [nvarchar](8)
,[postalCode] [nvarchar](32)
,[countryRegion] [nvarchar](250)
,[curbside_coordinates] [nvarchar](MAX)
,[address_without_number] [nvarchar](250)
,[place_id] [nvarchar](250)
,CONSTRAINT [PK_GeocodingRouteDestinationID] PRIMARY KEY ([route_destination_id])
) ON [PRIMARY]
GO


DROP TABLE IF EXISTS [AddressNote];

CREATE TABLE [AddressNote] (
[note_id] [int] 
,[route_id] [nvarchar](250)
,[route_destination_id] [int] NOT NULL
,[upload_id] [nvarchar](250)
,[ts_added] [bigint]
,[lat] [double]
,[lng] [double]
,[activity_type] [nvarchar](64)
,[contents] [nvarchar](max)
,[upload_type] [nvarchar](64)
,[upload_url] [nvarchar](250)
,[upload_extension] [nvarchar](250)
,[device_type] [nvarchar](32) NOT NULL
,CONSTRAINT [PK_RouteDestinationNoteID] PRIMARY KEY ([route_destination_id])
) ON [PRIMARY]
GO


DROP TABLE IF EXISTS [DirectionLocations];

CREATE TABLE [DirectionLocations] (
[direction_location_id] [bigint]
[route_id] [nvarchar](32) NOT NULL
[name] [nvarchar](250) NOT NULL
,[time] [int]
,[segment_distance] [double]
,[start_location] [nvarchar](250)
,[end_location] [nvarchar](250)
,[directions_error] [nvarchar](250)
,[error_code] [int]
,CONSTRAINT [PK_DirectionLocationID] PRIMARY KEY ([direction_location_id])
) ON [PRIMARY]
GO


DROP TABLE IF EXISTS [DirectionLocationsSteps];

CREATE TABLE [DirectionLocationsSteps] (
[direction_location_id] [bigint]
,[route_id] [nvarchar](32)
,[direction] [nvarchar](250) 
,[directions] [nvarchar](250) 
,[distance] [double] 
,[distance_unit] [nvarchar](16)
,[maneuverType] [nvarchar](32)
,[compass_direction] [nvarchar](32)
,[duration_sec] [int]
,[maneuverPoint] [nvarchar](32)
,CONSTRAINT [PK_DirectionLocationStepID] PRIMARY KEY ([direction_location_id])
) ON [PRIMARY]
GO



DROP TABLE IF EXISTS [TrackingHistory];

CREATE TABLE [TrackingHistory] (
[route_id] [nvarchar](32) NOT NULL
,[optimization_problem_id] [nvarchar](32) NOT NULL
,[s] [double] 
,[lt] [double] NOT NULL
,[lg] [double] NOT NULL
,[d] [nvarchar](250)
,[ts] [nvarchar](250) NOT NULL
,[ts_friendly] [nvarchar](250) NOT NULL
) ON [PRIMARY]
GO



DROP TABLE IF EXISTS [Routes];

CREATE TABLE [Routes] (
[route_id] [nvarchar](32) NOT NULL
,[created_timestamp] [bigint]
,[member_id] [int]
,[member_email] [nvarchar](250)
,[member_first_name] [nvarchar](64)
,[member_last_name] [nvarchar](64)
,[member_picture] [nvarchar](250)
,[member_tracking_subheadline] [nvarchar](250)
,[user_route_rating] [int]
,[approved_for_execution] [bit]
,[is_unrouted] [bit]
,[approved_revisions_counter] [int]
,[vehicle_alias] [nvarchar](250)
,[driver_alias] [nvarchar](250)
,[route_cost] [double]
,[route_revenue] [double]
,[net_revenue_per_distance_unit] [double]
,[parameters] [nvarchar](MAX)
,[mpg] NUMERIC(8, 2)
,[trip_distance] [double]
,[gas_price] [double]
,[route_duration_sec] [int]
,[planned_total_route_duration] [int]
,[actual_travel_distance] NUMERIC(8, 2)
,[actual_travel_time] [int]
,[actual_footsteps] [int]
,[working_time] [int]
,[driving_time] [int]
,[idling_time] [int]
,[paying_miles] [int]
,[channel_name] [nvarchar](250)
,[geofence_polygon_type] [nvarchar](16)
,[geofence_polygon_size] [int]
,[destination_count] [int]
,[notes] [nvarchar](MAX)
,[notes_count] [int]
,[optimization_problem_id] [nvarchar](32)
,[directions] [nvarchar](MAX)
,[tracking_history] [nvarchar](MAX)
,[path] [nvarchar](MAX)
,[links] [nvarchar](MAX)
,[vehicle] [nvarchar](32)
,[member_config_storage] [nvarchar](MAX)
,CONSTRAINT [PK_Activity] PRIMARY KEY ([route_id])
) ON [PRIMARY]
GO


DROP TABLE IF EXISTS [Optimizations];

CREATE TABLE [Optimizations] (
[optimization_problem_id] [nvarchar](32) NOT NULL
,[state] [int]
,[user_errors] [nvarchar](MAX)
,[optimization_errors] [nvarchar](MAX)
,[optimization_completed_timestamp] [bigint]
,[sent_to_background] [bit]
,[created_timestamp] [bigint]
,[scheduled_for] [bigint] 
,[parameters] [nvarchar](MAX)
,[addresses] [nvarchar](MAX)
,[routes] [nvarchar](MAX)
,[links] [nvarchar](MAX)
,[tracking_history] [nvarchar](MAX)
,[total_addresses] [int]
,CONSTRAINT [PK_OptimizationProblemID] PRIMARY KEY ([optimization_problem_id])
) ON [PRIMARY]
GO






