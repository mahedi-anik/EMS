using EMS.Domain.Entities;

namespace EMS.Domain.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department> CreateDepartment(Department department);
        Task<(long Count, Department[] departments)> GetDepartments(string searchTerm, int pageIndex, int pageSize, string sortField, string sortOrder);
        Task<Department> UpdateDepartment(Department department);
        Task<bool> BeAnExistingDepartment(string departmentName);
    }
}
