CREATE PROCEDURE [dbo].[Read_Leaderboards]
AS
BEGIN
    Select Country,Score from Leaderboards ORDER BY Score DESC
END
GO
