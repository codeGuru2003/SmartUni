using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class AccountType : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
