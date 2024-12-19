using EMS.Application.Attributes;
using EMS.Application.Commands.PerformanceReviews;
using EMS.Application.DTOs;
using EMS.Application.Queries.PerformanceReviews;
using EMS.Application.Responses;
using EMS.Application.StoredProcedureService;
using EMS.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Api.Controllers
{
    [ApiController]
    [AuthorizationNotRequired]
    [Route("api/[controller]")]
    public class PerformanceReviewController  : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PerformanceReviewService _performanceReviewService;

        #endregion

        #region Ctor
        public PerformanceReviewController(IMediator mediator, IHttpContextAccessor httpContextAccessor, PerformanceReviewService performanceReviewService)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _performanceReviewService = performanceReviewService;
        }
        #endregion

        #region Methods
        [HttpGet(EndpointRoutes.Action_GetPerformanceReview)]
        public async Task<QueryRecordResponse<PerformanceReviewResponse>> GetPerformanceReview([FromQuery] GetPerformanceReviewQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet(EndpointRoutes.Action_GetPerformanceReviews)]
        public async Task<QueryRecordsResponse<PerformanceReviewResponse>> GetPerformanceReviews([FromQuery] GetPerformanceReviewsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost(EndpointRoutes.Action_CreatePerformanceReview)]
        public async Task<ServiceResponse> CreatePerformanceReview(CreatePerformanceReviewCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(EndpointRoutes.Action_UpdatePerformanceReview)]
        public async Task<ServiceResponse> UpdatePerformanceReview(UpdatePerformanceReviewCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetEmployeePerformanceHistory/{id}")]
        public async Task<IActionResult> GetEmployeePerformanceHistory(string id)
        {
            try
            {
                var performanceHistory = await _performanceReviewService.GetEmployeePerformanceHistoryAsync(id);

                if (performanceHistory == null || !performanceHistory.Any())
                    return NotFound(new { message = "No performance history found for the given employee ID." });

                return Ok(performanceHistory);
            }
            catch (Exception ex)
            {
                // Add logging if needed
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching performance history.", details = ex.Message });
            }
        }
        #endregion
    }
}
