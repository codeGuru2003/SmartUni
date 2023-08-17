namespace SmartUni.Models
{
    public class DeleteLogs
    {
        public int Id { get; set; }
        public string? ResourceName { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
