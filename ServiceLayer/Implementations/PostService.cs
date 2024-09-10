using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementations;
public class PostService(BlogContext context) : IPostService
{
    public async Task PostDetailAsync(int id)
    {
        var post = await context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<List<Post>> AllPostsAsync()
    {
        var post = await context.Posts
           .Include(p => p.User)
           .Include(p => p.Comments)
           .ThenInclude(c => c.User)
           .ToListAsync();
        return post;
    }
    public async Task DeletePostAsync(int id)
    {
        var post = await context.Posts
               .Include(p => p.Comments)
               .FirstOrDefaultAsync(p => p.Id == id);

        context.Comments.RemoveRange(post.Comments);
        context.Posts.Remove (post);
        await context.SaveChangesAsync();
    }
    public async Task<bool> CreatePostAsync(PostCreateRequestDTO request)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Username == request.Username);

        if (user == null)
        {
            return false;
        }
        if (user != null)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                PostedDate = DateTime.Now,
                UserId = user.Id
            };

            user.Posts.Add(post);
            await context.SaveChangesAsync();

            return true;
        }
        return false;
    }





}
