using EMS.Domain.Entities;
using EMS.Domain.Interfaces;
using EMS.Infrastructure.GenericRepository;
using EMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace EMS.Infrastructure.Repositories.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        #region Fields
        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public EmployeeRepository(ApplicationDbContext dbContext, ApplicationDbContext applicationDbContext)
            : base(dbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #endregion

        #region Methods
        public async Task<bool> BeAnExistingEmployee(string employeeName, string phone, string departmentId)
        {
            return await _applicationDbContext.Employees.AnyAsync(x => x.EmployeeName == employeeName && x.Phone == phone && x.DepartmentId == departmentId);
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            employee.CreateDate = DateTime.UtcNow;
            employee.CreateBy = "Unknown";
            await InsertAsync(employee);
            return employee;
        }

        public async Task<(long Count, Employee[] employees)> GetEmployees(string searchTerm, int pageIndex, int pageSize, string sortField, string sortOrder)
        {
            Expression<Func<Employee, bool>> filter = null;

            if (!searchTerm.IsNullOrEmpty())
            {
                filter = x => x.EmployeeName.ToLower().Contains(searchTerm.ToLower());
            }

            var count = await CountAsync(filter);

            var dbset = _dbSet.Include(i => i.Department).AsQueryable();

            var Employees = await FilterAsync(
                predicate: filter,
                pageIndex: pageIndex,
                pageSize: pageSize,
                dbset: dbset);

            return (count, Employees is not null ? Employees.ToArray() : Array.Empty<Employee>());
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            employee.UpdateDate = DateTime.UtcNow;
            employee.UpdateBy = "Unknown";
            await UpdateAsync(employee);
            return employee;
        }
        #endregion
    }
}
