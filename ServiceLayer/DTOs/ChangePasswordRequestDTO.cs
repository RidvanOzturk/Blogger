using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs;
public class ChangePasswordRequestDTO
{

    public int Id { get; set; } // User ID

    [Required(ErrorMessage = "New Password is required")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not matsch")]
    public string ConfirmPassword { get; set; }
}
