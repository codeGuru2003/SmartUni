using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class UserGroup : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public string UserID { get; set; }
		[ForeignKey(nameof(UserID))]
		public virtual ApplicationUser? ApplicationUser { get; set; }
		public int GroupID { get; set; }
		[ForeignKey(nameof(GroupID))]
		public virtual Group? Group { get; set; }

	}
}
