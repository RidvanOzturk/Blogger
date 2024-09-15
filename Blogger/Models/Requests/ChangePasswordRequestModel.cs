using System.ComponentModel.DataAnnotations;

public class ChangePasswordRequestModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "New password is required")]
    public string NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}
