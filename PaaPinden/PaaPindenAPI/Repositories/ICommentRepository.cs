using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment, int id);
        Task<List<Comment>> GetAllPostCommentsAsync(int id);
        Task DeleteCommentAsync(int id);
    }
}

