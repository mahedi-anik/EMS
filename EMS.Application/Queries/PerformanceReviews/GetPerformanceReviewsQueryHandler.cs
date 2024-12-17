using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Queries.PerformanceReviews
{
    public class GetPerformanceReviewsQueryHandler : IRequestHandler<GetPerformanceReviewsQuery, QueryRecordsResponse<PerformanceReviewResponse>>
    {
        #region Fields
        private readonly ILogger<GetPerformanceReviewsQueryHandler> _logger;
        private readonly GetPerformanceReviewsQueryValidator _validator;
        private readonly IPerformanceReviewRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public GetPerformanceReviewsQueryHandler(
            ILogger<GetPerformanceReviewsQueryHandler> logger,
            GetPerformanceReviewsQueryValidator validator,
            IPerformanceReviewRepository repository,
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
        public async Task<QueryRecordsResponse<PerformanceReviewResponse>> Handle(GetPerformanceReviewsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidation();

                var result = await _repository.GetPerformanceReviews(
                    searchTerm: request.SearchTerm,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    sortField: request.SortField,
                    sortOrder: request.SortOrder);
                var performanceReviewResponse = _mapper.Map<PerformanceReviewResponse[]>(result.performanceReviews);

                return new QueryRecordsResponse<PerformanceReviewResponse>().BuildSuccessResponse(
                count: result.Count,
                records: performanceReviewResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return Response.BuildQueryRecordsResponse<PerformanceReviewResponse>().BuildErrorResponse(Response.BuildErrorResponse().BuildExternalError(ex.Message));

            }
        }

        #endregion
    }
}
