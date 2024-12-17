using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Queries.Employees
{
    public class GetEmployeeQueryValidator : AbstractValidator<GetEmployeeQuery>
    {
        private readonly IEmployeeRepository _repository;
        public GetEmployeeQueryValidator(IEmployeeRepository repository)
        {
            _repository = repository;
        }
    }
}
