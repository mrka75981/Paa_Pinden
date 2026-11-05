using PaaPindenAPI.Data;
using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly PaaPindenContext _paaPindenContext;

        public CommentRepository(PaaPindenContext paaPindenContext)
        {
            _paaPindenContext = paaPindenContext;
        }

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

        public Task<Comment> UpdateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
