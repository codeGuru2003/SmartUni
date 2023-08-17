using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class MajorMinor : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		public bool? IsMajor { get; set; }
	}
}
