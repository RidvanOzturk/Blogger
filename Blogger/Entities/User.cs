namespace Blogger.Entities
{
    public class User
    {
        public int Id { get; init; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>(); 
    }
}
