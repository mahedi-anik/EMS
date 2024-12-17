using AutoMapper;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Entities;
using EMS.Domain.Interfaces;
using EMS.Infrastructure.Repositories.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Commands.Departments
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ServiceResponse>
    {
        #region Fileds
        private readonly ILogger<CreateDepartmentCommandHandler> _logger;
        private readonly CreateDepartmentCommandValidator _validator;
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public CreateDepartmentCommandHandler(
            ILogger<CreateDepartmentCommandHandler> logger,
            CreateDepartmentCommandValidator validator,
            IDepartmentRepository repository,
            IMapper mapper
            )
        {
            _logger = logger;
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region methods
        public async Task<ServiceResponse> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidation();

                var department = _mapper.Map<Department>(command);
                var result = await _repository.CreateDepartment(department);
                var departmentResponse = _mapper.Map<DepartmentRepository>(result);

                return Response.BuildServiceResponse().BuildSuccessResponse(departmentResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Response.BuildServiceResponse().BuildErrorResponse(ex.Message);

            }
        }
        #endregion
    }
}
