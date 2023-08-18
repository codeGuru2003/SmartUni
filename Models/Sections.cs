using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
    public class Sections : AuditTrail
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course? Course { get; set; }
        public int SectionTypeId { get; set; }
        [ForeignKey(nameof(SectionTypeId))]
        public virtual SectionType SectionType { get; set; }
        public int FacultyId { get; set; }
        [ForeignKey(nameof(FacultyId))]
        public virtual Faculty? Faculty { get; set; }
        public int AcademicSemesterId { get; set; }
        [ForeignKey(nameof(AcademicSemesterId))]
        public virtual AcademicSemester?  AcademicSemester { get; set; }

        public int Capacity { get; set; }
        public int Occupant { get; set; }
        public string? Room { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        [Required]
        public string SectionTime { get; set; }

    }
}
