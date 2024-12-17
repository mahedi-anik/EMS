using EMS.Domain.Entities;
using EMS.Domain.Interfaces;
using EMS.Infrastructure.GenericRepository;
using EMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace EMS.Infrastructure.Repositories.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        #region Fields
        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DepartmentRepository(ApplicationDbContext dbContext, ApplicationDbContext applicationDbContext)
            : base(dbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #endregion

        #region Methods
        public async Task<bool> BeAnExistingDepartment(string departmentName)
        {
            return await _applicationDbContext.Departments.AnyAsync(x => x.DepartmentName == departmentName);

        }

        public async Task<Department> CreateDepartment(Department department)
        {
            department.CreateDate = DateTime.UtcNow;
            department.CreateBy = "Unknown";
            await InsertAsync(department);
            return department;
        }

        public async Task<(long Count, Department[] departments)> GetDepartments(string searchTerm, int pageIndex, int pageSize, string sortField, string sortOrder)
        {
            Expression<Func<Department, bool>> filter = null;

            if (!searchTerm.IsNullOrEmpty())
            {
                filter = x => x.DepartmentName.ToLower().Contains(searchTerm.ToLower());
            }

            var count = await CountAsync(filter);

            var dbset = _dbSet.Include(i => i.Employee).AsQueryable();

            var Departments = await FilterAsync(
                predicate: filter,
                pageIndex: pageIndex,
                pageSize: pageSize,
                dbset: dbset);

            return (count, Departments is not null ? Departments.ToArray() : Array.Empty<Department>());
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            department.UpdateDate = DateTime.UtcNow;
            department.UpdateBy = "Unknown";
            await UpdateAsync(department);
            return department;
        }

        #endregion
    }
}
