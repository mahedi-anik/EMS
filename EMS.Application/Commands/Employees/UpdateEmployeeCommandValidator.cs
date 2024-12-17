using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Commands.Employees
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;
        public UpdateEmployeeCommandValidator(IEmployeeRepository repository)
        {
            _repository = repository;
            RuleFor(X => X.Id).NotNull().NotEmpty().WithMessage("ID is not valid.");
            RuleFor(x => x.EmployeeName).NotNull().NotEmpty();
        }
    }
}