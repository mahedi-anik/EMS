using EMS.Application.DTOs;
using EMS.Application.Requests;

namespace EMS.Application.Queries.Departments
{
    public class GetDepartmentsQuery : PagedQueryRequestBase<DepartmentResponse>
    {
        public string? SearchTerm { get; set; } 
    }
}
