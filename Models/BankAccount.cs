using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class BankAccount : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int BankID { get; set; }
		[ForeignKey(nameof(BankID))]
		public virtual Bank? Bank { get; set; }
		public int CurrencyID { get; set; }
		[ForeignKey(nameof(CurrencyID))]
		public virtual CurrencyType? CurrencyType { get; set; }
		public string? AccountNumber { get; set; }
	}
}
