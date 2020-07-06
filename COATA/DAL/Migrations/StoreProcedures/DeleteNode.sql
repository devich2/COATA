--Only delete if it is a leaf
CREATE PROCEDURE DeleteNode
	@pnodeId INT--node need to move
AS
BEGIN
	DECLARE @leftNodeId INT, @rightNodeId INT, @width INT, @count INT;
    SELECT @leftNodeId = Lft, @rightNodeId = Rgt FROM dbo.Units WHERE Id = @pnodeId;
	SET @width = @rightNodeId - @leftNodeId + 1;
	BEGIN
	    DELETE dbo.Units WHERE Lft >= @leftNodeId AND Rgt <= @rightNodeId
		UPDATE dbo.Units SET Lft = Lft - @width WHERE Lft > @leftNodeId;
		UPDATE dbo.Units SET Rgt = Rgt - @width WHERE Rgt > @rightNodeId;
	END
END
