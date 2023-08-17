using System.ComponentModel.DataAnnotations;

namespace SmartUni.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string? Old_Password { get; set; }
        [Required]
        public string? New_Password { get; set; }

    }
}
