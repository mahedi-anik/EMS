using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Queries.Employees
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, QueryRecordResponse<EmployeeResponse>>
    {
        #region Fields
        private readonly ILogger<GetEmployeeQueryHandler> _logger;
        private readonly GetEmployeeQueryValidator _validator;
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        public GetEmployeeQueryHandler(
            ILogger<GetEmployeeQueryHandler> logger,
            GetEmployeeQueryValidator validator,
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
        public async Task<QueryRecordResponse<EmployeeResponse>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidation();

                var result = await _repository.FindAsync(request.Id);
                var employeeResponse = _mapper.Map<EmployeeResponse>(result);

                return Response.BuildQueryRecordResponse<EmployeeResponse>().BuildSuccessResponse(employeeResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return Response.BuildQueryRecordResponse<EmployeeResponse>().BuildErrorResponse(
                    Response.BuildErrorResponse().BuildExternalError(ex.Message));

            }
        }
        #endregion
    }
}
