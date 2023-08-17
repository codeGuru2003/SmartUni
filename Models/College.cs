using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class College : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int? CollegeTypeID { get; set; }
		[ForeignKey(nameof(CollegeTypeID))]
		public virtual CollegeType? CollegeType { get; set; }
		[Required]
		public string? Name { get; set; }
		[Display(Name = "Short Name")]
		[Required]
		public string? ShortName { get; set; }
		[Display(Name = "Phone Number")]
		[StringLength(14)]
		public string? PhoneNumber { get; set; }
		[Display(Name = "Email Address")]
		public string? EmailAddress { get; set; }
		[Display(Name = "Address")]
		public string? Address { get; set; }
	}
}
