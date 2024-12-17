using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Commands.Departments
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        #region Fields
        private readonly IDepartmentRepository _repository;
        #endregion

        #region Ctor
        public CreateDepartmentCommandValidator(IDepartmentRepository repository)
        {
            _repository = repository;
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty();
            RuleFor(x => x).MustAsync(NotBeAnExistingDepartment).WithMessage("Department already in use");


        }

        #endregion

        #region Methods
        private async Task<bool> NotBeAnExistingDepartment(CreateDepartmentCommand command, CancellationToken token)
        {
            return !await _repository.BeAnExistingDepartment(command.DepartmentName);
        }

        #endregion
    }
}
