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
GO

CREATE PROCEDURE BuildNewFan @workstation_id INT, @order_id INT
AS
	UPDATE Workstation
	SET lamps_built = lamps_built + 1
	WHERE workstation_id = @workstation_id

	UPDATE WorkstationSession
	SET lamps_built = lamps_built + 1
	WHERE workstation_id = @workstation_id
	AND order_id = @order_id

	UPDATE LampOrder
	SET order_fulfilled = order_fulfilled + 1
	WHERE order_id = @order_id

	UPDATE Bin
	SET part_count = part_count - 1
	WHERE @workstation_id = workstation_id
GO


CREATE PROCEDURE BuildNewDefect @workstation_id INT, @order_id INT
AS
	UPDATE Workstation
	SET defects = defects + 1
	WHERE workstation_id = @workstation_id

	UPDATE WorkstationSession
	SET defects = defects + 1
	WHERE workstation_id = @workstation_id
	AND order_id = @order_id

	UPDATE LampOrder
	SET defects = defects + 1
	WHERE order_id = @order_id

	UPDATE Bin
	SET part_count = part_count - 1
	WHERE @workstation_id = workstation_id
EXECUTE BuildNewFan 1, 1

EXECUTE BuildNewDefect 1,1


CREATE PROCEDURE BuildNewDefect @workstation_id