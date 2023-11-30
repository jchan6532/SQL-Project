/*
* FILE : Views.sql
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Creates the views for the database.
*/

CREATE VIEW WorkstationOverview
AS
SELECT workstation_id, Workstation.employee_id, employee_name, employee_type, lamps_built, defects, (CAST(defects AS FLOAT) / CAST(lamps_built AS FLOAT) ) AS defect_rate
FROM Workstation
JOIN Employee ON Workstation.employee_id = Employee.employee_id
GO

CREATE VIEW BinOverview
AS 
SELECT bin_id, part_name, part_count, bin_size as refill, Bin.workstation_id FROM Bin
INNER JOIN Workstation ON Bin.workstation_id = Workstation.workstation_id
INNER JOIN Part ON Bin.part_id = Part.part_id
GO


CREATE VIEW SessionOverview
AS
SELECT 
[workstation_id]
,[order_id]
,[lamps_built]
,[defects]
, CAST (defects AS FLOAT) / CAST(lamps_built AS FLOAT) AS defect_rate
GO