using System.ComponentModel.DataAnnotations;

namespace Blogger.Models.Requests;

public class LoginRequestModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
