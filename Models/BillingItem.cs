using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class BillingItem : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? CollegeID { get; set; }
		[ForeignKey(nameof(CollegeID))]
		public virtual CollegeType? CollegeType { get; set; }
		public int? BillingTypeID { get; set; }
		[ForeignKey(nameof(BillingTypeID))]
		public virtual BillingType? BillingType { get; set; }
		public int? CurrencyTypeID { get; set; }
		[ForeignKey(nameof(CurrencyTypeID))]
		public virtual CurrencyType? CurrencyType { get; set; }
		[Required]
		public decimal CurrencyAmount { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
	}
}
