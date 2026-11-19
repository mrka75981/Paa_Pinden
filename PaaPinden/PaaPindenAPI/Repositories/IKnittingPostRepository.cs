using Shared.Models;

namespace PaaPindenAPI.Repositories
{
    public interface IKnittingPostRepository
    {
        public Task CreateKnittingPostAsync(KnittingPost knittingPost);

        public Task<KnittingPost> GetKnittingPostAsync(int id);

        public Task<List<KnittingPost>> GetAllKnittingPostAsync();

        public Task DeleteKnittingPostAsync(int id);
        
        public Task UpdateKnittingPostAsync(KnittingPost knittingPost);
    }
}
