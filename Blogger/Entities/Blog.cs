namespace Blogger.Entities
{
    public class Blog
    {
        public int BlogId { get; init; }  
        public string Title { get; set; }  
        public string Description { get; set; }  
        public int UserId { get; init; }  
        public User User { get; init; }  
        public ICollection<Post> Posts { get; init; } = new List<Post>();  
    }

}
