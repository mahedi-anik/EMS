CREATE PROCEDURE EmployeeWisePerformanceHistory
    @EmployeeId UNIQUEIDENTIFIER
AS
BEGIN
    -- Retrieve employee-wise performance review details
    SELECT 
        DepartmentName AS Department,
        M.EmployeeName AS Manager,
        EM.EmployeeName,
        EM.Email,
        EM.Phone,
        EM.Position,
        CONVERT(VARCHAR, EM.JoiningDate, 103) AS DOJ,
        EM.Address,
        CASE 
            WHEN EM.IsActive = '1' THEN 'Active' 
            ELSE 'Inactive' 
        END AS Status,
        CONVERT(VARCHAR, PR.ReviewDate, 103) AS ReviewDate,
        PR.ReviewScore,
        PR.ReviewNote
    FROM Employees EM
    LEFT JOIN Departments DEP ON EM.DepartmentId = DEP.Id
    LEFT JOIN Employees M ON DEP.ManagerId = M.Id
    LEFT JOIN PerformanceReviews PR ON EM.Id = PR.EmployeeId
    WHERE EM.Id = @EmployeeId
END
