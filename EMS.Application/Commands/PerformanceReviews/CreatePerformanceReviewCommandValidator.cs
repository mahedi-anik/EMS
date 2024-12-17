using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Commands.PerformanceReviews
{
    public class CreatePerformanceReviewCommandValidator : AbstractValidator<CreatePerformanceReviewCommand>
    {
        #region Fields
        private readonly IPerformanceReviewRepository _repository;
        #endregion

        #region Ctor
        public CreatePerformanceReviewCommandValidator(IPerformanceReviewRepository repository)
        {
            _repository = repository;
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty();



        }

        #endregion


    }
}
