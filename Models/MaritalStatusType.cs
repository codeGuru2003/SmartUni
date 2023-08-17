using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class MaritalStatusType : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = String.Empty;
	}
}
