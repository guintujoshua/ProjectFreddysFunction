CREATE TABLE [dbo].[LeaderBoards] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Country] NVARCHAR (100) NOT NULL,
    [Score]   BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

