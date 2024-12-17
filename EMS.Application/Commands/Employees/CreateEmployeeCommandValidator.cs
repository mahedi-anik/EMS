using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Commands.Employees
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        #region Fields
        private readonly IEmployeeRepository _repository;
        #endregion

        #region Ctor
        public CreateEmployeeCommandValidator(IEmployeeRepository repository)
        {
            _repository = repository;
            RuleFor(x => x.EmployeeName).NotNull().NotEmpty();
            RuleFor(x => x).MustAsync(NotBeAnExistingEmployee).WithMessage("Employee already in use");


        }

        #endregion

        #region Methods
        private async Task<bool> NotBeAnExistingEmployee(CreateEmployeeCommand command, CancellationToken token)
        {
            return !await _repository.BeAnExistingEmployee(command.EmployeeName, command.Phone, command.DepartmentId);
        }

        #endregion
    }
}
