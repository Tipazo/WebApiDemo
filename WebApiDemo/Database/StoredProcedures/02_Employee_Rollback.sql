-- ========================================
-- ROLLBACK SCRIPT (DROP OBJECTS)
-- ========================================

-- Drop stored procedures if they exist
IF OBJECT_ID('dbo.sp_UpdateEmployee', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateEmployee;
GO

IF OBJECT_ID('dbo.sp_InsertEmployee', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_InsertEmployee;
GO

IF OBJECT_ID('dbo.sp_GetEmployeesById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetEmployeesById;
GO

IF OBJECT_ID('dbo.sp_GetEmployeeHierarchy', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetEmployeeHierarchy;
GO

-- Drop the Employee table
IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL
    DROP TABLE dbo.Employee;
GO
