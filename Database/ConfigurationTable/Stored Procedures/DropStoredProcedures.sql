USE [SQL-PROJECT];
GO

CREATE PROCEDURE DropStoredProcedures
AS
BEGIN
   DROP PROCEDURE ClearConfigurationTables;
   DROP PROCEDURE DropConfigurationTables;
END;
GO

EXEC DropStoredProcedures;