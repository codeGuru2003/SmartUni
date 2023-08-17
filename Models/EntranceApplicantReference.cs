using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class EntranceApplicantReference : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? EntranceApplicantID { get; set; }
		[ForeignKey(nameof(EntranceApplicantID))]
		public virtual EntranceApplicant? EntranceApplicant { get; set;}
		[Display(Name = "Referee Name")]
		public string? RefereeName { get; set; }

		[Display(Name = "Institution")]
		public string? Institution { get; set; }
		[Display(Name = "Position")]
		public string? Position { get; set; }
		[Display(Name = "Email")]
		public string? EmailAddress { get; set; }
		public int StatusTypeID { get; set; }
		[ForeignKey(nameof(StatusTypeID))]
		public virtual StatusType? StatusType { get; set; }

	}
}
