using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOs
{
    public class ChangePasswordResponseDTO
    {
        public int Id { get; set; } // User ID

        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
