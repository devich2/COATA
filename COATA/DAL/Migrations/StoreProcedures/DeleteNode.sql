--Only delete if it is a leaf
CREATE PROCEDURE DeleteNode
	@pnodeId INT--node need to move
AS
BEGIN
	DECLARE @leftNodeId INT, @rightNodeId INT, @width INT, @count INT;
    SELECT @leftNodeId = Lft, @rightNodeId = Rgt FROM dbo.Units WHERE Id = @pnodeId;
	SET @width = @rightNodeId - @leftNodeId + 1;
	SET @count = @width / 2;
	IF(@count = 1)
	BEGIN
		UPDATE dbo.Units SET Lft = Lft - @width WHERE Lft > @leftNodeId;
		UPDATE dbo.Units SET Rgt = Rgt - @width WHERE Rgt > @rightNodeId;
		DELETE dbo.Units WHERE Id = @pnodeId
	END
END