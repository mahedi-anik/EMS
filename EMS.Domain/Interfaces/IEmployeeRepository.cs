using EMS.Domain.Entities;
namespace EMS.Domain.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> CreateEmployee(Employee employee);
        Task<(long Count, Employee[] employees)> GetEmployees(string searchTerm, int pageIndex, int pageSize, string sortField, string sortOrder);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<bool> BeAnExistingEmployee(string employeeName,string phone,string departmentId);

    }
}
