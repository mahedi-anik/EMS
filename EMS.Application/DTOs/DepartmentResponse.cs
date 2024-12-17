namespace EMS.Application.DTOs
{
    public class DepartmentResponse
    {
        public string? Id { get; set; }
        public string DepartmentName { get; set; }
        public string? ManagerId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Budget { get; set; }
        public bool IsActive { get; set; }
    }
}
