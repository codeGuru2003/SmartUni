using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class Bank : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int ClientID { get; set; }
		[Required]
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? ContactNumber { get; set; }
	}
}
