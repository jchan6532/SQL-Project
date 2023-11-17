USE [SQL-PROJECT];
GO

CREATE PROCEDURE DropConfigurationTables
AS
BEGIN
   DROP TABLE ConfigSettings;
   DROP TABLE DefaultSettings;
END;
GO