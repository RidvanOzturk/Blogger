namespace DataLayer.Entities;

public class Comment
{
    public int Id { get; init; }
    public string Text { get; set; }
    public DateTime CommentedDate { get; init; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
