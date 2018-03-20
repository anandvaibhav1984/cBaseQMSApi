IF OBJECT_ID('usp_GetTestMasterByTestId', 'P') IS NOT NULL
    DROP PROCEDURE usp_GetTestMasterByTestId
GO
------------------------------------------------------------------------------------------
-- Name        : usp_GetTestMasterByTestId
-- History     : user             date       - desc
--             : anand singh	8 feb 2018
-- Created     : 
-- Description : This procedure gets data
-- Input       : 
-- Output      : list of test master for testid
-- Example     : exec usp_GetTestMasterByTestId 0
------------------------------------------------------------------------------------------
 CREATE PROCEDURE usp_GetTestMasterByTestId
(
  @Testid INT
)
AS
BEGIN
	IF(@Testid  <>0 or @Testid  <>'')
	BEGIN
		SELECT TM.TestMasterID,TM.TestMasterName,'' AS [Description], TM.IsActive, GETDATE() AS CreatedOn,111 AS CreatedBy,GETDATE() AS ModifiedOn,
		333 AS ModifiedBy 
		FROM  TestMaster(NOLOCK) TM 	  INNER JOIN TESTS (NOLOCK) T ON TM.TestMasterID=T.TESTMASTERID WHERE TESTID=@Testid 
	END
	ELSE
		BEGIN
		SELECT TM.TestMasterID,TM.TestMasterName,'' AS [Description], TM.IsActive, GETDATE() AS CreatedOn,111 AS CreatedBy,GETDATE() AS ModifiedOn,
		333 AS ModifiedBy 
		FROM  TestMaster(NOLOCK) TM 	
	END
END
GO