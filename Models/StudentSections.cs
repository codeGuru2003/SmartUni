using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
    public class StudentSections : AuditTrail
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student? Student { get; set; }
        public int? SectionId { get; set;}
        [ForeignKey(nameof(SectionId))]
        public virtual Sections? Sections { get; set; }
        public int? CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course? Course { get; set; }
        public int? AcademicSemesterId { get; set;}
        [ForeignKey(nameof(AcademicSemesterId))]
        public virtual AcademicSemester? AcademicSemester { get; set; }
        public int? StatusTypesId { get; set; }
        [ForeignKey(nameof(StatusTypesId))]
        public virtual StatusType? StatusTypes { get; set; }    
        
    }
}
