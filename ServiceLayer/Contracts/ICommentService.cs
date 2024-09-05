using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface ICommentService
    {
        Task<bool> CreateCommentAsync(int postId, string text, string username);
        Task<bool> DeleteCommentAsync(int commentId, string username);
    }
}
