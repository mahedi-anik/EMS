namespace EMS.Application.DTOs
{
    public class EmployeePerformanceReviewDto
    {
        public string Id { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOJ { get; set; }
        public string Status { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewScore { get; set; }
        public string ReviewNote { get; set; }
    }
}
