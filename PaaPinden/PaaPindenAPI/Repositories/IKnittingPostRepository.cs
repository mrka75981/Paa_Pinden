using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface IKnittingPostRepository
    {
        Task CreateKnittingPostAsync(KnittingPost knittingPost);

        Task<KnittingPost> GetKnittingPostAsync(int id);

        Task<List<KnittingPost>> GetAllKnittingPostAsync();

        Task DeleteKnittingPostAsync(int id);

        Task UpdateKnittingPostAsync(KnittingPost knittingPost);
    }
}
