using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface ICommentRepository
    {
        public Task CreateCommentAsync(Comment comment);

        public Task<Comment> UpdateCommentAsync(Comment comment);

        public Task<List<Comment>> GetAllPostCommentsAsync(int id);

        public Task DeleteCommentAsync(int id);
    }
}
