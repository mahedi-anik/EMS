using AutoMapper;
using EMS.Application.Extensions;
using EMS.Application.Responses;
using EMS.Domain.Entities;
using EMS.Domain.Interfaces;
using EMS.Infrastructure.Repositories.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Commands.PerformanceReviews
{
    public class CreatePerformanceReviewCommandHandler : IRequestHandler<CreatePerformanceReviewCommand, ServiceResponse>
    {
        #region Fileds
        private readonly ILogger<CreatePerformanceReviewCommandHandler> _logger;
        private readonly CreatePerformanceReviewCommandValidator _validator;
        private readonly IPerformanceReviewRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public CreatePerformanceReviewCommandHandler(
            ILogger<CreatePerformanceReviewCommandHandler> logger,
            CreatePerformanceReviewCommandValidator validator,
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

        #region methods
        public async Task<ServiceResponse> Handle(CreatePerformanceReviewCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidation();

                var performanceReview = _mapper.Map<PerformanceReview>(command);
                var result = await _repository.CreatePerformanceReview(performanceReview);
                var performanceReviewResponse = _mapper.Map<PerformanceReviewRepository>(result);

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
