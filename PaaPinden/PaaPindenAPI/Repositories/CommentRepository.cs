using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using Shared.Models;

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

        public async Task<Comment> GetCommentAsync(int id)
        {
            var comment = await _paaPindenContext.Comments.SingleOrDefaultAsync(c => c.Id == id);
            if (comment == null) return null;
            return comment;
        }

        public async Task<List<Comment>> GetAllPostCommentsAsync(int id)
        {
            var knittingPostComments = await _paaPindenContext.Comments.Where(c => c.CommentKnittingPostId == id).ToListAsync();
            if (knittingPostComments != null)
            {
                return knittingPostComments;
            }
            return null;
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            //_paaPindenContext.Comments.Update(comment);

            await _paaPindenContext.SaveChangesAsync();
        }
    }
}
