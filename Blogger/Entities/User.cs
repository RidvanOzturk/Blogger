namespace Blogger.Entities
{
    public class User
    {
        public int UserId { get; init; } 
        public string Username { get; set; }  
        public string Password { get; set; }  
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();  
        public ICollection<Comment> Comments { get; set; } = new List<Comment>(); 
    }
}
