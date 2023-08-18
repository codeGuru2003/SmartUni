using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class CourseCredit : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = "";
	}
}
