using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Queries.PerformanceReviews
{
    public class GetPerformanceReviewQueryHandler : IRequestHandler<GetPerformanceReviewQuery, QueryRecordResponse<PerformanceReviewResponse>>
    {
        #region Fields
        private readonly ILogger<GetPerformanceReviewQueryHandler> _logger;
        private readonly GetPerformanceReviewQueryValidator _validator;
        private readonly IPerformanceReviewRepository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        public GetPerformanceReviewQueryHandler(
            ILogger<GetPerformanceReviewQueryHandler> logger,
            GetPerformanceReviewQueryValidator validator,
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
        public async Task<QueryRecordResponse<PerformanceReviewResponse>> Handle(GetPerformanceReviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidation();

                var result = await _repository.FindAsync(request.Id);
                var performanceReviewResponse = _mapper.Map<PerformanceReviewResponse>(result);

                return Response.BuildQueryRecordResponse<PerformanceReviewResponse>().BuildSuccessResponse(performanceReviewResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return Response.BuildQueryRecordResponse<PerformanceReviewResponse>().BuildErrorResponse(
                    Response.BuildErrorResponse().BuildExternalError(ex.Message));

            }
        }
        #endregion
    }
}
