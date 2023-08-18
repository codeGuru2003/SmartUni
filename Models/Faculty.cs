using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
    public class Faculty : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public int? FacultyTypeID { get; set; }
        [ForeignKey(nameof(FacultyTypeID))]
        public virtual FacultyType? FacultyType { get; set; }
        public string? UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public virtual ApplicationUser? User { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int NationalityID { get; set; }
        [ForeignKey(nameof(NationalityID))]
        public virtual NationalityType? Nationality { get; set; }
        public int MaritalStatusTypeID { get; set; }
        [ForeignKey(nameof(MaritalStatusTypeID))]
        public virtual MaritalStatusType? MaritalStatusType { get; set; }
        public int DepartmentID { get; set; }
        [ForeignKey(nameof(DepartmentID))]
        public virtual Department? Department { get; set; }

    }
}
