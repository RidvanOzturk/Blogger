namespace Blogger.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
