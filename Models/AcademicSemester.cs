using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class AcademicSemester : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int AcademicYearId { get; set; }
		[ForeignKey(nameof(AcademicYearId))]
		public virtual AcademicYearType? AcademicYearType { get; set; }
		public int SemesterId { get; set; }
		[ForeignKey(nameof(SemesterId))]
		public virtual SemesterType? SemesterType { get; set; }	
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsActive { get; set; }

	}
}
