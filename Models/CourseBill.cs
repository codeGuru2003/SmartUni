using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class CourseBill : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? CourseID { get; set; }
		[ForeignKey(nameof(CourseID))]
		public virtual Course? Course { get; set; }
		public int? LabCourseID { get; set; }
		[ForeignKey(nameof(LabCourseID))]
		public virtual Course? LabCourse { get; set; }

	}
}
