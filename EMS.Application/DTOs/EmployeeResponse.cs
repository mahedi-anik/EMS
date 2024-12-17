using EMS.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Application.DTOs
{
    public class EmployeeResponse
    {
        public string? Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

    }
}
