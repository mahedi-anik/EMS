using EMS.Application.DTOs;
using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Queries.Departments
{
    public class GetDepartmentQuery : IRequest<QueryRecordResponse<DepartmentResponse>>
    {
        public string Id { get; set; }
    }
}
