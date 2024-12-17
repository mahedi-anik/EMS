using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Queries.Employees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, QueryRecordsResponse<EmployeeResponse>>
    {
        #region Fields
        private readonly ILogger<GetEmployeesQueryHandler> _logger;
        private readonly GetEmployeesQueryValidator _validator;
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public GetEmployeesQueryHandler(
            ILogger<GetEmployeesQueryHandler> logger,
            GetEmployeesQueryValidator validator,
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
        public async Task<QueryRecordsResponse<EmployeeResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidation();

                var result = await _repository.GetEmployees(
                    searchTerm: request.SearchTerm,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    sortField: request.SortField,
                    sortOrder: request.SortOrder);
                var employeeResponse = _mapper.Map<EmployeeResponse[]>(result.employees);

                return new QueryRecordsResponse<EmployeeResponse>().BuildSuccessResponse(
                count: result.Count,
                records: employeeResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return Response.BuildQueryRecordsResponse<EmployeeResponse>().BuildErrorResponse(Response.BuildErrorResponse().BuildExternalError(ex.Message));

            }
        }

        #endregion
    }
}
