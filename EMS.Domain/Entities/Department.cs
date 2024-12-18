using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Domain.Entities
{
    public class Department : AuditTraialBase
    {
        public string DepartmentName { get; set; }
        public string? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public decimal? Budget { get; set; }
    }
}
