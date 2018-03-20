IF OBJECT_ID('usp_UpdateLocationAndPartMapping', 'P') IS NOT NULL
    DROP PROCEDURE usp_UpdateLocationAndPartMapping
GO
------------------------------------------------------------------------------------------
-- Name        : usp_UpdateLocationAndPartMapping
-- History     : user             date       - desc
--             : 
-- Created     : 
-- Description : This procedure updates data
-- Input       : 
-- Output      : none
-- Example     : exec usp_UpdateLocationAndPartMapping {params}
------------------------------------------------------------------------------------------
CREATE PROCEDURE usp_UpdateLocationAndPartMapping
	@IsActive			BIT,
	@UpdatedBy			INT ,
	@UpdatedOn			DATETIME,
	@TestMasterMapID	INT
AS
BEGIN
	UPDATE [dbo].[TestMasterMapping]
	SET	[IsActive] = @IsActive, [UpdatedBy] = @UpdatedBy ,[UpdatedOn] = @UpdatedOn 
	WHERE  [TestMasterMapID] =@TestMasterMapID


END
GO
