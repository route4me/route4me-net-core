CREATE TABLE [dbo].[Activity] (
    [ActivityId]         NVARCHAR (32)  NOT NULL,
    [ActivityType]       NVARCHAR (64)  NOT NULL,
    [ActivityTimestamp]  INT            NOT NULL,
    [ActivityMessage]    NVARCHAR (250) NOT NULL,
    [MemberId]           NVARCHAR (32)  NULL,
    [RouteId]            NVARCHAR (32)  NULL,
    [RouteDestinationId] NVARCHAR (32)  NULL,
    [NoteId]             NVARCHAR (32)  NULL,
    [NoteType]           NVARCHAR (64)  NULL,
    [NoteContents]       NVARCHAR (250) NULL,
    [NoteFile]           NVARCHAR (250) NULL,
    [RouteName]          NVARCHAR (250) NULL,
    [DestinationName]    NVARCHAR (250) NULL,
    [DestinationAlias]   NVARCHAR (250) NULL,
    [activity_membr_id]  INT            NULL, 
    CONSTRAINT [PK_Activity] PRIMARY KEY ([ActivityId])
);

