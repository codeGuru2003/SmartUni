using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class Test : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		public TimeSpan Duration { get; set; }
		public string? Remarks { get; set; }
	}
}
