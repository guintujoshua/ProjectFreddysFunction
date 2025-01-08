CREATE TABLE [dbo].[DailyRecord] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [DateCollected] DATETIME2 (7) NOT NULL,
    [Total]         BIGINT        NULL,
    CONSTRAINT [PK_NewTable] PRIMARY KEY CLUSTERED ([Id] ASC)
);

