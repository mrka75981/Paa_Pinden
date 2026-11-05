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

        public async Task CreateCommentAsync(Comment comment)
        {
            await _paaPindenContext.Comments.AddAsync(comment);
            await _paaPindenContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _paaPindenContext.Comments.FindAsync(id);
            if (comment != null)
            {
                _paaPindenContext.Comments.Remove(comment);
                await _paaPindenContext.SaveChangesAsync();
            }
        }

        public async Task<List<Comment>> GetAllPostCommentsAsync(int id)
        {
            var knittingPost = await _paaPindenContext.KnittingPosts.FindAsync(id);
            if (knittingPost != null)
            {
                return knittingPost.Comments;
            }
            return null;
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _paaPindenContext.Comments.Update(comment);

            await _paaPindenContext.SaveChangesAsync();
        }
    }
}
