using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Commands.Departments
{
    public class UpdateDepartmentCommand : IRequest<ServiceResponse>
    {
        public string Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? ManagerId { get; set; }
        public decimal Budget { get; set; }
        public bool IsActive { get; set; }
    }
}
