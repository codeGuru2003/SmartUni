using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class CourseCost : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? CourseID { get; set; }
		[ForeignKey(nameof(CourseID))]
		public virtual Course? Course { get; set; }

		public int? DepartmentID { get; set; }
		[ForeignKey(nameof(DepartmentID))]
		public virtual Department? Department { get; set; }
		public float NationalAmount { get; set; }
		public float InternationalAmount { get; set; }
	}
}
