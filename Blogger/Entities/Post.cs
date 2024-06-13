namespace Blogger.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
