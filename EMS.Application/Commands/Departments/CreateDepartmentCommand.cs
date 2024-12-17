using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Commands.Departments
{
    public class CreateDepartmentCommand : IRequest<ServiceResponse>
    {
        public string DepartmentName { get; set; }
        public string? ManagerId { get; set; }
        public decimal Budget { get; set; }
        public bool IsActive { get; set; }
    }
}
