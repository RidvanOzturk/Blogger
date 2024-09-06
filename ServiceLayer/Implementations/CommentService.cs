using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;


namespace ServiceLayer.Implementations;
public class CommentService(BlogContext context) : ICommentService
{
    public async Task<bool> CreateCommentAsync(int postId, string text, string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
        {
            return false;
        }

        var comment = new Comment
        {
            Text = text,
            CommentedDate = DateTime.Now,
            UserId = user.Id,
            PostId = postId
        };

        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCommentAsync(int commentId, string username)
    {
        var comment = await context.Comments.FindAsync(commentId);

        if (comment == null)
        {
            return false;
        }

        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null || comment.UserId != user.Id)
        {
            return false;
        }

        context.Comments.Remove(comment);
        await context.SaveChangesAsync();

        return true;
    }
}
