using FluentValidation;

namespace EMS.Application.Queries.Departments
{
    public class GetDepartmentsQueryValidator : AbstractValidator<GetDepartmentsQuery>
    {
        public GetDepartmentsQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
