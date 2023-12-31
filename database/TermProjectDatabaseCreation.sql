/*
* FILE : TermProjectDatabaseCreation.sql
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Creates the database schema for the final term project.
* As of 2023-11-20, only covers creation of configuration table.
*/
CREATE DATABASE Sql_Term_Project

USE Sql_Term_Project

CREATE TABLE ConfigSettings (
	config_key NVARCHAR(60) PRIMARY KEY NOT NULL,
	config_value NVARCHAR(64) NOT NULL,
	config_type NVARCHAR(12) NOT NULL
)

CREATE TABLE EmployeeType (
	type_id INT PRIMARY KEY IDENTITY(1,1),
	type_name NVARCHAR(64) NOT NULL
)

CREATE TABLE Employee (
	employee_id INT PRIMARY KEY IDENTITY(1,1),
	employee_type INT FOREIGN KEY REFERENCES EmployeeType(type_id) NOT NULL,
	employee_name NVARCHAR(64) NOT NULL
)

CREATE TABLE Part (
	part_id INT PRIMARY KEY IDENTITY(1,1),
	part_name NVARCHAR(64) NOT NULL,
	part_desc NVARCHAR(64),
	bin_size INT CHECK (bin_size > 0) NOT NULL DEFAULT 1
)

CREATE TABLE Workstation (
	workstation_id INT PRIMARY KEY IDENTITY(1,1),
	employee_id INT FOREIGN KEY REFERENCES Employee(employee_id) NOT NULL,
	lamps_built INT CHECK (lamps_built >= 0) NOT NULL DEFAULT 0,
	defects INT CHECK (defects >= 0) DEFAULT 0
)

CREATE TABLE Bin (
	bin_id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	part_id INT FOREIGN KEY REFERENCES Part(part_id) NOT NULL,
	workstation_id INT FOREIGN KEY REFERENCES Workstation(workstation_id) NOT NULL,
	part_count INT NOT NULL DEFAULT 0
)

CREATE TABLE LampOrder
(
	order_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	order_amount INT DEFAULT 0 CHECK (order_amount > 0) NOT NULL,
	order_fulfilled INT DEFAULT 0 CHECK (order_fulfilled >= 0) NOT NULL,
	defects INT DEFAULT 0 CHECK (defects >= 0) NOT NULL
)

CREATE TABLE WorkstationSession
(
	workstation_id INT FOREIGN KEY REFERENCES Workstation(workstation_id) NOT NULL,
	order_id INT FOREIGN KEY REFERENCES LampOrder(order_id) NOT NULL,
	lamps_built INT DEFAULT 0 CHECK (lamps_built >= 0),
	defects INT DEFAULT 0 CHECK (defects >= 0),
	PRIMARY KEY (workstation_id, order_id)
)

-- Add our employee types
INSERT INTO EmployeeType VALUES('New')
INSERT INTO EmployeeType VALUES('Average')
INSERT INTO EmployeeType VALUES('Experienced')

-- Add our parts w/ their bin refill amounts (no description atm.)
INSERT INTO Part VALUES ('Harness',NULL,55)
INSERT INTO Part VALUES ('Reflector',NULL,35)
INSERT INTO Part VALUES ('Housing',NULL,24)
INSERT INTO Part VALUES ('Lens',NULL,40)
INSERT INTO Part VALUES ('Bulb',NULL,60)
INSERT INTO Part VALUES ('Bezel',NULL,75)

-- Add our employees
INSERT INTO Employee VALUES(1,'John Smith') -- New
INSERT INTO Employee VALUES(2,'Justin Chan') -- Average
INSERT INTO Employee VALUES(3,'Gerritt Hooyer') -- Experienced

-- Add the workstations
INSERT INTO Workstation VALUES(1,0,0) -- John Smith
INSERT INTO Workstation VALUES(2,0,0) -- Justin Chan
INSERT INTO Workstation VALUES(3,0,0) -- Gerritt Hooyer

INSERT INTO ConfigSettings VALUES ('employee.new.build_speed','1.5','float')
INSERT INTO ConfigSettings VALUES ('employee.new.defect_rate','0.85','float')
INSERT INTO ConfigSettings VALUES ('employee.average.build_speed','1.0','float')
INSERT INTO ConfigSettings VALUES ('employee.average.defect_rate','0.5','float')
INSERT INTO ConfigSettings VALUES ('employee.experienced.build_speed','0.85','float')
INSERT INTO ConfigSettings VALUES ('employee.experienced.defect_rate','0.15','float')
INSERT INTO ConfigSettings VALUES ('system.sim_speed','4','float')
INSERT INTO ConfigSettings VALUES ('system.build_time','60','int')
INSERT INTO ConfigSettings VALUES ('system.refill_warning_amount','5','int')
INSERT INTO ConfigSettings VALUES ('system.refill_interval','300','int')
INSERT INTO ConfigSettings VALUES ('system.tick_rate','60','int')

-- Add bins for each workstation, already filled to the database.
DECLARE @workstation_id INT = 1
DECLARE @part_id INT = 1
DECLARE @part_count INT = 0
-- Iterate through each workstation
WHILE @workstation_id <= 3
BEGIN
	-- Reset the part ID
	SET @part_id = 1
	-- Iterate through the part IDs
	WHILE @part_id <= 6
	BEGIN		
		-- Insert the bin w/ the part id and set to be filled to the refill capacity
		SET @part_count = (SELECT TOP 1 bin_size FROM Part WHERE part_id = @part_id)
		INSERT INTO Bin VALUES(@part_id,@workstation_id,@part_count)
		SET @part_id = @part_id + 1
	END
	SET @workstation_id = @workstation_id + 1
END

INSERT INTO LampOrder VALUES(500,0,0)