using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class DepartmentBillingItem : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? DepartmentID { get; set; }
		[ForeignKey(nameof(DepartmentID))]
		public virtual Department? Department { get; set; }
		public int? BillingTypeID { get; set; }
		[ForeignKey(nameof(BillingTypeID))]
		public virtual BillingType? BillingType { get; set; }
		public int? NationalCurrencyTypeID { get; set; }
		[ForeignKey(nameof(NationalCurrencyTypeID))]
		public virtual CurrencyType? CurrencyType { get; set; }
		public float NationalAmount { get; set; }
		public int? InternationalCurrencyTypeID { get; set; }
		[ForeignKey(nameof(InternationalCurrencyTypeID))]
		public virtual CurrencyType? InternationalCurrencyType { get; set; }
		public float InternationalAmount { get; set; }
		public string? ItemName { get; set; }
	}
}
