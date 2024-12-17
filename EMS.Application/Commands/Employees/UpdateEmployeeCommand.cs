using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest<ServiceResponse>
    {
        public string Id { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? DepartmentId { get; set; }
        public string? Position { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
