namespace DataLayer.Entities;

public class Post
{
    public int Id { get; init; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PostedDate { get; set; } = DateTime.Now;
    public ICollection<Comment> Comments { get; init; } = new List<Comment>();
    public int UserId { get; set; }
    public User User { get; set; }
}