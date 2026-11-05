using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface IKnittingPostRepository
    {
        public Task CreateKnittingPostAsync();

        public Task<KnittingPost> GetKnittingPostAsync(int id);

        public Task<List<KnittingPost>> GetAllKnittingPostAsync();

        public Task DeleteKnittingPostAsync(int id);
        
        public Task UpdateKnittingPostAsync(KnittingPost knittingPost);
    }
}
