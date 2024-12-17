namespace EMS.Application.DTOs
{
    public class PerformanceReview
    {
        public string? Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ReviewDate { get; set; }
        public decimal ReviewScore { get; set; }
        public string ReviewNote { get; set; }
        public bool IsActive { get; set; }
    }
}
