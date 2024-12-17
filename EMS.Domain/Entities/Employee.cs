using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Domain.Entities
{
    public class Employee : AuditTraialBase
    {
        public string EmployeeName { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public string DepartmentId {  get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
        public string Position { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Address { get; set; }
    }
}
