using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public Task CreateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllPostCommentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateCommentAsync(Comment comment, int id)
        {
            throw new NotImplementedException();
        }
    }
}
