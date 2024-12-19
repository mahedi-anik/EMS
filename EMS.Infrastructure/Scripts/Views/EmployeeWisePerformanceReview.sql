CREATE VIEW EmployeeWisePerformanceReview AS
SELECT 
    dep.DepartmentName AS Department,
    m.EmployeeName AS Manager,
    em.EmployeeName,
    em.Email,
    em.Phone,
    em.Position,
    CONVERT(VARCHAR, em.JoiningDate, 103) AS DOJ,
    em.Address,
    CASE 
        WHEN em.IsActive = '1' THEN 'Active' 
        ELSE 'Inactive' 
    END AS Status,
    CONVERT(VARCHAR, pr.ReviewDate, 103) AS ReviewDate,
    pr.ReviewScore,
    pr.ReviewNote
FROM 
    Employees em
LEFT JOIN 
    Departments dep ON em.DepartmentId = dep.Id
LEFT JOIN 
    Employees m ON dep.ManagerId = m.Id
LEFT JOIN 
    PerformanceReviews pr ON em.Id = pr.EmployeeId;
