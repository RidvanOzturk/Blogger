using DataLayer.Entities;
using ServiceLayer.DTOs;

namespace ServiceLayer.Contracts;
public interface IPostService
{
    Task PostDetailAsync(int id);
    Task<List<Post>> AllPostsAsync();
    Task DeletePostAsync(int id);
    Task <bool> CreatePostAsync(PostCreateRequestDTO request);
}
