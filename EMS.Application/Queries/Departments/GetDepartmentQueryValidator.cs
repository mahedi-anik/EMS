using EMS.Domain.Interfaces;
using FluentValidation;

namespace EMS.Application.Queries.Departments
{
    public class GetDepartmentQueryValidator : AbstractValidator<GetDepartmentQuery>
    {
        private readonly IDepartmentRepository _repository;
        public GetDepartmentQueryValidator(IDepartmentRepository repository)
        {
            _repository = repository;
        }
    }
}
