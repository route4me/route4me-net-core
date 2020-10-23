DROP TABLE IF EXISTS RouteParameters;

CREATE TABLE RouteParameters (
`route_id` varchar(32)
,`optimization_problem_id` varchar(32)
,`is_upload` boolean
,`rt` boolean
,`disable_optimization` boolean
,`route_name` varchar(250) CHARACTER SET utf8
,`route_date` bigint
,`route_time` bigint
,`optimize` varchar(32)
,`lock_last` boolean
,`vehicle_capacity` int
,`vehicle_max_distance_mi` int
,`subtour_max_revenue` int
,`vehicle_max_cargo_volume` decimal(8, 2)
,`vehicle_max_cargo_weight` decimal(8, 2)
,`distance_unit` ENUM('mi','km') CHARACTER SET binary
,`travel_mode` ENUM('Driving','Walking','Trucking','Cycling','Transit') CHARACTER SET binary
,`avoid` ENUM('Highways','Tolls','minimizeHighways','minimizeTolls','') CHARACTER SET binary
,`avoidance_zones` text
,`vehicle_id` varchar(32)
,`dev_position` POINT SRID 4326
,`route_max_duration` int
,`route_email` varchar(250)
,`route_type` ENUM('api','null') CHARACTER SET binary
,`metric` ENUM('ROUTE4ME_METRIC_EUCLIDEAN','ROUTE4ME_METRIC_MANHATTAN','ROUTE4ME_METRIC_GEODESIC','ROUTE4ME_METRIC_MATRIX','ROUTE4ME_METRIC_EXACT_2D') CHARACTER SET binary
,`algorithm_type` ENUM('TSP','VRP','CVRP_TW_SD','CVRP_TW_MD','TSP_TW','TSP_TW_CR','BBCVRP','ALG_NONE','ALG_LEGACY_DISTRIBUTED') CHARACTER SET binary
,`member_id` int
,`ip` bigint
,`dm` int
,`dirm` int
,`parts` int
,`parts_min` int
,`device_type` ENUM('web','iphone','ipad','android_phone','android_tablet') CHARACTER SET binary
,`has_trailer` boolean
,`first_drive_then_wait_between_stops` boolean
,`trailer_weight_t` decimal(8, 2)
,`limited_weight_t` decimal(8, 2)
,`weight_per_axle_t` decimal(8, 2)
,`truck_height` decimal(8, 2)
,`truck_width` decimal(8, 2)
,`truck_length` decimal(8, 2)
,`min_tour_size` int
,`max_tour_size` int
,`optimization_quality` int
,`uturn` int
,`leftturn` int
,`rightturn` int
,`override_addresses` varchar(64)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS Addresses;

CREATE TABLE Addresses (
`route_destination_id` int NOT NULL
,`alias` varchar(250) CHARACTER SET utf8
,`member_id` int
,`first_name` varchar(64) CHARACTER SET utf8
,`last_name` varchar(64) CHARACTER SET utf8
,`address` varchar(250) CHARACTER SET utf8
,`address_stop_type` ENUM('PICKUP','DELIVERY','BREAK','MEETUP') CHARACTER SET binary
,`is_depot` boolean
,`timeframe_violation_state` int
,`timeframe_violation_time` bigint
,`timeframe_violation_rate` double
,`address_position` POINT SRID 4326
,`route_id` varchar(32)
,`original_route_id` varchar(32)
,`optimization_problem_id` varchar(32)
,`route_name` varchar(255) CHARACTER SET utf8
,`sequence_no` int
,`geocoded` boolean
,`preferred_geocoding` int
,`failed_geocoding` boolean
,`geocodings` text
,`contact_id` int
,`is_visited` boolean
,`is_departed` boolean
,`timestamp_last_visited` bigint
,`timestamp_last_departed` bigint
,`visited_position` POINT SRID 4326
,`departed_position` POINT SRID 4326
,`group` varchar(128)
,`customer_po` varchar(128)
,`invoice_no` varchar(128)
,`reference_no` varchar(128)
,`order_no` varchar(128)
,`order_id` int
,`weight` decimal(8, 2)
,`cost` decimal(8, 2)
,`revenue` decimal(8, 2)
,`cube` decimal(8, 2)
,`pieces` int
,`email` varchar(128)
,`phone` varchar(128)
,`destination_note_count` int
,`drive_time_to_next_destination` int
,`abnormal_traffic_time_to_next_destination` int
,`uncongested_time_to_next_destination` int
,`traffic_time_to_next_destination` int
,`distance_to_next_destination` decimal(8, 2)
,`generated_time_window_start` int
,`generated_time_window_end` int
,`channel_name` varchar(250)
,`time_window_start` int
,`time_window_end` int
,`time_window_start_2` int
,`time_window_end_2` int
,`wait_time_to_next_destination` int
,`geofence_detected_visited_timestamp` bigint
,`geofence_detected_departed_timestamp` bigint
,`geofence_detected_service_time` int
,`geofence_detected_visited_position` POINT SRID 4326
,`geofence_detected_departed_position` POINT SRID 4326
,`time` int
,`notes` text CHARACTER SET utf8
,`path_to_next` blob
,`priority` int
,`curbside_position` POINT SRID 4326
,`custom_fields` text CHARACTER SET utf8
,`custom_fields_str_json` text CHARACTER SET utf8
,`custom_fields_config` text CHARACTER SET utf8
,`custom_fields_config_str_json` text CHARACTER SET utf8
,`tracking_number` varchar(64)
,CONSTRAINT PK_RouteDestinationID PRIMARY KEY (`route_destination_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS AddressManifest;

CREATE TABLE AddressManifest (
`route_destination_id` int NOT NULL
,`running_service_time` int
,`running_travel_time` int
,`running_wait_time` int
,`running_distance` decimal(8, 2)
,`fuel_from_start` decimal(8, 2)
,`fuel_cost_from_start` decimal(8, 2)
,`projected_arrival_time_ts` int
,`projected_departure_time_ts` int
,`actual_arrival_time_ts` int
,`actual_departure_time_ts` int
,`estimated_arrival_time_ts` int
,`estimated_departure_time_ts` int
,`time_impact` int
,CONSTRAINT PK_AddressManifestID PRIMARY KEY (`route_destination_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS Geocoding;

CREATE TABLE Geocoding (
`route_destination_id` int NOT NULL
,`key` varchar(250) CHARACTER SET utf8
,`name` varchar(250) CHARACTER SET utf8
,`bbox` MULTIPOINT SRID 4326
,`lat_lng` POINT SRID 4326
,`confidence` varchar(8)
,`postalCode` varchar(32)
,`countryRegion` varchar(250) CHARACTER SET utf8
,`curbside_coordinates` MULTIPOINT SRID 4326
,`address_without_number` varchar(250) CHARACTER SET utf8
,`place_id` varchar(250)
,CONSTRAINT PK_GeocodingRouteDestinationID PRIMARY KEY (`route_destination_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS AddressNote;

CREATE TABLE AddressNote (
`note_id` int
,`route_id` varchar(32)
,`route_destination_id` int NOT NULL
,`upload_id` varchar(32)
,`ts_added` bigint
,`lat_lng` POINT SRID 4326
,`activity_type` ENUM('','area-removed','area-added','area-updated','delete-destination','insert-destination','destination-out-sequence','driver-arrived-early','driver-arrived-late','driver-arrived-on-time','geofence-left','geofence-entered','mark-destination-departed','mark-destination-visited','member-created','member-deleted','member-modified','move-destination','note-insert','route-delete','route-optimized','route-owner-changed','route-duplicate','update-destinations','user_message') CHARACTER SET binary
,`contents` text CHARACTER SET utf8
,`upload_type` ENUM('DRIVER_IMG','VEHICLE_IMG','ADDRESS_IMG','CSV_FILE','XLS_FILE','ANY_FILE') CHARACTER SET binary
,`upload_url` varchar(250)
,`upload_extension` varchar(16)
,`device_type` ENUM('web','iphone','ipad','android_phone','android_tablet') CHARACTER SET binary
,CONSTRAINT PK_RouteDestinationNoteID PRIMARY KEY (`route_destination_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS DirectionLocations;

CREATE TABLE DirectionLocations (
`direction_location_id` int
,`route_id` varchar(32) NOT NULL
,`name` varchar(250) CHARACTER SET utf8
,`time` int
,`segment_distance` decimal(8, 2)
,`start_location` varchar(250) CHARACTER SET utf8
,`end_location` varchar(250) CHARACTER SET utf8
,`directions_error` varchar(250)
,`error_code` int
,CONSTRAINT PK_DirectionLocationID PRIMARY KEY (`direction_location_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS DirectionLocationsSteps;

CREATE TABLE DirectionLocationsSteps (
`direction_location_id` int
,`route_id` varchar(32)
,`direction` varchar(250) CHARACTER SET utf8
,`directions` ENUM('Head','Go Straight','Turn Left','Turn Right','Turn Slight Left','Turn Slight Right','Turn Sharp Left','Turn Sharp Right','Roundabout Left','Roundabout Right','Uturn Left','Uturn Right','Ramp Left','Ramp Right','Fork Left','Fork Right','Keep Left','Keep Right','Ferry','Ferry Train','Merge','Reached Your Destination') CHARACTER SET binary
,`distance` decimal(8, 2)
,`distance_unit` ENUM('mi','km') CHARACTER SET binary
,`maneuverType` ENUM('HEAD','GO_STRAIGHT','TURN_LEFT','TURN_RIGHT','TURN_SLIGHT_LEFT','TURN_SLIGHT_RIGHT','TURN_SHARP_LEFT','TURN_SHARP_RIGHT','ROUNDABOUT_LEFT','ROUNDABOUT_RIGHT','UTURN_LEFT','UTURN_RIGHT','RAMP_LEFT','RAMP_RIGHT','FORK_LEFT','FORK_RIGHT','KEEP_LEFT','KEEP_RIGHT','FERRY','FERRY_TRAIN','MERGE','REACHED_YOUR_DESTINATION') CHARACTER SET binary
,`compass_direction` ENUM('N','S','W','E','NW','NE','SW','SE') CHARACTER SET binary
,`duration_sec` int
 ,CONSTRAINT PK_DirectionLocationStepID PRIMARY KEY (`direction_location_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS TrackingHistory;

CREATE TABLE TrackingHistory (
`route_id` varchar(32) NOT NULL
,`optimization_problem_id` varchar(32) NOT NULL
,`s` decimal(8, 2)
,`lt_lg` POINT SRID 4326
,`d` smallint
,`ts` bigint
,`ts_friendly` varchar(24)
 ,CONSTRAINT PK_TrackingHistoryRouteID PRIMARY KEY (`route_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS Routes;

CREATE TABLE Routes (
`route_id` nvarchar(32) NOT NULL
,`created_timestamp` bigint
,`member_id` int
,`member_email` varchar(250)
,`member_first_name` varchar(64) CHARACTER SET utf8
,`member_last_name` varchar(64) CHARACTER SET utf8
,`member_picture` varchar(250)
,`member_tracking_subheadline` varchar(250) CHARACTER SET utf8
,`user_route_rating` tinyint
,`approved_for_execution` boolean
,`is_unrouted` boolean
,`approved_revisions_counter` smallint
,`vehicle_alias` varchar(64) CHARACTER SET utf8
,`driver_alias` varchar(64) CHARACTER SET utf8
,`route_cost` decimal(8, 2)
,`route_revenue` decimal(8, 2)
,`net_revenue_per_distance_unit` decimal(8, 2)
,`mpg` tinyint
,`trip_distance` decimal(8, 2)
,`gas_price` decimal(8, 2)
,`route_duration_sec` int
,`planned_total_route_duration` int
,`actual_travel_distance` decimal(8, 2)
,`actual_travel_time` int
,`actual_footsteps` int
,`working_time` int
,`driving_time` int
,`idling_time` int
,`paying_miles` int
,`channel_name` varchar(250)
,`geofence_polygon_type` ENUM('circle', 'poly', 'rect') CHARACTER SET binary
,`geofence_polygon_size` int
,`destination_count` int
,`notes_count` int
,`optimization_problem_id` varchar(32)
,`links` blob
,`vehicle` varchar(32)
,`member_config_storage` text CHARACTER SET utf8
,CONSTRAINT PK_Activity PRIMARY KEY (`route_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS Optimizations;

CREATE TABLE Optimizations (
`optimization_problem_id` varchar(32) NOT NULL
,`state` tinyint
,`user_errors` text
,`optimization_errors` text
,`optimization_completed_timestamp` bigint
,sent_to_background boolean
,created_timestamp bigint
,scheduled_for bigint
,`links` blob
,`total_addresses` int
,CONSTRAINT PK_OptimizationProblemID PRIMARY KEY (`optimization_problem_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;