using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task<List<Comment>> GetAllPostCommentsAsync(int knittingPostId);
        Task DeleteCommentAsync(int id);
    }
}

