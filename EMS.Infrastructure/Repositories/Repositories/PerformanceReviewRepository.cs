using EMS.Application.Extensions;
using EMS.Domain.Entities;
using EMS.Domain.Interfaces;
using EMS.Infrastructure.GenericRepository;
using EMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace EMS.Infrastructure.Repositories.Repositories
{
    public class PerformanceReviewRepository : GenericRepository<PerformanceReview>, IPerformanceReviewRepository
    {
        #region Fields
        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public PerformanceReviewRepository(ApplicationDbContext dbContext, ApplicationDbContext applicationDbContext)
            : base(dbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #endregion

        #region Methods
        public async Task<PerformanceReview> CreatePerformanceReview(PerformanceReview performanceReview)
        {
            performanceReview.CreateDate = DateTime.UtcNow;
            performanceReview.CreateBy = "Tester";
            performanceReview.UpdateBy = "Tester";
            await InsertAsync(performanceReview);
            return performanceReview;
        }

        public async Task<(long Count, PerformanceReview[] performanceReviews)> GetPerformanceReviews(string searchTerm, int pageIndex, int pageSize, string sortField, string sortOrder)
        {
            Expression<Func<PerformanceReview, bool>> filter = null;

            if (!searchTerm.IsNullOrBlank())
            {
                filter = x => x.Employee.EmployeeName.ToLower().Contains(searchTerm.ToLower());
            }

            var count = await CountAsync(filter);

            var dbset = _dbSet.Include(i => i.Employee).AsQueryable();

            var PerformanceReviews = await FilterAsync(
                predicate: filter,
                pageIndex: pageIndex,
                pageSize: pageSize,
                dbset: dbset);

            return (count, PerformanceReviews is not null ? PerformanceReviews.ToArray() : Array.Empty<PerformanceReview>());
        }

        public async Task<PerformanceReview> UpdatePerformanceReview(PerformanceReview performanceReview)
        {
            performanceReview.UpdateDate = DateTime.UtcNow;
            performanceReview.UpdateBy = "Tester";
            performanceReview.CreateBy = "Tester";
            await UpdateAsync(performanceReview);
            return performanceReview;
        }

        #endregion
    }
}
