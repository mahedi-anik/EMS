using AutoMapper;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Entities;
using EMS.Domain.Interfaces;
using EMS.Infrastructure.Repositories.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Commands.Employees
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ServiceResponse>
    {
        #region Fileds
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;
        private readonly CreateEmployeeCommandValidator _validator;
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public CreateEmployeeCommandHandler(
            ILogger<CreateEmployeeCommandHandler> logger,
            CreateEmployeeCommandValidator validator,
            IEmployeeRepository repository,
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
        public async Task<ServiceResponse> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidation();

                var employee = _mapper.Map<Employee>(command);
                var result = await _repository.CreateEmployee(employee);
                var employeeResponse = _mapper.Map<EmployeeRepository>(result);

                return Response.BuildServiceResponse().BuildSuccessResponse(employeeResponse);

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
