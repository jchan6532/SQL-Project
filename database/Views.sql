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
SELECT        dbo.Workstation.workstation_id, dbo.Workstation.employee_id, dbo.Employee.employee_name, dbo.EmployeeType.type_name AS employee_type, dbo.Workstation.lamps_built, dbo.Workstation.defects, dbo.Part.part_name, 
                         dbo.Bin.part_count
FROM            dbo.Workstation INNER JOIN
                         dbo.Employee ON dbo.Workstation.employee_id = dbo.Employee.employee_id INNER JOIN
                         dbo.EmployeeType ON dbo.Employee.employee_type = dbo.EmployeeType.type_id INNER JOIN
                         dbo.Bin ON dbo.Workstation.workstation_id = dbo.Bin.workstation_id INNER JOIN
                         dbo.Part ON dbo.Bin.part_id = dbo.Part.part_id
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
workstation_id, 
WorkstationSession.order_id, 
lamps_built, 
WorkstationSession.defects, 
order_fulfilled as total_lamps_built, 
LampOrder.defects as total_defects,
order_amount
FROM WorkstationSession
INNER JOIN LampOrder on WorkstationSession.order_id = LampOrder.order_id
GO