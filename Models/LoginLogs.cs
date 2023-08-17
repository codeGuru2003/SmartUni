using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public class LoginLogs
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public DateTime LogInDate { get; set; }
    }
}
