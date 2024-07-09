namespace Blogger.Requests
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
