using AutoMapper;
using EMS.Application.DTOs;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Commands.PerformanceReviews
{
    public class UpdatePerformanceReviewCommandHandler : IRequestHandler<UpdatePerformanceReviewCommand, ServiceResponse>
    {
        #region Fields
        private readonly ILogger<UpdatePerformanceReviewCommandHandler> _logger;
        private readonly UpdatePerformanceReviewCommandValidator _validator;
        private readonly IPerformanceReviewRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public UpdatePerformanceReviewCommandHandler(
            ILogger<UpdatePerformanceReviewCommandHandler> logger,
            UpdatePerformanceReviewCommandValidator validator,
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
        public async Task<ServiceResponse> Handle(UpdatePerformanceReviewCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidation();

                var performanceReview = await _repository.FindAsync(command.Id);
                _mapper.Map(command, performanceReview);

                var result = await _repository.UpdatePerformanceReview(performanceReview);
                var performanceReviewResponse = _mapper.Map<PerformanceReviewResponse>(result);

                return Response.BuildServiceResponse().BuildSuccessResponse(performanceReviewResponse);

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
