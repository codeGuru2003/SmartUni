using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class LevelType : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
	}
}
