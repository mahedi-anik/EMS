using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Commands.PerformanceReviews
{
    public class UpdatePerformanceReviewCommandValidator : AbstractValidator<UpdatePerformanceReviewCommand>
    {
        private readonly IPerformanceReviewRepository _repository;
        public UpdatePerformanceReviewCommandValidator(IPerformanceReviewRepository repository)
        {
            _repository = repository;
            RuleFor(X => X.Id).NotNull().NotEmpty().WithMessage("ID is not valid.");
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty();
        }
    }
}