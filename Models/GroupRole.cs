using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class GroupRole : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int GroupID { get; set; }
		[ForeignKey(nameof(GroupID))]
		public virtual Group? Group { get; set; }
		public string RoleID { get; set; }
		[ForeignKey(nameof(RoleID))]
		public virtual IdentityRole? IdentityRole { get; set; }
	}
}
