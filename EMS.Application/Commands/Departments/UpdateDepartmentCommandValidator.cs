using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Commands.Departments
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        private readonly IDepartmentRepository _repository;
        public UpdateDepartmentCommandValidator(IDepartmentRepository repository)
        {
            _repository = repository;
            RuleFor(X => X.Id).NotNull().NotEmpty().WithMessage("ID is not valid.");
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty();
        }
    }
}