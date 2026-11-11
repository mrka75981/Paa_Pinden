using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface ICommentRepository
    {
        public Task CreateCommentAsync(Comment comment);

        public Task UpdateCommentAsync(Comment comment);

        public Task<List<Comment>> GetAllPostCommentsAsync(int id);

        public Task<Comment> GetCommentAsync(int id);

        public Task DeleteCommentAsync(int id);
    }
}
