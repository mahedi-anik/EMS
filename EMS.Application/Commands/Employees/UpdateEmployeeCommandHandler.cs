using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Commands.Employees
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ServiceResponse>
    {
        #region Fields
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
        private readonly UpdateEmployeeCommandValidator _validator;
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public UpdateEmployeeCommandHandler(
            ILogger<UpdateEmployeeCommandHandler> logger,
            UpdateEmployeeCommandValidator validator,
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

        #region Methods
        public async Task<ServiceResponse> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidation();

                var employee = await _repository.FindAsync(command.Id);
                _mapper.Map(command, employee);

                var result = await _repository.UpdateEmployee(employee);
                var employeeResponse = _mapper.Map<EmployeeResponse>(result);

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
