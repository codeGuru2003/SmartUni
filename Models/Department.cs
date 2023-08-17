using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace SmartUni.Models
{
	public class Department : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int CollegeID { get; set; }
		[ForeignKey(nameof(CollegeID))]
		public virtual College?	College { get; set; }
		public int CurrencyID { get; set; }
		[ForeignKey(nameof(CurrencyID))]
		public virtual CurrencyType? CurrencyType { get; set; }
		[Required] 
		public string? Name { get; set; }
		[Display(Name = "Contact Number")]
		public string? ContactNumber { get; set; }
		[Display(Name = "Pre-Requisite Required")]
		public bool PrerequisiteRequired { get; set; }
		[Display(Name = "Graduation Credits")]
		public int GraduationCredits { get; set; }
		[Display(Name = "Freshman Credit")]
		public int FreshmanCredit { get; set; }
		[Display(Name = "Email Address")]
		public string? EmailAddress { get; set; }
		public string? Address { get; set; }
		[Display(Name = "Max Payment Days")]
		public int MaxPaymentDays { get; set; }
		public string? Description { get; set; }
		[Display(Name = "Minimum Payment At Registration")]
		public float? MinPaymentDays { get; set; }
		[Display(Name = "Is Flat Rate")]
		public bool IsFlatRate { get; set; } = false;
		public float? Money { get; set; }
	}
}
