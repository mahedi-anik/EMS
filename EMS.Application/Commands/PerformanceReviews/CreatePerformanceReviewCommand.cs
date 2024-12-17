using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Commands.PerformanceReviews
{
    public class CreatePerformanceReviewCommand : IRequest<ServiceResponse>
    {
        public string EmployeeId { get; set; }
        public DateTime ReviewDate { get; set; }
        public decimal ReviewScore { get; set; }
        public string ReviewNote { get; set; }
        public bool IsActive { get; set; }
    }
}
