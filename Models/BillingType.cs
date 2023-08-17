using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class BillingType : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
	}
}
