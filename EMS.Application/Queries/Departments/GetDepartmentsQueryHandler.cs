using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Queries.Departments
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, QueryRecordsResponse<DepartmentResponse>>
    {
        #region Fields
        private readonly ILogger<GetDepartmentsQueryHandler> _logger;
        private readonly GetDepartmentsQueryValidator _validator;
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public GetDepartmentsQueryHandler(
            ILogger<GetDepartmentsQueryHandler> logger,
            GetDepartmentsQueryValidator validator,
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
        public async Task<QueryRecordsResponse<DepartmentResponse>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidation();

                var result = await _repository.GetDepartments(
                    searchTerm: request.SearchTerm,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    sortField: request.SortField,
                    sortOrder: request.SortOrder);
                var departmentResponse = _mapper.Map<DepartmentResponse[]>(result.departments);

                return new QueryRecordsResponse<DepartmentResponse>().BuildSuccessResponse(
                count: result.Count,
                records: departmentResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return Response.BuildQueryRecordsResponse<DepartmentResponse>().BuildErrorResponse(Response.BuildErrorResponse().BuildExternalError(ex.Message));

            }
        }

        #endregion
    }
}
