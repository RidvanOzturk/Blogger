namespace Blogger.Models
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
