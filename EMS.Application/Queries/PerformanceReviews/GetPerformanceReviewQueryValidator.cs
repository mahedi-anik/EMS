using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Queries.PerformanceReviews
{
    public class GetPerformanceReviewQueryValidator : AbstractValidator<GetPerformanceReviewQuery>
    {
        private readonly IPerformanceReviewRepository _repository;
        public GetPerformanceReviewQueryValidator(IPerformanceReviewRepository repository)
        {
            _repository = repository;
        }
    }
}
