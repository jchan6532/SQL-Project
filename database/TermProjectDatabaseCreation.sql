/*
* FILE : TermProjectDatabaseCreation.sql
* PROJECT : PROG3070 - Gerritt Hooyer
* PROGRAMMER : Gerritt Hooyer
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Creates the database schema for the final term project.
* As of 2023-11-20, only covers creation of configuration table.
*/
CREATE TABLE ConfigSettings (
	config_key NVARCHAR(60) PRIMARY KEY NOT NULL,
	config_value FLOAT
)