using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class DepartmentDegree : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? DepartmentID { get; set; }
		[ForeignKey(nameof(DepartmentID))]
		public virtual Department? Department { get; set; }
		public string? Name { get; set; }
	}
}
