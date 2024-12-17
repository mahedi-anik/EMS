using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Queries.Departments
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, QueryRecordResponse<DepartmentResponse>>
    {
        #region Fields
        private readonly ILogger<GetDepartmentQueryHandler> _logger;
        private readonly GetDepartmentQueryValidator _validator;
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        public GetDepartmentQueryHandler(
            ILogger<GetDepartmentQueryHandler> logger,
            GetDepartmentQueryValidator validator,
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
        public async Task<QueryRecordResponse<DepartmentResponse>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidation();

                var result = await _repository.FindAsync(request.Id);
                var departmentResponse = _mapper.Map<DepartmentResponse>(result);

                return Response.BuildQueryRecordResponse<DepartmentResponse>().BuildSuccessResponse(departmentResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return Response.BuildQueryRecordResponse<DepartmentResponse>().BuildErrorResponse(
                    Response.BuildErrorResponse().BuildExternalError(ex.Message));

            }
        }
        #endregion
    }
}
