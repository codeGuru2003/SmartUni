using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
    public class StudentMajorMinior : AuditTrail
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student? Student { get; set; }
        public int DepartmentMajorId { get; set; }
        [ForeignKey(nameof(DepartmentMajorId))]
        public virtual Department? DepartmentMajor { get; set;}
        public int? DepartmentMiniorId { get; set; }
        [ForeignKey(nameof(DepartmentMiniorId))]
        public virtual Department? DepartmentMinior { get; set; }
        public bool IsActive { get; set; }


    }
}
