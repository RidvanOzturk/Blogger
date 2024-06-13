namespace Blogger.Entities
{
    public class Post
    {
        public int PostId { get; init; }  
        public string Title { get; set; }  
        public string Content { get; set; }  
        public DateTime PostedDate { get; init; }  
        public int BlogId { get; init; }  
        public Blog Blog { get; init; }  
        public ICollection<Comment> Comments { get; init; } = new List<Comment>();  
    }

}
