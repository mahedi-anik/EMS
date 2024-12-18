namespace EMS.Domain.Entities
{
    public class AuditTraialBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive {  get; set; }
        public bool IsDelete { get; set; }
    }
}
