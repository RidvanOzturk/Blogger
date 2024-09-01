using DataLayer.Entities;

namespace Blogger.Models;

public class ProfileDetailViewModel
{
    public string Username { get; set; }
    public List<Post> Posts { get; set; }
}
