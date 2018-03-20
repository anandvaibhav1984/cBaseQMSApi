IF OBJECT_ID('usp_SaveLocationAndPartMapping', 'P') IS NOT NULL
    DROP PROCEDURE usp_SaveLocationAndPartMapping
GO
------------------------------------------------------------------------------------------
-- Name        : usp_SaveLocationAndPartMapping
-- History     : user             date       - desc
--             : 
-- Created     : 
-- Description : This procedure saves data
-- Input       : 
-- Output      : none
-- Example     : exec usp_SaveLocationAndPartMapping {params}
------------------------------------------------------------------------------------------
CREATE PROCEDURE usp_SaveLocationAndPartMapping
	@TestMasterID		INT,
	@PartMasterID		INT,
    @LocationMasterID	INT,
	@IsActive			BIT,
	@CreatedBy			INT,
	@CreatedOn			DATETIME,
	@UpdatedBy			INT ,
	@UpdatedOn			DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[TestMasterMapping] ([TestMasterID],[PartMasterID] ,[LocationMasterID]
           ,[IsActive]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[UpdatedBy]
           ,[UpdatedOn])
     VALUES
           (@TestMasterID 
           ,@PartMasterID
           ,@LocationMasterID
           ,@IsActive
           ,@CreatedBy
           ,@CreatedOn
           ,@UpdatedBy
           ,@UpdatedOn)


END
GO
