using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class StudentBill : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? StudentID { get; set; }
		[ForeignKey(nameof(StudentID))]
		public virtual Student? Student { get; set; }
		public int StatusTypeID { get; set; }
		[ForeignKey(nameof(StatusTypeID))]
		public virtual StatusType? StatusType { get; set; }
		public int CurrencyID { get; set; }
		[ForeignKey(nameof(CurrencyID))]
		public virtual CurrencyType? CurrencyType { get; set; }
		public float TotalAmount { get; set; }
		public bool IsValid { get; set; }
		public string? Barcode { get; set; }
	}
}
