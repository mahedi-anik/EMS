using FluentValidation;

namespace EMS.Application.Queries.Employees
{
    public class GetEmployeesQueryValidator  : AbstractValidator<GetEmployeesQuery>
    {
        public GetEmployeesQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
