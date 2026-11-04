using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface IKnittingPostRepository
    {
        void CreateKnittingPostAsync(KnittingPost knittingPost);

        KnittingPost GetKnittingPostAsync(int id);

        List<KnittingPost> GetAllKnittingPostAsync();

        void DeleteKnittingPostAsync(int id);

        void UpdateKnittingPostAsync(KnittingPost knittingPost);
    }
}
