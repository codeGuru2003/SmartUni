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
        public int GenderID { get; set; }
        [ForeignKey(nameof(GenderID))]
        public virtual GenderType? GenderType { get; set; }
        public int GroupID { get; set; }
        [ForeignKey(nameof(GroupID))]
        public virtual Group? Group { get; set; }
        public int TitleID { get; set; }
        [ForeignKey(nameof(TitleID))]
        public virtual TitleType? TitleType { get; set; }
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
