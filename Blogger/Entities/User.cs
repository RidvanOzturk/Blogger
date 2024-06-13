namespace Blogger.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
