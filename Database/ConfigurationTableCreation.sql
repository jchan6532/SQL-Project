DROP DATABASE IF EXISTS [SQL-PROJECT];
CREATE DATABASE [SQL-PROJECT];
GO

USE [SQL-PROJECT];

CREATE TABLE DefaultSettings (
	config_key NVARCHAR(60) PRIMARY KEY NOT NULL,
	int_value INT,
	float_value FLOAT,
	string_value NVARCHAR(60)
)
GO

CREATE TABLE ConfigSettings (
	config_key NVARCHAR(60) PRIMARY KEY FOREIGN KEY REFERENCES DefaultSettings(config_key) NOT NULL,
	int_value INT,
	float_value FLOAT,
	string_value NVARCHAR(60)
)
GO

INSERT INTO DefaultSettings (config_key, int_value) VALUES ('tick_per_minute',60) /* How many ticks we simulate per minute (60 = 1 tick p/ second) */
INSERT INTO DefaultSettings (config_key, float_value) VALUES ('avg_employee_build_speed',1.0) /* Build speed is in minutes */
INSERT INTO DefaultSettings (config_key, float_value) VALUES ('new_employee_build_speed',1.5) /* Higher is slower */
INSERT INTO DefaultSettings (config_key, float_value) VALUES ('exp_employee_build_speed',0.85)
INSERT INTO DefaultSettings (config_key, float_value) VALUES ('avg_employee_defect_rate',0.5)
INSERT INTO DefaultSettings (config_key, float_value) VALUES ('new_employee_defect_rate',0.85)
INSERT INTO DefaultSettings (config_key, float_value) VALUES ('exp_employee_defect_rate',0.15)
GO

INSERT INTO ConfigSettings (config_key, int_value) VALUES ('tick_per_minute',60) /* How many ticks we simulate per minute (60 = 1 tick p/ second) */
INSERT INTO ConfigSettings (config_key, float_value) VALUES ('avg_employee_build_speed',1.2) /* Build speed is in minutes */ 
INSERT INTO ConfigSettings (config_key, float_value) VALUES ('avg_employee_defect_rate',0.5)
INSERT INTO ConfigSettings (config_key, float_value) VALUES ('new_employee_defect_rate',0.75)
GO