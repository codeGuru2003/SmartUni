using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class Course : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int DepartmentID { get; set; }
		[ForeignKey(nameof(DepartmentID))]
		public virtual Department? Department { get; set; }
		public int CourseTypeID { get; set; }
		[ForeignKey(nameof(CourseTypeID))]
		public virtual CourseType? CourseType { get; set; }
		[Display(Name = "Course Name")]
		public string? CourseName { get; set; }
		[Display(Name = "Abbreviated Name")]
		public string? CourseShortName { get; set; }
		public  string? CourseCode { get; set; }
		public decimal CreditHours { get; set; }
		public string? Description { get; set; }
		public bool IsLab { get; set; }
		public bool IsActive { get; set; }
	}
}
