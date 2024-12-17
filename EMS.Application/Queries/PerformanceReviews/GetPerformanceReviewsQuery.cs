using EMS.Application.DTOs;
using EMS.Application.Requests;

namespace EMS.Application.Queries.PerformanceReviews
{
    public class GetPerformanceReviewsQuery : PagedQueryRequestBase<PerformanceReviewResponse>
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}
