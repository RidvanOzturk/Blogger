namespace Blogger.Entities
{
    public class Post
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; init; }
        public ICollection<Comment> Comments { get; init; } = new List<Comment>();
    }

}
