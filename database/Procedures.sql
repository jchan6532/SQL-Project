/*
* FILE : Views.sql
* PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
* PROGRAMMER : Gerritt Hooyer, Justin Chan
* FIRST VERSION : 2023-11-20
* DESCRIPTION :
* Creates the procedures for the database.
*/
CREATE PROCEDURE RefillBin @bin_id INT
AS
	UPDATE BinOverview
	SET part_count = part_count + refill
	WHERE bin_id = @bin_id