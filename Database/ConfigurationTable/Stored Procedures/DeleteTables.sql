USE [SQL-PROJECT];
GO

CREATE PROCEDURE ClearConfigurationTables
AS
BEGIN
   DELETE FROM ConfigSettings;
   DELETE FROM DefaultSettings;
END;
GO
