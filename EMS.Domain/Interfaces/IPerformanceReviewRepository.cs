using EMS.Domain.Entities;

namespace EMS.Domain.Interfaces
{
    public interface IPerformanceReviewRepository : IGenericRepository<PerformanceReview>
    {
        Task<PerformanceReview> CreatePerformanceReview(PerformanceReview performanceReview);
        Task<(long Count, PerformanceReview[] performanceReviews)> GetPerformanceReviews(string searchTerm, int pageIndex, int pageSize, string sortField, string sortOrder);
        Task<PerformanceReview> UpdatePerformanceReview(PerformanceReview performanceReview);
    }
}
