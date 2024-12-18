using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Domain.Entities
{
    public class PerformanceReview : AuditTraialBase
    {
        public string? EmployeeId {  get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }
        public DateTime ReviewDate { get; set; }
        public decimal? ReviewScore { get; set; }
        public string? ReviewNote { get; set; }
    }
}
