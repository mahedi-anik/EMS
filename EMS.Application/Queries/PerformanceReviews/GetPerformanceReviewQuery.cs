using EMS.Application.DTOs;
using EMS.Application.Responses;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Queries.PerformanceReviews
{
    public class GetPerformanceReviewQuery : IRequest<QueryRecordResponse<PerformanceReviewResponse>>
    {
        public string Id { get; set; }
    }

}