using EMS.Application.DTOs;
using EMS.Application.Requests;

namespace EMS.Application.Queries.Employees
{
    public class GetEmployeesQuery : PagedQueryRequestBase<EmployeeResponse>
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}
