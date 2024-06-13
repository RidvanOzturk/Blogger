namespace Blogger.Entities
{
    public class Comment
    {
        public int CommentId { get; init; } 
        public string Text { get; set; } 
        public DateTime CommentedDate { get; init; } 
        public int PostId { get; set; } 
        public Post Post { get; set; } 
    }

}
