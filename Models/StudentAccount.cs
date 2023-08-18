using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class StudentAccount : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int StudentID { get; set; }
		[ForeignKey(nameof(StudentID))]
		public virtual Student? Student { get; set; }
		public int? AcademicID { get; set; }
		[ForeignKey(nameof(AcademicID))]
		public virtual AcademicSemester? AcademicSemester { get; set; }
		public int? CurrencyID { get; set; }
		[ForeignKey(nameof(CurrencyID))]
		public virtual CurrencyType? CurrencyType { get; set; }
		public int BankID { get; set; }
		[ForeignKey("BankID")]
		public virtual Bank? Bank { get; set; }
		public int AccountTypeID { get; set; }
		[ForeignKey(nameof(AccountTypeID))]
		public virtual AccountType? AccountType { get; set;}
		public string AccountNumber { get; set; } = "";
		public string? TransactionCode { get; set; }
		public float Amount { get; set; }
		public string? ReferenceNumber { get; set; }
		public float RunningBalance { get; set; }
		public bool IsValid { get; set; }
	}
}
