using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class DepartmentMajor : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int CollegeID { get; set; }
		[ForeignKey(nameof(CollegeID))]
		public virtual College? College { get; set; }
	}
}
