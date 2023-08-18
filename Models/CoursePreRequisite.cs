using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class CoursePreRequisite : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? CourseID { get; set; }
		[ForeignKey(nameof(CourseID))]
		public virtual Course? Course { get; set; }
		public int? PreRequisiteCourseID { get; set; }
		[ForeignKey(nameof(PreRequisiteCourseID))]
		public virtual Course? PreRequisiteCourse { get; set; }
	}
}
