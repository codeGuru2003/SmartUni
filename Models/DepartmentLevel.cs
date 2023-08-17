using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class DepartmentLevel : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int DepartmentID { get; set; }
		[ForeignKey(nameof(DepartmentID))]
		public virtual Department? Department { get; set; }
		public int LevelID { get; set; }
		[ForeignKey(nameof(LevelID))]
		public virtual LevelType? LevelType { get; set; }
		[Display(Name = "Minimum Credit")]
		public int MinCredit { get; set; }
		[Display(Name = "Maximum Credit")]
		public int MaxCredit { get; set; }
	}
}
