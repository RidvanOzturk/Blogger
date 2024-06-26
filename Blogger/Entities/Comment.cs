namespace Blogger.Entities
{
    public class Comment
    {
        public int Id { get; init; }
        public string Text { get; set; }
        public DateTime CommentedDate { get; init; }
    }


}
