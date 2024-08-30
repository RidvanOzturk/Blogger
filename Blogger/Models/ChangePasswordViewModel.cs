using System.ComponentModel.DataAnnotations;

namespace Blogger.Models
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; } // Kullanıcı ID'si

        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }

}
