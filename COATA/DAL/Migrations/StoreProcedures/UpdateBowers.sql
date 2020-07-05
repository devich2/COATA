CREATE PROCEDURE UpdateBowers
	@parent_id INT,
	@id INT
AS
BEGIN
	DECLARE @nodeId INT;
    BEGIN
		SELECT @nodeId = Rgt FROM dbo.Units WHERE Id = @parent_id;
				UPDATE dbo.Units SET Rgt = Rgt + 2 WHERE Rgt >= @nodeId;
				UPDATE dbo.Units SET Lft = Lft + 2 WHERE Lft > @nodeId;
                UPDATE dbo.Units SET Lft = @nodeId, Rgt = @nodeId + 1 WHERE Id = @id;
	END
END

