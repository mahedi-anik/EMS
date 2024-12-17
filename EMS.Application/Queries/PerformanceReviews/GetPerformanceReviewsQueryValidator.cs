using FluentValidation;

namespace EMS.Application.Queries.PerformanceReviews
{
    public class GetPerformanceReviewsQueryValidator : AbstractValidator<GetPerformanceReviewsQuery>
    {
        public GetPerformanceReviewsQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
