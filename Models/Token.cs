using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public class Token : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Reference No")]
        [Required]
        public string? ReferenceNo { get; set; }
        public string? Value { get; set; }
        [Display(Name = "Has Entered")]
        public bool HasEntered { get; set; }
        [Display(Name = "Date Entered")]
        public DateTime DateEntered { get; set; }
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

    }
}
