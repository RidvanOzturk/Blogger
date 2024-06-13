namespace Blogger.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CommentedDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
