namespace ServiceLayer.Contracts;
public interface IPostService
{
    Task PostDetailAsync(int id);
    Task AllPostsAsync();
    Task DeletePostAsync(int id);
}
