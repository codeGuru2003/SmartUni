using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
	public class TestSchedule : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int TestId { get; set; }
		[ForeignKey(nameof(TestId))]
		public virtual Test? Test { get; set; }
		public DateTime ScheduleDate { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
