using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using Shared.Models;
using Shared.DTOs;

namespace PaaPindenAPI.Repositories
{
    public class KnittingPostRepository : IKnittingPostRepository
    {
        private readonly PaaPindenContext _paaPindenContext;

        public KnittingPostRepository(PaaPindenContext paaPindenContext)
        {
            _paaPindenContext = paaPindenContext;
        }

        public async Task CreateKnittingPostAsync(KnittingPost knittingPost)
        {
            await _paaPindenContext.KnittingPosts.AddAsync(knittingPost);
            await _paaPindenContext.SaveChangesAsync();
        }

        public async Task DeleteKnittingPostAsync(int id)
        {
            var knittingPost = await _paaPindenContext.KnittingPosts.FindAsync(id);
            if (knittingPost != null)
            {
                _paaPindenContext.KnittingPosts.Remove(knittingPost);
                await _paaPindenContext.SaveChangesAsync();
            }
        }

        public async Task<List<KnittingPost>> GetAllKnittingPostAsync()
        {
            List<KnittingPost> knittingPosts = await _paaPindenContext.KnittingPosts
                .Include(k => k.Comments)
                .Include(k => k.KnittingPostContent)
                .Include(k => k.KnittingPostContent.Videos)
                .Include(k => k.KnittingPostContent.Pictures).ToListAsync();

            return knittingPosts;
        }

        public async Task<KnittingPost> GetKnittingPostAsync(int id)
        {
            var knittingPost = await _paaPindenContext.KnittingPosts
                .Include(k => k.Comments)
                .Include(k => k.KnittingPostContent)
                .Include(k => k.KnittingPostContent.Videos)
                .Include(k => k.KnittingPostContent.Pictures)
                .SingleOrDefaultAsync(k => k.Id == id);
            if (knittingPost != null)
            {
                return knittingPost;
            }
            return null;
        }

        public async Task UpdateKnittingPostAsync(KnittingPost knittingPost)
        {
            //Not needed anymore cause of tracking <3
            //_paaPindenContext.KnittingPosts.Update(knittingPost);

            await _paaPindenContext.SaveChangesAsync();
        }
    }
}
