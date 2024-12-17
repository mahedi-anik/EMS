using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Commands.Departments
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ServiceResponse>
    {
        #region Fields
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger;
        private readonly UpdateDepartmentCommandValidator _validator;
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public UpdateDepartmentCommandHandler(
            ILogger<UpdateDepartmentCommandHandler> logger,
            UpdateDepartmentCommandValidator validator,
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

        #region Methods
        public async Task<ServiceResponse> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidation();

                var department = await _repository.FindAsync(command.Id);
                _mapper.Map(command, department);

                var result = await _repository.UpdateDepartment(department);
                var departmentResponse = _mapper.Map<DepartmentResponse>(result);

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
