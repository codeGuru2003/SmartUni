using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class CourseType : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
