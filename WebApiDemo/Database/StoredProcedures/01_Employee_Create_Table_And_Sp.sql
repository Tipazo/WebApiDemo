-- ========================================
-- CREATION OF RECURSIVE TABLE (dbo) WITH STATUS FLAG
-- ========================================
CREATE TABLE dbo.Employee (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,   -- Unique identifier
    Position NVARCHAR(100) NOT NULL,            -- Job title
    FullName NVARCHAR(150) NOT NULL,            -- Employee name
    ManagerId INT NULL,                         -- Reference to immediate manager
    IsEnabled BIT NOT NULL DEFAULT 1,           -- Active = 1, Inactive = 0
    CONSTRAINT FK_Employee_Manager FOREIGN KEY (ManagerId)
        REFERENCES dbo.Employee(EmployeeId)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);
GO

-- ========================================
-- CRUD STORED PROCEDURES WITH SOFT DELETE
-- ========================================

-- Insert
CREATE OR ALTER PROCEDURE dbo.sp_InsertEmployee
    @Position NVARCHAR(100),
    @FullName NVARCHAR(150),
    @ManagerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Employee (Position, FullName, ManagerId, IsEnabled)
    VALUES (@Position, @FullName, @ManagerId, 1);

    SELECT TOP 1 *
    FROM Employee
    WHERE EmployeeId = SCOPE_IDENTITY();
END;
GO

-- Update
CREATE OR ALTER PROCEDURE dbo.sp_UpdateEmployee
    @EmployeeId INT,
    @Position NVARCHAR(100),
    @FullName NVARCHAR(150),
    @ManagerId INT = NULL,
    @IsEnabled BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Employee
    SET Position = @Position,
        FullName = @FullName,
        ManagerId = @ManagerId,
        IsEnabled = @IsEnabled
    WHERE EmployeeId = @EmployeeId;

    SELECT *
    FROM dbo.Employee
    WHERE EmployeeId = @EmployeeId;
END;
GO


-- Get Employees by @EmployeeId
CREATE OR ALTER PROCEDURE dbo.sp_GetEmployeesById
    @EmployeeId INT
AS
BEGIN
    SELECT EmployeeId, Position, FullName, ManagerId, IsEnabled
    FROM dbo.Employee
    WHERE EmployeeId = @EmployeeId;
END;
GO

-- Get Employees Hierarchy
CREATE OR ALTER PROCEDURE dbo.sp_GetEmployeeHierarchy
    @RootEmployeeId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Hierarchy AS (
        -- Root node: either top-level employees or a specific employee
        SELECT 
            EmployeeId,
            Position,
            FullName,
            IsEnabled,
            ManagerId,
            0 AS Level
        FROM dbo.Employee
        WHERE (@RootEmployeeId IS NULL AND ManagerId IS NULL)
           OR (@RootEmployeeId IS NOT NULL AND EmployeeId = @RootEmployeeId)

        UNION ALL

        -- Recursive part: find subordinates
        SELECT 
            e.EmployeeId,
            e.Position,
            e.FullName,
            e.IsEnabled,
            e.ManagerId,
            h.Level + 1
        FROM dbo.Employee e
        INNER JOIN Hierarchy h ON e.ManagerId = h.EmployeeId
    )
    SELECT 
        EmployeeId,
        Position,
        FullName,
        IsEnabled,
        ManagerId,
        Level
    FROM Hierarchy
    ORDER BY Level, EmployeeId
END;
GO
