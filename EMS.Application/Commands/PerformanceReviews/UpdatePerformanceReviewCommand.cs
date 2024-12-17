using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Commands.PerformanceReviews
{
    public class UpdatePerformanceReviewCommand : IRequest<ServiceResponse>
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime ReviewDate { get; set; }
        public decimal ReviewScore { get; set; }
        public string ReviewNote { get; set; }
        public bool IsActive { get; set; }
    }
}
