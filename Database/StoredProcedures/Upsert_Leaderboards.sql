
CREATE PROCEDURE [dbo].[Upsert_Leaderboards](
    @Country NVARCHAR(100),
    @Score INT
)
AS
BEGIN

    DECLARE @LeaderboardId INT
    Select TOP 1 @LeaderboardId=Id FROM Leaderboards WHERE Country = @Country
    IF @LeaderboardId IS NULL
    BEGIN
        INSERT INTO Leaderboards (Country, Score) VALUES (@Country, @Score)
    END
    ELSE
    BEGIN
        UPDATE Leaderboards SET Score = Score+@Score WHERE Id = @LeaderboardId
    END

     DECLARE @DateToday DATETIME2 = CAST(CAST(GETUTCDATE() AS DATE) AS DATETIME2)
     Declare @DailyRecordId INT
     SELECT TOP 1 @DailyRecordId = Id FROM [dbo].[DailyRecord] WHERE [DateCollected] = @DateToday
        IF @DailyRecordId IS NULL
        BEGIN
            INSERT INTO [dbo].[DailyRecord] ([DateCollected], Total) VALUES (@DateToday,@Score)
        END
        ELSE
        BEGIN
            UPDATE [dbo].[DailyRecord] SET Total = Total+@Score WHERE Id = @DailyRecordId
        END
END
GO
